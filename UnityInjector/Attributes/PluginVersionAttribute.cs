using System;

namespace UnityInjector.Attributes
{

    [AttributeUsage(AttributeTargets.Class)]
    public class PluginVersionAttribute : Attribute
    {
        #region Properties
        public string Version { get; }
        #endregion

        #region (De)Constructors
        public PluginVersionAttribute( string version)
        {
            Version = version;
        }
        #endregion
    }

}