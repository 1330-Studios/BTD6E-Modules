using System.Dynamic;

using static MelonLoader.Modules.MelonModule;

namespace AdditionalTiers.Tasks.Towers.Progressive;
internal class ProgressiveKingCrimson : TowerTask {
    public static TowerModel baseTwrMdl;
    private static int time = -1;
    public ProgressiveKingCrimson() {
        identifier = "King Crimson";
        getTower = () => baseTwrMdl;
        baseTower = AddedTierName.KINGCRIMSON;
        tower = AddedTierEnum.KINGCRIMSON;
        requirements += tts => tts.tower.towerModel.baseId.Equals("Druid") && tts.tower.towerModel.tiers[2] == 5;
        onComplete += tts => {
            if (time < 50) {
                time++;
                return;
            }
            TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
            tts.tower.worth = 0;
            tts.tower.UpdateRootModel(getTower());
            tts.tower.UpdatedModel(getTower());
            tts.TAdd(scale1: .25f);

            AbilityMenu.instance.TowerChanged(tts);
            AbilityMenu.instance.RebuildAbilities();
        };
        gameLoad += gm => {
            baseTwrMdl = gm.towers.First(a => a.name.Equals(baseTower)).CloneCast();
            baseTwrMdl.name = $"{identifier} Base";
            baseTwrMdl.SetDisplay("AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T0.png");
            baseTwrMdl.SetIcons("AdditionalTiers.Resources.V2.KingCrimson.KingCrimsonIcon.png");

            baseTwrMdl.range += 5;
            baseTwrMdl.dontDisplayUpgrades = true;

            for (int i = 0; i < baseTwrMdl.behaviors.Length; i++) {
                if (baseTwrMdl.behaviors[i].Is<AttackModel>(out var am)) {
                    am.range += 5;
                    foreach (var weap in am.weapons) {
                        weap.projectile.pierce++;
                        weap.projectile.display = new() { guidRef = "AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_Thorn.png" };
                    }
                }
            }

            baseTwrMdl.behaviors = baseTwrMdl.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true));

            UpgradeMenuManager.towers["King Crimson Base"] = new {
                CurrentUpgrade = 0, Name = "King Crimson", TowerModel = baseTwrMdl,
                TowerType = "GROUND", UpgradeCost = 75_000, Portrait = "AdditionalTiers.Resources.V2.KingCrimson.KingCrimsonIcon.png",
                CurrentSPA = 0.55, CurrentDamage = 4,
                NextSPA = -0.05, NextDamage = 4, NextRange = 5,
                Extra = "", MaxUpgrade = false, NextUpgradeName = "King Crimson T1"
            };

            var t1 = baseTwrMdl.CloneCast();
            t1.name = $"{identifier} T1";
            t1.SetDisplay("AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T1.png");
            t1.range += 5;
            foreach (var beh in t1.behaviors) {
                if (beh.Is<AttackModel>(out var am)) {
                    am.range += 5;
                    foreach (var weap in am.weapons) {
                        weap.Rate -= 0.05f;
                        weap.projectile.pierce++;
                        foreach (var pbeh in weap.projectile.behaviors) {
                            if (pbeh.Is<DamageModel>(out var dm)) {
                                dm.damage += 4;
                            }
                        }
                    }
                }
            }

            UpgradeMenuManager.towers["King Crimson T1"] = new {
                CurrentUpgrade = 1, Name = "King Crimson", TowerModel = t1,
                TowerType = "GROUND", UpgradeCost = 150_000, Portrait = "AdditionalTiers.Resources.V2.KingCrimson.KingCrimsonIcon.png",
                CurrentSPA = 0.5, CurrentDamage = 8,
                NextSPA = -0.2, NextDamage = 56, NextRange = 5,
                Extra = "Septuple Shots", MaxUpgrade = false, NextUpgradeName = "King Crimson T2"
            };

            var t2 = t1.CloneCast();
            t2.name = $"{identifier} T2";
            t2.SetDisplay("AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T2.png");
            t2.range += 5;
            foreach (var beh in t2.behaviors) {
                if (beh.Is<AttackModel>(out var am)) {
                    am.range += 5;
                    foreach (var weap in am.weapons) {
                        weap.Rate -= 0.2f;
                        weap.projectile.pierce++;
                        weap.emission.Cast<RandomEmissionModel>().count = 7;
                        foreach (var pbeh in weap.projectile.behaviors) {
                            if (pbeh.Is<DamageModel>(out var dm)) {
                                dm.damage += 56;
                            }
                        }
                    }
                }
            }

