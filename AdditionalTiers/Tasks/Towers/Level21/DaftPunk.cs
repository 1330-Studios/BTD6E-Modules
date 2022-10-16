
namespace AdditionalTiers.Tasks.Towers.Level21 {
    public sealed class DaftPunk : TowerTask {
        public static TowerModel daftPunk;
        private static int time = -1;
        public DaftPunk() {
            identifier = "Daft Punk";
            getTower = () => daftPunk;
            baseTower = AddedTierName.DAFTPUNK;
            tower = AddedTierEnum.DAFTPUNK;
            requirements += tts => tts.tower.towerModel.baseId.Equals("Quincy") && tts.tower.towerModel.tier == 20;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(daftPunk);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                daftPunk = gm.towers.First(a => a.name.Contains(AddedTierName.DAFTPUNK)).CloneCast();
                var taipei = gm.towers.First(a => a.name.Contains("BoomerangMonkey-250")).CloneCast();
                var hongkong = gm.towers.First(a => a.name.Contains("TackShooter-520")).CloneCast();
                var taiwan = gm.towers.First(a => a.name.Contains("SpikeFactory-520")).CloneCast();

                daftPunk.range = 150;
                daftPunk.cost = 0;
                daftPunk.name = "Daft Punk";
                daftPunk.baseId = "Quincy";
                daftPunk.SetDisplay("DaftPunk");
                daftPunk.dontDisplayUpgrades = true;
                var beh = daftPunk.behaviors;

                for (var i = 0; i < beh.Length; i++) {
                    var behavior = beh[i];
                    if (behavior.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var am = behavior.Cast<AttackModel>();

                        am.behaviors = am.behaviors.Remove(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>());

                        for (var j = 0; j < am.weapons.Length; j++) {
                            am.weapons[j].projectile.display.guidRef = "DaftPunkProjectile";
                            am.weapons[j].projectile.pierce *= 5;
                            am.weapons[j].rate = 0;
                            am.weapons[j].emission = new ArcEmissionModel("aem_", 5, 0, 45, null, false);
                            for (int k = 0; k < am.weapons[j].projectile.behaviors.Length; k++) {
                                if (am.weapons[j].projectile.behaviors[k].Is<DamageModel>(out var dm)) {
                                    dm.immuneBloonProperties = BloonProperties.None;
                                    dm.damage *= 5;
                                }
                            }
                        }

                        am.range = 150;
                        beh[i] = am;
                    }

                    if (behavior.Is<AbilityModel>(out var abm)) {
                        for (int k = 0; k < abm.behaviors.Length; k++) {
                            if (abm.behaviors[k].Is<TurboModel>(out var tm)) {
                                tm.projectileDisplay = new("apm_", new("DaftPunkTurboProjectile"));
                                abm.behaviors[k] = tm;
                            }
                        }
                        beh[i] = abm;
                    }

                    beh[i] = behavior;
                }


                AttackModel stolentheattack = null;
                for (var i = 0; i < taipei.behaviors.Length; i++) {
                    var behavior = taipei.behaviors[i];
                    if (behavior.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var am = behavior.Cast<AttackModel>();

                        am.behaviors = am.behaviors.Remove(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>());

                        for (var j = 0; j < am.weapons.Length; j++) {
                            am.weapons[j].projectile.display.guidRef = "DaftPunkOrbit";
                            am.weapons[j].projectile.pierce *= 5;
                            am.weapons[j].emission = new ArcEmissionModel("aem_", 3, -15, 45, null, false);
                            for (int k = 0; k < am.weapons[j].projectile.behaviors.Length; k++) {
                                if (am.weapons[j].projectile.behaviors[k].Is<DamageModel>(out var dm)) {
                                    dm.immuneBloonProperties = BloonProperties.None;
                                    dm.damage *= 5;
                                }
                            }
                        }

                        stolentheattack = am;
                    }
                }

                AttackModel stolentheattack2 = null;
                for (var i = 0; i < hongkong.behaviors.Length; i++)
                    if (hongkong.behaviors[i].GetIl2CppType() == Il2CppType.Of<AttackModel>() && stolentheattack2 == null)
                        stolentheattack2 = hongkong.behaviors[i].Cast<AttackModel>();

                AttackModel stolentheattack3 = null;
                for (var i = 0; i < taiwan.behaviors.Length; i++)
                    if (taiwan.behaviors[i].GetIl2CppType() == Il2CppType.Of<AttackModel>())
                        stolentheattack3 = taiwan.behaviors[i].Cast<AttackModel>();

                daftPunk.behaviors = daftPunk.behaviors.Add(stolentheattack, stolentheattack2, stolentheattack3);
            };
            recurring += tts => { };
            onLeave += () => { time = -1; };
            assetsToRead.Add(new("DaftPunk", "1d4c4910301557641bf43f84c8ea4c77", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("DaftPunkProjectile", "b79ca21b322aba74cb09d8a1d4ee3dfe", RendererType.SPRITERENDERER));
            assetsToRead.Add(new("DaftPunkTurboProjectile", "b79ca21b322aba74cb09d8a1d4ee3dfe", RendererType.SPRITERENDERER));
            assetsToRead.Add(new("DaftPunkOrbit", "0a990350818c21e4bb5039a00c8a4977", RendererType.SPRITERENDERER));
        }
    }
}