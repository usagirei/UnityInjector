// --------------------------------------------------
// UnityInjector - PluginBase.cs
// --------------------------------------------------

#region Usings

using System.IO;

using ExIni;

using UnityEngine;

#endregion

namespace UnityInjector
{
    /// <summary>
    ///     Abstract Class for generic unity plugins
    /// </summary>
    public abstract partial class PluginBase : MonoBehaviour
    {
        private IniFile _prefs;
        internal string ConfigPath => Extensions.CombinePaths(DataPath, Name.Asciify() + ".ini");

        /// <summary>
        ///     Plugins Data Path
        /// </summary>
        public string DataPath => Extensions.UserDataPath;

        /// <summary>
        ///     Plugin Name
        /// </summary>
        public string Name => GetType().Name;

        /// <summary>
        ///     Preferences Allowing for User Configuration
        /// </summary>
        public IniFile Preferences => _prefs ?? (_prefs = ReloadConfig());

        /// <summary>
        ///     Call to Reload Configuration File
        /// </summary>
        protected IniFile ReloadConfig()
        {
            if (!File.Exists(ConfigPath))
                return _prefs ?? new IniFile();

            var loaded = IniFile.FromFile(ConfigPath);
            if (_prefs == null)
                return _prefs = loaded;
            _prefs.Merge(loaded);
            return _prefs;
        }

        /// <summary>
        ///     Call to Save Configuration File
        /// </summary>
        protected void SaveConfig() => Preferences.Save(ConfigPath);
    }
}
