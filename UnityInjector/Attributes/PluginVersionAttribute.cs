// --------------------------------------------------
// UnityInjector - PluginVersionAttribute.cs
// --------------------------------------------------

using System;

namespace UnityInjector.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginVersionAttribute : Attribute
    {
        public string Version { get; }

        public PluginVersionAttribute(string version)
        {
            Version = version;
        }
    }
}
