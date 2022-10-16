
using AdditionalBloons.Tasks;
using AdditionalBloons.Utils;

using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Track;
using Assets.Scripts.Unity.UI_New.InGame.BloonMenu;

[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
[assembly: MelonInfo(typeof(AdditionalBloons.AdditionalBloons), "Additional Bloon Addon", "1.7", "1330 Studios LLC")]

namespace AdditionalBloons {
    public class AdditionalBloons : MelonMod {
        public override void OnApplicationStart() {
            HarmonyInstance.Patch(Method(typeof(GameModelLoader), nameof(GameModelLoader.Load)), postfix: new HarmonyMethod(Method(typeof(BloonCreator), nameof(BloonCreator.GameLoad))));
            HarmonyInstance.Patch(Method(typeof(BloonMenu), nameof(BloonMenu.CreateBloonButtons)), prefix: new HarmonyMethod(Method(typeof(BloonCreator), nameof(BloonCreator.BloonMenuCreate))));
            HarmonyInstance.Patch(Method(typeof(SpawnBloonButton), nameof(SpawnBloonButton.SpawnBloon)), prefix: new HarmonyMethod(Method(typeof(BloonCreator), nameof(BloonCreator.SpawnBloon))));
            HarmonyInstance.Patch(Method(typeof(InGame), nameof(InGame.Update)), postfix: new HarmonyMethod(Method(typeof(BloonTaskRunner), nameof(BloonTaskRunner.Run))));
            HarmonyInstance.Patch(Method(typeof(Bloon), nameof(Bloon.Damage)), prefix: new HarmonyMethod(Method(typeof(BloonTaskRunner), nameof(BloonTaskRunner.Damage))));
            HarmonyInstance.Patch(Method(typeof(Spawner), nameof(Spawner.Emit)), prefix: new HarmonyMethod(Method(typeof(BloonTaskRunner), nameof(BloonTaskRunner.Emit))));
            HarmonyInstance.Patch(Method(typeof(InGame), nameof(InGame.Quit)), postfix: new HarmonyMethod(Method(typeof(BloonTaskRunner), nameof(BloonTaskRunner.Quit))));
            HarmonyInstance.Patch(Method(typeof(InGame), nameof(InGame.Restart)), postfix: new HarmonyMethod(Method(typeof(BloonTaskRunner), nameof(BloonTaskRunner.Quit))));

            CacheBuilder.Build();

            MelonLogger.Msg(ConsoleColor.Red, "Additional Bloon Addon Loaded!");

            InternalVerification.Verify();
        }
    }
}