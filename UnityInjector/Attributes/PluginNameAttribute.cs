// --------------------------------------------------
// UnityInjector - PluginNameAttribute.cs
// Copyright (c) Usagirei 2015 - 2015
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
