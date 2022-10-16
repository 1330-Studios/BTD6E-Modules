

namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class BigJuggus : TowerTask {
        public static TowerModel bigJuggus;
        private static int time = -1;
        public BigJuggus() {
            identifier = "Big Juggus";
            getTower = () => bigJuggus;
            baseTower = AddedTierName.BIGJUGGUS;
            tower = AddedTierEnum.BIGJUGGUS;
            requirements += tts => tts.tower.towerModel.baseId.Equals("DartMonkey") && tts.tower.towerModel.tiers[0] == 5;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(bigJuggus);
                tts.TAdd(scale1: .25f);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                bigJuggus = gm.towers.First(a => a.name.Contains("DartMonkey-520")).Clone().Cast<TowerModel>();

                bigJuggus.range = 150;
                bigJuggus.cost = 0;
                bigJuggus.name = "Big Juggus";
                bigJuggus.baseId = "DartMonkey";
                bigJuggus.display.guidRef = "BigJuggus";
                bigJuggus.dontDisplayUpgrades = true;
                bigJuggus.portrait = new("BigJuggusIcon");
                bigJuggus.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>())
                    .Cast<DisplayModel>().display.guidRef = "BigJuggus";
                bigJuggus.mods = bigJuggus.mods.Add(new ApplyModModel("ApplyModModel_", "BigJuggus", ""));
                var beh = bigJuggus.behaviors;
                ProjectileModel proj = null;
                AttackModel attack = null;
                var speed = 0f;
                for (var i = 0; i < beh.Length; i++) {
                    var behavior = beh[i];
                    if (behavior.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var am = behavior.Cast<AttackModel>();

                        for (var j = 0; j < am.weapons.Length; j++) {
                            proj = am.weapons[j].projectile.Clone().Cast<ProjectileModel>();
                            am.weapons[j].projectile.display.guidRef = "BigJuggusProj";
                            am.weapons[j].projectile.pierce *= 5;
                            am.weapons[j].projectile.scale *= 1.125f;
                            am.weapons[j].emission = new ArcEmissionModel("ArcEmissionModel_", 3, 0, 150, null, false).Cast<EmissionModel>();
                            am.weapons[j].rate /= 2;
                            am.weapons[j].rateFrames /= 2;
                            speed = am.weapons[j].rate;
                            for (var k = 0; k < am.weapons[j].projectile.behaviors.Length; k++) {
                                var b = am.weapons[j].projectile.behaviors[k];
                                if (b.GetIl2CppType() == Il2CppType.Of<CreateProjectileOnExhaustFractionModel>()) {
                                    var p = b.Cast<CreateProjectileOnExhaustFractionModel>();
                                    var em = p.emission.Cast<ArcEmissionModel>();
                                    em.Count = 4;
                                    p.emission = em;
                                    p.projectile = proj;
                                    am.weapons[j].projectile.behaviors[k] = p;
                                }

                                if (b.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                                    var p = b.Cast<DamageModel>();
                                    p.damage *= 3; ;
                                    am.weapons[j].projectile.behaviors[k] = p;
                                }
                            }
                        }

                        am.range = 150;
                        beh[i] = am;
                        attack = am.Clone().Cast<AttackModel>();
                    }

                    beh[i] = behavior;
                }

                for (var j = 0; j < attack.weapons.Length; j++) {
                    attack.weapons[j].emission = new SingleEmissionModel("SingleEmissionModel_", null).Cast<EmissionModel>();
                    attack.weapons[j].projectile.scale *= 2;
                    attack.weapons[j].projectile.ignorePierceExhaustion = true;
                    for (var i = 0; i < attack.weapons[j].projectile.behaviors.Length; i++) {
                        var b = attack.weapons[j].projectile.behaviors[i];
                        if (b.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var p = b.Cast<DamageModel>();
                            p.damage = 25000 * Globals.SixthTierDamageMulti;
                            attack.weapons[j].projectile.behaviors[i] = p;
                        }

                        if (b.GetIl2CppType() == Il2CppType.Of<TravelStraitModel>()) {
                            var a = b.Cast<TravelStraitModel>();
                            a.lifespan *= 5;
                            a.speed /= 4;
                            a.speedFrames /= 4;
                            attack.weapons[j].projectile.behaviors[i] = a;
                        }
                    }
                }

                var ability = new AbilityModel("AbilityModel_", "Meteor Strike",
                    "Launches a meteor sized projectile to the bloons", 1, 0, new("BigJuggusIcon"), 80,
                    new(new[] { new ActivateAttackModel("ActivateAttackModel_", 1, false, new(new[] { attack }), true, false, false, false, true) }),
                    false, false, "BigJuggus", 0, 0, 2, false, false);
                var mfc = new MonkeyFanClubModel("MonkeyFanClubModel_", 15, 5, 50, 160, speed, BloonProperties.None,
                    new("SmallerJuggusProj"), new("SmallerJuggus"), new(""), new("SmallerJuggus"), new("9fdef1ee822fdae49a3ed3ba322d17d9"),
                    new("ef97abc30fa87e54f9f6ab4813f0fec9"), new("d25a04e9666f523488a19690b2806f64"),
                    new("EE_", new("d25a04e9666f523488a19690b2806f64"), 1, 0.8f, false, false, false, false, false,
                        false, false), 60, 6, 250, null, 0, 0, 0, "BigJuggus");
                var ability2 = new AbilityModel("AbilityModel_2", "Juggus Conversion",
                    "Turns nearby monkeys into Smaller Juggus", 1, 0, new("SmallerJuggusIcon"), 45,
                    new(new[] { mfc }), false, false, "BigJuggus", 0, 0, 5, false, false);

                bigJuggus.behaviors = beh.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true).Cast<Model>(), ability.Cast<Model>(), ability2.Cast<Model>());
            };
            recurring += tts => { };
            onLeave += () => { time = -1; };
            assetsToRead.Add(new("BigJuggus", "b194c58ed09f1aa468e935b453c6843c", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("BigJuggusProj", "c4b8e7aa3e07d764fb9c3c773ceec2ab", RendererType.MESHRENDERER));
            assetsToRead.Add(new("SmallerJuggus", "b194c58ed09f1aa468e935b453c6843c", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("SmallerJuggusProj", "c4b8e7aa3e07d764fb9c3c773ceec2ab", RendererType.MESHRENDERER));
        }
    }
}