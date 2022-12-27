namespace AdditionalTiers.Tasks.Round8;
internal class KingOfDarkness : TowerTask {
    public static TowerModel baseTwrMdl;
    private static int time = -1;
    public KingOfDarkness() {
        identifier = "King Of Darkness";
        getTower = () => baseTwrMdl;
        baseTower = AddedTierName.KINGOFDARKNESS;
        tower = AddedTierEnum.KINGOFDARKNESS;
        requirements += tts => tts.tower.towerModel.baseId.Equals("WizardMonkey") && tts.tower.towerModel.tiers[2] == 5;
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
            baseTwrMdl = gm.towers.First(a => a.name.Equals(baseTower)).CloneCast();
            baseTwrMdl.name = $"{identifier} Base";
            baseTwrMdl.SetDisplay("Round8_KingOfDarkness#1");
            baseTwrMdl.SetIcons("Round8_KOD_Portrait");

            baseTwrMdl.range += 5;
            baseTwrMdl.dontDisplayUpgrades = true;

            float damageStat = 0;

            for (int i = 0; i < baseTwrMdl.behaviors.Length; i++) {
                if (baseTwrMdl.behaviors[i].Is<AttackModel>(out var am)) {
                    am.range += 5;

                    if (am.name.Contains("Necromancer")) {
                        am.weapons[0].emission.Cast<NecromancerEmissionModel>().maxRbeSpawnedPerSecond /= 16;

                        var ew = am.weapons[1].CloneCast();

                        ew.projectile.SetDisplay("AdditionalTiers.Resources.V2.KingOfDarkness.ZombieBloonarius.png");
                        ew.projectile.scale = 0.5f;
                        ew.projectile.pierce *= 200;

                        var ewpodem = ew.emission.Cast<PrinceOfDarknessEmissionModel>();
                        ewpodem.alternateProjectile.SetDisplay("AdditionalTiers.Resources.V2.KingOfDarkness.ZombieDreadbloon.png");
                        ewpodem.alternateProjectile.scale = 0.5f;
                        ewpodem.alternateProjectile.pierce *= 300;

                        var nw = am.weapons[1].CloneCast();

                        nw.projectile.SetDisplay("AdditionalTiers.Resources.V2.KingOfDarkness.ZombieLych.png");
                        nw.projectile.scale = 0.5f;
                        nw.projectile.pierce *= 225;

                        var nwpodem = nw.emission.Cast<PrinceOfDarknessEmissionModel>();
                        nwpodem.alternateProjectile.SetDisplay("AdditionalTiers.Resources.V2.KingOfDarkness.ZombieVortex.png");
                        nwpodem.alternateProjectile.scale = 0.5f;
                        nwpodem.alternateProjectile.pierce *= 250;

                        am.weapons[1] = ew;

                        am.weapons = am.weapons.Add(nw);
                    }

                    foreach (var weap in am.weapons) {
                        if (!am.name.Contains("Necromancer"))
                            weap.Rate = 0.1f;
                        weap.projectile.pierce += 32;
                        foreach (var pb in weap.projectile.behaviors) {
                            if (pb.Is<DamageModel>(out var dm)) {
                                damageStat = dm.damage += 16;
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            }
                        }
                    }
                }
            }

            baseTwrMdl.behaviors = baseTwrMdl.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true));

            UpgradeMenuManager.towers[$"{identifier} Base"] = new UMM_Tower(
                0,
                identifier,
                baseTwrMdl, "GROUND",
                1_250_000, "Round8_KOD_Portrait", 0.1,
                Mathf.CeilToInt(damageStat),
                -0.05,
                48,
                15,
                "",
                false,
                $"{identifier} T1"
            );

            var t1 = baseTwrMdl.CloneCast();
            t1.name = $"{identifier} T1";
            t1.range += 15;
            foreach (var beh in t1.behaviors) {
                if (beh.Is<AttackModel>(out var am)) {
                    am.range += 15;
                    foreach (var weap in am.weapons) {
                        if (!am.name.Contains("Necromancer"))
                            weap.Rate = 0.05f;
                        weap.projectile.pierce += 32;
                        foreach (var pb in weap.projectile.behaviors) {
                            if (pb.Is<DamageModel>(out var dm)) {
                                damageStat = dm.damage += 48;
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            }
                        }
                    }
                }
            }

            UpgradeMenuManager.towers[$"{identifier} T1"] = new UMM_Tower(
                 1,
                identifier,
                t1,
                "GROUND",
                5_000_000,
                "Round8_KOD_Portrait",
                0.05,
                 Mathf.CeilToInt(damageStat),
                -0.04,
                448,
                30,
                "",
                false,
                $"{identifier} T2"
            );

            var t2 = t1.CloneCast();
            t2.name = $"{identifier} T2";
            t2.range += 30;
            foreach (var beh in t2.behaviors) {
                if (beh.Is<AttackModel>(out var am)) {
                    am.range += 30;
                    foreach (var weap in am.weapons) {
                        if (!am.name.Contains("Necromancer")) {
                            weap.Rate = 0.01f;
                        }
                        if (am.name.Contains("Fireball")) {
                            weap.Rate = 0.05f;
                            weap.emission = new ArcEmissionModel("AEM_", 16, 0, 360, null, false);
                            foreach (var pb in weap.projectile.behaviors) {
                                if (pb.Is<CreateProjectileOnContactModel>(out var cpocm)) {
                                    foreach (var cpb in cpocm.projectile.behaviors) {
                                        if (cpb.Is<DamageModel>(out var dm)) {
                                            damageStat = dm.damage += 512;
                                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                                        }
                                    }
                                }
                            }
                        }
                        if (am.name.Contains("Shimmer")) {
                            weap.projectile.pierce += 128;
                            weap.projectile.AddDamageModel(DamageModelCreation.Standard, 2048, true, BloonProperties.None);
                        }
                        weap.projectile.pierce += 64;
                        foreach (var pb in weap.projectile.behaviors) {
                            if (pb.Is<DamageModel>(out var dm)) {
                                damageStat = dm.damage += 448;
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            }
                        }
                    }
                }
            }

            UpgradeMenuManager.towers[$"{identifier} T2"] = new UMM_Tower(
                2,
                identifier,
                t2,
                "GROUND",
                0,
                "Round8_KOD_Portrait",
                0.01,
                Mathf.CeilToInt(damageStat),
                0,
                0,
                0,
                "",
                true,
                ""
            );
        };
        recurring += (TowerToSimulation tts) => { };
        onLeave += () => { time = -1; };
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.KingOfDarkness.ZombieBloonarius.png", "735e5fa84f3c4f24ea3aa4c39d50ddda", RendererType.ANYRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.KingOfDarkness.ZombieDreadbloon.png", "c1e6cb196a4d3254f82730ca5e9a15ae", RendererType.ANYRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.KingOfDarkness.ZombieLych.png", "a86ed84b8461f7a489119421ac4df429", RendererType.ANYRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.KingOfDarkness.ZombieVortex.png", "b7704fd01bfd0214693e28cc5d43fe4a", RendererType.ANYRENDERER));
    }
}