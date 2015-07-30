// --------------------------------------------------
// UnityInjector_RP - RP_Patch.cs
// --------------------------------------------------

#region Usings
using System;
using System.IO;
using System.Linq;

using Mono.Cecil;
using Mono.Cecil.Cil;

using ReiPatcher;
using ReiPatcher.Patch;

#endregion

namespace UnityInjector.Patcher
{

    internal class RpPatch : PatchBase
    {
        #region Constants
        private const string TOKEN = "UnityInjector";
        #endregion

        #region Properties
        public string ClassName
        {
            get { return RPConfig.ConfigFile[TOKEN]["Class"].Value; }
            set { RPConfig.ConfigFile[TOKEN]["Class"].Value = value; }
        }

        public string MethodName
        {
            get { return RPConfig.ConfigFile[TOKEN]["Method"].Value; }
            set { RPConfig.ConfigFile[TOKEN]["Method"].Value = value; }
        }

        public override string Name => "UnityInjector Patch";
        public override string Version => GetType().Assembly.GetName().Version.ToString();
        private AssemblyDefinition InjectorDef { get; set; }
        #endregion

        #region Public Methods
        public override bool CanPatch(PatcherArguments args)
        {
            if (args.Assembly.Name.Name != "Assembly-CSharp"
                || InjectorDef == null
                || string.IsNullOrEmpty(ClassName)
                || string.IsNullOrEmpty(MethodName))
                return false;

            var module = args.Assembly.MainModule;
            var @class = module.GetType(ClassName);
            var patchAttr = GetPatchedAttributes(@class);

            if (patchAttr.Any(attr => attr.Info.Equals(TOKEN)))
                return false;

            return true;
        }

        public override void Patch(PatcherArguments args)
        {
            var module = args.Assembly.MainModule;

            var @class = module.GetType(ClassName);

            var meth = @class.Methods.FirstOrDefault(def => def.Name == MethodName);
            if (meth == null)
                return;

            var init = module.Import
                (InjectorDef.MainModule.GetType("UnityInjector.PluginManager")
                            .Methods.First(def => def.Name == "Init"));

            var ilp = meth.Body.GetILProcessor();
            var first = meth.Body.Instructions.First();
            ilp.InsertBefore(first, ilp.Create(OpCodes.Call, init));

            SetPatchedAttribute(@class, TOKEN);
        }

        public override void PrePatch()
        {
            if (string.IsNullOrEmpty(ClassName))
            {
                ClassName = string.Empty;
                RPConfig.Save();
            }
            if (string.IsNullOrEmpty(MethodName))
            {
                MethodName = string.Empty;
                RPConfig.Save();
            }

            RPConfig.RequestAssembly("Assembly-CSharp.dll");
            var path = Path.Combine(AssembliesDir, "UnityInjector.dll");
            if (!File.Exists(path))
            {
                Console.WriteLine("UnityInjector DLL Missing");
                return;
            }
            using (Stream s = File.OpenRead(path))
                InjectorDef = AssemblyDefinition.ReadAssembly(s);
        }
        #endregion
    }

}