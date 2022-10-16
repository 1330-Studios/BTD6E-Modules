namespace AdditionalTiers.Tasks.Towers.AltT4s {
    public sealed class SunGod : TowerTask {
        public static TowerModel btd4SunGod;
        public static TowerModel btd4SunGodV;
        private static int time = -1;
        public SunGod() {
            identifier = "BTD4 Sun God";
            baseTower = AddedTierName.BTD4SUNGOD;
            tower = AddedTierEnum.BTD4SUNGOD;
            getTower = () => btd4SunGod;
            requirements += tts => !TransformationManager.VALUE.Contains(tts.tower) && !TransformationManager.VALUE.Get(tts.tower).Name.Equals("BTD4 Vengeful Sun God") && tts.tower.towerModel.baseId.Equals("SuperMonkey") && tts.tower.towerModel.tiers[0] == 3 && tts.tower.towerModel.tiers[1] == 2;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.UpdateRootModel(btd4SunGod);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "???", false);
            };
            gameLoad += gm => {
                btd4SunGod = gm.towers.First(a => a.name.Contains("SuperMonkey-320")).CloneCast();

                btd4SunGod.display.guidRef = "BTD4SunGod";
                btd4SunGod.portrait = new("BTD4SunGodPor");
                btd4SunGod.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display.guidRef = "BTD4SunGod";
                var beh = btd4SunGod.behaviors;
                for (var i = 0; i < beh.Length; i++) {
                    var behavior = beh[i];
                    if (behavior.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var am = behavior.Cast<AttackModel>();

                        for (var j = 0; j < am.weapons.Length; j++) {
                            var weapon = am.weapons[j];

                            if (weapon.emission.Is<RandomArcEmissionModel>(out var raem)) {
                                raem.offset = 0;
                                raem.angle = 0;
                                raem.randomAngle = 5;
                                raem.startOffset = 0;
                                raem.offsetStart = 0;
                                raem.sliceSize = 0;

                                weapon.emission = raem;
                            }

                            weapon.projectile.display.guidRef = "BTD4SunGodProj";
                            am.weapons[j] = weapon;
                        }

                        beh[i] = am;
                    }

                    beh[i] = behavior;
                }

                btd4SunGod.behaviors = beh;

                // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

                btd4SunGodV = gm.towers.First(a => a.name.Contains("SuperMonkey-320")).CloneCast();

                btd4SunGodV.display.guidRef = "BTD4SunGodV";
                btd4SunGodV.portrait = new("BTD4SunGodPorV");
                btd4SunGodV.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display.guidRef = "BTD4SunGodV";
                btd4SunGodV.upgrades = new UpgradePathModel[0];
                btd4SunGodV.dontDisplayUpgrades = true;

                var vbeh = btd4SunGodV.behaviors;
                for (var i = 0; i < vbeh.Length; i++) {
                    var behavior = vbeh[i];
                    if (behavior.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var am = behavior.Cast<AttackModel>();

                        for (var j = 0; j < am.weapons.Length; j++) {
                            var weapon = am.weapons[j];

                            weapon.rate /= 2;
                            weapon.rateFrames /= 2;

                            if (weapon.emission.Is<RandomArcEmissionModel>(out var raem)) {
                                raem.count = 7;

                                weapon.emission = raem;

                                var pbeh = weapon.projectile.behaviors;

                                for (int p = 0; p < pbeh.Length; p++) {
                                    if (pbeh[p].Is<DamageModel>(out var dm)) {
                                        dm.damage *= 5;
                                    }
                                }
                                weapon.projectile.display.guidRef = "BTD4SunGodProjV";
                                weapon.projectile.behaviors = pbeh;
                            }

                            am.weapons[j] = weapon;
                        }

                        vbeh[i] = am;
                    }

                    vbeh[i] = behavior;
                }

                btd4SunGodV.behaviors = vbeh.Add(new OverrideCamoDetectionModel("OCDM_", true));
            };
            recurring += tts => {
                if (!tts.tower.towerModel.name.Contains("-3")) {
                    TransformationManager.VALUE.Remove(tts.tower);
                    return;
                }
                if (TransformationManager.VALUE.Contains(tts.tower) && !TransformationManager.VALUE.Get(tts.tower).Name.Equals("BTD4 Vengeful Sun God") && tts.damageDealt > (int)AddedTierEnum.BTD4SUNGOD * Globals.SixthTierPopCountMulti * 5) {
                    var sim = tts.sim;
                    var towers = sim.ttss.ToArray();
                    var condition1 = towers.Any(t => t.tower.towerModel.name.StartsWith("SuperMonkey") && t.tower.towerModel.tiers[1] == 3);
                    var condition2 = towers.Any(t => t.tower.towerModel.name.StartsWith("SuperMonkey") && t.tower.towerModel.tiers[2] == 3);

                    if (condition1 && condition2) {
                        var s1 = towers.First(t => t.tower.towerModel.name.StartsWith("SuperMonkey") && t.tower.towerModel.tiers[1] == 3);
                        var s2 = towers.First(t => t.tower.towerModel.name.StartsWith("SuperMonkey") && t.tower.towerModel.tiers[2] == 3);

                        var moolah = sim.GetCash(sim.GetInputId());

                        sim.SellTower(s1.id);
                        sim.SellTower(s2.id);

                        sim.SetCash(moolah);

                        TransformationManager.VALUE.Replace(tts.tower, new ("BTD4 Vengeful Sun God", tts.tower.Id.Id));
                        tts.tower.UpdateRootModel(btd4SunGodV);
                        tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "???", false);
                    }
                }
            };
            onLeave += () => { time = -1; };
            assetsToRead.Add(new("BTD4SunGod", "e23d594d3bf5af44c8b1e2445fe10a9e", RendererType.SPRITERENDERER));
            assetsToRead.Add(new("BTD4SunGodV", "e23d594d3bf5af44c8b1e2445fe10a9e", RendererType.SPRITERENDERER));
            assetsToRead.Add(new("BTD4SunGodProj", "9dccc16d26c1c8a45b129e2a8cbd17ba", RendererType.SPRITERENDERER));
            assetsToRead.Add(new("BTD4SunGodProjV", "9dccc16d26c1c8a45b129e2a8cbd17ba", RendererType.SPRITERENDERER));
        }
    }
}
