using CreateEffectOnExpireModel = Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors.CreateEffectOnExpireModel;

namespace AdditionalTiers.Tasks.Round8;
internal class GlaiveCreator : TowerTask {
    public static TowerModel baseTwrMdl;
    private static int time = -1;
    public GlaiveCreator() {
        identifier = "Glaive Creator";
        getTower = () => baseTwrMdl;
        baseTower = AddedTierName.GLAIVECREATOR;
        tower = AddedTierEnum.GLAIVECREATOR;
        requirements += tts => tts.tower.towerModel.baseId.Equals("BoomerangMonkey") && tts.tower.towerModel.tiers[0] == 5;
        onComplete += tts => {
            if (time < 50) {
                time++;
                return;
            }
            TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
            tts.tower.worth = 0;
            tts.tower.UpdateRootModel(getTower());
            tts.tower.UpdatedModel(getTower());

            AbilityMenu.instance.TowerChanged(tts);
            AbilityMenu.instance.RebuildAbilities();
        };
        gameLoad += gm => {
            var flames = gm.towers.First(a => a.name.Equals("TackShooter-500")).CloneCast().behaviors.First(a => a.Is<AttackModel>() && !a.name.Contains("Meteor")).CloneCast<AttackModel>();
            flames.name = "AttackModel_AttackFlames_";
            flames.weapons[0].projectile.collisionPasses = new int[] { 0 };

            baseTwrMdl = gm.towers.First(a => a.name.Equals(baseTower)).CloneCast();
            baseTwrMdl.name = $"{identifier} Base";
            baseTwrMdl.SetDisplay("Round8_GLord#10");
            baseTwrMdl.SetIcons("Round8_GC_Portrait");

            baseTwrMdl.range += 5;
            baseTwrMdl.dontDisplayUpgrades = true;

            float damageStat = 15;

            for (int i = 0; i < baseTwrMdl.behaviors.Length; i++) {
                if (baseTwrMdl.behaviors[i].Is<AttackModel>(out var am)) {
                    am.range += 5;
                    if (am.name.Contains("Orbit")) {
                        foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                            if (projectileBehavior.Is<DamageModel>(out var dm)) {
                                dm.damage = damageStat / 3;
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            }
                        }
                    }

                    if (am.name.Contains("AttackModel_Attack_")) {
                        foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                            if (projectileBehavior.Is<DamageModel>(out var dm)) {
                                dm.damage = damageStat;
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            } else if (projectileBehavior.Is<TravelStraitModel>(out var tsm)) {
                                tsm.Lifespan += 8;
                            } else if (projectileBehavior.Is<DamageModifierForTagModel>(out var dmftm)) {
                                dmftm.damageMultiplier++;
                            }
                        }

                        am.weapons[0].projectile.behaviors = am.weapons[0].projectile.behaviors.Add(new CreateEffectOnExpireModel("CEOEM_", new() { guidRef = "6d84b13b7622d2744b8e8369565bc058" },
                            1, false, true, new EffectModel("Effect", new() { guidRef = "6d84b13b7622d2744b8e8369565bc058" }, 1, 1)));
                    }
                } else if (baseTwrMdl.behaviors[i].Is<OrbitModel>(out var om)) {
                    om.range += 5;
                    om.count++;
                }
            }

