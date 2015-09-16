// --------------------------------------------------
// UnityInjector - DebugPlugin.cs
// --------------------------------------------------

using System;

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

        private int CodePage
        {
            get
            {
                int codePage;
                var parsed = int.TryParse(Preferences["Config"][nameof(CodePage)].Value, out codePage);

                return parsed ? codePage : -1;
            }
        }

        private bool Enabled
        {
            get
            {
                bool enable;
                var parsed = bool.TryParse(Preferences["Config"][nameof(Enabled)].Value, out enable);

                return (!parsed || enable);
            }
        }

        private bool Mirror
        {
            get
            {
                bool enable;
                var parsed = bool.TryParse(Preferences["Config"][nameof(Mirror)].Value, out enable);

                return (!parsed || enable);
            }
        }

        public DebugPlugin()
        {
            var init = false;
            init |= InitConfig("Config", nameof(Enabled), bool.TrueString, "Enables Debug Plugin");
            init |= InitConfig("Config", nameof(Mirror), bool.FalseString, "Enables Mirroring to debug.log");
            init |= InitConfig("Config", nameof(CodePage), "-1", "CodePage, -1 for System Default");
            if (init)
                SaveConfig();
        }

        private bool InitConfig(string section, string key, string value, params string[] comments)
        {
            if (!Preferences.HasSection(section)
                || !Preferences[section].HasKey(key)
                || string.IsNullOrEmpty(Preferences[section][key].Value))
            {
                Preferences[section][key].Value = value;
                Preferences[section][key].Comments.Comments.AddRange(comments);
                return true;
            }
            return false;
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

                if (CodePage != -1)
                {
                    uint uCodePage = (uint) CodePage;
                    ConsoleEncoding.ConsoleCodePage = uCodePage;
                    Console.OutputEncoding = ConsoleEncoding.GetEncoding(uCodePage);
                    Console.WriteLine($"Console CodePage set to '{CodePage}'");
                }

                const int width = 79;

                SafeConsole.ForegroundColor = ConsoleColor.Cyan;
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
