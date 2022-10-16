namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class BlackHoleSun : TowerTask {
        public static TowerModel BHS;
        private static int time = -1;

        public BlackHoleSun() {
            identifier = "Black Hole Sun";
            getTower = () => BHS;
            baseTower = AddedTierName.BLACKHOLESUN;
            tower = AddedTierEnum.BLACKHOLESUN;
            requirements += tts => tts.tower.towerModel.baseId.Equals("DartMonkey") && tts.tower.towerModel.tiers[1] == 5;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(BHS);
                tts.tower.display.SetScaleOffset(new(1.25f, 1.25f, 1.25f));
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                BHS = gm.towers.First(a => a.name.Contains("DartMonkey-250")).Clone()
                    .Cast<TowerModel>();

                BHS.range = 150;
                BHS.cost = 0;
                BHS.name = "Black Hole Sun";
                BHS.baseId = "DartMonkey";
                BHS.display.guidRef = "BlackHoleSun";
                BHS.dontDisplayUpgrades = true;
                BHS.portrait = new("BlackHoleSunIcon");
                BHS.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>())
                    .Cast<DisplayModel>().display.guidRef = "BlackHoleSun";
                var beh = BHS.behaviors;

                for (var i = 0; i < beh.Length; i++) {
                    var behavior = beh[i];
                    if (behavior.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var am = behavior.Cast<AttackModel>();
                        am.range = 150;

                        for (var j = 0; j < am.weapons.Length; j++) {
                            am.weapons[j].emission = new AdoraEmissionModel("Adora_", 1, 0, null).Cast<EmissionModel>();

                            am.weapons[j].projectile.display.guidRef = "BlackHoleSunProjectile";
                            am.weapons[j].projectile.pierce *= 500;
                            am.weapons[j].projectile.scale *= 1.125f;
                            am.weapons[j].rate /= 15;
                            am.weapons[j].rateFrames /= 15;

                            for (var k = 0; k < am.weapons[j].projectile.behaviors.Length; k++) {
                                if (am.weapons[j].projectile.behaviors[k].GetIl2CppType() == Il2CppType.Of<TravelStraitModel>()) {
                                    var p = am.weapons[j].projectile.behaviors[k].Cast<TravelStraitModel>();
                                    p.lifespan *= 100;
                                    p.Lifespan *= 100;
                                    p.lifespanFrames *= 100;
                                    p.speed *= 2;
                                    p.Speed *= 2;
                                    p.speedFrames *= 2;
                                    am.weapons[j].projectile.behaviors[k] = p;
                                }
                                if (am.weapons[j].projectile.behaviors[k].GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                                    var p = am.weapons[j].projectile.behaviors[k].Cast<DamageModel>();
                                    p.immuneBloonProperties = BloonProperties.Lead;
                                    p.damage *= 2 * Globals.SixthTierDamageMulti;
                                    am.weapons[j].projectile.behaviors[k] = p;
                                }
                            }

                            am.weapons[j].projectile.behaviors = am.weapons[j].projectile.behaviors.Add(
                                new RotateModel("RotateModel_", 720).Cast<Model>(),
                                new TrackTargetWithinTimeModel("TrackTargetWithinTimeModel_",
                                    9999999, true, false, 365, false, 9999999, false, 3.48f, true).Cast<Model>());
                        }
                    }

                    if (behavior.GetIl2CppType() == Il2CppType.Of<AbilityModel>()) {
                        var a = behavior.Cast<AbilityModel>();

                        a.icon = new("BlackHoleSunIcon");
                        for (var j = 0; j < a.behaviors.Length; j++) {
                            var b = a.behaviors[j];

                            if (b.GetIl2CppType() == Il2CppType.Of<MonkeyFanClubModel>()) {
                                var mfcm = b.Cast<MonkeyFanClubModel>();

                                mfcm.display.guidRef = "BlackHoleSunProjectile";
                                mfcm.towerOriginDisplay.guidRef = "BlackHoleSun";
                                mfcm.bonusDamage++;
                                mfcm.bonusPierce++;
                                mfcm.maxTier = 5;
                                mfcm.range = 200;

                                b = mfcm;
                            }

                            a.behaviors[j] = b;
                        }

                        beh[i] = a;
                    }
                }

                BHS.behaviors = beh.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true));
            };
            recurring += tts => { };
            onLeave += () => { time = -1; };
            assetsToRead.Add(new("BlackHoleSun", "8f3b1daf26cefc34cbf78aa45210317b", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("BlackHoleSunProjectile", "ae8cebf807b15984daf0219b66f42897", RendererType.SPRITERENDERER));
        }
    }
}