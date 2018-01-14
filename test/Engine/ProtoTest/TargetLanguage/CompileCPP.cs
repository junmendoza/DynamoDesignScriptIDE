using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using ProtoCore.DSASM.Mirror;
using ProtoCore.Lang;
using ProtoTest.TD;
using ProtoTestFx.TD;

using ProtoCore;
using ProtoScript.Runners;

namespace ProtoTest.TargetLanguage
{
    class CompileCPP
    {
        [Test]
        public void TestCompileCPP01()
        {

            String code =
@"def foosa()
{
    a : IN = 10;
    b : int = a;

    c : OUT = 20;
    d : int = c;

    c = a;
}";
            // Compile
            ProtoScriptRunner runner = new ProtoScriptRunner();
            ProtoCore.Utils.ParseResult parseResult;
            bool compileSucceeded = runner.CompileCPP(code, out parseResult);
            Assert.IsTrue(compileSucceeded == true);
        }
    }
}