﻿using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using ProtoCore.DSASM.Mirror;
using ProtoCore.Lang;
using ProtoTest.TD;
using ProtoScript.Runners;
using ProtoTestFx.TD;
using System.Linq;
using ProtoCore.AST.AssociativeAST;
using ProtoCore.DSASM;
using ProtoCore.Mirror;
using System.Collections;

namespace ProtoTest.LiveRunner
{
    public class HDLExecutionTest : ProtoTestBase
    {
        private ProtoScript.Runners.LiveRunner liveRunner = null;
        private Random randomGen = new Random();

        public override void Setup()
        {
            base.Setup();
            liveRunner = new ProtoScript.Runners.LiveRunner();
            liveRunner.ResetVMAndResyncGraph(new List<string> { "FFITarget.dll" });
            runtimeCore = liveRunner.RuntimeCore;
        }

        public override void TearDown()
        {
            base.TearDown();
            liveRunner.Dispose();
        }

        private void AssertValue(string varname, object value)
        {
            var mirror = liveRunner.InspectNodeValue(varname);
            MirrorData data = mirror.GetData();
            object svValue = data.Data;
            if (value is double)
            {
                Assert.AreEqual((double)svValue, Convert.ToDouble(value));
            }
            else if (value is int)
            {
                Assert.AreEqual((Int64)svValue, Convert.ToInt64(value));
            }
            else if (value is bool)
            {
                Assert.AreEqual((bool)svValue, Convert.ToBoolean(value));
            }
            else if (value is IEnumerable<int>)
            {
                Assert.IsTrue(data.IsCollection);
                var values = (value as IEnumerable<int>).ToList().Select(v => (object)v).ToList();
                Assert.IsTrue(mirror.GetUtils().CompareArrays(varname, values, typeof(Int64)));
            }
            else if (value is IEnumerable<double>)
            {
                Assert.IsTrue(data.IsCollection);
                var values = (value as IEnumerable<double>).ToList().Select(v => (object)v).ToList();
                Assert.IsTrue(mirror.GetUtils().CompareArrays(varname, values, typeof(double)));
            }
        }

        [Test]
        public void TestImperativeArgsSingleCBN()
        {
            List<string> codes = new List<string>()
            {
@"
a = 1;
t = [Imperative](a)
{
	return = a + 1;
};
",

@"
a = 11;
t = [Imperative](a)
{
	return = a + 1;
};
"
            };

            Guid guid = System.Guid.NewGuid();

            List<Subtree> added = new List<Subtree>();
            added.Add(ProtoTestFx.TD.TestFrameWork.CreateSubTreeFromCode(guid, codes[0]));
            var syncData = new GraphSyncData(null, added, null);
            liveRunner.UpdateGraph(syncData);
            AssertValue("t", 2);

            // Modify imperative dependency value
            List<Subtree> modified = new List<Subtree>();
            modified.Add(ProtoTestFx.TD.TestFrameWork.CreateSubTreeFromCode(guid, codes[1]));

            syncData = new GraphSyncData(null, null, modified);
            liveRunner.UpdateGraph(syncData);
            AssertValue("t", 12);
        }

        [Test]
        public void TestImperativeArgsConnectedCBN()
        {
            List<string> codes = new List<string>()
            {
                @"
                    a = 1;
                ",
                @"
                    t = [Imperative](a)
                    {
	                    return = a + 1;
                    };
                ",
                @"
                    a = 11;
                "
            };

            // Simulate 2 CBNs
            Guid cbnGuid1 = System.Guid.NewGuid();
            Guid cbnGuid2 = System.Guid.NewGuid();
            List<Subtree> added = new List<Subtree>();
            added.Add(ProtoTestFx.TD.TestFrameWork.CreateSubTreeFromCode(cbnGuid1, codes[0]));
            added.Add(ProtoTestFx.TD.TestFrameWork.CreateSubTreeFromCode(cbnGuid2, codes[1]));
            var syncData = new GraphSyncData(null, added, null);

            // Sending the CBN node data to the VM
            // Execute the DS code
            liveRunner.UpdateGraph(syncData);

            // Verify the values of the variables in the CBNs
            AssertValue("t", 2);

            // Modify imperative dependency value
            List<Subtree> modified = new List<Subtree>();
            modified.Add(ProtoTestFx.TD.TestFrameWork.CreateSubTreeFromCode(cbnGuid1, codes[2]));

            syncData = new GraphSyncData(null, null, modified);
            liveRunner.UpdateGraph(syncData);
            AssertValue("t", 12);
        }

