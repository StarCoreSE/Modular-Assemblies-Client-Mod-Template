using VRage.Game.Components;
using VRage.Utils;
using static YourMod.ModularAssemblies.Communication.DefinitionDefs;

namespace YourMod.ModularAssemblies.Communication
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
            StoredDef = global::YourMod.ModularAssemblies.ModularDefinition.GetBaseDefinitions();

            // Send definitions over as soon as the API loads, and create the API before anything else can init.
            global::YourMod.ModularAssemblies.ModularDefinition.ModularApi.Init(ModContext, SendDefinitions);
        }

        protected override void UnloadData()
        {
            global::YourMod.ModularAssemblies.ModularDefinition.ModularApi.UnloadData();
        }

        private void SendDefinitions()
        {
            global::YourMod.ModularAssemblies.ModularDefinition.ModularApi.RegisterDefinitions(StoredDef);
        }
    }
}