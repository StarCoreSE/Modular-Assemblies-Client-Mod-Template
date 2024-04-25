using System.Collections.Generic;
using Sandbox.ModAPI;
using VRageMath;
using static Scripts.ModularAssemblies.Communication.DefinitionDefs;

namespace Scripts.ModularAssemblies
{
    /* Hey there modders!
     *
     * This file is a *template*. Make sure to keep up-to-date with the latest version, which can be found at 
     */
    internal partial class ModularDefinition
    {
        // You can declare functions in here, and they are shared between all other ModularDefinition files.
        // However, for all but the simplest of assemblies it would be wise to have a separate utilities class.

        // This is the important bit.
        internal ModularPhysicalDefinition ExampleDefinition => new ModularPhysicalDefinition
        {
            // Unique name of the definition.
            Name = "ExampleDefinition",

            OnInit = () =>
            {
                MyAPIGateway.Utilities.ShowMessage("Modular Assemblies", "ExampleDefinition.OnInit called.");
            },

            // Triggers whenever a new part is added to an assembly.
            OnPartAdd = (assemblyId, block, isBasePart) =>
            {
                MyAPIGateway.Utilities.ShowMessage("Modular Assemblies", $"ExampleDefinition.OnPartAdd called.\nAssembly: {assemblyId}\nBlock: {block.DisplayNameText}\nIsBasePart: {isBasePart}");
            },

            // Triggers whenever a part is removed from an assembly.
            OnPartRemove = (assemblyId, block, isBasePart) =>
            {
                MyAPIGateway.Utilities.ShowMessage("Modular Assemblies", $"ExampleDefinition.OnPartAdd called.\nAssembly: {assemblyId}\nBlock: {block.DisplayNameText}\nIsBasePart: {isBasePart}");
            },

            // Triggers whenever a part is destroyed, just after OnPartRemove.
            OnPartDestroy = (assemblyId, block, isBasePart) =>
            {
                // You can remove this function, and any others if need be.
                MyAPIGateway.Utilities.ShowMessage("Modular Assemblies", $"ExampleDefinition.OnPartAdd called.\nAssembly: {assemblyId}\nBlock: {block.DisplayNameText}\nIsBasePart: {isBasePart}");
            },

            // The most important block in an assembly. Connection checking starts here.
            BaseBlock = null,

            // All SubtypeIds that can be part of this assembly.
            AllowedBlocks = new string[]
            {
                
            },

            // Allowed connection directions & whitelists, measured in blocks.
            // If an allowed SubtypeId is not included here, connections are allowed on all sides.
            // If the connection type whitelist is empty, all allowed subtypes may connect on that side.
            AllowedConnections = new Dictionary<string, Dictionary<Vector3I, string[]>>
            {
                
            },
        };
    }
}
