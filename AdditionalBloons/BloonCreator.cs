
using AdditionalBloons.Tasks;
using AdditionalBloons.Utils;

using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Unity.UI_New.InGame.BloonMenu;

namespace AdditionalBloons {
    public static class BloonCreator {
        public static List<BloonModel> bloons = new();
        public static List<AssetInfo> assets = new();
        internal static BloonModel bloonBase = null!;

        internal static void GameLoad(ref GameModel __result) {
            bloonBase = __result.bloons[0].Clone().Cast<BloonModel>();

            #region Bloon Defs

            #region Base Game

            ModelStorage.whiteBloon = __result.bloons.First(a => a.baseId.Equals("White")).Clone().Cast<BloonModel>();

            #endregion

            #region Golden

            bloons.Add(__result.bloons.First(a => a.baseId.Equals("golden", StringComparison.OrdinalIgnoreCase)));

            #endregion

            #region Bloonarius

            bloons.Add(__result.bloons.First(a => a.id.Equals("bloonarius1", StringComparison.OrdinalIgnoreCase)));
            bloons.Add(__result.bloons.First(a => a.id.Equals("bloonarius5", StringComparison.OrdinalIgnoreCase)));

            #endregion

            #region Bloonarius Elite

            bloons.Add(__result.bloons.First(a => a.id.Equals("bloonariuselite1", StringComparison.OrdinalIgnoreCase)));
            bloons.Add(__result.bloons.First(a => a.id.Equals("bloonariuselite5", StringComparison.OrdinalIgnoreCase)));

            #endregion

            #region Lych

            bloons.Add(__result.bloons.First(a => a.id.Equals("lych1", StringComparison.OrdinalIgnoreCase)));
            bloons.Add(__result.bloons.First(a => a.id.Equals("lych5", StringComparison.OrdinalIgnoreCase)));

            #endregion

            #region Lych Elite

            bloons.Add(__result.bloons.First(a => a.id.Equals("lychelite1", StringComparison.OrdinalIgnoreCase)));
            bloons.Add(__result.bloons.First(a => a.id.Equals("lychelite5", StringComparison.OrdinalIgnoreCase)));

            #endregion

            #region Vortex

            bloons.Add(__result.bloons.First(a => a.id.Equals("vortex1", StringComparison.OrdinalIgnoreCase)));
            bloons.Add(__result.bloons.First(a => a.id.Equals("vortex5", StringComparison.OrdinalIgnoreCase)));

            #endregion

            #region Vortex Elite

            bloons.Add(__result.bloons.First(a => a.id.Equals("vortexelite1", StringComparison.OrdinalIgnoreCase)));
            bloons.Add(__result.bloons.First(a => a.id.Equals("vortexelite5", StringComparison.OrdinalIgnoreCase)));

            #endregion

            #region Cop BAD

            var copBAD = __result.bloons.First(a => a.baseId.Equals("Bad")).CloneCast();
            copBAD.display = new() { guidRef = "DOOM" };
            copBAD.icon = new() { guidRef = "Ui[AdditionalBloons.Resources.CopBAD.CopBADIcon.png]" };
            copBAD.Speed /= 3;
            copBAD.maxHealth = copBAD.leakDamage = 500_500_500;
            copBAD.tags = new Il2CppStringArray(new string[] { "DOOM", "NA" });
            copBAD.damageDisplayStates = Array.Empty<DamageStateModel>();
            for (int i = 0; i < copBAD.behaviors.Length; i++) {
                var behavior = copBAD.behaviors[i];

                if (behavior.GetIl2CppType() == Il2CppType.Of<DamageStateModel>()) {
                    behavior.Cast<DamageStateModel>().displayPath = new() { guidRef = "DOOM" };
                }

                copBAD.behaviors[i] = behavior;
            }

            bloons.Add(copBAD);
            
            #endregion

            #endregion
        }

        internal static bool BloonMenuCreate(ref Il2CppSystem.Collections.Generic.List<BloonModel> sortedBloons) {
            for (int i = 0; i < bloons.Count; i++) sortedBloons.Add(bloons[i]);
            return true;
        }

