namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class PointOfKnowReturn : TowerTask {
        public static TowerModel POKR;
        private static int time = -1;

        public PointOfKnowReturn() {
            identifier = "Point of Know Return";
            getTower = () => POKR;
            baseTower = AddedTierName.POINTOFNORETURN;
            tower = AddedTierEnum.POINTOFNORETURN;
            requirements += tts => tts.tower.towerModel.baseId.Equals("BombShooter") && tts.tower.towerModel.tiers[1] == 5;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(POKR);
                tts.tower.display.SetScaleOffset(new(1.2f, 1.2f, 1.2f));
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                POKR = gm.towers.First(a => a.name.Contains("BombShooter-250")).Clone().Cast<TowerModel>();

                POKR.range = 150;
                POKR.cost = 0;
                POKR.name = "Point of no Return";
                POKR.baseId = "BombShooter";
                POKR.display.guidRef = "PONR";
                POKR.dontDisplayUpgrades = true;
                POKR.portrait = new("PONRIcon");
                POKR.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>())
                    .Cast<DisplayModel>().display.guidRef = "PONR";
                var beh = POKR.behaviors;
                ProjectileModel proj = null;
                ProjectileModel proj2 = null;

                for (var i = 0; i < beh.Length; i++) {
                    var behavior = beh[i];
                    if (behavior.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var am = behavior.Cast<AttackModel>();
                        am.range = 150;
                        am.weapons[0].projectile.ignoreBlockers = true;
                        am.weapons[0].projectile.ignoreNonTargetable = true;
                        proj = am.weapons[0].projectile.Clone().Cast<ProjectileModel>();
                        proj.pierce = 50;
                        proj.maxPierce = 50;
                        proj.behaviors = proj.behaviors.Add(
                            new TrackTargetWithinTimeModel("TrackTargetWithinTimeModel_",
                                9999999, true, false, 361, false, 9999999, false, 8f, true).Cast<Model>());
                        am.weapons[0].projectile.scale = 1.2f;
                        am.weapons[0].projectile.display.guidRef = "PONRProj";
                        am.weapons[0].rate *= 4;
                        am.weapons[0].rateFrames *= 4;
                        am.weapons[0].rate /= 3;
                        am.weapons[0].rateFrames /= 3;


                        am.weapons[0].projectile.behaviors = am.weapons[0].projectile.behaviors.Add(
                            new RotateModel("RotateModel", 180).Cast<Model>(),
                            new TrackTargetWithinTimeModel("TrackTargetWithinTimeModel_",
                                9999999, true, false, 120, false, 9999999, false, 8f, true).Cast<Model>(),
                            new DamageModifierForTagModel("DamageModifierForTagModel_", "Moabs", 2, 5, false, false));

                        for (var k = 0; k < am.weapons[0].projectile.behaviors.Length; k++) {
                            if (am.weapons[0].projectile.behaviors[k].GetIl2CppType() == Il2CppType.Of<TravelStraitModel>()) {
                                var p = am.weapons[0].projectile.behaviors[k].Cast<TravelStraitModel>();
                                p.lifespan *= 2;
                                p.Lifespan *= 2;
                                p.lifespanFrames *= 2;
                                am.weapons[0].projectile.behaviors[k] = p;
                            }
                            if (am.weapons[0].projectile.behaviors[k].GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                                var p = am.weapons[0].projectile.behaviors[k].Cast<DamageModel>();
                                p.damage *= 10 * Globals.SixthTierDamageMulti;
                                am.weapons[0].projectile.behaviors[k] = p;
                            }
                        }

                        var projectile = gm.towers.FirstOrDefault(a => a.name.Equals("BombShooter-300")).behaviors.First((Model a) => a.GetIl2CppType() == Il2CppType.Of<AttackModel>()).Clone().Cast<AttackModel>().weapons[0].projectile;
                        var cpocm = projectile.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<CreateProjectileOnContactModel>()).Cast<CreateProjectileOnContactModel>();
                        cpocm.projectile = proj;
                        cpocm.emission = new AdoraEmissionModel("AdoraEmissionModel_", 8, 45, null)/*new ArcEmissionModel("ArcEmissionModel_", 8, 15, 360, null, false, false)*/.Cast<EmissionModel>();
                        var csopcm = projectile.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<CreateSoundOnProjectileCollisionModel>());
                        var ceocm = projectile.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<CreateEffectOnContactModel>());
                        am.weapons[0].projectile.behaviors = am.weapons[0].projectile.behaviors.Add(cpocm, csopcm, ceocm);

                        proj2 = am.weapons[0].projectile;
                        proj2.pierce += 5;

                        for (var k = 0; k < am.weapons[0].projectile.behaviors.Length; k++)
                            if (am.weapons[0].projectile.behaviors[k].GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                                var p = am.weapons[0].projectile.behaviors[k].Cast<DamageModel>();
                                p.damage *= 10 * Globals.SixthTierDamageMulti;
                                am.weapons[0].projectile.behaviors[k] = p;
                            }
                    }

                    if (behavior.GetIl2CppType() == Il2CppType.Of<AbilityModel>()) {
                        var ab = behavior.Cast<AbilityModel>();
                        ab.icon = new("PONRIcon");
                        for (var j = 0; j < ab.behaviors.Length; j++)
                            if (ab.behaviors[j].GetIl2CppType() == Il2CppType.Of<ActivateAttackModel>()) {
                                var aam = ab.behaviors[j].Cast<ActivateAttackModel>();
                                aam.attacks[0].weapons[0].emission = new AdoraEmissionModel("AdoraEmissionModel", 24, 15, null).Cast<EmissionModel>();
                                aam.attacks[0].weapons[0].projectile = proj2;
                            }

                        beh[i] = ab;
                    }
                }

                POKR.behaviors = beh.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true));
            };
            recurring += tts => { };
            onLeave += () => { time = -1; };
            assetsToRead.Add(new("PONR", "31a16eecf9211a64b8dcdfad2ff7974e", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("PONRProj", "e5edd901992846e409326a506d272633", RendererType.MESHRENDERER));
        }
    }
}