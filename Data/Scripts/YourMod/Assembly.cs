using Sandbox.ModAPI;
using System.Collections.Generic;
using VRage.Game.ModAPI;
using YourMod.ModularAssemblies.Communication;

namespace YourMod
{
    /// <summary>
    /// Single assembly instance logic class. You should put your assembly logic here!
    /// </summary>
    internal class Assembly
    {
        private static ModularDefinitionApi ModularApi => ModularAssemblies.ModularDefinition.ModularApi;

        public readonly int AssemblyId;
        public readonly IMyCubeGrid Grid;

        // example assembly property; Modular Assemblies stores these as best as it can through assembly splits and session reloads. Publicly accessible.
        // IMPORTANT - NONE OF THIS IS FULLY SYNCED IN MULTIPLAYER!
        // Assembly properties are saved to the grid every two seconds.
        // SimpleSync from Detection Equipment may be a useful starting point if desync is a concern. https://github.com/ari-steas/Detection-Equipment/blob/master/Data/Scripts/DetectionEquipment/Shared/Networking/SimpleSync.cs
        public float NumberBatteries
        {
            get { return ModularApi.GetAssemblyProperty<float>(AssemblyId, "NumberBatteries"); }
            set { ModularApi.SetAssemblyProperty(AssemblyId, "NumberBatteries", value); }
        }

        private HashSet<IMyCubeBlock> _blocks = new HashSet<IMyCubeBlock>();

        /// <summary>
        /// Triggered when the assembly is first created.
        /// </summary>
        /// <param name="assemblyId"></param>
        public Assembly(int assemblyId)
        {
            AssemblyId = assemblyId;
            Grid = ModularApi.GetAssemblyGrid(assemblyId);

            // instantiation logic here
            MyAPIGateway.Utilities.ShowMessage("Modular Assemblies", $"ExampleDefinition_WithLogic constructor called. I'm on grid \"{Grid.CustomName}\"!\nTry turning on debug mode.");
        }

        /// <summary>
        /// Triggered once per game tick (60ups).
        /// </summary>
        public void UpdateTick()
        {
            // update logic here
            if (ModularApi.IsDebug())
            {
                MyAPIGateway.Utilities.ShowNotification($"Hello from assembly {AssemblyId}! I have {_blocks.Count} blocks.", 1000/60);
            }
        }

        /// <summary>
        /// Triggered when the assembly is closed (zero blocks contained).
        /// </summary>
        public void Unload()
        {
            // unload logic here
            MyAPIGateway.Utilities.ShowMessage("Modular Assemblies", $"ExampleDefinition_WithLogic.OnAssemblyClose called.\nAssembly: {AssemblyId}");
        }

        /// <summary>
        /// Triggers whenever a new part is added to an assembly.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="isBasePart"></param>
        public void OnPartAdd(IMyCubeBlock block, bool isBasePart)
        {
            _blocks.Add(block);

            // part add logic here
            MyAPIGateway.Utilities.ShowMessage("Modular Assemblies", $"ExampleDefinition_WithLogic.OnPartAdd called.\nAssembly: {AssemblyId}\nBlock: {block.DisplayNameText}\nIsBasePart: {isBasePart}");
            MyAPIGateway.Utilities.ShowNotification("Assembly has " + ModularApi.GetMemberParts(AssemblyId).Length + " blocks.");

            // example per-subtype logic
            switch (block.BlockDefinition.SubtypeId)
            {
                case "LargeBlockBatteryBlock":
                    NumberBatteries++;
                    MyAPIGateway.Utilities.ShowMessage($"Assembly {AssemblyId}", $"A battery was added!!! Battery number: {NumberBatteries}");
                    break;
                default:
                    MyAPIGateway.Utilities.ShowMessage($"Assembly {AssemblyId}", $"Something that isn't a battery was added. Battery number: {NumberBatteries}");
                    break;
            }
        }

        /// <summary>
        /// Triggers whenever a part is removed from an assembly.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="isBasePart"></param>
        public void OnPartRemove(IMyCubeBlock block, bool isBasePart)
        {
            // add any logic to be triggered on part removed
            MyAPIGateway.Utilities.ShowMessage("Modular Assemblies", $"ExampleDefinition_WithLogic.OnPartRemove called.\nAssembly: {AssemblyId}\nBlock: {block.DisplayNameText}\nIsBasePart: {isBasePart}");
            MyAPIGateway.Utilities.ShowNotification("Assembly has " + ModularApi.GetMemberParts(AssemblyId).Length + " blocks.");

            _blocks.Remove(block);
        }

        /// <summary>
        /// Triggers whenever a part is destroyed, just after OnPartRemove.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="isBasePart"></param>
        public void OnPartDestroy(IMyCubeBlock block, bool isBasePart)
        {
            // part destroy logic here (i.e. explosions or whatever)
            MyAPIGateway.Utilities.ShowMessage("Modular Assemblies", $"ExampleDefinition_WithLogic.OnPartDestroy called.\nI hope the explosion was pretty.");
        }
    }
}
