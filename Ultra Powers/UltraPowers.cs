global using Assets.Scripts.Models.Powers;

global using Ultra_Powers.PowerAdapters;

[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
[assembly: MelonInfo(typeof(Ultra_Powers.UltraPowers), "Ultra Powers", "1.7", "1330 Studios LLC")]

namespace Ultra_Powers;
internal sealed class UltraPowers : MelonMod {
    internal static bool PontoonDown, PortableLakeDown;

    public static readonly List<IPowerAdapter> powerAdapters = new() {
        new UltraCashDrop(),
        new UltraThrive(),
        new UltraGlueTrap(),
        new UltraTechBot(),
        new UltraRoadSpikes(),
        new UltraDartTime(),
        new UltraMonkeyBoost(),
        new UltraCamoDetector(),
        new UltraMoabMine(),
        new UltraBananaFarmer(),
        new UltraSuperMonkeyStorm(),
        new UltraEnergisingTotem()
    };

    public static GameModel gameModel;

    public override void OnApplicationStart() {
        MelonLogger.Msg("Ultra Powers loaded!");
        HarmonyInstance.Patch(Method(typeof(GameModelLoader), nameof(GameModelLoader.Load)), null, new(Method(GetType(), nameof(GameLoaded))));
        HarmonyInstance.Patch(Method(typeof(InGame), nameof(InGame.Update)), null, new(Method(GetType(), nameof(UpdateInGame))));
        HarmonyInstance.Patch(Method(typeof(TowerModel), nameof(TowerModel.IsTowerPlaceableInAreaType)), null, new(Method(GetType(), nameof(PlaceModification))));

        powerAdapters.ForEach(adapter => adapter.Setup(ref Assets.SpriteAssets, ref Assets.RendererAssets));
    }

    public static void GameLoaded(ref GameModel __result) {
        gameModel = __result;

        for (int i = 0; i < __result.powers.Length; i++) {
            var power = __result.powers[i];

            foreach (var adapter in powerAdapters)
                adapter.ModifyPower(ref power);

            __result.powers[i] = power;
        }
    }

    public static void UpdateInGame(ref InGame __instance) {
        if (__instance == null || __instance.UnityToSimulation == null || __instance.UnityToSimulation.ttss == null || __instance.UnityToSimulation.ttss.Count <= 0)
            return;

        PortableLakeDown = __instance.UnityToSimulation.ttss.ToArray().Any(a => a.tower.towerModel.name.Equals("PortableLake"));
        PontoonDown = __instance.UnityToSimulation.ttss.ToArray().Any(a => a.tower.towerModel.name.Equals("Pontoon"));
    }

    public static void PlaceModification(ref AreaType areaType, ref bool __result) {
        if (PortableLakeDown && areaType == AreaType.land)
            __result = true;
        if (PontoonDown && areaType == AreaType.water)
            __result = true;
    }
}