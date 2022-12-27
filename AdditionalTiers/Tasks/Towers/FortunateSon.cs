using System.Collections.Immutable;

using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;

namespace AdditionalTiers.Tasks.Towers;
internal class FortunateSon : TowerTask {
    public static TowerModel baseTwrMdl;
    private static int time = -1;
    private static float prog = 0;
    public FortunateSon() {
        identifier = "Fortunate Son";
        getTower = () => baseTwrMdl;
        baseTower = AddedTierName.FORTUNATESON;
        tower = AddedTierEnum.FORTUNATESON;
        requirements += tts => tts.tower.towerModel.baseId.Equals("HeliPilot") && tts.tower.towerModel.tiers[2] == 5;
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
            baseTwrMdl.SetIcons("AdditionalTiers.Resources.V2.FortunateSon.Display.png");

            baseTwrMdl.dontDisplayUpgrades = true;

            for (int i = 0; i < baseTwrMdl.behaviors.Length; i++) {
                if (baseTwrMdl.behaviors[i].Is<AttackAirUnitModel>(out var aaum)) {
                    aaum.range += 5;
                    foreach (var weap in aaum.weapons) {
                        weap.Rate /= 2;
                    }
                }
                if (baseTwrMdl.behaviors[i].Is<AttackModel>(out var am)) {
                    am.range += 30;
                    foreach (var weap in am.weapons) {
                        weap.Rate = 0.075f;
                        weap.projectile.display = new() { guidRef = "904432f1c20b4b145998e26a6e56cbb1" };
                        foreach (var pb in weap.projectile.behaviors) {
                            if (pb.Is<DamageModel>(out var dm)) {
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            }
                        }
                    }
                }
                if (baseTwrMdl.behaviors[i].Is<AirUnitModel>(out var aum)) {
                    aum.display = new() { guidRef = "AdditionalTiers.Resources.V2.FortunateSon.Display.png" };
                }
                if (baseTwrMdl.behaviors[i].Is<ComancheDefenceModel>(out var cdm)) {
                    cdm.reinforcementCount = 5;
                    foreach (var beh in cdm.towerModel.behaviors) {
                        if (beh.Is<AirUnitModel>(out var au)) {
                            au.display = new() { guidRef = "AdditionalTiers.Resources.V2.FortunateSon.Display.png " };
                        }
                        if (beh.Is<AttackModel>(out var aam)) {
                            aam.range += 5;
                            foreach (var weap in aam.weapons) {
                                weap.Rate /= 1.5f;
                                weap.projectile.display = new() { guidRef = "904432f1c20b4b145998e26a6e56cbb1" };
                                foreach (var pb in weap.projectile.behaviors) {
                                    if (pb.Is<DamageModel>(out var dm)) {
                                        dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            baseTwrMdl.behaviors = baseTwrMdl.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true));



            UpgradeMenuManager.towers[baseTwrMdl.name] = new UMM_Tower {
                CurrentUpgrade = 0, Name = identifier, TowerModel = baseTwrMdl,
                TowerType = "GROUND", UpgradeCost = 1_000_000, Portrait = "AdditionalTiers.Resources.V2.FortunateSon.Portrait.png",
                CurrentSPA = 0.075, CurrentDamage = 3,
                NextSPA = -0.025, NextDamage = 29, NextRange = 5,
                Extra = "Warmed Up Barrels", MaxUpgrade = false, NextUpgradeName = $"{identifier} T1"
            };

            var t1 = baseTwrMdl.CloneCast();
            t1.name = $"{identifier} T1";
            foreach (var beh in t1.behaviors) {
                if (beh.Is<AttackModel>(out var am)) {
                    am.range += 5;
                    foreach (var weap in am.weapons) {
                        weap.Rate -= 0.025f;
                        weap.projectile.display = new() { guidRef = "2f62958cc36daf74d88833447725e357" };
                        foreach (var pb in weap.projectile.behaviors) {
                            if (pb.Is<DamageModel>(out var dm)) {
                                dm.damage += 29;
                            }
                        }
                    }
                }
                if (beh.Is<AttackAirUnitModel>(out var aaum)) {
                    aaum.range += 5;
                    foreach (var weap in aaum.weapons) {
                        foreach (var pb in weap.projectile.behaviors) {
                            if (pb.Is<DamageModel>(out var dm)) {
                                dm.damage += 29;
                            }
                        }
                    }
                }
                if (beh.Is<AirUnitModel>(out var aum)) {
                    aum.display = new() { guidRef = "AdditionalTiers.Resources.V2.FortunateSon.Display2.png" };
                }
                if (beh.Is<ComancheDefenceModel>(out var cdm)) {
                    cdm.reinforcementCount = 5;
                    foreach (var cbeh in cdm.towerModel.behaviors) {
                        if (cbeh.Is<AttackModel>(out var aam)) {
                            aam.range += 5;
                            foreach (var weap in aam.weapons) {
                                weap.Rate /= 1.5f;
                                foreach (var pb in weap.projectile.behaviors) {
                                    if (pb.Is<DamageModel>(out var dm)) {
                                        dm.damage += 5;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            UpgradeMenuManager.towers[t1.name] = new UMM_Tower {
                CurrentUpgrade = 1, Name = identifier, TowerModel = t1,
                TowerType = "GROUND", UpgradeCost = 2_500_000, Portrait = "AdditionalTiers.Resources.V2.FortunateSon.Portrait.png",
                CurrentSPA = 0.05, CurrentDamage = 32,
                NextSPA = -0.045, NextDamage = 32, NextRange = 0,
                Extra = "Solar Beams", MaxUpgrade = false, NextUpgradeName = $"{identifier} T2"
            };



            var t2 = baseTwrMdl.CloneCast();
            t2.name = $"{identifier} T2";
            foreach (var beh in t2.behaviors) {
                if (beh.Is<AttackModel>(out var am)) {
                    am.range += 5;
                    foreach (var weap in am.weapons) {
                        weap.Rate -= 0.045f;
                        weap.projectile.display = new() { guidRef = "dcd6cd8511c9a03458a32f42f860882c" };
                        weap.projectile.scale = .5f;
                        foreach (var pb in weap.projectile.behaviors) {
                            if (pb.Is<DamageModel>(out var dm)) {
                                dm.damage += 32;
                            }
                        }
                    }
                }
                if (beh.Is<AttackAirUnitModel>(out var aaum)) {
                    aaum.range += 5;
                    foreach (var weap in aaum.weapons) {
                        weap.Rate -= 0.045f;
                        weap.emission = new ParallelEmissionModel("PEM", 4, 35, 0, false, null);
                        foreach (var pb in weap.projectile.behaviors) {
                            if (pb.Is<CreateProjectileOnContactModel>(out var cpocm)) {
                                cpocm.projectile.AddKnockbackModel();
                                cpocm.projectile.pierce = 5600;
                                cpocm.projectile.ModifyDamageModel(new DamageChange() { damage = 500, set = true });
                            }
                        }
                    }
                }
                if (beh.Is<AirUnitModel>(out var aum)) {
                    aum.display = new() { guidRef = "AdditionalTiers.Resources.V2.FortunateSon.Display3.png" };
                }
                if (beh.Is<ComancheDefenceModel>(out var cdm)) {
                    cdm.reinforcementCount = 7;
                    foreach (var cbeh in cdm.towerModel.behaviors) {
                        if (cbeh.Is<AttackModel>(out var aam)) {
                            aam.range += 5;
                            foreach (var weap in aam.weapons) {
                                weap.Rate /= 3f;
                                weap.projectile.display = new() { guidRef = "5737e26c93d5fc149ade2f7df1156c74" };
                                foreach (var pb in weap.projectile.behaviors) {
                                    if (pb.Is<DamageModel>(out var dm)) {
                                        dm.damage += 24;
                                    }
                                }
                            }
                        }
                        if (cbeh.Is<AirUnitModel>(out var auum)) {
                            auum.display = new() { guidRef = "AdditionalTiers.Resources.V2.FortunateSon.Display3.png " };
                        }
                    }
                }
            }

            UpgradeMenuManager.towers[t2.name] = new UMM_Tower {
                CurrentUpgrade = 2, Name = identifier, TowerModel = t2,
                TowerType = "GROUND", UpgradeCost = 2_500_000, Portrait = "AdditionalTiers.Resources.V2.FortunateSon.Portrait.png",
                CurrentSPA = 0.0005, CurrentDamage = 64,
                NextSPA = 0, NextDamage = 0, NextRange = 0,
                Extra = "", MaxUpgrade = true, NextUpgradeName = $"{identifier} T2"
            };
        };
        recurring += (TowerToSimulation tts) => {
            if (prog > 1)
                return;

            if (CameraMotionManager.instance.Action == CameraMotion.None) {
                OverlayManager.instance.towerName = identifier;
                OverlayManager.instance.StartOverlay();
                CameraMotionManager.instance.StartCameraMotion(CameraMotion.Pan, 260, 100);

                CameraMotionManager.instance.UpdateAction = (CameraMotionManager cmm) => {
                    var immT = InGame.Bridge.GetAllTowers().ToArray().ToImmutableArray();
                    var tower = immT.First(a => a.tower.towerModel.name.Contains(identifier));
                    for (int i = 0; i < tower.tower.towerBehaviors.Count; i++) {
                        if (tower.tower.towerBehaviors[i].Is<AirUnit>(out var au)) {
                            cmm.target = au.display.node.graphic.gameObject.transform.position;
                        }
                    }
                };
            }

            prog += Time.deltaTime / 2;
        };
        onLeave += () => { time = -1; prog = 0; };
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.FortunateSon.Display3.png", "6ff55b62a3a5bb7459ceb731804aa039", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.FortunateSon.Display2.png", "6ff55b62a3a5bb7459ceb731804aa039", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.FortunateSon.Display.png", "6ff55b62a3a5bb7459ceb731804aa039", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.FortunateSon.Display.png ", "5643bdec5ee8b214b86651b78a0af9d1", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.FortunateSon.Display3.png ", "5643bdec5ee8b214b86651b78a0af9d1", RendererType.SKINNEDMESHRENDERER));
    }
}
