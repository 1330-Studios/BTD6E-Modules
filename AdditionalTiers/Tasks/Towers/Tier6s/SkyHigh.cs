namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class SkyHigh : TowerTask {
        public static TowerModel skyHigh;
        private static int time = -1;
        public SkyHigh() {
            identifier = "Sky High";
            getTower = () => skyHigh;
            baseTower = AddedTierName.SKYHIGH;
            tower = AddedTierEnum.SKYHIGH;
            requirements += tts => tts.tower.towerModel.baseId.Equals("DartMonkey") && tts.tower.towerModel.tiers[2] == 5;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(skyHigh);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                skyHigh = gm.towers.First(a => a.name.Contains("DartMonkey-025")).Clone()
                    .Cast<TowerModel>();

                skyHigh.cost = 0;
                skyHigh.name = "Sky High";
                skyHigh.baseId = "DartMonkey";
                skyHigh.display.guidRef = "SkyHigh";
                skyHigh.dontDisplayUpgrades = true;
                skyHigh.portrait = new("SkyHighIcon");
                skyHigh.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display.guidRef = "SkyHigh";

                var beh = skyHigh.behaviors;

                for (var i = 0; i < beh.Length; i++)
                    if (beh[i].GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var am = beh[i].Cast<AttackModel>();

                        for (var j = 0; j < am.weapons.Length; j++) {
                            var we = am.weapons[j];
                            CritMultiplierModel cmm = null;

                            for (var k = 0; k < we.behaviors.Length; k++)
                                if (we.behaviors[k].GetIl2CppType() == Il2CppType.Of<CritMultiplierModel>()) {
                                    var web = we.behaviors[k].Cast<CritMultiplierModel>();

                                    web.display.guidRef = "";
                                    web.damage = 100;
                                    web.lower = 10;
                                    web.upper = 10;
                                    cmm = web.Clone().Cast<CritMultiplierModel>();

                                    we.behaviors[k] = web;
                                }

                            cmm.name = "CritMultiplierModel__";
                            we.rate = 0.02f;
                            we.emission = new ArcEmissionModel("ArcEmissionModel_", 5, 0, 75, null, false).Cast<EmissionModel>();
                            we.behaviors = we.behaviors.Add(cmm);

                            we.projectile.pierce = 500000;
                            we.projectile.display.guidRef = "SkyHighProj";

                            for (var k = 0; k < we.projectile.behaviors.Length; k++) {
                                if (we.projectile.behaviors[k].GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                                    var dm = we.projectile.behaviors[k].Cast<DamageModel>();

                                    dm.damage *= 2 * Globals.SixthTierDamageMulti;

                                    we.projectile.behaviors[k] = dm;
                                }
                                if (we.projectile.behaviors[k].GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                                    var dm = we.projectile.behaviors[k].Cast<DisplayModel>();

                                    dm.display.guidRef = "SkyHighProj";

                                    we.projectile.behaviors[k] = dm;
                                }
                                if (we.projectile.behaviors[k].GetIl2CppType() == Il2CppType.Of<TravelStraitModel>()) {
                                    var tsm = we.projectile.behaviors[k].Cast<TravelStraitModel>();

                                    tsm.lifespan *= 2;

                                    we.projectile.behaviors[k] = tsm;
                                }
                            }

                            am.weapons[j] = we;
                        }

                        beh[i] = am;
                    }

                skyHigh.behaviors = beh;
            };
            recurring += tts => { };
            onLeave += () => { time = -1; };
            assetsToRead.Add(new("SkyHigh", "f7a1b5c14ded01146b80bd7121f3fcd7", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("SkyHighProj", "ae8cebf807b15984daf0219b66f42897", RendererType.SPRITERENDERER));
        }
    }
}