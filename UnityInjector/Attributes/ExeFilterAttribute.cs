// --------------------------------------------------
// UnityInjector - ExeFilterAttribute.cs
// Copyright (c) Usagirei 2015 - 2015
// --------------------------------------------------

using System;

namespace UnityInjector.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class PluginFilterAttribute : Attribute
    {
        public string ExeName { get; }

        public PluginFilterAttribute(string exeName)
        {
            ExeName = exeName;
        }
    }
}
