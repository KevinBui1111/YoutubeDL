﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YoutubeDL.Properties {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("YoutubeDL.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to INSERT INTO video (vid, vidFID,vidURL,vidFilename,vidSize,audFID,audURL,audFilename,audSize,resolution,ext,status,title,size,jsonYDL,jsonRecord)
        ///VALUES(&apos;{0}&apos;,&apos;{1}&apos;,&apos;{2}&apos;,&apos;{3}&apos;,&apos;{4}&apos;,&apos;{5}&apos;,&apos;{6}&apos;,&apos;{7}&apos;,&apos;{8}&apos;,&apos;{9}&apos;,&apos;{10}&apos;,&apos;{11}&apos;,&apos;{12}&apos;,&apos;{13}&apos;,&apos;{14}&apos;,&apos;{15}&apos;);.
        /// </summary>
        internal static string INSERT {
            get {
                return ResourceManager.GetString("INSERT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO video (vid, [group], channel_id, status, date_add)
        ///VALUES({0}, {1}, {2}, 0, {3});.
        /// </summary>
        internal static string INSERT_FORMAT {
            get {
                return ResourceManager.GetString("INSERT_FORMAT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE video 
        ///SET vidFID = &apos;{1}&apos;,
        ///    vidURL = &apos;{2}&apos;,
        ///    vidFilename = &apos;{3}&apos;,    
        ///    vidSize = &apos;{4}&apos;,
        ///    audFID = &apos;{5}&apos;,
        ///    audURL = &apos;{6}&apos;,
        ///    audFilename = &apos;{7}&apos;,
        ///    audSize = &apos;{8}&apos;,
        ///    resolution = &apos;{9}&apos;,
        ///    ext = &apos;{10}&apos;,
        ///    status = &apos;{11}&apos;,
        ///    title = &apos;{12}&apos;,
        ///    size = &apos;{13}&apos;,
        ///    jsonYDL = &apos;{14}&apos;,
        ///    jsonRecord = &apos;{15}&apos;
        ///WHERE vid = &apos;{0}&apos;;.
        /// </summary>
        internal static string UPDATE {
            get {
                return ResourceManager.GetString("UPDATE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE video 
        ///SET status = {1}
        ///    ,title = {2}    
        ///    ,fps60 = {3}    
        ///    ,date_format = {4}    
        ///    ,jsonYDL = {5}
        ///WHERE vid = {0};.
        /// </summary>
        internal static string UPDATE_AFTERLOADING {
            get {
                return ResourceManager.GetString("UPDATE_AFTERLOADING", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE video
        ///SET filename = {1}
        ///WHERE vid = {0};.
        /// </summary>
        internal static string UPDATE_FILENAME {
            get {
                return ResourceManager.GetString("UPDATE_FILENAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE video 
        ///SET vidFID = {1},
        ///    vidURL = {2},
        ///    vidFilename = {3},    
        ///    vidSize = {4},    
        ///
        ///    audFID = {5},
        ///    audURL = {6},
        ///    audFilename = {7},
        ///    audSize = {8},    
        ///
        ///    resolution = {9},
        ///    ext = {10},
        ///    filename = {11},
        ///    size = {12},
        ///    status = {13}
        ///WHERE vid = {0};.
        /// </summary>
        internal static string UPDATE_FORMAT {
            get {
                return ResourceManager.GetString("UPDATE_FORMAT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE video 
        ///SET [group] = {1}
        ///    ,channel_id = {2}
        ///WHERE vid = {0};.
        /// </summary>
        internal static string UPDATE_GROUP {
            get {
                return ResourceManager.GetString("UPDATE_GROUP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE video 
        ///SET status = {1}
        ///    ,filename = {2}
        ///    ,date_merge = {3}
        ///WHERE vid = {0};.
        /// </summary>
        internal static string UPDATE_MERGING {
            get {
                return ResourceManager.GetString("UPDATE_MERGING", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE video 
        ///SET filename = {1},
        ///    [group] = {2},
        ///    fps60 = {3},
        ///    date_add = {4},
        ///    date_format = {5},
        ///    date_merge = {6}
        ///WHERE vid = {0};.
        /// </summary>
        internal static string UPDATE_NEWFIELD {
            get {
                return ResourceManager.GetString("UPDATE_NEWFIELD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE video 
        ///SET status = {1}
        ///WHERE vid = {0};.
        /// </summary>
        internal static string UPDATE_STATUS {
            get {
                return ResourceManager.GetString("UPDATE_STATUS", resourceCulture);
            }
        }
    }
}
