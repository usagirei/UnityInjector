using System;

namespace UnityInjector.Attributes
{

    [AttributeUsage(AttributeTargets.Class)]
    public class PluginNameAttribute : Attribute
    {
        #region Properties
        public string Name { get; }
        #endregion

        #region (De)Constructors
        public PluginNameAttribute(string name)
        {
            Name = name;
        }
        #endregion
    }

}