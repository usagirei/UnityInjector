// --------------------------------------------------
// UnityInjector - DebugPlugin.cs
// --------------------------------------------------

using System;

using UnityEngine;

using UnityInjector.Attributes;

namespace UnityInjector.Plugins
{
    [PluginName("Debug Plugin")]
    [PluginVersion("1.0.0.1")]
    public class DebugPlugin : PluginBase
    {
        private bool Enabled
        {
            get
            {
                bool enable;
                var parsed = bool.TryParse(Preferences["Config"]["Enable"].Value, out enable);

                return (!parsed || enable);
            }
        }

        private bool Mirror
        {
            get
            {
                bool enable;
                var parsed = bool.TryParse(Preferences["Config"]["Mirror"].Value, out enable);

                return (!parsed || enable);
            }
        }

        public DebugPlugin()
        {
            Console.WriteLine("Initializing Debug Plugin");
            if (!Preferences.HasSection("Config") || !Preferences["Config"].HasKey("Enable") ||
                string.IsNullOrEmpty(Preferences["Config"]["Enable"].Value))
            {
                Preferences["Config"]["Enable"].Value = bool.TrueString;
                SaveConfig();
            }

            if (!Preferences.HasSection("Config") || !Preferences["Config"].HasKey("Mirror") ||
                string.IsNullOrEmpty(Preferences["Config"]["Mirror"].Value))
            {
                Preferences["Config"]["Mirror"].Value = bool.FalseString;
                SaveConfig();
            }
        }

        private static void HandleLog(string message, string stackTrace, LogType type)
        {
#if COLOR
            switch (type)
            {
                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }
#endif
            Console.WriteLine(message);
#if COLOR
            Console.ForegroundColor = ConsoleColor.Gray;
#endif
        }

        public void Awake()
        {
            if (!Enabled)
                return;
            try
            {
                ConsoleWindow.Attach();
                if (Mirror)
                {
                    var mirr = new ConsoleMirror("debug.log");
                }

                Console.WriteLine("Callback Hooked");
                Application.RegisterLogCallback(HandleLog);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
