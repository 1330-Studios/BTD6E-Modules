namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class Whitesnake : TowerTask {
        public static TowerModel whitesnake;
        public static TowerModel whitesnakePheonix;
        public static TowerModel whitesnakeDarkPheonix;
        private static int _time = -1;
        public Whitesnake() {
            identifier = "Whitesnake";
            getTower = () => whitesnake;
            baseTower = AddedTierName.WHITESNAKE;
            tower = AddedTierEnum.WHITESNAKE;
            requirements += tts => tts.tower.towerModel.baseId.Equals("WizardMonkey") && tts.tower.towerModel.tiers[1] == 5;
            onComplete += tts => {
                if (_time < 50) {
                    _time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(whitesnake);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                var sim = tts.sim;
                foreach (var towerToSimulation in sim.GetAllTowers())
                    if (towerToSimulation.tower.towerModel.baseId.Equals("PermaPhoenix")) {
                        towerToSimulation.destroyed = true;
                        sim.simulation.SellTower(towerToSimulation.tower, -1);
                    }

                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                whitesnake = gm.towers.First(a => a.name.Contains("WizardMonkey")).Clone().Cast<TowerModel>();

                whitesnake.cost = 0;
                whitesnake.range = 120;
                whitesnake.baseId = "WizardMonkey";
                whitesnake.display.guidRef = "Whitesnake";
                whitesnake.dontDisplayUpgrades = true;
                whitesnake.portrait = new("WhitesnakeIcon");
                whitesnake.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display.guidRef = "Whitesnake";

                var beh = whitesnake.behaviors;

                for (var i = 0; i < beh.Length; i++)
                    if (beh[i].Is<AttackModel>(out var am)) {
                        for (var j = 0; j < am.weapons.Length; j++) {
                            var we = am.weapons[j];

                            for (var k = 0; k < we.projectile.behaviors.Length; k++) {
                                if (we.projectile.behaviors[k].Is<DamageModel>(out var dm)) {
                                    dm.damage = 55000 * Globals.SixthTierDamageMulti;

                                    we.projectile.behaviors[k] = dm;
                                }
                                if (we.projectile.behaviors[k].Is<DisplayModel>(out var display)) {
                                    display.display.guidRef = "WhitesnakeProj";

                                    we.projectile.behaviors[k] = display;
                                }
                                if (we.projectile.behaviors[k].Is<TravelStraitModel>(out var tsm)) {
                                    tsm.Lifespan *= 12;
                                    tsm.lifespanFrames *= 12;
                                    tsm.Speed /= 10;
                                    tsm.speedFrames /= 10;

                                    we.projectile.behaviors[k] = tsm;
                                }
                            }

                            we.name = "WSW1";
                            we.rate *= 10;
                            we.emission = new ArcEmissionModel("AEM_", 1, 0, 0, null, false);
                            we.projectile.pierce = 500000;
                            we.projectile.radius = 50;
                            we.projectile.display.guidRef = "WhitesnakeProj";
                            we.projectile.ignoreNonTargetable = true;
                            we.projectile.ignoreBlockers = true;
                            we.projectile.ignorePierceExhaustion = true;

                            we.projectile.behaviors = we.projectile.behaviors.Add(new CreateLightningEffectModel("CLEM_", 0.3f, new PrefabReference[] {
                                    new("548c26e4e668dac4a850a4c016916016"), new("ffed377b3e146f649b3e6d5767726a44"), new("c5e4bf0202becd0459c47b8184b4588f"),
                                    new("3e113b397a21a3a4687cf2ed0c436ec8"), new("c6c2049a0c01e8a4d9904db8c9b84ca0"), new("e9b2a3d6f0fe0e4419a423e4d2ebe6f6"),
                                    new("c8471dcde4c65fc459f7846c6a932a8c"), new("a73b565de9c31c14ebcd3317705ab17e"), new("bd23939e7362b8e40a3a39f595a2a1dc")
                                }, new float[] { 18, 18, 18, 50, 50, 50, 85, 85, 85 }),
                                new LightningModel("LM_", 5, new ArcEmissionModel("AEM_", 3, 0, 360, null, false), 360, 5),
                                new DamagePercentOfMaxModel("DPOMM_", 0.25f, new string[] { "Moabs" }, false),
                                new DistributeToChildrenBloonModifierModel("DTCBMM_", "Moabs"),
                                new WindModel("WM_", 50, 150, 1, true, "", 1));

                            am.weapons[j] = we;
                        }

                        am.range = 120;
                        am.attackThroughWalls = true;
                        beh[i] = am;
                    }

                whitesnake.behaviors = beh.Add(new OverrideCamoDetectionModel("OCDM_", true), new PerRoundCashBonusTowerModel("PRCBTM_", 2500, 1.125f, 10, new("CashText"), true));

                #region Light Pheonix

                whitesnakePheonix = gm.towers.FirstOrDefault(a => a.baseId.Equals("PermaPhoenix")).Clone().Cast<TowerModel>();
                whitesnakePheonix.display.guidRef = "WhitesnakePheonix";
                whitesnakePheonix.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display.guidRef = "WhitesnakePheonix";
                var pbeh = whitesnakePheonix.behaviors;

                for (var i = 0; i < pbeh.Length; i++) {
                    if (pbeh[i].Is<AttackModel>(out var am)) {
                        for (var i1 = 0; i1 < am.weapons.Count; i1++) {
                            var weap = am.weapons[i1];

                            weap.rate /= 3;
                            weap.rateFrames /= 3;

                            weap.emission = new AdoraEmissionModel("AEM_", 3, 45, null);
                            weap.projectile.ignorePierceExhaustion = true;
                            weap.projectile.display.guidRef = "WhitesnakePheonixProj";
                            weap.projectile.behaviors = weap.projectile.behaviors.Add(new DamagePercentOfMaxModel("DPOMM_", 0.0033f, new string[] { "NA" }, false),
                                new TrackTargetWithinTimeModel("TTWM_", 9999999, true, false, 365, false, float.MaxValue, false, 3.48f, true));

                            for (var j = 0; j < weap.projectile.behaviors.Length; j++)
                                if (weap.projectile.behaviors[j].Is<TravelStraitModel>(out var tsm)) {
                                    tsm.Speed *= 1.25f;
                                    tsm.speedFrames *= 1.25f;

                                    weap.projectile.behaviors[j] = tsm;
                                }

                            am.weapons[i1] = weap;
                        }

                        pbeh[i] = am;
                    }

                    if (pbeh[i].Is<PathMovementFromScreenCenterModel>(out var pmfscm)) {
                        pmfscm.speed *= 1.5f;
                        pmfscm.speedFrames *= 1.5f;
                    }
                }

                whitesnakePheonix.behaviors = pbeh.Add(new OverrideCamoDetectionModel("OCDM_", true),
                    new DisplayModel("DM_", new("9ed0d0de732cabe48898f8dddb7023ca"), 0, new(), 1, true, 0));

                #endregion

                #region Dark Pheonix

                whitesnakeDarkPheonix = gm.towers.FirstOrDefault(a => a.baseId.Equals("PermaPhoenix")).Clone().Cast<TowerModel>();
                whitesnakeDarkPheonix.display.guidRef = "WhitesnakeDarkPheonix";
                whitesnakeDarkPheonix.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display.guidRef = "WhitesnakeDarkPheonix";
                whitesnakeDarkPheonix.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().positionOffset = new(0, 0, 10);
                var dpbeh = whitesnakeDarkPheonix.behaviors;

                for (var i = 0; i < dpbeh.Length; i++) {
                    if (dpbeh[i].Is<AttackModel>(out var am)) {
                        for (var i1 = 0; i1 < am.weapons.Count; i1++) {
                            var weap = am.weapons[i1];

                            weap.rate *= 2;
                            weap.rateFrames *= 2;

                            weap.projectile.ignorePierceExhaustion = true;
                            weap.projectile.display.guidRef = "WhitesnakeDarkPheonixProj";
                            weap.projectile.behaviors = weap.projectile.behaviors.Add(new TrackTargetWithinTimeModel("TTWM_", 9999999, true, false, 365, false, float.MaxValue, false, 3.48f, true));

                            for (var j = 0; j < weap.projectile.behaviors.Length; j++) {
                                if (weap.projectile.behaviors[j].Is<DamageModel>(out var dm)) {
                                    dm.damage *= 750 * Globals.SixthTierDamageMulti;

                                    weap.projectile.behaviors[j] = dm;
                                }

                                if (weap.projectile.behaviors[j].Is<TravelStraitModel>(out var tsm)) {
                                    tsm.Speed /= 1.25f;
                                    tsm.speedFrames /= 1.25f;

                                    weap.projectile.behaviors[j] = tsm;
                                }
                            }

                            am.weapons[i1] = weap;
                        }

                        dpbeh[i] = am;
                    }

                    if (dpbeh[i].Is<PathMovementFromScreenCenterModel>(out var pmfscm)) {
                        pmfscm.speed /= 1.5f;
                        pmfscm.speedFrames /= 1.5f;
                    }
                }

                whitesnakeDarkPheonix.behaviors = dpbeh.Add(new OverrideCamoDetectionModel("OCDM_", true),
                    new DisplayModel("DM_", new("9ed0d0de732cabe48898f8dddb7023ca"), 0, new(), 1, true, 0),
                    new TowerCreateTowerModel("TCTM_", whitesnakePheonix, true));

                #endregion

                whitesnake.behaviors = whitesnake.behaviors.Add(new TowerCreateTowerModel("TCTM_", whitesnakeDarkPheonix, true));
            };
            recurring += tts => { };
            onLeave += () => { _time = -1; };
            assetsToRead.Add(new("Whitesnake", "d8a45c17dcf700a499c031dff73684a1", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("WhitesnakeProj", "bdbeaa256e6c63b45829535831843376", RendererType.SPRITERENDERER));
            assetsToRead.Add(new("WhitesnakePheonix", "1e5aa5cc44941da43a90880b50d5d112", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("WhitesnakeDarkPheonix", "1e5aa5cc44941da43a90880b50d5d112", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("WhitesnakePheonixProj", "bdbeaa256e6c63b45829535831843376", RendererType.SPRITERENDERER));
            assetsToRead.Add(new("WhitesnakeDarkPheonixProj", "dac321e299fa10c468d66187d3e0e34c", RendererType.SPRITERENDERER));
        }
    }
}