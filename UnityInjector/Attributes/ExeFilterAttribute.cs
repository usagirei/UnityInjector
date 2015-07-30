// --------------------------------------------------
// UnityInjector - ExeFilterAttribute.cs
// --------------------------------------------------

#region Usings
using System;

#endregion

namespace UnityInjector.Attributes
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class PluginFilterAttribute : Attribute
    {
        #region Properties
        public string ExeName { get; }
        #endregion

        #region (De)Constructors
        public PluginFilterAttribute(string exeName)
        {
            ExeName = exeName;
        }
        #endregion
    }

}