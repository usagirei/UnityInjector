// --------------------------------------------------
// DebugPlugin - DebugPlugin.cs
// --------------------------------------------------

#region Usings
using System;

using UnityEngine;

using UnityInjector.Attributes;

#endregion

namespace UnityInjector.Plugins
{

    [PluginName("Debug Plugin")]
    [PluginVersion("1.0.0.0")]
    public class DebugPlugin : PluginBase
    {
        #region Properties
        private bool Enabled
        {
            get
            {
                bool enable;
                bool parsed = bool.TryParse(Preferences["Config"]["Enable"].Value, out enable);

                return (!parsed || enable);
            }
        }
        #endregion

        #region (De)Constructors
        public DebugPlugin()
        {
            Console.WriteLine("Initializing Debug Plugin");
            if (Preferences.HasSection("Config")
                && Preferences["Config"].HasKey("Enable")
                && !string.IsNullOrEmpty(Preferences["Config"]["Enable"].Value))
                return;

            Preferences["Config"]["Enable"].Value = bool.TrueString;
            SaveConfig();
        }
        #endregion

        #region Public Methods
        public void Awake()
        {
            if (!Enabled)
                return;
            try
            {
                ConsoleWindow.Attach();
                var mirr = new ConsoleMirror("debug.log");

                Console.WriteLine("Callback Hooked");
                Application.RegisterLogCallback(HandleLog);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion

        #region Static Methods
        private static void HandleLog(string message, string stackTrace, LogType type)
        {
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
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        #endregion
    }

}