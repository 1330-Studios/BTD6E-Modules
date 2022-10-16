

namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class GoldenApexPlasmaMaster : TowerTask {
        public static TowerModel gapm;
        private static int time = -1;
        public GoldenApexPlasmaMaster() {
            identifier = "The Gold Experience";
            getTower = () => gapm;
            baseTower = AddedTierName.THEGOLDEXPERIENCE;
            tower = AddedTierEnum.THEGOLDEXPERIENCE;
            requirements += tts => tts.tower.towerModel.baseId.Equals("DartMonkey") && tts.tower.towerModel.isParagon || tts.tower.towerModel.baseId.Equals("ParagonDartMonkey");
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(gapm);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                gapm = gm.towers.First(a => a.name.Contains(AddedTierName.THEGOLDEXPERIENCE)).CloneCast();

                gapm.range += 50;
                gapm.cost = 0;
                gapm.dontDisplayUpgrades = true;
                gapm.portrait = new("APMGoldIcon");
                gapm.name = "The Gold Experience";
                gapm.display.guidRef = "APMGold2";
                var beh = gapm.behaviors;
                for (int i = 0; i < beh.Length; i++) {
                    if (beh[i].Is<AttackModel>(out var attackModel)) {
                        attackModel.behaviors.First(a => a.Is<DisplayModel>(out _)).Cast<DisplayModel>().display.guidRef = "APMGold";
                        attackModel.range += 50;

                        var weaponModel = attackModel.weapons[0];

                        weaponModel.emission.Cast<ParallelEmissionModel>().count += 2;
                        weaponModel.projectile.display.guidRef = "APMUltraJuggernautGold";
                        weaponModel.rate /= 5;
                        weaponModel.rateFrames /= 5;
                        weaponModel.rate *= 2;
                        weaponModel.rateFrames *= 2;
                        var projBeh = weaponModel.projectile.behaviors;
                        for (int j = 0; j < projBeh.Length; j++) {
                            if (projBeh[j].Is<DamageModel>(out var damageModel)) {
                                damageModel.damage = 250;
                                damageModel.maxDamage = 5000;

                                projBeh[j] = damageModel;
                            }
                            if (projBeh[j].Is<TravelStraitModel>(out var travelStraitModel)) {
                                travelStraitModel.lifespan *= 5;
                                travelStraitModel.lifespanFrames *= 5;

                                projBeh[j] = travelStraitModel;
                            }
                            if (projBeh[j].Is<CreateProjectileOnExhaustFractionModel>(out var createProjectileOnExhaustFractionModel)) {
                                createProjectileOnExhaustFractionModel.projectile.display.guidRef = "APMJuggernautGold";

                                var cprojBeh = createProjectileOnExhaustFractionModel.projectile.behaviors;
                                for (int k = 0; k < cprojBeh.Length; k++) {
                                    if (cprojBeh[k].Is<DisplayModel>(out var cprojDisplayModel)) {
                                        cprojDisplayModel.display.guidRef = "APMJuggernautGold";
                                    }
                                    if (cprojBeh[k].Is<DamageModel>(out var cprojDamageModel)) {
                                        cprojDamageModel.damage *= 50;
                                        cprojDamageModel.maxDamage *= 50;
                                    }
                                }

                                createProjectileOnExhaustFractionModel.projectile.behaviors = cprojBeh.Add(new RotateModel("RotateModel", 180).Cast<Model>(),
                                new TrackTargetWithinTimeModel("TrackTargetWithinTimeModel_", 9999999, true, false, 120, false, 9999999, false, 8f, true).Cast<Model>(),
                                new DamageModifierForTagModel("DamageModifierForTagModel_", "Moabs", 2, 5, false, false));

                                createProjectileOnExhaustFractionModel.emission.Cast<ArcEmissionModel>().count = 5;

                                projBeh[j] = createProjectileOnExhaustFractionModel;
                            }
                            if (projBeh[j].Is<DisplayModel>(out var displayModel)) {
                                displayModel.display.guidRef = "APMUltraJuggernautGold";

                                projBeh[j] = displayModel;
                            }
                        }
                        weaponModel.projectile.behaviors = projBeh;
                        attackModel.weapons[0] = weaponModel;
                        beh[i] = attackModel;
                    }
                }
                gapm.behaviors = beh.Remove(a => a.Is<ParagonTowerModel>(out _));
            };
            recurring += tts => { };
            onLeave += () => { time = -1; };
            assetsToRead.Add(new("APMGold", "3227ca69bb352dc4c9c667712825b985", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("APMGold2", "f6aa9eed583ceef44b813e221abc5b70", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("APMUltraJuggernautGold", "655d4b5d0730e2949859a7fbeb3330fe", RendererType.MESHRENDERER));
            assetsToRead.Add(new("APMJuggernautGold", "72288b06ef230b644976478047ff0768", RendererType.MESHRENDERER));
        }
    }
}