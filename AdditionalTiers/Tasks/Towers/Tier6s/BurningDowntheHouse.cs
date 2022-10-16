namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class BurningDowntheHouse : TowerTask {
        public static TowerModel burningDownTheHouse;
        private static int time = -1;
        public BurningDowntheHouse() {
            identifier = "Burning Down the House";
            getTower = () => burningDownTheHouse;
            baseTower = AddedTierName.BURNINGDOWNTHEHOUSE;
            tower = AddedTierEnum.BURNINGDOWNTHEHOUSE;
            requirements += tts => tts.tower.towerModel.baseId.Equals("TackShooter") && tts.tower.towerModel.tiers[2] == 5;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(burningDownTheHouse);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                burningDownTheHouse = gm.towers.First(a => a.name.Contains(AddedTierName.BURNINGDOWNTHEHOUSE)).CloneCast();

                burningDownTheHouse.cost = 0;
                burningDownTheHouse.name = "Burning Down the House";
                burningDownTheHouse.baseId = "TackShooter";
                burningDownTheHouse.SetDisplay("BurningDownTheHouse");
                burningDownTheHouse.dontDisplayUpgrades = true;
                burningDownTheHouse.portrait = new("BurningDownTheHousePortrait");
                burningDownTheHouse.range += 50;

                var beh = burningDownTheHouse.behaviors;

                for (var i = 0; i < beh.Length; i++) {
                    var behavior = beh[i];
                    if (behavior.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var am = behavior.Cast<AttackModel>();

                        am.behaviors = am.behaviors.Remove(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>());

                        for (var j = 0; j < am.weapons.Length; j++) {
                            am.weapons[j].projectile.display.guidRef = "BurningDownTheHouseProj";
                            am.weapons[j].projectile.pierce *= 5;
                            am.weapons[j].projectile.scale *= 1.25f;
                            am.weapons[j].projectile.pierce = 9999999;
                            am.weapons[j].projectile.ignorePierceExhaustion = true;
                            am.weapons[j].rate = 0;
                            am.weapons[j].rateFrames = 0;
                            am.weapons[j].rate = 0.02f;
                            am.weapons[j].emission = new ArcEmissionModel("ArcEmissionModel", 50, 0, 360, null, false);

                            for (int k = 0; k < am.weapons[j].projectile.behaviors.Length; k++) {
                                if (am.weapons[j].projectile.behaviors[k].Is<TravelStraitModel>(out var tsm))
                                    tsm.Lifespan = 0.45f;
                                if (am.weapons[j].projectile.behaviors[k].Is<DamageModel>(out var dmg)) {
                                    dmg.immuneBloonProperties = BloonProperties.None;
                                    dmg.damage = 45;
                                }
                            }
                            am.range += 50;
                        }

                        beh[i] = am;
                    }

                    beh[i] = behavior;
                }

                burningDownTheHouse.behaviors = beh.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true));
            };
            recurring += _ => { };
            onLeave += () => time = -1;
            assetsToRead.Add(new("BurningDownTheHouse", "31b430825256d3146b420d5ad89e21fc", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("BurningDownTheHouseProj", "26e9c99e89180fd468cb47d76c7536b6", RendererType.SPRITERENDERER));
        }
    }
}