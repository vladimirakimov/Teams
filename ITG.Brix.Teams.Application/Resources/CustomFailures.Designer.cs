﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ITG.Brix.Teams.Application.Resources
{


    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class CustomFailures
    {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CustomFailures()
        {
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ITG.Brix.Teams.Application.Resources.CustomFailures", typeof(CustomFailures).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Operator was not created..
        /// </summary>
        public static string CreateOperatorFailure
        {
            get
            {
                return ResourceManager.GetString("CreateOperatorFailure", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Team was not created..
        /// </summary>
        public static string CreateTeamFailure
        {
            get
            {
                return ResourceManager.GetString("CreateTeamFailure", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Operator cannot be deleted..
        /// </summary>
        public static string DeleteOperatorFailure
        {
            get
            {
                return ResourceManager.GetString("DeleteOperatorFailure", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Team was not deleted..
        /// </summary>
        public static string DeleteTeamFailure
        {
            get
            {
                return ResourceManager.GetString("DeleteTeamFailure", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Team cannot be read..
        /// </summary>
        public static string GetTeamFailure
        {
            get
            {
                return ResourceManager.GetString("GetTeamFailure", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Operators cannot be read..
        /// </summary>
        public static string ListOperatorFailure
        {
            get
            {
                return ResourceManager.GetString("ListOperatorFailure", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Teams cannot be read..
        /// </summary>
        public static string ListTeamFailure
        {
            get
            {
                return ResourceManager.GetString("ListTeamFailure", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Query parameter $skip must be a sequence of digits..
        /// </summary>
        public static string SkipInvalid
        {
            get
            {
                return ResourceManager.GetString("SkipInvalid", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Query parameter $skip must be greater or equal to 0 and less than {0}..
        /// </summary>
        public static string SkipRange
        {
            get
            {
                return ResourceManager.GetString("SkipRange", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Operator not found.
        /// </summary>
        public static string OperatorNotFound
        {
            get
            {
                return ResourceManager.GetString("OperatorNotFound", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Team no found.
        /// </summary>
        public static string TeamNotFound
        {
            get
            {
                return ResourceManager.GetString("TeamNotFound", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Query parameter $top must be a sequence of digits..
        /// </summary>
        public static string TopInvalid
        {
            get
            {
                return ResourceManager.GetString("TopInvalid", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Query parameter $top must be greater than 0 and less than {0}..
        /// </summary>
        public static string TopRange
        {
            get
            {
                return ResourceManager.GetString("TopRange", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Team was not updated..
        /// </summary>
        public static string UpdateTeamFailure
        {
            get
            {
                return ResourceManager.GetString("UpdateTeamFailure", resourceCulture);
            }
        }
    }
}
