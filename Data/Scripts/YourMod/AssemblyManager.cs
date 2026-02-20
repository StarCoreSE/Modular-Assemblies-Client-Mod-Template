using Sandbox.ModAPI;
using System.Collections.Generic;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using YourMod.ModularAssemblies.Communication;
namespace YourMod
{
    /// <summary>
    /// Example SessionComponent for managing assemblies with external logic.
    /// </summary>
    [MySessionComponentDescriptor(MyUpdateOrder.AfterSimulation)]
    // ReSharper disable once ClassNeverInstantiated.Global - class instantiated by game
    internal class AssemblyManager : MySessionComponentBase
    {
        // This class is a singleton.
        public static AssemblyManager I { get; private set; } = null;
        private static ModularDefinitionApi ModularApi => ModularAssemblies.ModularDefinition.ModularApi;

        public IEnumerable<Assembly> GetAssemblies => _assemblies.Values;

        private Dictionary<int, Assembly> _assemblies = new Dictionary<int, Assembly>();


        public override void LoadData()
        {
            I = this;
            ModularApi.Log("AssemblyManager ready.");
        }

        protected override void UnloadData()
        {
            foreach (var system in _assemblies.Values)
            {
                system.Unload();
            }
            I = null;
            ModularApi.Log("AssemblyManager closed.");
        }

        public override void UpdateAfterSimulation()
        {
            foreach (var assembly in _assemblies.Values)
            {
                assembly.UpdateTick();
            }
        }

        public static void OnPartAdd(int assemblyId, IMyCubeBlock block, bool isBaseBlock)
        {
            if (I == null)
                return;

            // find assembly, and register new one if not present.
            // ID is unique per-session
            Assembly assembly;
            if (!I._assemblies.TryGetValue(assemblyId, out assembly))
            {
                assembly = new Assembly(assemblyId);
                I._assemblies.Add(assemblyId, assembly);
                ModularApi.Log($"AssemblyManager created new assembly {assemblyId}.");
            }

            assembly.OnPartAdd(block, isBaseBlock);
        }

        public static void OnPartRemove(int assemblyId, IMyCubeBlock block, bool isBaseBlock)
        {
            // find assembly, and skip if not present.
            Assembly assembly;
            if (I == null || !I._assemblies.TryGetValue(assemblyId, out assembly))
                return;

            assembly.OnPartRemove(block, isBaseBlock);
        }

        public static void OnPartDestroy(int assemblyId, IMyCubeBlock block, bool isBaseBlock)
        {
            // find assembly, and skip if not present.
            Assembly assembly;
            if (I == null || !I._assemblies.TryGetValue(assemblyId, out assembly))
                return;

            assembly.OnPartDestroy(block, isBaseBlock);
        }

        public static void OnAssemblyClose(int assemblyId)
        {
            // find assembly, and skip if not present.
            Assembly assembly;
            if (I == null || !I._assemblies.TryGetValue(assemblyId, out assembly))
                return;

            assembly.Unload();
            I._assemblies.Remove(assemblyId);
            ModularApi.Log($"AssemblyManager removed assembly {assemblyId}.");
        }
    }
}
