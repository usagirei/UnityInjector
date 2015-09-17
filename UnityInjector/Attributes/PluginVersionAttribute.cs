// --------------------------------------------------
// UnityInjector - PluginVersionAttribute.cs
// Copyright (c) Usagirei 2015 - 2015
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
