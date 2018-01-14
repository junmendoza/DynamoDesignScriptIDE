using System;
using NUnit.Framework;
using ProtoCore.DSASM.Mirror;
using ProtoTest.TD;
using ProtoTestFx.TD;
namespace ProtoTest.Associative
{
    class MethodResolution : ProtoTestBase
    {
        [Test]
        [Category("DSDefinedClass_Ported")]
        public void SimpleAssignment()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            Assert.IsTrue((Int64)mirror.GetValue("x").Payload == 123);
            Assert.IsTrue((Int64)mirror.GetValue("y").Payload == 345);
        }

        [Test]
        [Category("DSDefinedClass_Ported")]
        public void SimpleMethodOverload1()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            Assert.IsTrue((Int64)mirror.GetValue("x").Payload == 123);
            Assert.IsTrue((Int64)mirror.GetValue("y").Payload == 345);
        }

        [Test]
        [Category("DSDefinedClass_Ported")]
        public void SimpleMethodOverload2()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            Assert.IsTrue((Int64)mirror.GetValue("x").Payload == 200);
            Assert.IsTrue((Int64)mirror.GetValue("y").Payload == 800);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored")]
        public void SimpleCtorResolution04()
        {
            String code =
@"
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            Assert.IsTrue((double)mirror.GetValue("d").Payload == 1);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored")]
        public void TestMethodOverload1()
        {
            string code =
                @"
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            Assert.IsTrue((Int64)mirror.GetValue("val").Payload == 1);
            Assert.IsTrue(core.BuildStatus.WarningCount == 0);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored")]
        public void TestMethodOverload2()
        {
            string code =
                @"
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            Assert.IsTrue((Int64)mirror.GetValue("val").Payload == 1);
            Assert.IsTrue(core.BuildStatus.WarningCount == 0);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored")]
        public void TestMethodOverload3()
        {
            string code =
                @"
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            Assert.IsTrue((Int64)mirror.GetValue("val").Payload == 1);
            Assert.IsTrue(core.BuildStatus.WarningCount == 0);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored")]
        public void TestMethodOverload4()
        {
            string code =
                @"
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            Assert.IsTrue((Int64)mirror.GetValue("val").Payload == 2);
            Assert.IsTrue(core.BuildStatus.WarningCount == 0);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored")]
        public void TestMethodResolutionOverInheritance()
        {
            string code =
                @"
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            Assert.IsTrue((Int64)mirror.GetValue("val").Payload == 1);
            Assert.IsTrue(core.BuildStatus.WarningCount == 0);
        }

        [Test]
        [Category("DSDefinedClass_Ported")]
        public void TestMethodOverloadArray()
        {
            string code =
                @"
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            Assert.IsTrue((Int64)mirror.GetValue("val").Payload == 2);
            Assert.IsTrue(core.BuildStatus.WarningCount == 0);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored")]
        public void TestMethodOverlaodAndArrayInput2()
        {
            string code =
                @"

            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("val", 2);
            thisTest.VerifyBuildWarningCount(0);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored")]
        public void TestMethodOverlaodAndArrayInput3()
        {
            string code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("val", 2);
            thisTest.VerifyBuildWarningCount(0);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored")]
        public void TestMethodOverlaodAndArrayInput4()
        {
            string code =
                @"
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            Assert.IsTrue((Int64)mirror.GetValue("val1").Payload == -1);
            Assert.IsTrue((Int64)mirror.GetValue("val2").Payload == -1);
            Assert.IsTrue((Int64)mirror.GetValue("val3").Payload == -1);
            Assert.IsTrue(core.BuildStatus.WarningCount == 0);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored")]
        public void TestMethodOverlaodAndArrayInput4Min()
        {
            string code =
                @"
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            Assert.IsTrue((Int64)mirror.GetValue("val1").Payload == -1);
            Assert.IsTrue((Int64)mirror.GetValue("val2").Payload == -1);
            Assert.IsTrue(core.BuildStatus.WarningCount == 0);
        }

        [Test]
        [Category("DSDefinedClass_Ported")]
        [Category("Method Resolution")]
        public void TestDispatchArray()
        {
            //Recorded as defect: DNL-1467146
            string code =
                @"def execute(b : var)
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            Assert.IsTrue((Int64)mirror.GetValue("val").Payload == 100);
            Assert.IsTrue(core.BuildStatus.WarningCount == 0);
        }

        [Test]
        [Category("Method Resolution")]
        [Category("Escalate")]
        [Category("DSDefinedClass_Ported")]
        public void TestDispatchEmptyArray()
        {
            string code =
                @"def execute(b : var)
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            //Assert.IsTrue((Int64)mirror.GetValue("val").Payload == 100);
            Assert.IsTrue(core.BuildStatus.WarningCount == 0);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored")]
        public void TestMethodOverlaodAndArrayInput5()
        {
            string code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("val", 2);
            thisTest.VerifyBuildWarningCount(0);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored")]
        public void TestMethodOverlaodAndArrayInput6()
        {
            string code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("val", 2);
            thisTest.VerifyBuildWarningCount(0);
        }

        [Test]
        [Category("DSDefinedClass_Ported")]
        public void TestMethodWithArrayInput()
        {
            string code = @"
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            Assert.IsTrue((Int64)mirror.GetValue("val").Payload == 123);
            Assert.IsTrue(core.BuildStatus.WarningCount == 0);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored")]
        public void TestMethodWithArrayInput2()
        {
            string code = @"
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            Assert.IsTrue((Int64)mirror.GetValue("val").Payload == 123);
            Assert.IsTrue(core.BuildStatus.WarningCount == 0);
        }

        [Test]
        [Category("Method Resolution")]
        [Category("DSDefinedClass_Ported")]
        public void TestMethodWithArrayInputOverload()
        {
            string code = @"
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            Assert.IsTrue((Int64)mirror.GetValue("val").Payload == 2);
            Assert.IsTrue(core.BuildStatus.WarningCount == 0);
        }

        [Test]
        [Category("Method Resolution")]
        [Category("DSDefinedClass_Ported")]
        public void TestMethodWithArrayInputOverloadDirectType()
        {
            string code = @"
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            Assert.IsTrue((Int64)mirror.GetValue("val").Payload == 2);
            Assert.IsTrue(core.BuildStatus.WarningCount == 0);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored")]
        public void TestMethodWithOverrides()
        {
            string code = @"
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            Assert.IsTrue((Int64)mirror.GetValue("val1").Payload == 1);
            //Assert.IsTrue((Int64)mirror.GetValue("val2").Payload == 2);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored")]
        public void TestOverridenMethod()
        {
            string code = @"
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(code, core); ExecutionMirror mirror = runtimeCore.Mirror;
            //Assert.IsTrue((Int64)mirror.GetValue("val1").Payload == 1);
            Assert.IsTrue((Int64)mirror.GetValue("val2").Payload == 2);
        }

        [Test]
        [Category("Failure")]
        public void TestMethodResolutionForSingleton()
        {
            // Tracked by: http://adsk-oss.myjetbrains.com/youtrack/issue/MAGN-4116
            string code = @"
            string error = "MAGN-4116 DesignIssue: Method resolution - When a single value is passed to overloads with different rank, which one is chosen";
            thisTest.VerifyRunScriptSource(code, error);
            thisTest.Verify("d", 2);
        }

        [Test]
        public void TestMethodResolutionForArray()
        {
            string code = @"
            string error = "";
            thisTest.VerifyRunScriptSource(code, error);
            thisTest.Verify("r1", new object[] { 1, 1, 1 });
            thisTest.Verify("r2", new object[] { 2, 2, 2 });
        }

        [Test]
        [Category("Failure")]
        public void TestMethodResolutionForInforLoss()
        {
            // Tracked by: http://adsk-oss.myjetbrains.com/youtrack/issue/MAGN-4114
            string code = @"
            string error = "MAGN-4114 Method resolution issue - type promotion should be preferred to type demotion";
            thisTest.VerifyRunScriptSource(code, error);
            thisTest.Verify("r", 282);

        }

        [Test]
        public void TestMethodResultionForNonExistType()
        {
            string code = @"
            string error = "";
            thisTest.VerifyRunScriptSource(code, error);
            thisTest.Verify("z", null);
        }


        [Test]
        public void TestArraySubtypeDispatch1D()
        {
            string code = @"
r2 = foo({1.0, 2.0});   // r1 = {false, false}
r3 = foo({1, 2.0});
";
            string error = "";
            thisTest.VerifyRunScriptSource(code, error);
            thisTest.Verify("r1", new object[] { true, true });
            thisTest.Verify("r2", new object[] { false, false });
            thisTest.Verify("r3", new object[] { true, false });
        }

        [Test]
        public void TestArraySubtypeDispatch1D_R1()
        {
            string code = @"
r2 = foo({1.0, 2.0}<1>);   // r1 = {false, false}
r3 = foo({1, 2.0}<1>);
";
            string error = "";
            thisTest.VerifyRunScriptSource(code, error);
            thisTest.Verify("r1", new object[] { true, true });
            thisTest.Verify("r2", new object[] { false, false });
            thisTest.Verify("r3", new object[] { true, false });
        }

        [Test]
        public void MAGN5729_Repro()
        {
            string code = @"
";
            string error = "";
            thisTest.VerifyRunScriptSource(code, error);
            thisTest.Verify("r3", new object[] { true, false });
        }


        [Test]
        public void MAGN5729_Repro_Simple()
        {
            string code = @"
";
            string error = "";
            thisTest.VerifyRunScriptSource(code, error);
            thisTest.Verify("r3", new object[] { true, false });
        }

        [Test]
        public void MAGN7807_ParameterNameSameAsFunctionName()
        {
            string code =
@"
def foo(foo : int)
{
    return = foo;
}

def bar(bar: int)
{
return = [Imperative]
{
    return = bar;
}
}

def qux1()
{
    qux1 = 21;
    return = qux1 + 1;
}

def qux2()
{
    return = [Imperative] {
       qux2 = 21;
       return = qux2 + 1;
    }
}

x = foo({1,2,3});
y = bar({4,5,6});
z1 = qux1();
z2 = qux2();
";
            thisTest.VerifyRunScriptSource(code, "");
            thisTest.Verify("x", new object[] { 1, 2, 3 });
            thisTest.Verify("y", new object[] { 4, 5, 6 });
            thisTest.Verify("z1", 22);
            thisTest.Verify("z2", 22);
        }
    }
}