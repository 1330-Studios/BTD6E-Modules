
using Assets.Scripts.Models.Audio;
using Assets.Scripts.Models.Effects;

namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class UnderWorld : TowerTask {
        public static TowerModel underWorld;
        private static int time = -1;
        public UnderWorld() {
            identifier = "Under World";
            getTower = () => underWorld;
            baseTower = AddedTierName.UNDERWORLD;
            tower = AddedTierEnum.UNDERWORLD;
            requirements += tts => tts.tower.towerModel.baseId.Equals("WizardMonkey") && tts.tower.towerModel.tiers[0] == 5;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(underWorld);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                underWorld = gm.towers.First(a => a.name.Contains("WizardMonkey-502")).Clone()
                    .Cast<TowerModel>();

                underWorld.range = 130;
                underWorld.cost = 0;
                underWorld.name = "Under World";
                underWorld.baseId = "WizardMonkey";
                underWorld.display.guidRef = "Underworld";
                underWorld.dontDisplayUpgrades = true;
                underWorld.portrait = new("UnderworldIcon");
                underWorld.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display.guidRef = "Underworld";

                SpriteReference sr = new("UnderworldAbility");
                CreateEffectOnAbilityModel effectOnAbility = new("CreateEffectOnAbilityModel_Ability_", new("EffectModel_", new("14b648f9ccb204d4eb8a79cc203c670f"), 1.0f, 50.0f, false, false, true, false, false, false, false), false, false, true, false, false);
                CreateEffectOnAbilityModel effectOnAbility2 = new("CreateEffectOnAbilityModel_Ability_", new("EffectModel_", new("587c28ba86e6282419d8a89066ba4fd0"), 1.0f, 15.0f, false, false, true, false, false, false, false), false, false, true, false, false);
                CreateEffectOnAbilityModel effectOnAbility3 = new("CreateEffectOnAbilityModel_Ability_", new("EffectModel_", new("617f1117bf0d35d4b9e59c80128982a6"), 1.0f, 60.0f, false, false, true, false, false, false, false), false, false, true, false, false);
                CreateSoundOnAbilityModel soundOnAbility = new("CreateSoundOnAbilityModel_Ability", new("SoundModel_ActivatedUnholyGatesSound", new("c72781a0643d41c4b976110d1516fabc")), new("",new("")), new("", new("")));
                EffectModel effectModel = new("EffectModel_", new("617f1117bf0d35d4b9e59c80128982a6"), 1.0f, 60.0f, false, false, true, false, false, false, false);
                DamageUpModel damageUpModel = new("DamageUpModel_", 899, 5000, new("assetPathIG", new("4fb0baaa656410f4ba1f2fd07b37eda4")));
                SwitchDisplayModel switchDisplayModel = new("SwitchDisplayModel_", 14.7f, true, new("UnderworldInverted"), effectModel, false);

                var UnholyGatesModel = new AbilityModel("AbilityModel_Ability", "Unholy Gates", "The wizard summons unholy creatures to help him out against the bloons.", 1, -59, sr, 40.0f, new(new Model[] { effectOnAbility, soundOnAbility, damageUpModel, effectOnAbility2, switchDisplayModel }), false, false, "", 0.0f, 0, 99999, true, false);
                UnholyGatesModel.icon = new("UnderworldAbility");
                var beh = underWorld.behaviors;
                for (var i = 0; i < beh.Length; i++)
                    if (beh[i].GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var att = beh[i].Cast<AttackModel>();
                        att.range = 130;
                        for (var j = 0; j < att.weapons.Length; j++)
                            if (att.weapons[j] != null && att.weapons[j].projectile != null &&
                                att.weapons[j].projectile.display != null &&
                                !att.weapons[j].projectile.display.guidRef.Trim().Equals("")) {
                                if (att.weapons[j].projectile.id.Contains("DB")) {
                                    att.weapons[j].projectile.display.guidRef = "ac751aad50cdbef41a9e1f6bbac5349a";
                                    att.weapons[j].projectile.pierce = 155550;
                                    var pbhav = att.weapons[j].projectile.behaviors;
                                    for (var k = 0; k < pbhav.Length; k++)
                                        if (pbhav[k].GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                                            var pb = pbhav[k].Cast<DamageModel>();
                                            pb.damage *= 10 * Globals.SixthTierDamageMulti;
                                            pbhav[k] = pb;
                                        }

                                    att.weapons[j].projectile.behaviors = pbhav;
                                }

                                if (att.weapons[j].projectile.id.Contains("Base")) {
                                    att.weapons[j].rate /= 2;
                                    att.weapons[j].projectile.pierce = 155550;
                                    att.weapons[j].projectile.display.guidRef = "ffa3be03eb9b2d24da77aeff09693b00";
                                    var pbhav = att.weapons[j].projectile.behaviors;
                                    for (var k = 0; k < pbhav.Length; k++) {
                                        if (pbhav[k].GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                                            var pb = pbhav[k].Cast<DamageModel>();
                                            pb.damage *= 13 * Globals.SixthTierDamageMulti;
                                            pbhav[k] = pb;
                                        }
                                        if (pbhav[k].GetIl2CppType() == Il2CppType.Of<TravelStraitModel>()) {
                                            var pb = pbhav[k].Cast<TravelStraitModel>();
                                            pb.lifespan *= 3;
                                            pbhav[k] = pb;
                                        }
                                    }

                                    att.weapons[j].projectile.behaviors = pbhav;
                                }
                            }
                    }

                underWorld.behaviors = beh.Add(UnholyGatesModel);
            };
            recurring += tts => { };
            onLeave += () => { time = -1; };
            assetsToRead.Add(new("Underworld", "8ccff862eab169c4884bac8bbd878529", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("UnderworldInverted", "8ccff862eab169c4884bac8bbd878529", RendererType.SKINNEDMESHRENDERER));
        }
    }
}