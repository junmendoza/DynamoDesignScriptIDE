﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GeometryUI.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GeometryUI.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exports the specified geometry to the given SAT  file path..
        /// </summary>
        internal static string ExportToSATDescripiton {
            get {
                return ResourceManager.GetString("ExportToSATDescripiton", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File to export the geometry to..
        /// </summary>
        internal static string ExportToSatFilePathDescription {
            get {
                return ResourceManager.GetString("ExportToSatFilePathDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The file path of the exported file. Note this may change from the input in it contains non-ASCII characters..
        /// </summary>
        internal static string ExportToSatFilePathOutputDescription {
            get {
                return ResourceManager.GetString("ExportToSatFilePathOutputDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Geometry to export into a SAT file..
        /// </summary>
        internal static string ExportToSatGeometryInputDescription {
            get {
                return ResourceManager.GetString("ExportToSatGeometryInputDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Export;SAT.
        /// </summary>
        internal static string ExportWithUnitsSearchTags {
            get {
                return ResourceManager.GetString("ExportWithUnitsSearchTags", resourceCulture);
            }
        }
    }
}