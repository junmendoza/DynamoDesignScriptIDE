using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using ProtoCore.DSASM.Mirror;
using ProtoTest.TD;
using ProtoTestFx.TD;
namespace ProtoTest.Imperative
{
    public class MicroFeatureTests
    {
        public TestFrameWork thisTest = new TestFrameWork();
        readonly string testCasePath = Path.GetFullPath(@"..\..\..\Scripts\imperative\MicroFeatureTests\");
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestAssignment01()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object o = mirror.GetValue("foo").Payload;
            Assert.IsTrue((Int64)o == 5);
        }

        [Test]
        public void TestAssignment02()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object o = mirror.GetValue("foo").Payload;
            Assert.IsTrue((Int64)o == 6);
        }

        [Test]
        public void TestNull01()
        {
            String code =
                @"aa;bb;a;b;c;[Imperative]
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("aa").Payload == 0);
            Assert.IsTrue((Int64)mirror.GetValue("bb").Payload == 20);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 2);
            Assert.IsTrue(mirror.GetValue("b").DsasmValue.IsNull);
            Assert.IsTrue(mirror.GetValue("c").DsasmValue.IsNull);
        }

        [Test]
        public void TestNull02()
        {
            String code =
                @"[Imperative]
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            TestFrameWork.VerifyBuildWarning(ProtoCore.BuildData.WarningID.kIdUnboundIdentifier);
        }
        public void Fibonacci_recusion()
        {
            Setup();
            String code =
                        @"fib10;[Imperative]
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            var fib10 = (Int64)mirror.GetValue("fib10").Payload;
            Assert.IsTrue(fib10 == 55);
        }

        [Test]
        public void TestFunction01()
        {
            String code =
@"
;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 99);
        }

        [Test]
        public void TestFunction02()
        {
            string code =
                    @"test;[Imperative]
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Double)mirror.GetValue("test").Payload == 8.5);
        }

        [Test]
        public void TestFunction03()
        {
            string code =
                    @"x;temp2;[Imperative]
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("x").Payload == 1);
            Assert.IsTrue((Int64)mirror.GetValue("temp2").Payload == 1);
        }

        [Test]
        public void IfStatement01()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 1);
        }

        [Test]
        public void IfStatement02()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 1);
        }

        [Test]
        public void IfStatement03()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 1);
        }

        [Test]
        public void IfStatement04()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 2);
        }

        [Test]
        public void IfStatement05()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 1);
        }

        [Test]
        public void IfStatement06()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 2);
        }

        [Test]
        public void IfStatement07()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("temp1").Payload == 11);
        }

        [Test]
        public void IfStatement08()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 28);
        }

        [Test]
        public void IfStatement09()
        {
            String code =
                    @"a;b;[Imperative]
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue(mirror.GetValue("a").DsasmValue.IsNull);
            Assert.IsTrue(mirror.GetValue("b").DsasmValue.IsNull);
        }

        [Test]
        public void IfStatement10()
        {
            String code =
                    @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 0);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 0);
        }

        [Test]
        public void NestedBlocks001()
        {
            String code =
                        @"a;[Imperative]
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue(mirror.GetValue("a").DsasmValue.IsNull);
        }

        [Test]
        public void NegativeFloat001()
        {
            String code =
                        @"x;y;[Imperative]
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Double)mirror.GetValue("x").Payload == -2.5);
            Assert.IsTrue((Double)mirror.GetValue("y").Payload == 0.0);
        }

        [Test]
        public void ForLoop01()
        {
            String code =
                        @"x;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("x").Payload == 100);
        }

        [Test]
        public void ForLoop02()
        {
            String code =
                        @"x;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("x").Payload == 1000);
        }

        [Test]
        public void ForLoop03()
        {
            String code =
                        @"x;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("x").Payload == 1004);
        }

        [Test]
        public void ForLoop04()
        {
            String code =
                        @"x;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("x").Payload == 10);
        }

        [Test]
        public void ForLoop05()
        {
            String code =
                        @"y;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("y").Payload == 11);
        }
        [Ignore]
        public void BitwiseOp001()
        {
            String code =
                        @"c;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 2);
        }
        [Ignore]
        public void BitwiseOp002()
        {
            String code =
                        @"c;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 3);
        }
        [Ignore]
        public void BitwiseOp003()
        {
            String code =
                        @"b;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == -3);
        }
        [Ignore]
        public void BitwiseOp004()
        {
            String code =
                        @"c;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("c", null);
        }

        [Test]
        public void LogicalOp001()
        {
            String code =
                        @"e;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("e", false);
        }

        [Test]
        public void LogicalOp002()
        {
            String code =
                        @"e;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("e", true);
        }

        [Test]
        public void LogicalOp003()
        {
            String code =
                        @"c;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("c", 2);
        }

        [Test]
        public void LogicalOp004()
        {
            String code =
                        @"c;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 1);
        }

        [Test]
        public void LogicalOp005()
        {
            String code =
                        @"c;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 2);
        }

        [Test]
        public void LogicalOp006()
        {
            String code =
                        @"c;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("c", false);
        }

        [Test]
        public void LogicalOp007()
        {
            String code =
                        @"temp;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("temp").Payload == 3);
        }

        [Test]
        public void LogicalOp008()
        {
            String code =
                        @"b;c;d;e;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 0);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 0);
            Assert.IsTrue((Int64)mirror.GetValue("d").Payload == 0);
            Assert.IsTrue((Int64)mirror.GetValue("e").Payload == 0);
        }

        [Test]
        public void DoubleOp()
        {
            String code =
                        @"b;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Double)mirror.GetValue("b").Payload == 5.0);
        }

        [Test]
        public void RangeExpr001()
        {
            String code =
                        @"a;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> result = new List<Object> { 1.0, 1.2, 1.4 };
            Assert.IsTrue(mirror.CompareArrays("a", result, typeof(System.Double)));
        }

        [Test]
        public void RangeExpr002()
        {
            String code =
                        @"b;c;d;e;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Double)mirror.GetValue("b").Payload == 1.5);
            Assert.IsTrue((Double)mirror.GetValue("c").Payload == 2.6);
            Assert.IsTrue((Double)mirror.GetValue("d").Payload == 3.7);
            Assert.IsTrue((Double)mirror.GetValue("e").Payload == 4.8);

        }

        [Test]
        public void RangeExpr003()
        {
            String code =
                        @"b;c;d;e;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Double)mirror.GetValue("b").Payload == 15.0);
            Assert.IsTrue((Double)mirror.GetValue("c").Payload == 13.5);
            Assert.IsTrue((Double)mirror.GetValue("d").Payload == 12.0);
            Assert.IsTrue((Double)mirror.GetValue("e").Payload == 10.5);
        }

        [Test]
        public void RangeExpr004()
        {
            String code =
                        @"b;c;d;e;f;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Double)mirror.GetValue("b").Payload == 0);
            Assert.IsTrue((Double)mirror.GetValue("c").Payload == 3.75);
            Assert.IsTrue((Double)mirror.GetValue("d").Payload == 7.5);
            Assert.IsTrue((Double)mirror.GetValue("e").Payload == 11.25);
            Assert.IsTrue((Double)mirror.GetValue("f").Payload == 15);
        }

        [Test]
        public void RangeExpr005()
        {
            String code =
                        @"b;c;d;e;f;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Double)mirror.GetValue("b").Payload == 0);
            Assert.IsTrue((Double)mirror.GetValue("c").Payload == 3.75);
            Assert.IsTrue((Double)mirror.GetValue("d").Payload == 7.5);
            Assert.IsTrue((Double)mirror.GetValue("e").Payload == 11.25);
            Assert.IsTrue((Double)mirror.GetValue("f").Payload == 15);
        }

        [Test]
        public void RangeExpr06()
        {
            string code = @"
x1; x2; x3; x4;
[Imperative]
{
    x1 = 0..#(-1)..5;
    x2 = 0..#0..5;
    x3 = 0..#1..10;
    x4 = 0..#5..10;
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("x1", null);
            thisTest.Verify("x2", new object[] {});
            thisTest.Verify("x3", new object[] {0});
            thisTest.Verify("x4", new object[] {0, 10, 20, 30, 40});
        }

        [Test]
        public void WhileStatement01()
        {
            String code =
                        @"i;temp;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("i").Payload == 5);
            Assert.IsTrue((Int64)mirror.GetValue("temp").Payload == 0);
        }

        [Test]
        public void WhileStatement02()
        {
            String code =
                        @"i;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("i").Payload == 3);
        }

        [Test]
        public void RecurringDecimal01()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Double)mirror.GetValue("c").Payload == -0.66666666666666663);
        }

        [Test]
        public void Factorial01()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("val").Payload == 120);
        }

        [Test]
        public void ToleranceTest()
        {
            String code =
                        @"a;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Double)mirror.GetValue("a").Payload == 0.3);
        }

        [Test]
        public void InlineCondition001()
        {
            String code =
                        @"c;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 10);
        }

        [Test]
        public void InlineCondition002()
        {
            String code =
                        @"c;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 20);
        }

        [Test]
        public void InlineCondition003()
        {
            String code =
                        @"c;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 30);
        }
        [Ignore]
        public void PrePostFix001()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 6);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 6);
        }
        [Ignore]
        public void PrePostFix002()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 6);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 5);
        }
        [Ignore]
        public void PrePostFix003()
        {
            String code =
                        @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("a").Payload == 8);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 6);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 7);
        }

        [Test]
        public void Modulo001()
        {
            String code =
                @"  c;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 0);
        }

        [Test]
        public void Modulo002()
        {
            String code =
               @"   c;
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("c").Payload == 2);
        }

        [Test]
        public void NegativeIndexOnCollection001()
        {
            String code =
                @"  b;[Imperative]
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 3);
        }

        [Test]
        public void NegativeIndexOnCollection002()
        {
            String code =
                @"  b;[Imperative]
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 3);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored_NegativeIndexCovered")]
        public void NegativeIndexOnCollection003()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("b").Payload == 4);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored_NegativeIndexCovered")]
        public void PopListWithDimension()
        {
            String code =
                @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("watch1", 2);
            thisTest.Verify("watch2", 3);
            thisTest.Verify("watch3", 3);
            thisTest.Verify("watch4", -3);
        }

        [Test]
        public void TestArrayOverIndexing01()
        {
            string code = @"
            thisTest.RunScriptSource(code);
            TestFrameWork.VerifyRuntimeWarning(ProtoCore.Runtime.WarningID.kOverIndexing);
        }

        [Test]
        public void TestTemporaryArrayIndexing01()
        {
            string code = @"
            thisTest.RunScriptSource(code);
            thisTest.Verify("t", 4);
        }

        [Test]
        public void TestTemporaryArrayIndexing02()
        {
            string code = @"
            thisTest.RunScriptSource(code);
            thisTest.Verify("t", 4);
        }

        [Test]
        public void TestTemporaryArrayIndexing03()
        {
            string code = @"
            thisTest.RunScriptSource(code);
            thisTest.Verify("t", 4);
        }

        [Test]
        public void TestTemporaryArrayIndexing04()
        {
            string code = @"
            thisTest.RunScriptSource(code);
            thisTest.Verify("t", 4);
        }

        [Test]
        public void TestTemporaryArrayIndexing05()
        {
            string code = @"
            thisTest.RunScriptSource(code);
            thisTest.Verify("t", new object[] { 2, 3, 4 });
        }

        [Test]
        public void TestTemporaryArrayIndexing06()
        {
            string code = @"
            thisTest.RunScriptSource(code);
            thisTest.Verify("t", new object[] { 2, 3, 4 });
        }

        [Test]
        public void TestDynamicArray001()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            ProtoCore.Lang.Obj o = mirror.GetFirstValue("loc");
            ProtoCore.DSASM.Mirror.DsasmArray arr = (ProtoCore.DSASM.Mirror.DsasmArray)o.Payload;
            Assert.IsTrue((Int64)arr.members[0].Payload == 2);
        }
        [Test, Ignore]
        public void TestTryCatch001()
        {
            String code =
@"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("x").Payload == 1);
            Assert.IsTrue((Int64)mirror.GetValue("t2").Payload == 1);
            Assert.IsTrue((Int64)mirror.GetValue("y2").Payload == 1);
            Assert.IsTrue((Int64)mirror.GetValue("y3").Payload == 1);
            Assert.IsTrue((Int64)mirror.GetValue("t3").Payload == 300);
            Assert.IsTrue((Int64)mirror.GetValue("z").Payload == 2);
        }
        [Test]
        [Ignore][Category("DSDefinedClass_Ignored_TryCatchUnsupported")]
        public void TestTryCatch002()
        {
            string code = @"
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("x").Payload == 1);
            Assert.IsTrue((Int64)mirror.GetValue("y1").Payload == 1);
            Assert.IsTrue((Int64)mirror.GetValue("y2").Payload == 0);
            Assert.IsTrue((Int64)mirror.GetValue("y3").Payload == 0);
            Assert.IsTrue((Int64)mirror.GetValue("y4").Payload == 4);
            Assert.IsTrue((Int64)mirror.GetValue("y5").Payload == 5);
            Assert.IsTrue((Int64)mirror.GetValue("y6").Payload == 0);
            Assert.IsTrue((Int64)mirror.GetValue("z").Payload == 5);
        }
        [Test, Ignore] // Jun: Ignore for now until we allow deeply nested function definitions
        public void TestTryCatchStackUnwinding01()
        {
            string code = @"    y0 = 0;
    y1 = 0;
    y2 = 0;
    y3 = 0;
    y4 = 0;
    y5 = 0;
    y6 = 0;
    y7 = 0;
    y8 = 0;
    y9 = 0;
    y10 = 0;
    y11 = 0;
    y12 = 0;
    y13 = 0;
[Imperative]
{
    y1 = 1;
    try
    {
        y2 = 2;
        [Associative]
        {
            [Imperative]
            {
                y3 = 3;
                def foo()
                {
                    try
                    {
                        y4 = 4;
                        throw 3;
                        y5 = 5;
                    }
                    catch (e:bool)
                    {
                        y6 = 6;
                    }
                    y7 = 7;
                }
                y8 = 8;
                try
                {
                    r = foo();
                    y9 = 9;
                }
                catch (e:char)
                {
                    y10 = 10;
                }
                y11 = 11;
            }
        }
    }
    catch (e:double)
    {
        y12 = 12;
    }
    catch (e:int)
    {
        y13 = 13;
    }
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("y1").Payload == 1);
            Assert.IsTrue((Int64)mirror.GetValue("y2").Payload == 2);
            Assert.IsTrue((Int64)mirror.GetValue("y3").Payload == 3);
            Assert.IsTrue((Int64)mirror.GetValue("y4").Payload == 4);
            Assert.IsTrue((Int64)mirror.GetValue("y5").Payload == 0);
            Assert.IsTrue((Int64)mirror.GetValue("y6").Payload == 0);
            Assert.IsTrue((Int64)mirror.GetValue("y7").Payload == 0);
            Assert.IsTrue((Int64)mirror.GetValue("y8").Payload == 8);
            Assert.IsTrue((Int64)mirror.GetValue("y9").Payload == 0);
            Assert.IsTrue((Int64)mirror.GetValue("y10").Payload == 0);
            Assert.IsTrue((Int64)mirror.GetValue("y11").Payload == 0);
            Assert.IsTrue((Int64)mirror.GetValue("y12").Payload == 0);
            Assert.IsTrue((Int64)mirror.GetValue("y13").Payload == 13);
        }
        [Test, Ignore]
        public void TestTryCatchGetExceptionValue01()
        {
            string code = @"y = 0;
[Imperative]
{
    try
    {
        throw 1+2;
    }
    catch (e:int)
    {
        y = e;
    }
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("y").Payload == 3);
        }
        [Test, Ignore]
        public void TestTryCatchGetExceptionValue02()
        {
            string code = @"y = 0;
[Imperative]
{
    try
    {
        [Associative]
        {
            [Imperative]
            {
                throw 1+2;
            }
        }
    }
    catch (e:int)
    {
        y = e;
    }
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("y").Payload == 3);
        }
        [Test, Ignore]
        public void TestTryCatchGetExceptionValue03()
        {
            string code = @"y = 0;
[Imperative]
{
    def foo()
    {
        [Associative]
        {
            [Imperative]
            {
                throw 3;
            }
        }
    }
    try
    {
        foo();
    }
    catch (e:int)
    {
        y = e;
    }
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Assert.IsTrue((Int64)mirror.GetValue("y").Payload == 3);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored_DSClassAttribute")]
        public void TestAttributeOnGlobalFunction()
        {
            string code = @"class TestAttribute
{
	constructor TestAttribute()
	{}
}
class VisibilityAttribute
{
	x : var;
	constructor VisibilityAttribute(_x : var)
	{
		x = _x;
	}
}
[Imperative]
{
	[Test, Visibility(1)]
	def foo : int()
	{
		return = 10;
	}
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyBuildWarningCount(0);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored_DSClassAttribute")]
        public void TestAttributeOnLanguageBlock()
        {
            string code = @"class TestAttribute
{
	constructor TestAttribute()
	{}
}
class VisibilityAttribute
{
	x : var;
	constructor VisibilityAttribute(_x : var)
	{
		x = _x;
	}
}
[Imperative]
{
	[Associative, version=""###"", Visibility(10 + 1), fingerprint=""FS54"", Test] 
	{
		a = 19;
	}
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyBuildWarningCount(0);
        }

        [Test]
        [Ignore][Category("DSDefinedClass_Ignored_DSClassAttribute")]
        public void TestAttributeWithLanguageBlockAndArrayExpression()
        {
            string code = @"class TestAttribute
{
	constructor TestAttribute()
	{}
}
class VisibilityAttribute
{
	x : var;
	constructor VisibilityAttribute(_x : var)
	{
		x = _x;
	}
}
[Imperative]
{
	def foo : int[]..[](p : var[]..[])
	{
		a = { 1, { 2, 3 }, 4 };
		return = a[1];
	}
	[Associative, version=""###"", Visibility(10 + 1), fingerprint=""FS54"", Test] 
	{
		a = {1, 2, 3};
		b = a[1];
		c = a[0];
	}
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.VerifyBuildWarningCount(0);
        }

        [Test]
        public void TestStringConcatenation01()
        {
            string code = @"s3;s6;s9;
[Imperative]
{
	s1='a';
	s2=""bcd"";
	s3=s1+s2;
	s4=""abc"";
	s5='d';
	s6=s4+s5;
	s7=""ab"";
	s8=""cd"";
	s9=s7+s8;
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("s3", "abcd");
            thisTest.Verify("s6", "abcd");
            thisTest.Verify("s9", "abcd");
        }

        [Test]
        [Category("Failure")]
        public void TestStringOperations()
        {
            // Tracked by: http://adsk-oss.myjetbrains.com/youtrack/issue/MAGN-4118
            string code = @"[Imperative]
{
	s = ""ab"";
	r1 = s + 3;
	r2 = s + false;
	r3 = s + null;
	r4 = !s;
	r5 = s == ""ab"";
	r6 = s == s;
	r7 = ""ab"" == ""ab"";
	ns = s;
	ns[0] = 1;
	r8 = ns == {1, 'b'};
	r9 = s != ""ab"";
    ss = ""abc"";
    ss[0] = 'x';
    r10 = """" == null;
}
";
            string err = "MAGN-4118 null upgrades to string when added to string";
            ExecutionMirror mirror = thisTest.RunScriptSource(code, err);
            thisTest.Verify("r1", "ab3");
            thisTest.Verify("r2", "abfalse");
            thisTest.Verify("r3", null);
            thisTest.Verify("r4", false);
            thisTest.Verify("r5", true);
            thisTest.Verify("r6", true);
            thisTest.Verify("r7", true);
            thisTest.Verify("r8", false);
            thisTest.Verify("r9", false);
            thisTest.Verify("ss", "xbc");
            thisTest.Verify("r10", null);
        }

        [Test]
        [Category("Failure")]
        public void TestStringTypeConversion()
        {
            // Tracked by http://adsk-oss.myjetbrains.com/youtrack/issue/MAGN-4119
            string code = @"[Imperative]
{
	def foo:bool(x:bool)
	{
	    return=x;
	}
	r1 = foo('h');
	r2 = 'h' && true;
	r3 = 'h' + 1;
}";
            string err = "MAGN-4119 Char does not upgrade to 'int' in arithmetic expression";
            ExecutionMirror mirror = thisTest.RunScriptSource(code, err);
            thisTest.Verify("r1", true);
            thisTest.Verify("r2", true);
            thisTest.Verify("r3", Convert.ToInt64('h') + 1);
        }

        [Test]
        public void TestStringForloop()
        {
            string code = 
@"
r = [Imperative]
{
    s = ""foo"";
    for (x in ""bar"")
    {
         s  = s + x;
    }
    return = s;
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("r", "foobar");
        }

        [Test]
        public void TestLocalKeyword01()
        {
            string code =
@"
i = [Imperative]
{
    a : local int = 1;      
    return = a;
}
";
            thisTest.RunScriptSource(code);
            thisTest.Verify("i", 1);
        }

        [Test]
        public void TestLocalKeyword02()
        {
            string code =
@"
i = [Imperative]
{
    a : local int = 1;      
    b : local int = 2;       
    return = a + b;
}
";
            thisTest.RunScriptSource(code);
            thisTest.Verify("i", 3);
        }

        [Test]
        public void TestLocalKeyword03()
        {
            string code =
@"
a = 1;
b = [Imperative]
{
    a : local = 2;
    x : local = a;
    return = x;
}

c = a;
d = b;
";
            thisTest.RunScriptSource(code);
            thisTest.Verify("c", 1);
            thisTest.Verify("d", 2);
        }

        [Test]
        public void TestLocalKeyword04()
        {
            string code =
@"
a = 1;
b = [Imperative]
{
    a : local = 2;
    return = a;
}

c = a;
d = b;
";
            thisTest.RunScriptSource(code);
            thisTest.Verify("c", 1);
            thisTest.Verify("d", 2);
        }


        [Test]
        public void TestLocalKeyword05()
        {
            string code =
@"
a = [Imperative]
{
    a : local = 1;
    b = 0;
    if (a == 1)
    {
        a : local = 2;
        b = a;
    }
    else
    {
        a : local = 3;
        b = a;
    }
    return = b;
}
";
            thisTest.RunScriptSource(code);
            thisTest.Verify("a", 2);
        }

        [Test]
        public void TestLocalKeyword06()
        {
            string code =
@"
a = [Imperative]
{
    a : local = 1;
    b = 0;
    if (a != 1)
    {
        a : local = 2;
        b = a;
    }
    else
    {
        a : local = 3;
        b = a;
    }
    return = b;
}
";
            thisTest.RunScriptSource(code);
            thisTest.Verify("a", 3);
        }

                [Test]
        public void TestLocalVariableNoUpdate01()
        {
            string code =
@"
a = 1;
b = a;
c = [Imperative]
{
    a : local = 2; // Updating local 'a' should not update global 'a'
    return = 1;
}
";
            thisTest.RunScriptSource(code);
            thisTest.Verify("a", 1);
            thisTest.Verify("b", 1);
            thisTest.Verify("c", 1);
        }

        [Test]
        public void TestLocalVariableNoUpdate02()
        {
            string code =
@"
a : local = 1;      // Tagging a variable local at the global scope has no semantic effect
b : local = a;
c = [Imperative]
{
    a : local = 2; // Updating local 'a' should not update global 'a'
    return = a;
}
";
            thisTest.RunScriptSource(code);
            thisTest.Verify("a", 1);
            thisTest.Verify("b", 1);
            thisTest.Verify("c", 2);
        }
    }
}