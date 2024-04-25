﻿using System;
using VRage.Game;
using VRage.Game.Components;
using VRage.Utils;
using static Scripts.ModularAssemblies.Communication.DefinitionDefs;

namespace Scripts.ModularAssemblies.Communication
{
    [MySessionComponentDescriptor(MyUpdateOrder.Simulation, int.MinValue)]
    internal class ModularDefinitionSender : MySessionComponentBase
    {
        internal ModularDefinitionContainer StoredDef;

        public override void LoadData()
        {
            MyLog.Default.WriteLineAndConsole(
                $"{ModContext.ModName}.ModularDefinition: Init new ModularAssembliesDefinition");

            // Init
            StoredDef = Modular_Assemblies_Client_Mod_Template.Data.Scripts.ModularAssemblies.ModularDefinition.GetBaseDefinitions();

            // Send definitions over as soon as the API loads, and create the API before anything else can init.
            Modular_Assemblies_Client_Mod_Template.Data.Scripts.ModularAssemblies.ModularDefinition.ModularApi.Init(ModContext, SendDefinitions);
        }

        protected override void UnloadData()
        {
            Modular_Assemblies_Client_Mod_Template.Data.Scripts.ModularAssemblies.ModularDefinition.ModularApi.UnloadData();
        }

        private void SendDefinitions()
        {
            Modular_Assemblies_Client_Mod_Template.Data.Scripts.ModularAssemblies.ModularDefinition.ModularApi.RegisterDefinitions(StoredDef);
        }
    }
}