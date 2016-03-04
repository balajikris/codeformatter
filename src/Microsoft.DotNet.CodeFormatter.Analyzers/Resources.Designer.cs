﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microsoft.DotNet.CodeFormatter.Analyzers {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Microsoft.DotNet.CodeFormatter.Analyzers.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Explicit &apos;this&apos; is not necessary in reference to private field &apos;{0}&apos;.
        /// </summary>
        internal static string ExplicitThisAnalyzer_MessageFormat {
            get {
                return ResourceManager.GetString("ExplicitThisAnalyzer_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Don&apos;t use explicit &apos;this&apos; for private fields.
        /// </summary>
        internal static string ExplicitThisAnalyzer_Title {
            get {
                return ResourceManager.GetString("ExplicitThisAnalyzer_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Remove &apos;this&apos; qualifier.
        /// </summary>
        internal static string ExplicitThisFixer_Title {
            get {
                return ResourceManager.GetString("ExplicitThisFixer_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Variable &apos;{0}&apos; should be declared with an explicit type.
        /// </summary>
        internal static string ExplicitVariableTypeAnalyzer_MessageFormat {
            get {
                return ResourceManager.GetString("ExplicitVariableTypeAnalyzer_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Replace the usage of &apos;var&apos; with an explicit type.
        /// </summary>
        internal static string ExplicitVariableTypeAnalyzer_Title {
            get {
                return ResourceManager.GetString("ExplicitVariableTypeAnalyzer_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Replace the usage of &apos;var&apos; with an explicit type.
        /// </summary>
        internal static string ExplicitVariableTypeFixer_Title {
            get {
                return ResourceManager.GetString("ExplicitVariableTypeFixer_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Namespace &apos;{0}&apos; is unused in this file.
        /// </summary>
        internal static string OptimizeNamespaceImportsAnalyzer_MessageFormat {
            get {
                return ResourceManager.GetString("OptimizeNamespaceImportsAnalyzer_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Remove unused namespace imports.
        /// </summary>
        internal static string OptimizeNamespaceImportsAnalyzer_Title {
            get {
                return ResourceManager.GetString("OptimizeNamespaceImportsAnalyzer_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Remove unused namespace imports.
        /// </summary>
        internal static string OptimizeNamespaceImportsFixer_Title {
            get {
                return ResourceManager.GetString("OptimizeNamespaceImportsFixer_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Place namespace import statements outside a namespace..
        /// </summary>
        internal static string PlaceImportsOutsideNamespace_MessageFormat {
            get {
                return ResourceManager.GetString("PlaceImportsOutsideNamespace_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Place namespace import statements outside a namespace..
        /// </summary>
        internal static string PlaceImportsOutsideNamespace_Title {
            get {
                return ResourceManager.GetString("PlaceImportsOutsideNamespace_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Move import statements outside the namespace..
        /// </summary>
        internal static string PlaceImportsOutsideNamespaceFixer_Title {
            get {
                return ResourceManager.GetString("PlaceImportsOutsideNamespaceFixer_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The field &apos;{0}&apos; is never written to and can be marked readonly.
        /// </summary>
        internal static string UnwrittenWritableFieldAnalyzer_MessageFormat {
            get {
                return ResourceManager.GetString("UnwrittenWritableFieldAnalyzer_MessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mark unwritten fields readonly.
        /// </summary>
        internal static string UnwrittenWritableFieldAnalyzer_Title {
            get {
                return ResourceManager.GetString("UnwrittenWritableFieldAnalyzer_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mark field as readonly.
        /// </summary>
        internal static string UnwrittenWritableFieldFixer_Title {
            get {
                return ResourceManager.GetString("UnwrittenWritableFieldFixer_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to use explicit type instead of &apos;var&apos;.
        /// </summary>
        internal static string UseExplicitType {
            get {
                return ResourceManager.GetString("UseExplicitType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to use explicit type.
        /// </summary>
        internal static string UseExplicitTypeDiagnosticTitle {
            get {
                return ResourceManager.GetString("UseExplicitTypeDiagnosticTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to use &apos;var&apos; instead of explicit type.
        /// </summary>
        internal static string UseImplicitType {
            get {
                return ResourceManager.GetString("UseImplicitType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to use var.
        /// </summary>
        internal static string UseImplicitTypeDiagnosticTitle {
            get {
                return ResourceManager.GetString("UseImplicitTypeDiagnosticTitle", resourceCulture);
            }
        }
    }
}