            UpgradeMenuManager.towers["King Crimson T2"] = new {
                CurrentUpgrade = 2, Name = "King Crimson", TowerModel = t2,
                TowerType = "GROUND", UpgradeCost = 250_000, Portrait = "AdditionalTiers.Resources.V2.KingCrimson.KingCrimsonIcon.png",
                CurrentSPA = 0.3, CurrentDamage = 64,
                NextSPA = -0.05, NextDamage = 36, NextRange = 15,
                Extra = "", MaxUpgrade = false, NextUpgradeName = "King Crimson T3"
            };

            var t3 = t2.CloneCast();
            t3.name = $"{identifier} T3";
            t3.SetDisplay("AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T3.png");
            t3.range += 15;
            foreach (var beh in t3.behaviors) {
                if (beh.Is<AttackModel>(out var am)) {
                    am.range += 15;
                    foreach (var weap in am.weapons) {
                        weap.Rate -= 0.2f;
                        weap.projectile.pierce++;
                        weap.emission.Cast<RandomEmissionModel>().count = 7;
                        foreach (var pbeh in weap.projectile.behaviors) {
                            if (pbeh.Is<DamageModel>(out var dm)) {
                                dm.damage += 56;
                            }
                        }
                    }
                }
            }

            UpgradeMenuManager.towers["King Crimson T3"] = new {
                CurrentUpgrade = 3, Name = "King Crimson", TowerModel = t3,
                TowerType = "GROUND", UpgradeCost = 500_000, Portrait = "AdditionalTiers.Resources.V2.KingCrimson.KingCrimsonIcon.png",
                CurrentSPA = 0.25, CurrentDamage = 100,
                NextSPA = 0, NextDamage = 0, NextRange = 0,
                Extra = "Burning Hearts", MaxUpgrade = false, NextUpgradeName = "King Crimson T4"
            };

            var t4 = t3.CloneCast();
            t4.name = $"{identifier} T4";
            t4.SetDisplay("AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T4.png");
            t4.behaviors = t4.behaviors.Add(new LifeRegenModel("LRM_", 10, 0, 1, new() { guidRef = "eb70b6823aec0644c81f873e94cb26cc" }));
            foreach (var beh in t3.behaviors) {
                if (beh.Is<AttackModel>(out var am)) {
                    foreach (var weap in am.weapons) {
                        weap.Rate -= 0.05f;
                    }
                }
            }

            UpgradeMenuManager.towers["King Crimson T4"] = new {
                CurrentUpgrade = 4, Name = "King Crimson", TowerModel = t4,
                TowerType = "GROUND", UpgradeCost = 1_500_000, Portrait = "AdditionalTiers.Resources.V2.KingCrimson.KingCrimsonIcon.png",
                CurrentSPA = 0.25, CurrentDamage = 100,
                NextSPA = 0, NextDamage = 900, NextRange = 0,
                Extra = "", MaxUpgrade = false, NextUpgradeName = "King Crimson T5"
            };

            var t5 = t4.CloneCast();
            t5.name = $"{identifier} T5";
            t5.SetDisplay("AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T5.png");
            foreach (var beh in t5.behaviors) {
                if (beh.Is<AttackModel>(out var am)) {
                    foreach (var weap in am.weapons) {
                        weap.projectile.pierce++;
                        foreach (var pbeh in weap.projectile.behaviors) {
                            if (pbeh.Is<DamageModel>(out var dm)) {
                                dm.damage += 900;
                            }
                        }
                    }
                }
            }

            UpgradeMenuManager.towers["King Crimson T5"] = new {
                CurrentUpgrade = 5, Name = "King Crimson", TowerModel = t5,
                TowerType = "GROUND", UpgradeCost = 5_000_000, Portrait = "AdditionalTiers.Resources.V2.KingCrimson.KingCrimsonIcon.png",
                CurrentSPA = 0.25, CurrentDamage = 1_000,
                NextSPA = 0, NextDamage = 0, NextRange = 0,
                Extra = "Foresight", MaxUpgrade = false, NextUpgradeName = "King Crimson T6"
            };

            var t6 = t5.CloneCast();
            t6.name = $"{identifier} T6";
            t6.SetDisplay("AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T6.png");

            var abm = gm.towers.First(a => a.name.Equals("SuperMonkey-050")).CloneCast().behaviors.First(a => a.Is<AbilityModel>()).Cast<AbilityModel>();

            abm.name = "Foresight";
            abm.icon = new() { guidRef = "Ui[AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_TR.png]" };
            abm.cooldown /= 3;
            abm.cooldownFrames /= 3;
            abm.behaviors = new Model[] { new CreateEffectOnAbilityModel("ForesightEffect", new EffectModel("Foresight", new() { guidRef = "6d84b13b7622d2744b8e8369565bc058" }, 1, 1), false, true, false, false, false),
            new CreateSoundOnAbilityModel("CreateSoundOnAbilityModel_", new("Sound", new() {guidRef = "cf5b5e74df29fcf47a7d48b559114a99"}), null, null)};

