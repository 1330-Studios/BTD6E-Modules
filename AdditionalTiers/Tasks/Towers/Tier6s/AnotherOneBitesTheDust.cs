namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class AnotherOneBitesTheDust : TowerTask {
        public static TowerModel aobtd;
        private static int time = -1;
        public AnotherOneBitesTheDust() {
            identifier = "Another One Bites the Dust";
            getTower = () => aobtd;
            baseTower = AddedTierName.ANOTHERONEBITESTHEDUST;
            tower = AddedTierEnum.ANOTHERONEBITESTHEDUST;
            requirements += tts => tts.tower.towerModel.baseId.Equals("SniperMonkey") && tts.tower.towerModel.tiers[0] == 5;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(aobtd);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                aobtd = gm.towers.First(a => a.name.Contains(AddedTierName.ANOTHERONEBITESTHEDUST)).CloneCast();

                aobtd.cost = 0;
                aobtd.name = "Another One Bites The Dust";
                aobtd.baseId = "SniperMonkey";
                aobtd.SetDisplay("AOBTD");
                aobtd.dontDisplayUpgrades = true;
                aobtd.portrait = new("AOBTDPortrait");
                aobtd.range += 25;

                var beh = aobtd.behaviors;

                for (var i = 0; i < beh.Length; i++) {
                    var behavior = beh[i];
                    if (behavior.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var am = behavior.Cast<AttackModel>();

                        am.behaviors = am.behaviors.Remove(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>());

                        for (var j = 0; j < am.weapons.Length; j++) {
                            am.weapons[j].projectile.display.guidRef = "AnotherOneBitesTheDustBomb";
                            am.weapons[j].projectile.pierce *= 5;
                            am.weapons[j].projectile.ignorePierceExhaustion = true;
                            am.weapons[j].rate *= 5f / 4f;

                            for (int k = 0; k < am.weapons[j].projectile.behaviors.Length; k++) {
                                if (am.weapons[j].projectile.behaviors[k].Is<DamageModel>(out var dmg2)) {
                                    dmg2.immuneBloonProperties = BloonProperties.None;
                                    dmg2.damage = 434521;
                                    am.weapons[j].projectile.behaviors[k] = dmg2;
                                }
                                if (am.weapons[j].projectile.behaviors[k].Is<SlowMaimMoabModel>(out var smmm)) {
                                    smmm.badDuration *= 5;
                                    smmm.ddtDuration *= 4;
                                    smmm.zomgDuration *= 4;
                                    smmm.bfbDuration *= 3;
                                    smmm.moabDuration *= 2;
                                    smmm.bloonPerHitDamageAddition = 500;
                                }
                                if (am.weapons[j].projectile.behaviors[k].Is<DamageModifierForTagModel>(out var dmftm)) {
                                    dmftm.mustIncludeAllTags = false;
                                    dmftm.tags = new string[] { "Ceramic", "Bad" };
                                    dmftm.damageMultiplier = 2;
                                    dmftm.applyOverMaxDamage = true;
                                }
                            }
                        }

                        am.range += 25;
                        beh[i] = am;
                    }

                    beh[i] = behavior;
                }

                aobtd.behaviors = beh.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true));
            };
            recurring += _ => { };
            onLeave += () => time = -1;
            assetsToRead.Add(new("AOBTD", "22ed7b557d493164495deaa7273d6651", RendererType.SKINNEDMESHRENDERER));
        }
    }
}