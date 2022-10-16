namespace AdditionalTiers.Tasks.Towers.Progressive;
internal class ProgressiveBAY : TowerTask {
    public static TowerModel baseTwrMdl;
    private static int time = -1;
    public ProgressiveBAY() {
        identifier = "Black And Yellow";
        getTower = () => baseTwrMdl;
        baseTower = AddedTierName.BLACKANDYELLOW;
        tower = AddedTierEnum.BLACKANDYELLOW;
        requirements += tts => tts.tower.towerModel.baseId.Equals("TackShooter") && tts.tower.towerModel.tiers[2] == 5;
        onComplete += tts => {
            if (time < 50) {
                time++;
                return;
            }
            TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
            tts.tower.worth = 0;
            tts.tower.UpdateRootModel(getTower());
            tts.tower.UpdatedModel(getTower());
            tts.TAdd(scale1: 0);

            AbilityMenu.instance.TowerChanged(tts);
            AbilityMenu.instance.RebuildAbilities();
        };
        gameLoad += gm => {
            baseTwrMdl = gm.towers.First(a => a.name.Equals(baseTower)).CloneCast();
            baseTwrMdl.name = $"{identifier} Base";
            baseTwrMdl.SetDisplay("AdditionalTiers.Resources.V2.B_Y");
            baseTwrMdl.SetIcons("AdditionalTiers.Resources.B_Y.bypor.png");

            baseTwrMdl.range += 5;
            baseTwrMdl.dontDisplayUpgrades = true;

            for (int i = 0; i < baseTwrMdl.behaviors.Length; i++) {
                if (baseTwrMdl.behaviors[i].Is<AttackModel>(out var am)) {
                    am.range += 5;
                    foreach (var weap in am.weapons) {
                        weap.Rate = 0.1f;
                        foreach (var pb in weap.projectile.behaviors) {
                            if (pb.Is<DamageModel>(out var dm)) {
                                dm.damage = 16;
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            }
                        }
                    }
                }
            }

            baseTwrMdl.behaviors = baseTwrMdl.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true));

            UpgradeMenuManager.towers["Black And Yellow Base"] = new {
                CurrentUpgrade = 0, Name = "Black And Yellow", TowerModel = baseTwrMdl,
                TowerType = "GROUND", UpgradeCost = 750_000, Portrait = "AdditionalTiers.Resources.B_Y.bypor.png",
                CurrentSPA = 0.1, CurrentDamage = 16,
                NextSPA = -0.05, NextDamage = 16, NextRange = 5,
                Extra = "", MaxUpgrade = false, NextUpgradeName = "Black And Yellow T1"
            };

            var t1 = baseTwrMdl.CloneCast();
            t1.name = $"{identifier} T1";
            t1.range += 5;
            foreach (var beh in t1.behaviors) {
                if (beh.Is<AttackModel>(out var am)) {
                    am.range += 5;
                    foreach (var weap in am.weapons) {
                        weap.Rate = 0.05f;
                        foreach (var pb in weap.projectile.behaviors) {
                            if (pb.Is<DamageModel>(out var dm)) {
                                dm.damage = 32;
                            }
                        }
                    }
                }
            }

            UpgradeMenuManager.towers["Black And Yellow T1"] = new {
                CurrentUpgrade = 1, Name = "Black And Yellow", TowerModel = t1,
                TowerType = "GROUND", UpgradeCost = 5_000_000, Portrait = "AdditionalTiers.Resources.B_Y.bypor.png",
                CurrentSPA = 0.05, CurrentDamage = 32,
                NextSPA = -0.04, NextDamage = 96, NextRange = 0,
                Extra = "", MaxUpgrade = false, NextUpgradeName = "Black And Yellow T2"
            };

            var t2 = t1.CloneCast();
            t2.name = $"{identifier} T2";
            foreach (var beh in t2.behaviors) {
                if (beh.Is<AttackModel>(out var am)) {
                    foreach (var weap in am.weapons) {
                        weap.Rate = 0.01f;
                        foreach (var pb in weap.projectile.behaviors) {
                            if (pb.Is<DamageModel>(out var dm)) {
                                dm.damage = 128;
                            }
                        }
                    }
                }
            }

            UpgradeMenuManager.towers["Black And Yellow T2"] = new {
                CurrentUpgrade = 2, Name = "Black And Yellow", TowerModel = t2,
                TowerType = "GROUND", UpgradeCost = 7_500_000, Portrait = "AdditionalTiers.Resources.B_Y.bypor.png",
                CurrentSPA = 0.01, CurrentDamage = 128,
                NextSPA = 0, NextDamage = 0, NextRange = 0,
                Extra = "", MaxUpgrade = true, NextUpgradeName = ""
            };
        };
        recurring += _ => { };
        onLeave += () => time = -1;
    }

    [HarmonyPatch(typeof(Weapon), nameof(Weapon.SpawnDart))]
    public static class WI {
        [HarmonyPostfix]
        private static void Postfix(Weapon __instance) {
            if (__instance == null) return;
            if (__instance.weaponModel == null) return;
            if (__instance.weaponModel.name == null) return;
            if (__instance.attack == null) return;
            if (__instance.attack.tower == null) return;
            if (__instance.attack.tower.Node == null) return;
            if (__instance.attack.tower.Node.graphic == null) return;

            try {
                if (__instance.attack.tower.towerModel.name.Equals("Black And Yellow Base") || __instance.attack.tower.towerModel.name.Equals("Black And Yellow T1") ||
                    __instance.attack.tower.towerModel.name.Equals("Black And Yellow T2")) {
                    __instance.attack.tower.Node.graphic.GetComponent<Animator>().StopPlayback();
                    __instance.attack.tower.Node.graphic.GetComponent<Animator>().Play("TackZoneAttack");
                }
            } catch { }
        }
    }
}