        internal static bool SpawnBloon(ref SpawnBloonButton __instance) {
            for (int i = 0; i < bloons.Count; i++)
                if (__instance.model.display.Equals(bloons[i].display)) {
                    int amount = int.Parse(__instance.count.text);
                    int delay = int.Parse(__instance.rate.text);

                    BloonTaskRunner.bloonQueue.Enqueue(new(bloons[i], amount, delay));

                    return false;
                }

            return true;
        }
    }
}

/*
#region Coconut

            for (int i = 1; i < 6; i++) assets.Add(new($"Coconut{i}", bloonBase.display, RendererType.SPRITERENDERER));

            var coconutBloon = __result.bloons.First(a => a.baseId.Equals("Ceramic")).Clone().Cast<BloonModel>();
            coconutBloon.display = "Coconut1";
            coconutBloon.icon = new("CoconutIcon");
            coconutBloon.radius = 8;
            coconutBloon.danger = 10;
            coconutBloon.distributeDamageToChildren = false;
            coconutBloon.speedFrames = 1.04166675f;
            coconutBloon.Speed = 62.5f;
            coconutBloon.leakDamage = coconutBloon.maxHealth = 20;
            coconutBloon.tags = new Il2CppStringArray(new string[] { "Coconut", "NA" });
            coconutBloon.damageDisplayStates = new(new DamageStateModel[] {
                new DamageStateModel("DamageStateModel_4", "Coconut5", 0.2f),
                new DamageStateModel("DamageStateModel_3", "Coconut4", 0.4f),
                new DamageStateModel("DamageStateModel_2", "Coconut3", 0.6f),
                new DamageStateModel("DamageStateModel_1", "Coconut2", 0.8f)
            });
            var coconutBloonChildren = new Il2CppSystem.Collections.Generic.List<BloonModel>();
            for (int i = 0; i < 5; i++) coconutBloonChildren.Add(ModelStorage.whiteBloon);
            coconutBloon.childBloonModels = coconutBloonChildren;
            coconutBloon.UpdateChildBloonModels();
            for (int i = 0; i < coconutBloon.behaviors.Length; i++) {
                var behavior = coconutBloon.behaviors[i];

                if (behavior.GetIl2CppType() == Il2CppType.Of<SpawnChildrenModel>())
                    behavior.Cast<SpawnChildrenModel>().children =
                        new string[] { "White", "White", "White", "White", "White" };
                if (behavior.GetIl2CppType() == Il2CppType.Of<DamageStateModel>()) {
                    switch (behavior.Cast<DamageStateModel>().healthPercent) {
                        case 0.2f:
                            behavior.Cast<DamageStateModel>().displayPath = "Coconut5";
                            break;
                        case 0.4f:
                            behavior.Cast<DamageStateModel>().displayPath = "Coconut4";
                            break;
                        case 0.6f:
                            behavior.Cast<DamageStateModel>().displayPath = "Coconut3";
                            break;
                        case 0.8f:
                            behavior.Cast<DamageStateModel>().displayPath = "Coconut2";
                            break;
                    }
                }

                coconutBloon.behaviors[i] = behavior;
            }

            bloons.Add(coconutBloon);

            #endregion

            #region Flame BAD

            assets.Add(new AssetInfo("FireBAD0", "8fd8a703a31154a49b25ba34235ab76c", RendererType.SKINNEDMESHRENDERER));
            assets.Add(new AssetInfo("FireBAD1", "e0db2c0609eefee4eb357037ff9bb4c8", RendererType.SKINNEDMESHRENDERER));
            assets.Add(new AssetInfo("FireBAD2", "4f156a75b1c709840b131495c537fec0", RendererType.SKINNEDMESHRENDERER));
            assets.Add(new AssetInfo("FireBAD3", "8f6b8e2c3be74c347a6825672589e463", RendererType.SKINNEDMESHRENDERER));
            assets.Add(new AssetInfo("FireBAD4", "5b2554745311aab4c8fe7d71708353ad", RendererType.SKINNEDMESHRENDERER));

            var flameBAD = __result.bloons.First(a => a.baseId.Equals("Bad")).CloneCast();
            flameBAD.display = "FireBAD0";
            flameBAD.icon = new("FireBADIcon");
            flameBAD.radius *= 1.5f;
            flameBAD.maxHealth = 30000000f;
            flameBAD.tags = new Il2CppStringArray(new string[] { "FireBAD", "NA" });
            flameBAD.damageDisplayStates = new(new DamageStateModel[] {
                new DamageStateModel("DamageStateModel_4", "FireBAD4", 0.2f),
                new DamageStateModel("DamageStateModel_3", "FireBAD3", 0.4f),
                new DamageStateModel("DamageStateModel_2", "FireBAD2", 0.6f),
                new DamageStateModel("DamageStateModel_1", "FireBAD1", 0.8f)
            });
            for (int i = 0; i < flameBAD.behaviors.Length; i++) {
                var behavior = flameBAD.behaviors[i];

                if (behavior.GetIl2CppType() == Il2CppType.Of<DamageStateModel>()) {
                    switch (behavior.Cast<DamageStateModel>().healthPercent) {
                        case 0.2f:
                            behavior.Cast<DamageStateModel>().displayPath = "FireBAD4";
                            break;
                        case 0.4f:
                            behavior.Cast<DamageStateModel>().displayPath = "FireBAD3";
                            break;
                        case 0.6f:
                            behavior.Cast<DamageStateModel>().displayPath = "FireBAD2";
                            break;
                        case 0.8f:
                            behavior.Cast<DamageStateModel>().displayPath = "FireBAD1";
                            break;
                    }
                }

                flameBAD.behaviors[i] = behavior;
            }

            const int radius = 16;
            const float scale = 2;
            List<(float x, float y)> list = new List<(float x, float y)>();

            for (int x = -radius; x <= radius; x++) {
                for (int y = -radius; y <= radius; y++) {
                    if (x * x + y * y <= radius * radius) {
                        list.Add((x * scale, y * scale));
                    }
                }
            }

            int iteration = 0;
            foreach (var (x, y) in list) {
                iteration++;
                flameBAD.behaviors = flameBAD.behaviors.Add(new DisplayModel($"FlameBAD_AuraRingX{iteration}", "Assets/Monkeys/TackShooter/Graphics/Effects/RingOfFireBurst.prefab", 1, new(x, y, 0), 1, false, iteration % 10 * 10f / 60f));
            }

            bloons.Add(flameBAD);

            #endregion

            #region Dontavious Farcleton

            assets.Add(new AssetInfo("Dontavious0", "eff76d2b677b4b6499ee03b459d9b3fa", RendererType.SKINNEDMESHRENDERER));
            assets.Add(new AssetInfo("Dontavious1", "9781c9445be958d46a9b01d2e9e4a9d6", RendererType.SKINNEDMESHRENDERER));
            assets.Add(new AssetInfo("Dontavious2", "2bcc48db04077b444a092e8d64329e17", RendererType.SKINNEDMESHRENDERER));
            assets.Add(new AssetInfo("Dontavious3", "f0d9394b902facf4eae171d04c0eff90", RendererType.SKINNEDMESHRENDERER));
            assets.Add(new AssetInfo("Dontavious4", "811130f80d158d441809221a89c88b51", RendererType.SKINNEDMESHRENDERER));

            var dontavious = __result.bloons.First(a => a.baseId.Equals("Moab")).CloneCast();
            dontavious.display = "Dontavious0";
            dontavious.icon = new("DontaviousIcon");
            dontavious.radius *= 1.5f;
            dontavious.maxHealth = 200000000;
            dontavious.tags = new Il2CppStringArray(new string[] { "Dontavious", "Farcleton", "NA" });
            dontavious.damageDisplayStates = new(new DamageStateModel[] {
                new DamageStateModel("DamageStateModel_4", "Dontavious4", 0.2f),
                new DamageStateModel("DamageStateModel_3", "Dontavious3", 0.4f),
                new DamageStateModel("DamageStateModel_2", "Dontavious2", 0.6f),
                new DamageStateModel("DamageStateModel_1", "Dontavious1", 0.8f)
            });
            for (int i = 0; i < dontavious.behaviors.Length; i++) {
                var behavior = dontavious.behaviors[i];

                if (behavior.GetIl2CppType() == Il2CppType.Of<DamageStateModel>()) {
                    switch (behavior.Cast<DamageStateModel>().healthPercent) {
                        case 0.2f:
                            behavior.Cast<DamageStateModel>().displayPath = "Dontavious4";
                            break;
                        case 0.4f:
                            behavior.Cast<DamageStateModel>().displayPath = "Dontavious3";
                            break;
                        case 0.6f:
                            behavior.Cast<DamageStateModel>().displayPath = "Dontavious2";
                            break;
                        case 0.8f:
                            behavior.Cast<DamageStateModel>().displayPath = "Dontavious1";
                            break;
                    }
                }

                dontavious.behaviors[i] = behavior;
            }

            bloons.Add(dontavious);

            #endregion

            #region Cop BAD


            assets.Add(new AssetInfo("CopBAD0", "8fd8a703a31154a49b25ba34235ab76c", RendererType.SKINNEDMESHRENDERER));
            assets.Add(new AssetInfo("CopBAD1", "e0db2c0609eefee4eb357037ff9bb4c8", RendererType.SKINNEDMESHRENDERER));
            assets.Add(new AssetInfo("CopBAD2", "4f156a75b1c709840b131495c537fec0", RendererType.SKINNEDMESHRENDERER));
            assets.Add(new AssetInfo("CopBAD3", "8f6b8e2c3be74c347a6825672589e463", RendererType.SKINNEDMESHRENDERER));
            assets.Add(new AssetInfo("CopBAD4", "5b2554745311aab4c8fe7d71708353ad", RendererType.SKINNEDMESHRENDERER));
            assets.Add(new AssetInfo("JailBars", bloonBase.display, RendererType.SPRITERENDERER));

            var copBAD = __result.bloons.First(a => a.baseId.Equals("Bad")).CloneCast();
            copBAD.display = "CopBAD0";
            copBAD.icon = new("CopBADIcon");
            copBAD.Speed = 3.0f;
            copBAD.maxHealth = copBAD.leakDamage = 33500000;
            copBAD.tags = new Il2CppStringArray(new string[] { "CopBAD", "NA" });
            copBAD.damageDisplayStates = new(new DamageStateModel[] {
                new DamageStateModel("DamageStateModel_4", "CopBAD4", 0.2f),
                new DamageStateModel("DamageStateModel_3", "CopBAD3", 0.4f),
                new DamageStateModel("DamageStateModel_2", "CopBAD2", 0.6f),
                new DamageStateModel("DamageStateModel_1", "CopBAD1", 0.8f)
            });
            for (int i = 0; i < copBAD.behaviors.Length; i++) {
                var behavior = copBAD.behaviors[i];

                if (behavior.GetIl2CppType() == Il2CppType.Of<DamageStateModel>()) {
                    switch (behavior.Cast<DamageStateModel>().healthPercent) {
                        case 0.2f:
                            behavior.Cast<DamageStateModel>().displayPath = "CopBAD4";
                            break;
                        case 0.4f:
                            behavior.Cast<DamageStateModel>().displayPath = "CopBAD3";
                            break;
                        case 0.6f:
                            behavior.Cast<DamageStateModel>().displayPath = "CopBAD2";
                            break;
                        case 0.8f:
                            behavior.Cast<DamageStateModel>().displayPath = "CopBAD1";
                            break;
                    }
                }

                copBAD.behaviors[i] = behavior;
            }

            bloons.Add(copBAD);
            

            #endregion
*/