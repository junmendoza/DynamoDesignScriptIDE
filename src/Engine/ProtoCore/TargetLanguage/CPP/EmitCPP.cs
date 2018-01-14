using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoCore;
using ProtoCore.Utils;
using ProtoCore.AST.AssociativeAST;

namespace ProtoCore.TargetLanguage.CPP
{
    public struct Constants
    {
        public const string CRLF = "\r\n";
        public const string EndLine = ";\r\n";

        public const string headerExt = ".h";
        public const string cppExt = ".cpp";
        public const string dsExt = ".ds";
    }

    public class EmitCPP : EmitInterface
    {
        private const string autogenCPPFolder = "CPP";

        public EmitCPP(string rootPath)
        {
            this.rootPath = rootPath + @"\" + autogenCPPFolder;
            if (!System.IO.Directory.Exists(this.rootPath))
            {
                System.IO.Directory.CreateDirectory(this.rootPath);
            }
            ProtoCore.TargetLanguage.CPP.TypeMap.InitializeMap();
        }

        private string GenerateHeaderFilename(string functionName)
        {
            return rootPath + @"\" + functionName + Constants.headerExt;
        }

        private string GenerateCPPFilename(string functionName)
        {
            return rootPath + @"\" + functionName + Constants.cppExt;
        }
        
        public override void Emit(string filename, List<AssociativeNode> sourceCode)
        {
            // Create a list of importNodes from the compiled code
            // Get the function definition if it exists
            List<AssociativeNode> stmts = new List<AssociativeNode>();
            List<FunctionDefinitionNode> funcDefList = new List<FunctionDefinitionNode>();
            foreach (AssociativeNode node in sourceCode)
            {
                if (node is FunctionDefinitionNode) {
                    funcDefList.Add(node as FunctionDefinitionNode);
                }
                else {
                    // Any other statement
                    stmts.Add(node);
                }
            }

            string headerSourcePathFilename = GenerateHeaderFilename(filename);
            string cppSourcePathFilename = GenerateCPPFilename(filename);

            string headerCode = string.Empty;
            string cppCode = string.Empty;

            //--------------------------------------
            // Generate header file code
            //--------------------------------------
            foreach (ProtoCore.AST.AssociativeAST.AssociativeNode assocNode in stmts) {
                headerCode += assocNode.ToStringCPP();
            }

            // Process the function if it exists
            if (funcDefList.Count > 0)
            {
                cppCode = EmitIncludeStmt(headerSourcePathFilename);
                foreach (FunctionDefinitionNode funcDef in funcDefList)
                {
                    headerCode += EmitFunctionSignature(funcDef) + ";" + Constants.CRLF;

                    //--------------------------------------
                    // Generate cpp code
                    //--------------------------------------
                    cppCode += EmitFunctionDefinition(funcDef);
                    cppCode += Constants.CRLF;
                }
            }

            Commit(headerSourcePathFilename, headerCode);
            Commit(cppSourcePathFilename, cppCode);
        }

        public override void Emit(string filename, ProtoCore.AST.AssociativeAST.FunctionDefinitionNode funcDef)
        {
            string headerSourcePathFilename = GenerateHeaderFilename(filename);
            string cppSourcePathFilename = GenerateCPPFilename(filename);

            string headerCode = string.Empty;
            string cppCode = string.Empty; 

            // Generate header code
            headerCode = EmitFunctionSignature(funcDef) + ";" + Constants.CRLF;

            // Generate cpp code
            cppCode = EmitFunctionDefinition(funcDef);

            Commit(headerSourcePathFilename, headerCode);
            Commit(cppSourcePathFilename, cppCode);
        }

        /// <summary>
        /// Generate a list of sensitivity list variables given the language block dependencies
        /// </summary>
        /// <param name="languageBlock"></param>
        /// <returns></returns>
        private static List<AssociativeNode> GenerateSensitivityList(AST.AssociativeAST.LanguageBlockNode languageBlock)
        {
            List<AssociativeNode> sensitivityList = new List<AssociativeNode>();
            foreach (AssociativeNode dependent in languageBlock.Dependencies)
            {
                IdentifierNode identNode = dependent as IdentifierNode;
                Validity.Assert(null != identNode);
                VarDeclNode typedIdent = AST.AssociativeAST.AstFactory.BuildVarDeclNode(identNode.Name, PrimitiveType.kTypeInt);
                sensitivityList.Add(typedIdent);
            }
            return sensitivityList;
        }
        

        /// <summary>
        /// Emit a region of code that annotates the start of a process
        /// </summary>
        /// <param name="languageBlock"></param>
        /// <returns></returns>
        private static string EmitProcessAnnotation(AST.AssociativeAST.LanguageBlockNode languageBlock)
        {
            string processHeader = string.Empty;
            return processHeader;
        }

        public static string EmitTypedIdentifier(AST.AssociativeAST.TypedIdentifierNode typedIdent)
        {
            string staticType = typedIdent.IsStatic ? "static " : string.Empty;
            string dsType = typedIdent.datatype.Name;
            string dsVarName = typedIdent.Name;
            string cppType = ProtoCore.TargetLanguage.CPP.TypeMap.GetCPPType(dsType);
            return staticType + cppType + " " + dsVarName;
        }

        public static string EmitIncludeStmt(AST.AssociativeAST.ImportNode importNode)
        {
            string headerPath = importNode.ModuleName.Replace(Constants.dsExt, Constants.headerExt);
            string includeStmt = "#include " + '"' + headerPath + '"' + Constants.CRLF;
            return includeStmt;
        }

        private static string EmitIncludeStmt(string headerPathFilename)
        {
            string includeStmt = "#include " + '"' + headerPathFilename + '"' + Constants.CRLF;
            return includeStmt;
        }

        public static string EmitVarDeclaration(AST.AssociativeAST.VarDeclNode varDeclNode)
        {
            string dsType = varDeclNode.ArgumentType.Name;
            string dsVarName = varDeclNode.NameNode.Name;
            string cppType = ProtoCore.TargetLanguage.CPP.TypeMap.GetCPPType(dsType);
            string staticKW = varDeclNode.IsStatic ? "static " : string.Empty;
            return staticKW + cppType + " " + dsVarName;
        }
        
        public static string EmitFunctionSignature(AST.AssociativeAST.FunctionDefinitionNode funcDef)
        {
            string retType = ProtoCore.TargetLanguage.CPP.TypeMap.GetCPPType(funcDef.ReturnType.Name);
            string name = funcDef.Name;
            string args = funcDef.Signature.ToStringCPP();
            string body = funcDef.FunctionBody.ToStringCPP();

            string cppFuncSig = retType + " " + name + "(" + args + ")";
            return cppFuncSig;
        }

        /// <summary>
        /// Generate the function signature and body
        /// </summary>
        /// <param name="funcDef"></param>
        /// <returns></returns>
        public static string EmitFunctionDefinition(AST.AssociativeAST.FunctionDefinitionNode funcDef)
        {
            string functionSig = EmitFunctionSignature(funcDef);
            string body = funcDef.FunctionBody.ToStringCPP();

            bool containsReturnStmt = funcDef.ReturnType.Name != "void";
            if (!containsReturnStmt) {
                body += Constants.CRLF + "return 0;";
            }


            string cppFuncDef =
                functionSig + Constants.CRLF
                + "{" 
                + Constants.CRLF
                + body
                + Constants.CRLF
                + "}";
            return cppFuncDef;
        }
    }
}