            t6.behaviors = t6.behaviors.Add(abm);

            UpgradeMenuManager.towers["King Crimson T6"] = new {
                CurrentUpgrade = 6, Name = "King Crimson", TowerModel = t6,
                TowerType = "GROUND", UpgradeCost = 7_500_000, Portrait = "AdditionalTiers.Resources.V2.KingCrimson.KingCrimsonIcon.png",
                CurrentSPA = 0.25, CurrentDamage = 1_000,
                NextSPA = -0.05, NextDamage = 1_500, NextRange = 10,
                Extra = "", MaxUpgrade = false, NextUpgradeName = "King Crimson T7"
            };

            var t7 = t6.CloneCast();
            t7.name = $"{identifier} T7";
            t7.SetDisplay("AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T7.png");
            t7.range += 10;
            foreach (var beh in t7.behaviors) {
                if (beh.Is<AttackModel>(out var am)) {
                    am.range += 10;
                    foreach (var weap in am.weapons) {
                        weap.projectile.pierce++;
                        foreach (var pbeh in weap.projectile.behaviors) {
                            if (pbeh.Is<DamageModel>(out var dm)) {
                                dm.damage += 1500;
                            }
                        }
                    }
                }
            }

            UpgradeMenuManager.towers["King Crimson T7"] = new {
                CurrentUpgrade = 7, Name = "King Crimson", TowerModel = t7,
                TowerType = "GROUND", UpgradeCost = 9_000_000, Portrait = "AdditionalTiers.Resources.V2.KingCrimson.KingCrimsonIcon.png",
                CurrentSPA = 0.2, CurrentDamage = 2_500,
                NextSPA = -0.1, NextDamage = 7_500, NextRange = 15,
                Extra = "Explosive Thorns", MaxUpgrade = false, NextUpgradeName = "King Crimson T8"
            };

            var t8 = t7.CloneCast();
            t8.name = $"{identifier} T8";
            t8.SetDisplay("AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T8.png");
            t8.range += 15;
            foreach (var beh in t8.behaviors) {
                if (beh.Is<AttackModel>(out var am)) {
                    am.range += 15;
                    foreach (var weap in am.weapons) {
                        weap.Rate = 0.1f;
                        weap.projectile.ignorePierceExhaustion = true;
                        weap.projectile.behaviors = weap.projectile.behaviors.Add(new CreateEffectOnContactModel("CreateEffectOnContactModel_",
                                new EffectModel("Explosion", new() { guidRef = "6d84b13b7622d2744b8e8369565bc058" }, 1, 1)));

                        foreach (var pbeh in weap.projectile.behaviors) {
                            if (pbeh.Is<DamageModel>(out var dm)) {
                                dm.damage += 7500;
                            }
                        }
                    }
                }
            }

            UpgradeMenuManager.towers["King Crimson T8"] = new {
                CurrentUpgrade = 8, Name = "King Crimson", TowerModel = t8,
                TowerType = "GROUND", UpgradeCost = 0, Portrait = "AdditionalTiers.Resources.V2.KingCrimson.KingCrimsonIcon.png",
                CurrentSPA = 0.1, CurrentDamage = 10_000,
                NextSPA = 0, NextDamage = 0, NextRange = 0,
                Extra = "", MaxUpgrade = true, NextUpgradeName = "King Crimson T8"
            };
        };
        recurring += _ => { };
        onLeave += () => time = -1;
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T0.png", "acf836014419c134787007ed2a6304b5", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T1.png", "acf836014419c134787007ed2a6304b5", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T2.png", "acf836014419c134787007ed2a6304b5", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T3.png", "acf836014419c134787007ed2a6304b5", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T4.png", "acf836014419c134787007ed2a6304b5", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T5.png", "acf836014419c134787007ed2a6304b5", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T6.png", "acf836014419c134787007ed2a6304b5", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T7.png", "acf836014419c134787007ed2a6304b5", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T8.png", "acf836014419c134787007ed2a6304b5", RendererType.SKINNEDMESHRENDERER));

        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_Thorn.png", "5a7af3162d20c8745a946b6c24681248", RendererType.SPRITERENDERER));
    }

    [HarmonyPatch(typeof(Ability), nameof(Ability.Activate))]
    public sealed class AA {
        [HarmonyPrefix]
        internal static unsafe bool Prefix(ref Ability __instance) {
            if (__instance?.abilityModel?.name?.EndsWith("Foresight") ?? true) {
                foreach (var bloon in InGame.instance.bridge.GetAllBloons()) {
                    if (bloon != null) {
                        bloon.GetSimBloon().Move(-bloon.Def.Speed * 15);
                    }
                }
            }

            return true;
        }
    }
}