        [Test]
        public void TestPipelineRegisterUpdate()
        {
            List<string> codes = new List<string>()
            {
                @"
                    clock = 1;
                ",
                @"
                    reset = 0;
                ",
                @"
                    instruction = 1111;
                ",
                @"
                __IN_clock = clock;
                __IN_reset = reset;
                __IN_Instr = instruction;
                __OUT_Instr = -1;
// Locals
propagatePipelineRegisters = 0;

// Preg vars
preg_Instr = -1;

__PROCESS_PipelineRegister_Dispatch =
[Imperative] (__IN_clock, __IN_reset)
{
	if (__IN_reset == 1)
	{
	}
	elseif(__IN_reset == 0)
	{
		// Rising Edge
		if (__IN_clock == 1)
		{
			if (propagatePipelineRegisters == 1)
			{
				propagatePipelineRegisters = 0;

				// Send the current state to the output signals
				__OUT_Instr = preg_Instr;
			}
		}
	}
};

__PROCESS_PipelineRegister_Latch =
[Imperative] (__IN_reset, __IN_Instr)
{
	if (__IN_reset == 1)
	{
	}
	elseif(__IN_reset == 0)
	{
        propagatePipelineRegisters = 1;

        // Save the current state
        preg_Instr = __IN_Instr;
	}
};
                ",
                @"
                    clock = 0;
                ",
            };

            // Simulate 2 CBNs
            Guid guid1 = System.Guid.NewGuid();
            Guid guid2 = System.Guid.NewGuid();
            Guid guid3 = System.Guid.NewGuid();
            Guid guid4 = System.Guid.NewGuid();

            List<Subtree> added = new List<Subtree>();
            added.Add(ProtoTestFx.TD.TestFrameWork.CreateSubTreeFromCode(guid1, codes[0]));
            added.Add(ProtoTestFx.TD.TestFrameWork.CreateSubTreeFromCode(guid2, codes[1]));
            added.Add(ProtoTestFx.TD.TestFrameWork.CreateSubTreeFromCode(guid3, codes[2]));
            added.Add(ProtoTestFx.TD.TestFrameWork.CreateSubTreeFromCode(guid4, codes[3]));
            var syncData = new GraphSyncData(null, added, null);

            // Sending the CBN node data to the VM
            // Execute the DS code
            liveRunner.UpdateGraph(syncData);

            // Verify the values of the variables in the CBNs
            AssertValue("preg_Instr", 1111);
            AssertValue("__OUT_Instr", -1);

            // Modify clock 0
            List<Subtree> modified = new List<Subtree>();
            modified.Add(ProtoTestFx.TD.TestFrameWork.CreateSubTreeFromCode(guid1, codes[4]));
            syncData = new GraphSyncData(null, null, modified);
            liveRunner.UpdateGraph(syncData);

            // Verify the values of the variables in the CBNs
            AssertValue("preg_Instr", 1111);
            AssertValue("__OUT_Instr", -1);     
            
            // Modify clock 1
            modified = new List<Subtree>();
            modified.Add(ProtoTestFx.TD.TestFrameWork.CreateSubTreeFromCode(guid1, codes[0]));
            syncData = new GraphSyncData(null, null, modified);
            liveRunner.UpdateGraph(syncData);

            // Verify the values of the variables in the CBNs
            AssertValue("__OUT_Instr", 1111);
        }
    }
}

