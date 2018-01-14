using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoCore;
using ProtoCore.Utils;

namespace ProtoCore.TargetLanguage
{
    public enum Language
    {
        kCPP,
        kNone
    }

    public class EmitInterface
    {
        // Destination source file
        protected string rootPath = string.Empty;
       
        public EmitInterface() { }

        public virtual void Emit(string filename, List<AST.AssociativeAST.AssociativeNode> sourceCode) { }
        public virtual void Emit(string filename, ProtoCore.AST.AssociativeAST.FunctionDefinitionNode funcDef) { }
        public virtual void EmitHeader(string filename, List<AST.AssociativeAST.AssociativeNode> sourceCode) { }

        public static EmitInterface GetEmitter(Language targetLanguage, string rootPath)
        {
            if (targetLanguage == Language.kCPP) {
                return new ProtoCore.TargetLanguage.CPP.EmitCPP(rootPath);
            }
            return new EmitInterface();
        }
        
        protected void Commit(string pathFilename, string sourceCode)
        {
            bool writeToFile = string.Empty != pathFilename && string.Empty != sourceCode;
            if (writeToFile) {
                WriteSourceToFile(pathFilename, sourceCode);
            }
        }

        /// <summary>
        /// Write the generated language to its sourcefile
        /// </summary>
        /// <param name="pathFilename"></param>
        /// <param name="code"></param>
        private void WriteSourceToFile(string pathFilename, string code)
        {
            // Path and filename should not exist 
            // If it does, then overwrite it
            System.IO.StreamWriter sw = System.IO.File.CreateText(pathFilename);
            sw.WriteLine(code);
            sw.Close();
        }
    }
}
