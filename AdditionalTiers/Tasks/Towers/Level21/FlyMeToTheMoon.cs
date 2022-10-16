using Assets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;

namespace AdditionalTiers.Tasks.Towers.Level21 {
    public sealed class FlyMeToTheMoon : TowerTask {
        public static TowerModel whiteWedding;
        private static int time = -1;
        public FlyMeToTheMoon() {
            identifier = "Fly Me To The Moon";
            getTower = () => whiteWedding;
            baseTower = AddedTierName.FLYMETOTHEMOON;
            tower = AddedTierEnum.FLYMETOTHEMOON;
            requirements += tts => tts.tower.towerModel.baseId.Equals("CaptainChurchill") && tts.tower.towerModel.tier == 20;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(whiteWedding);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                whiteWedding = gm.towers.First(a => a.name.Contains(AddedTierName.FLYMETOTHEMOON)).CloneCast();

                var beijing = gm.towers.First(a => a.name.Contains("MortarMonkey-520")).CloneCast();

                whiteWedding.range = 150;
                whiteWedding.cost = 0;
                whiteWedding.name = "Fly Me To The Moon";
                whiteWedding.baseId = "CaptainChurchill";
                whiteWedding.SetDisplay("FMTTM");
                whiteWedding.dontDisplayUpgrades = true;
                var beh = whiteWedding.behaviors;
                Model[] attackBehaviors = null;
                TargetSupplierModel tsm = null;
                for (var i = 0; i < beh.Length; i++) {
                    var behavior = beh[i];
                    if (behavior.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var am = behavior.Cast<AttackModel>();

                        for (int j = 0; j < am.behaviors.Length; j++) {
                            if (am.behaviors[j].Is<DisplayModel>(out var dm)) {
                                if (dm.display.guidRef == "adbe729f5bdee5c46ab9f08f9b23d721") { // Rocket Launcher
                                    dm.display.guidRef = "FMTTM3";

                                    for (int k = 0; k < am.weapons[0].projectile.behaviors.Length; k++) {
                                        if (am.weapons[0].projectile.behaviors[k].Is<CreateProjectileOnExhaustPierceModel>(out var cpoepm)) {
                                            cpoepm.projectile.ignorePierceExhaustion = true;
                                        }
                                    }

                                    am.weapons[0].projectile.AddDamageModel(DamageModelCreation.Standard, 5000, true, BloonProperties.None);
                                    am.weapons[0].projectile.AddKnockbackModel();
                                    am.weapons[0].rate /= 5;
                                    am.weapons[0].rate *= 4;
                                    attackBehaviors = am.behaviors;
                                    tsm = am.targetProvider;
                                } else if (dm.display.guidRef == "8e64a1a21daed814db0cd08674da230a") { // MG
                                    dm.display.guidRef = "FMTTM2";
                                    am.weapons[0].projectile.ModifyDamageModel(new DamageChange { immuneBloonProperties = BloonProperties.None, set = true, damage = 250 });
                                    am.weapons[0].rate = 0;
                                }
                            }
                        }

                        am.range = 150;
                        beh[i] = am;
                    }

                    beh[i] = behavior;
                }

                AttackModel stolentheattack = null;
                for (var i = 0; i < beijing.behaviors.Length; i++) {
                    var behavior = beijing.behaviors[i];
                    if (behavior.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var am = behavior.Cast<AttackModel>();
                        am.behaviors = attackBehaviors;
                        am.behaviors = am.behaviors.Remove(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>());
                        am.targetProvider = tsm;

                        am.weapons[0].projectile.AddDamageModel(DamageModelCreation.Standard, 5000, true, BloonProperties.None);

                        stolentheattack = am;
                    }
                }

                whiteWedding.behaviors = beh.Add(new OverrideCamoDetectionModel("OCDM_", true), stolentheattack);
            };
            recurring += tts => { };
            onLeave += () => { time = -1; };
            assetsToRead.Add(new("FMTTM", "bc31d2ab6cc07d749b4c3563df0b2806", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("FMTTM2", "6b914f46919703646b9a2aa931a2facd", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("FMTTM3", "f3fa63eda489f6148952c806811236d1", RendererType.SKINNEDMESHRENDERER));
        }
    }
}