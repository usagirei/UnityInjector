// --------------------------------------------------
// UnityInjector - PluginNameAttribute.cs
// --------------------------------------------------

using System;

namespace UnityInjector.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginNameAttribute : Attribute
    {
        public string Name { get; }

        public PluginNameAttribute(string name)
        {
            Name = name;
        }
    }
}
