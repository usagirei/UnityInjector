﻿// --------------------------------------------------
// UnityInjector - PluginManager.cs
// --------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

using UnityEngine;

using UnityInjector.Attributes;
using UnityInjector.Plugins;

namespace UnityInjector
{
    internal static class PluginManager
    {
        public static bool IsInitialized { get; private set; }

        public static void Init()
        {
            if (IsInitialized)
                return;

            IsInitialized = true;

            var managerObject = new GameObject(nameof(UnityInjector));

            managerObject.AddComponent<DebugPlugin>();

            var plugins = new List<Type>();
            try
            {
                plugins.AddRange(LoadPlugins());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            foreach (var type in plugins)
            {
                Console.WriteLine("Adding Component: '{0}'", type.Name);
                try
                {
                    var x = managerObject.AddComponent(type);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            Console.WriteLine("Plugin Manager End");
            managerObject.SetActive(true);
        }

        private static IEnumerable<Type> LoadPlugins()
        {
            var exeName = (Process.GetCurrentProcess().ProcessName);

            if (!Directory.Exists(Extensions.PluginsPath))
            {
                Directory.CreateDirectory(Extensions.PluginsPath);
                return Enumerable.Empty<Type>();
            }

            var plugins = Directory.GetFiles(Extensions.PluginsPath, "*.dll")
                                   .SelectMany(s => LoadPlugins_DLL(s, exeName));

            return plugins;
        }

        private static IEnumerable<Type> LoadPlugins_DLL(string dll, string exe)
        {
            var plugins = new List<Type>();
            try
            {
                Console.WriteLine($"Loading Assembly: '{dll}'");
                var assembly = Assembly.LoadFile(dll);

                foreach (var t in assembly.GetTypes().Where(t => typeof(PluginBase).IsAssignableFrom(t)))
                {
                    try
                    {
                        //var instance = (PluginBase) Activator.CreateInstance(t);
                        var attribs = t.GetCustomAttributes(false);

                        var filter = attribs.OfType<PluginFilterAttribute>().Select(a => a.ExeName).ToList();
                        var name = attribs.OfType<PluginNameAttribute>().FirstOrDefault()?.Name ??
                                   t.Assembly.GetName().Name;
                        var ver = attribs.OfType<PluginVersionAttribute>().FirstOrDefault()?.Version ??
                                  t.Assembly.GetName().Version.ToString();

                        if (!filter.Any() || filter.Contains(exe, StringComparer.InvariantCultureIgnoreCase))
                            plugins.Add(t);

                        Console.WriteLine($"Loaded Plugin: '{name} {ver}'");
                    }
                    catch (Exception ex)
                    {
#if COLOR
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(ex.ToString());
                        Console.ForegroundColor = ConsoleColor.Gray;
#else
                        Console.WriteLine(ex.ToString());
#endif
                    }
                }
            }
            catch (Exception ex)
            {
#if COLOR
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = ConsoleColor.Gray;
#else
                Console.WriteLine(ex.ToString());
#endif
            }
            return plugins;
        }
    }
}
