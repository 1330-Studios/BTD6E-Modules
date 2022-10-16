namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class HeyYa : TowerTask {
        public static TowerModel heyYa;
        private static int time = -1;
        public HeyYa() {
            identifier = "Hey Ya!";
            getTower = () => heyYa;
            baseTower = AddedTierName.HEYYA;
            tower = AddedTierEnum.HEYYA;
            requirements += tts => tts.tower.towerModel.baseId.Equals("BoomerangMonkey") && tts.tower.towerModel.tiers[2] == 5;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(heyYa);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new() { guidRef = "UpgradedText" }, 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                heyYa = gm.towers.First(a => a.name.Contains(AddedTierName.HEYYA)).CloneCast();

                heyYa.cost = 0;
                heyYa.name = "Hey Ya!";
                heyYa.baseId = "BoomerangMonkey";
                heyYa.SetDisplay("HeyYa");
                heyYa.SetIcons("AdditionalTiers.Resources.HeyYaPortrait.png");
                heyYa.dontDisplayUpgrades = true;
                heyYa.range += 35;

                var beh = heyYa.behaviors;

                for (var i = 0; i < beh.Length; i++) {
                    var behavior = beh[i];
                    if (behavior.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var am = behavior.Cast<AttackModel>();

                        am.behaviors = am.behaviors.Remove(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>());

                        for (var j = 0; j < am.weapons.Length; j++) {
                            am.weapons[j].projectile.pierce *= 5;
                            am.weapons[j].projectile.ignorePierceExhaustion = true;
                            am.weapons[j].emission = new ArcEmissionModel("ArcEmissionModel_", 15, 0, 60, null, false);
                            am.weapons[j].rate /= 7;

                            for (int k = 0; k < am.weapons[j].projectile.behaviors.Length; k++) {
                                if (am.weapons[j].projectile.behaviors[k].Is<PushBackModel>(out var pbm)) {
                                    pbm.pushAmount = 5;
                                    pbm.multiplierBFB = 1.2f;
                                    pbm.multiplierDDT = 3;
                                    pbm.multiplierZOMG = 1.1f;
                                }
                                if (am.weapons[j].projectile.behaviors[k].Is<DamageModifierForTagModel>(out var dmftm)) {
                                    dmftm.damageAddative = 29;
                                    dmftm.damageMultiplier = 2;
                                }
                                if (am.weapons[j].projectile.behaviors[k].Is<DamageModel>(out var dm)) {
                                    dm.damage = 400;
                                    dm.overrideDistributeBlocker = true;
                                    am.weapons[j].projectile.AddKnockbackModel();
                                }
                                if (am.weapons[j].projectile.behaviors[k].Is<CreateProjectileOnExhaustFractionModel>(out var cpoefm)) {
                                    for (int l = 0; l < cpoefm.projectile.behaviors.Length; l++) {
                                        if (cpoefm.projectile.behaviors[l].Is<DamageModel>(out var cpoefmdm)) {
                                            cpoefmdm.damage = 1000;
                                            cpoefmdm.overrideDistributeBlocker = true;
                                        }
                                    }
                                    cpoefm.projectile.AddKnockbackModel();
                                }
                            }
                        }

                        am.range += 35;
                        beh[i] = am;
                    }

                    beh[i] = behavior;
                }

                heyYa.behaviors = beh.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true));
            };
            recurring += _ => { };
            onLeave += () => time = -1;
            assetsToRead.Add(new("HeyYa", "c73f298fe9c1187449a45ef0a7ae5fc2", RendererType.SKINNEDMESHRENDERER));
        }
    }
}