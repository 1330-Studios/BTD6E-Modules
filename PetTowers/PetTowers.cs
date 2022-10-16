using PetTowers.Towers;

[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
[assembly: MelonInfo(typeof(PetTowers.PetTowers), "Pet Towers", "1.7", "1330 Studios LLC")]

namespace PetTowers {
    public class PetTowers : MelonMod {
        public static Dictionary<string, TowerContainer> AddedTowers = new();

        public override void OnApplicationStart() {
            MelonLogger.Msg("Pet Towers loaded!");
        }

        [HarmonyPatch(typeof(GameModelLoader), nameof(GameModelLoader.Load))]
        public static class GameModelLoader_Load {
            public static List<ITower> TowersToInit = new() {
                new Kiwi(),
                new Mo(),
                new Heather()
                /*new GhostWolf(),
                new Luna()*/
            };

            [HarmonyPostfix]
            public static void Postfix(ref GameModel __result) {

                foreach (var tower in TowersToInit) {
                    tower.Initialize(ref __result);
                    var tc = tower.GetTower(__result);

                    if (tc.towers.Count > 0)
                        __result.towers = __result.towers.Add(tc.towers);
                    if (tc.upgrades.Count > 0) {
                        __result.upgrades = __result.upgrades.Add(tc.upgrades);
                        if (__result.upgradesByName == null)
                            __result.upgradesByName = new();
                        for (int i = 0; i < tc.upgrades.Count; i++) {
                            __result.upgradesByName.Add(tc.upgrades[i].name, tc.upgrades[i]);
                        }
                    }
                    if (tc.shop != null)
                        __result.towerSet = __result.towerSet.Add(tc.shop);

                    Logger13.Log($"Built {tc.towers[0].baseId}");

                    AddedTowers[tc.towers[0].baseId] = tc;
                }
            }
        }

        [HarmonyPatch(typeof(ProfileModel), "Validate")]
        public sealed class ProfileModel_Patch {
            [HarmonyPostfix]
            public static void Postfix(ref ProfileModel __instance) {
                var unlockedTowers = __instance.unlockedTowers;
                var unlockedUpgrades = __instance.acquiredUpgrades;

                foreach (var tower in AddedTowers.Values) {
                    if (!unlockedTowers.Contains(tower.towers[0].baseId))
                        unlockedTowers.Add(tower.towers[0].baseId);

                    foreach (var upgrade in tower.upgrades) {
                        if (!unlockedUpgrades.Contains(upgrade.name))
                            unlockedUpgrades.Add(upgrade.name);
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Btd6Player), "CheckForNewParagonPipEvent")]
        public sealed class Btd6PlayerIsBad {
            [HarmonyPrefix]
            public static bool Prefix(ref bool __result) => __result = false;
        }

        [HarmonyPatch(typeof(MonkeyTeamsIcon), nameof(MonkeyTeamsIcon.Init))]
        public sealed class MTIcon {
            [HarmonyPrefix]
            public static bool Prefix(ref MonkeyTeamsIcon __instance) {
                __instance.enabled = false;
                __instance?.gameObject?.SetActive(false);
                return false;
            }
        }

        [HarmonyPatch(typeof(UpgradeScreen), "UpdateUi")]
        public sealed class AddShopDetails {
            [HarmonyPrefix]
            public static bool Prefix(ref UpgradeScreen __instance, ref string towerId) {
                foreach (var tower in AddedTowers.Values)
                    if (towerId.Contains(tower.towers[0].baseId))
                        towerId = "DartMonkey";

                return true;
            }
        }
    }
}
