// --------------------------------------------------
// UnityInjector.Patcher - UnityInjectorPatch.cs
// Copyright (c) Usagirei 2015 - 2015
// --------------------------------------------------

using System;
using System.IO;
using System.Linq;

using Mono.Cecil;
using Mono.Cecil.Cil;

using ReiPatcher;
using ReiPatcher.Patch;

namespace UnityInjector.Patcher
{
    internal class UnityInjectorPatch : PatchBase
    {
        private const string TOKEN = "UnityInjector";

        public string ClassName
        {
            get { return RPConfig.GetConfig(TOKEN, "Class"); }
            set { RPConfig.SetConfig(TOKEN, "Class", value); }
        }

        private AssemblyDefinition InjectorDef { get; set; }

        public string MethodName
        {
            get { return RPConfig.GetConfig(TOKEN, "Method"); }
            set { RPConfig.SetConfig(TOKEN, "Method", value); }
        }

        private static string AssemblyName
        {
            get { return RPConfig.GetConfig(TOKEN, "Assembly"); }
            set { RPConfig.SetConfig(TOKEN, "Assembly", value); }
        }

        public override string Name => "UnityInjector Patch";
        public override string Version => GetType().Assembly.GetName().Version.ToString();

        public override bool CanPatch(PatcherArguments args)
        {
            if (
                string.IsNullOrEmpty(AssemblyName)
                || string.IsNullOrEmpty(ClassName)
                || string.IsNullOrEmpty(MethodName)
                )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid or Missing UnityInjector Configurations");
                Console.WriteLine("Check your INI configuration file");
                Console.ForegroundColor = ConsoleColor.Gray;
                return false;
            }
            if (args.Assembly.Name.Name != AssemblyName
                || InjectorDef == null
                || string.IsNullOrEmpty(ClassName)
                || string.IsNullOrEmpty(MethodName))
                return false;

            var patchAttr = GetPatchedAttributes(args.Assembly);

            if (patchAttr.Any(attr => attr.Info.Equals(TOKEN)))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Assembly Already Patched");
                Console.ForegroundColor = ConsoleColor.Gray;
                return false;
            }

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

            SetPatchedAttribute(args.Assembly, TOKEN);
        }

        public override void PrePatch()
        {
            bool invalidSetting = false;
            if (string.IsNullOrEmpty(ClassName))
            {
                ClassName = string.Empty;
                invalidSetting = true;
            }
            if (string.IsNullOrEmpty(MethodName))
            {
                MethodName = string.Empty;
                invalidSetting = true;
            }
            if (string.IsNullOrEmpty(AssemblyName))
            {
                AssemblyName = string.Empty;
                invalidSetting = true;
            }

            if (invalidSetting)
            {
                RPConfig.Save();
                return;
            }


            RPConfig.RequestAssembly($"{AssemblyName}.dll");
            var path = Path.Combine(AssembliesDir, "UnityInjector.dll");
            if (!File.Exists(path))
            {
                Console.WriteLine("UnityInjector DLL Missing");
                return;
            }
            using (Stream s = File.OpenRead(path))
                InjectorDef = AssemblyDefinition.ReadAssembly(s);
        }
    }
}