            baseTwrMdl.behaviors = baseTwrMdl.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true), flames);

            UpgradeMenuManager.AddTower(currentUpgrade: 0, name: identifier, towerModel: baseTwrMdl, towerType: "GROUND",
                                        upgradeCost: 100_000, portrait: "Round8_GC_Portrait", currentSPA: 0.6,
                                        currentDamage: Mathf.CeilToInt(damageStat), nextSPA: -0.05, nextDamage: 15,
                                        nextRange: 15, extra: "", maxUpgrade: false, nextUpgradeName: $"{identifier} T1"); //clarity

            var t1 = baseTwrMdl.CloneCast();
            t1.range += 15;
            t1.name = $"{identifier} T1";

            damageStat = 30;

            for (int i = 0; i < t1.behaviors.Length; i++) {
                if (t1.behaviors[i].Is<AttackModel>(out var am)) {
                    am.range += 15;
                    if (am.name.Contains("Orbit")) {
                        foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                            if (projectileBehavior.Is<DamageModel>(out var dm)) {
                                dm.damage = damageStat / 3;
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            }
                        }
                    }

                    if (am.name.Contains("AttackModel_Attack_") || am.name.Contains("AttackModel_AttackFlames_")) {
                        am.weapons[0].Rate -= 0.05f;
                        foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                            if (projectileBehavior.Is<DamageModel>(out var dm)) {
                                dm.damage = damageStat;
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            } else if (projectileBehavior.Is<DamageModifierForTagModel>(out var dmftm)) {
                                dmftm.damageMultiplier++;
                            }
                        }
                    }
                } else if (t1.behaviors[i].Is<OrbitModel>(out var om)) {
                    om.range += 5;
                    om.count++;
                }
            }

            UpgradeMenuManager.AddTower(currentUpgrade: 1, name: identifier, towerModel: t1, towerType: "GROUND",
                                        upgradeCost: 185_000, portrait: "Round8_GC_Portrait", currentSPA: 0.55,
                                        currentDamage: Mathf.CeilToInt(damageStat), nextSPA: -0.15, nextDamage: 30,
                                        nextRange: 15, extra: "Fireball", maxUpgrade: false, nextUpgradeName: $"{identifier} T2"); //clarity

            var fireball = gm.towers.First(a => a.name.Contains("WizardMonkey-020")).CloneCast().behaviors.First(a => a.name.Contains("Fireball")).CloneCast<AttackModel>();
            fireball.name = "AttackModel_AttackFireball_";
            fireball.weapons[0].projectile.behaviors.First(a => a.Is<CreateProjectileOnContactModel>()).Cast<CreateProjectileOnContactModel>().projectile.CapPierce(999999);

            var t2 = t1.CloneCast();
            t2.range += 15;
            t2.name = $"{identifier} T2";

            t2.behaviors = t2.behaviors.Add(fireball);

            damageStat = 60;

            for (int i = 0; i < t2.behaviors.Length; i++) {
                if (t2.behaviors[i].Is<AttackModel>(out var am)) {
                    am.range += 15;
                    if (am.name.Contains("Orbit")) {
                        foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                            if (projectileBehavior.Is<DamageModel>(out var dm)) {
                                dm.damage = damageStat / 3;
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            }
                        }
                    }

                    if (am.name.Contains("AttackModel_Attack_") || am.name.Contains("AttackModel_AttackFlames_")) {
                        am.weapons[0].Rate -= 0.15f;
                        foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                            if (projectileBehavior.Is<DamageModel>(out var dm)) {
                                dm.damage = damageStat;
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            } else if (projectileBehavior.Is<TravelStraitModel>(out var tsm)) {
                                tsm.Speed *= 1.25f;
                            } else if (projectileBehavior.Is<DamageModifierForTagModel>(out var dmftm)) {
                                dmftm.damageMultiplier++;
                            }
                        }
                    }

                    if (am.name.Contains("AttackModel_AttackFireball_")) {
                        foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                            if (projectileBehavior.Is<DamageModel>(out var dm)) {
                                dm.damage = damageStat;
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            } else if (projectileBehavior.Is<CreateProjectileOnContactModel>(out var cpocm)) {
                                foreach (var cprojectileBehavior in cpocm.projectile.behaviors) {
                                    if (cprojectileBehavior.Is<DamageModel>(out var cdm)) {
                                        cdm.damage = damageStat;
                                        cdm.immuneBloonProperties = cdm.immuneBloonPropertiesOriginal = BloonProperties.None;
                                    }
                                }
                            }
                        }
                    }
                } else if (t2.behaviors[i].Is<OrbitModel>(out var om)) {
                    om.count++;
                }
            }

            UpgradeMenuManager.AddTower(currentUpgrade: 2, name: identifier, towerModel: t2, towerType: "GROUND",
                                        upgradeCost: 200_000, portrait: "Round8_GC_Portrait", currentSPA: 0.4,
                                        currentDamage: Mathf.CeilToInt(damageStat), nextSPA: -0.2, nextDamage: 60,
                                        nextRange: 0, extra: "Triple Rangs", maxUpgrade: false, nextUpgradeName: $"{identifier} T3"); //clarity

            var t3 = t2.CloneCast();
            t3.name = $"{identifier} T3";

            damageStat = 120;

            for (int i = 0; i < t3.behaviors.Length; i++) {
                if (t3.behaviors[i].Is<AttackModel>(out var am)) {
                    if (am.name.Contains("Orbit")) {
                        foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                            if (projectileBehavior.Is<DamageModel>(out var dm)) {
                                dm.damage = damageStat / 3;
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            }
                        }
                    }

                    if (am.name.Contains("AttackModel_Attack_") || am.name.Contains("AttackModel_AttackFlames_")) {
                        am.weapons[0].emission = new ArcEmissionModel("AEM_", 3, 0, 30, null, false);
                        am.weapons[0].Rate -= 0.2f;

                        foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                            if (projectileBehavior.Is<DamageModel>(out var dm)) {
                                dm.damage = damageStat;
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            } else if (projectileBehavior.Is<TravelStraitModel>(out var tsm)) {
                                tsm.Speed *= 1.5f;
                            } else if (projectileBehavior.Is<DamageModifierForTagModel>(out var dmftm)) {
                                dmftm.damageMultiplier++;
                            }
                        }
                    }

                    if (am.name.Contains("AttackModel_AttackFireball_")) {
                        foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                            if (projectileBehavior.Is<DamageModel>(out var dm)) {
                                dm.damage = damageStat;
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            } else if (projectileBehavior.Is<CreateProjectileOnContactModel>(out var cpocm)) {
                                foreach (var cprojectileBehavior in cpocm.projectile.behaviors) {
                                    if (cprojectileBehavior.Is<DamageModel>(out var cdm)) {
                                        cdm.damage = damageStat;
                                        cdm.immuneBloonProperties = cdm.immuneBloonPropertiesOriginal = BloonProperties.None;
                                    }
                                }
                            }
                        }
                    }
                } else if (t3.behaviors[i].Is<OrbitModel>(out var om)) {
                    om.count++;
                }
            }

            UpgradeMenuManager.AddTower(currentUpgrade: 3, name: identifier, towerModel: t3, towerType: "GROUND",
                                        upgradeCost: 350_000, portrait: "Round8_GC_Portrait", currentSPA: 0.2,
                                        currentDamage: Mathf.CeilToInt(damageStat), nextSPA: -0.19, nextDamage: 2048 - 120,
                                        nextRange: 0, extra: "", maxUpgrade: false, nextUpgradeName: $"{identifier} T4"); //clarity

            var t4 = t3.CloneCast();
            t4.name = $"{identifier} T4";

            damageStat = 3072;

            for (int i = 0; i < t4.behaviors.Length; i++) {
                if (t4.behaviors[i].Is<AttackModel>(out var am)) {
                    am.weapons[0].Rate *= 0.05f;
                    if (am.name.Contains("Orbit")) {
                        foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                            if (projectileBehavior.Is<DamageModel>(out var dm)) {
                                dm.damage = damageStat;
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            }
                        }
                    }

                    if (am.name.Contains("AttackModel_Attack_") || am.name.Contains("AttackModel_AttackFlames_")) {
                        am.weapons[0].emission = new ArcEmissionModel("AEM_", 10, 0, 100, null, false);

                        if (am.name.Contains("AttackModel_Attack_")) {
                            am.weapons[0].behaviors = am.weapons[0].behaviors.Add(new CritMultiplierModel("CMM_", 50000, 5, 5, new() { guidRef = "252e82e70578330429a758339e10fd25" }, true));
                        }
                        if (am.name.Contains("AttackModel_AttackFlames_")) {
                            am.weapons[0].Rate *= 0.05f;
                        }

                        foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                            if (projectileBehavior.Is<DamageModel>(out var dm)) {
                                dm.damage = damageStat;
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            } else if (projectileBehavior.Is<TravelStraitModel>(out var tsm)) {
                                tsm.Speed *= 1.75f;
                            } else if (projectileBehavior.Is<DamageModifierForTagModel>(out var dmftm)) {
                                dmftm.damageMultiplier++;
                            }
                        }
                    }

                    if (am.name.Contains("AttackModel_AttackFireball_")) {
                        foreach (var projectileBehavior in am.weapons[0].projectile.behaviors) {
                            if (projectileBehavior.Is<DamageModel>(out var dm)) {
                                dm.damage = damageStat;
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            } else if (projectileBehavior.Is<CreateProjectileOnContactModel>(out var cpocm)) {
                                foreach (var cprojectileBehavior in cpocm.projectile.behaviors) {
                                    if (cprojectileBehavior.Is<DamageModel>(out var cdm)) {
                                        cdm.damage = damageStat;
                                        cdm.immuneBloonProperties = cdm.immuneBloonPropertiesOriginal = BloonProperties.None;
                                    }
                                }
                            }
                        }
                    }
                } else if (t4.behaviors[i].Is<OrbitModel>(out var om)) {
                    om.count += 2;
                }
            }

            UpgradeMenuManager.AddTower(currentUpgrade: 4, name: identifier, towerModel: t4, towerType: "GROUND",
                                        upgradeCost: 0, portrait: "Round8_GC_Portrait", currentSPA: 0.01,
                                        currentDamage: Mathf.CeilToInt(damageStat), nextSPA: 0, nextDamage: 0,
                                        nextRange: 0, extra: "", maxUpgrade: true, nextUpgradeName: ""); //clarity
        };
        recurring += (TowerToSimulation tts) => { };
        onLeave += () => { time = -1; };
    }

    [HarmonyPatch(typeof(Weapon), nameof(Weapon.SpawnDart))]
    public static class WI {
        [HarmonyPostfix]
        private static void Postfix(Weapon __instance) {
            if (__instance == null) return;
            if (__instance.attack == null) return;
            if (__instance.attack.tower == null) return;
            if (__instance.attack.tower.Node == null) return;
            if (__instance.attack.tower.Node.graphic == null) return;

            try {
                if ((__instance.attack.tower.towerModel.name.Equals("Glaive Creator Base") || __instance.attack.tower.towerModel.name.Equals("Glaive Creator T1") ||
                    __instance.attack.tower.towerModel.name.Equals("Glaive Creator T2")) && !__instance.attack.attackModel.name.Contains("Orbit")) {
                    __instance.attack.tower.Node.graphic.GetComponent<Animator>().StopPlayback();
                    __instance.attack.tower.Node.graphic.GetComponent<Animator>().Play("Attack");
                }
            } catch { }
        }
    }
}