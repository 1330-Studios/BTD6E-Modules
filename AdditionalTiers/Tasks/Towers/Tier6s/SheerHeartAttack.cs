namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class SheerHeartAttack : TowerTask {
        public static TowerModel sheerHeartAttack;
        private static int time = -1;
        public SheerHeartAttack() {
            identifier = "Sheer Heart Attack";
            getTower = () => sheerHeartAttack;
            baseTower = AddedTierName.SHEERHEARTATTACK;
            tower = AddedTierEnum.SHEERHEARTATTACK;
            requirements += tts => tts.tower.towerModel.baseId.Equals("BombShooter") && tts.tower.towerModel.tiers[2] == 5;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(sheerHeartAttack);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                sheerHeartAttack = gm.towers.First(a => a.name.Contains(AddedTierName.SHEERHEARTATTACK)).CloneCast();

                sheerHeartAttack.cost = 0;
                sheerHeartAttack.name = "Sheer Heart Attack";
                sheerHeartAttack.baseId = "BombShooter";
                sheerHeartAttack.SetDisplay("SheerHeartAttack");
                sheerHeartAttack.dontDisplayUpgrades = true;
                sheerHeartAttack.portrait = new("SheerHeartAttackPortrait");
                sheerHeartAttack.range += 25;

                var beh = sheerHeartAttack.behaviors;

                for (var i = 0; i < beh.Length; i++) {
                    var behavior = beh[i];
                    if (behavior.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var am = behavior.Cast<AttackModel>();

                        am.behaviors = am.behaviors.Remove(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>());

                        for (var j = 0; j < am.weapons.Length; j++) {
                            am.weapons[j].projectile.display.guidRef = "SheerHeartAttackBomb";
                            am.weapons[j].projectile.pierce *= 5;
                            am.weapons[j].projectile.ignorePierceExhaustion = true;
                            am.weapons[j].emission = new ParallelEmissionModel("ParallelEmissionModel_", 12, 50, 0, false, null);
                            am.weapons[j].rate /= 3;

                            for (int k = 0; k < am.weapons[j].projectile.behaviors.Length; k++) {
                                if (am.weapons[j].projectile.behaviors[k].Is<TravelStraitModel>(out var tsm))
                                    tsm.Lifespan *= 1.2f;
                                if (am.weapons[j].projectile.behaviors[k].Is<CreateProjectileOnExhaustFractionModel>(out var cpoefm)) {
                                    for (int l = 0; l < cpoefm.projectile.behaviors.Length; l++) {
                                        if (cpoefm.projectile.behaviors[l].Is<DamageModel>(out var dmg)) {
                                            dmg.immuneBloonProperties = BloonProperties.None;
                                            dmg.damage = 35422;
                                            cpoefm.projectile.behaviors[l] = dmg;
                                        }
                                        if (cpoefm.projectile.behaviors[l].Is<CreateProjectileOnExhaustFractionModel>(out var cpoefm2)) {
                                            for (int m = 0; m < cpoefm.projectile.behaviors.Length; m++) {
                                                if (cpoefm.projectile.behaviors[l].Is<DamageModel>(out var dmg2)) {
                                                    dmg2.immuneBloonProperties = BloonProperties.None;
                                                    dmg2.damage = 35422;
                                                    cpoefm.projectile.behaviors[l] = dmg2;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        am.range += 25;
                        beh[i] = am;
                    }

                    beh[i] = behavior;
                }

                sheerHeartAttack.behaviors = beh.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true));
            };
            recurring += _ => { };
            onLeave += () => time = -1;
            assetsToRead.Add(new("SheerHeartAttack", "acdf7d2e12bac2544b77df77ab1ae203", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("SheerHeartAttackBomb", "4bce3e766a25dc74085e2427d1db6160", RendererType.SPRITERENDERER));
        }
    }
}