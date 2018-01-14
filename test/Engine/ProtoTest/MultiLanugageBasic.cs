using System;
using NUnit.Framework;
using ProtoCore.DSASM.Mirror;
namespace ProtoTest
{
    [TestFixture]
    class MultiLanugageBasic : ProtoTestBase
    {
        [Test]
        public void TestSingleLanguageImperative()
        {
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(
@"
        }
        [Test]
        public void TestSingleLanguageAssociative()
        {
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(
@"
        }
        [Test]
        public void TestMultLanguageAssociativeImperative()
        {
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(
@"
        }
        [Test]
        public void TestMultLanguageImperativeAssociative()
        {
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(
@"
        }
        [Test]
        public void TestMultLanguageVariableUsage()
        {
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();

            runtimeCore = fsr.Execute(
@"
        }
        [Test]
        [Category("DSDefinedClass_Ported")]
        public void TestClassUsageInImpeartive()
        {
            ProtoScript.Runners.ProtoScriptRunner fsr = new ProtoScript.Runners.ProtoScriptRunner();
            runtimeCore = fsr.Execute(
@"
import(""FFITarget.dll"");
    , core);
            ExecutionMirror mirror = runtimeCore.Mirror;
            Assert.IsTrue((Int64)mirror.GetValue("x", 0).Payload == 16);
            Assert.IsTrue((Int64)mirror.GetValue("y", 0).Payload == 32);
        }
    }
}