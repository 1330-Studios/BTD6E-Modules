namespace AdditionalTiers.Tasks.Towers.Progressive;
internal class ProgressiveMystery : TowerTask {
    public static TowerModel baseTwrMdl;
    private static int time = -1;
    public ProgressiveMystery() {
        identifier = "???";
        getTower = () => baseTwrMdl;
        baseTower = AddedTierName.MYSTERY;
        tower = AddedTierEnum.MYSTERY;
        requirements += tts => tts.tower.towerModel.baseId.Equals("NinjaMonkey") && tts.tower.towerModel.tiers[0] == 5;
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
            baseTwrMdl.SetDisplay("AdditionalTiers.Resources.V2.Ninja.Richard");
            baseTwrMdl.SetIcons("AdditionalTiers.Resources.Ninja.RichardIcon.png");

            baseTwrMdl.range += 5;
            baseTwrMdl.dontDisplayUpgrades = true;

            for (int i = 0; i < baseTwrMdl.behaviors.Length; i++) {
                if (baseTwrMdl.behaviors[i].Is<AttackModel>(out var am)) {
                    am.range += 5;
                    foreach (var weap in am.weapons) {
                        weap.Rate = 0.55f;
                        foreach (var pb in weap.projectile.behaviors) {
                            if (pb.Is<DamageModel>(out var dm)) {
                                dm.damage = 4;
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            }
                        }
                    }
                }
            }

            baseTwrMdl.behaviors = baseTwrMdl.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true));

            UpgradeMenuManager.towers["??? Base"] = new {
                CurrentUpgrade = 0, Name = "Richard", TowerModel = baseTwrMdl,
                TowerType = "GROUND", UpgradeCost = 750_000, Portrait = "AdditionalTiers.Resources.Ninja.RichardIcon.png",
                CurrentSPA = 0.55, CurrentDamage = 4,
                NextSPA = -0.05, NextDamage = 4, NextRange = 5,
                Extra = "Tyler \"Ninja\" Blevins", MaxUpgrade = false, NextUpgradeName = "??? T1"
            };

            var t1 = baseTwrMdl.CloneCast();
            t1.name = $"{identifier} T1";
            t1.range += 5;
            foreach (var beh in t1.behaviors) {
                if (beh.Is<AttackModel>(out var am)) {
                    am.range += 5;
                    foreach (var weap in am.weapons) {
                        weap.Rate = 0.5f;
                        foreach (var pb in weap.projectile.behaviors) {
                            if (pb.Is<DamageModel>(out var dm)) {
                                dm.damage = 8;
                            }
                        }
                    }
                }
            }

            UpgradeMenuManager.towers["??? T1"] = new {
                CurrentUpgrade = 1, Name = "Tyler", TowerModel = t1,
                TowerType = "GROUND", UpgradeCost = 1_500_000, Portrait = "AdditionalTiers.Resources.Ninja.RichardIcon.png",
                CurrentSPA = 0.5, CurrentDamage = 8,
                NextSPA = 0.0, NextDamage = 56, NextRange = 5,
                Extra = "Fortnite Win", MaxUpgrade = false, NextUpgradeName = "??? T2"
            };

            var t2 = t1.CloneCast();
            t2.name = $"{identifier} T2";
            t2.range += 5;
            foreach (var beh in t2.behaviors) {
                if (beh.Is<AttackModel>(out var am)) {
                    am.range += 5;
                    foreach (var weap in am.weapons) {
                        weap.emission = new ArcEmissionModel("AEM_", 20, 0, 360, null, false);
                        foreach (var pb in weap.projectile.behaviors) {
                            if (pb.Is<DamageModel>(out var dm)) {
                                dm.damage = 64;
                            }
                        }
                    }
                }
            }

            UpgradeMenuManager.towers["??? T2"] = new {
                CurrentUpgrade = 2, Name = "Fortnite", TowerModel = t2,
                TowerType = "GROUND", UpgradeCost = 2_500_000, Portrait = "AdditionalTiers.Resources.Ninja.RichardIcon.png",
                CurrentSPA = 0.5, CurrentDamage = 64,
                NextSPA = -0.25, NextDamage = 136, NextRange = 999,
                Extra = "Fortnite Loss", MaxUpgrade = false, NextUpgradeName = "??? T3"
            };

            var t3 = t2.CloneCast();
            t3.name = $"{identifier} T3";
            t3.SetDisplay("AdditionalTiers.Resources.V2.Ninja.EvilRichard");
            t3.range += 999;
            foreach (var beh in t3.behaviors) {
                if (beh.Is<AttackModel>(out var am)) {
                    am.range += 999;
                    foreach (var weap in am.weapons) {
                        weap.Rate = .25f;
                        foreach (var pb in weap.projectile.behaviors) {
                            if (pb.Is<DamageModel>(out var dm)) {
                                dm.damage = 200;
                            }
                        }
                    }
                }
            }

            UpgradeMenuManager.towers["??? T3"] = new {
                CurrentUpgrade = 3, Name = "Ninja", TowerModel = t3,
                TowerType = "GROUND", UpgradeCost = 0, Portrait = "AdditionalTiers.Resources.Ninja.EVILRICHARD.png",
                CurrentSPA = 0.25, CurrentDamage = 200,
                NextSPA = 0, NextDamage = 0, NextRange = 0,
                Extra = "", MaxUpgrade = true, NextUpgradeName = "??? Base"
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
                if (__instance.attack.tower.towerModel.name.Equals("??? Base") || __instance.attack.tower.towerModel.name.Equals("??? T1")) {
                    __instance.attack.tower.Node.graphic.GetComponent<Animator>().StopPlayback();
                    __instance.attack.tower.Node.graphic.GetComponent<Animator>().speed = 10;
                    __instance.attack.tower.Node.graphic.GetComponent<Animator>().Play("THROW");
                }
                if (__instance.attack.tower.towerModel.name.Equals("??? T2") || __instance.attack.tower.towerModel.name.Equals("??? T3")) {
                    __instance.attack.tower.Node.graphic.GetComponent<Animator>().StopPlayback();
                    __instance.attack.tower.Node.graphic.GetComponent<Animator>().speed = 10;
                    __instance.attack.tower.Node.graphic.GetComponent<Animator>().Play("TSZ");
                }
            } catch { }
        }
    }
}
