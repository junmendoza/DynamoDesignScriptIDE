using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoCore;
using ProtoCore.Utils;

namespace ProtoCore.TargetLanguage.CPP
{
    public struct Keyword
    {
        public static string kInt = "int";
        public static string kIntPointer = "int*";
        public static string kIntReference = "int&";
        public static string kDouble = "double";
        public static string kBool = "bool";
        public static string kUndefined = "CPPTypeUndefined";
    }

    public static class TypeMap 
    {
        private static Dictionary<string, string> dsToCPPMap;
        
        public static void InitializeMap()
        {
            dsToCPPMap = new Dictionary<string, string>();
            dsToCPPMap[TypeSystem.GetPrimitTypeName(PrimitiveType.kTypeVar)] = Keyword.kInt;
            dsToCPPMap[TypeSystem.GetPrimitTypeName(PrimitiveType.kTypeInt)] = Keyword.kInt;
            dsToCPPMap[TypeSystem.GetPrimitTypeName(PrimitiveType.kTypeInput)] = Keyword.kInt;
            dsToCPPMap[TypeSystem.GetPrimitTypeName(PrimitiveType.kTypeBool)] = Keyword.kBool;
            dsToCPPMap[TypeSystem.GetPrimitTypeName(PrimitiveType.kTypeDouble)] = Keyword.kDouble;
            dsToCPPMap[TypeSystem.GetPrimitTypeName(PrimitiveType.kTypeOutput)] = Keyword.kIntReference;
        }

        /// <summary>
        /// Return the associated Cpp keyword given the DS type
        /// </summary>
        /// <param name="dsType"></param>
        /// <returns></returns>
        public static string GetCPPType(string dsType)
        {
            if (dsToCPPMap.ContainsKey(dsType)) {
                return dsToCPPMap[dsType];
            }
            return Keyword.kUndefined;
        }
    }
}
