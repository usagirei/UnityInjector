// --------------------------------------------------
// UnityInjector - DebugPlugin.cs
// --------------------------------------------------

using System;
using System.Reflection;

using UnityEngine;

using UnityInjector.Attributes;
using UnityInjector.ConsoleUtil;

namespace UnityInjector.Plugins
{
    [PluginName("Debug Plugin")]
    [PluginVersion("1.0.0.2")]
    public class DebugPlugin : PluginBase
    {
        private static ConsoleMirror _consoleMirror;

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
            switch (type)
            {
                case LogType.Warning:
                case LogType.Assert:
                    SafeConsole.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogType.Error:
                case LogType.Exception:
                    SafeConsole.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    SafeConsole.ForegroundColor = ConsoleColor.Gray;
                    break;
            }

            Console.WriteLine(message);

            SafeConsole.ForegroundColor = ConsoleColor.Gray;
        }

        public void Awake()
        {
            if (!Enabled)
                return;
            DontDestroyOnLoad(this);
            try
            {
                ConsoleWindow.Attach();
                if (Mirror && _consoleMirror == null)
                    _consoleMirror = new ConsoleMirror("debug.log");

                Console.WriteLine("Initialized Debug Plugin");

                SafeConsole.ForegroundColor = ConsoleColor.Cyan;

                const int width = 79;

                Console.WriteLine(new string('=', width));
                Console.WriteLine("If this message is gray, this game was compiled".PadCenter(width));
                Console.WriteLine("With a mscorlib missing Console Properties".PadCenter(width));
                Console.WriteLine("In which case, avoid setting colors via the System.Console type.".PadCenter(width));
                Console.WriteLine("Use the SafeConsole type Properties provided in UnityInjector instead".PadCenter(width));
                Console.WriteLine(new string('=', width));

                SafeConsole.ForegroundColor = ConsoleColor.Gray;

                Application.RegisterLogCallback(HandleLog);
                Console.WriteLine("Log Callback Hooked");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
