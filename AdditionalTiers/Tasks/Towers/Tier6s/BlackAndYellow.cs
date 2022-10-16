namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class BlackAndYellow : TowerTask {
        public static TowerModel blackYellow;
        private static int time = -1;
        public BlackAndYellow() {
            identifier = "Black & Yellow";
            getTower = () => blackYellow;
            baseTower = AddedTierName.BLACKANDYELLOW;
            tower = AddedTierEnum.BLACKANDYELLOW;
            requirements += tts => tts.tower.towerModel.baseId.Equals("HeliPilot") && tts.tower.towerModel.tiers[0] == 5;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(blackYellow);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                blackYellow = gm.towers.First(a => a.name.Contains(AddedTierName.BLACKANDYELLOW)).CloneCast();

                blackYellow.cost = 0;
                blackYellow.name = "Black & Yellow";
                blackYellow.baseId = "HeliPilot";
                blackYellow.dontDisplayUpgrades = true;
                blackYellow.portrait = new("BlackYellowPortrait");
                blackYellow.SetDisplay("BlackYellowBase");
                var beh = blackYellow.behaviors;
                for (var i = 0; i < beh.Length; i++) {
                    var behavior = beh[i];
                    if (behavior.Is<AttackModel>(out var am)) {

                        for (var j = 0; j < am.weapons.Length; j++) { //coding like yanderedev
                            if (am.weapons[j].projectile.display == null) {
                                am.weapons[j].rate = 0;

                                am.weapons[j].projectile.ModifyDamageModel(new DamageChange() { set = true, damage = 50, immuneBloonProperties = BloonProperties.None });
                            } else
                            if (am.weapons[j].projectile.display.guidRef == "805de73c9d751734cb4cc61ffc9ed67a") {
                                am.weapons[j].projectile.display.guidRef = "BlackYellowLaser";
                                am.weapons[j].rate /= 3;
                                am.weapons[j].rate *= 2;

                                am.weapons[j].projectile.ModifyDamageModel(new DamageChange() { set = true, damage = 35, immuneBloonProperties = BloonProperties.None });
                            } else
                            if (am.weapons[j].projectile.display.guidRef == "ffa3be03eb9b2d24da77aeff09693b00") {
                                am.weapons[j].projectile.display.guidRef = "BlackYellowBullet";
                                am.weapons[j].rate = 0;

                                am.weapons[j].projectile.ModifyDamageModel(new DamageChange() { set = true, damage = 25, immuneBloonProperties = BloonProperties.None });
                                am.weapons[j].projectile.AddKnockbackModel();
                                am.weapons[j].projectile.behaviors = am.weapons[j].projectile.behaviors.Add(new DamageModifierForTagModel("DamageModifierForTagModel_", "Moabs", 2, 5, false, false));
                            } else
                            if (am.weapons[j].projectile.display.guidRef == "299258962393e444a990a66c9aa9b619") {
                                am.weapons[j].projectile.display.guidRef = "BlackYellowMissile";
                                am.weapons[j].projectile.behaviors = am.weapons[j].projectile.behaviors.Add(new DamageModifierForTagModel("DamageModifierForTagModel_", "Moabs", 10, 5, false, false));

                                var projectile = gm.towers.FirstOrDefault(a => a.name.Equals("BombShooter-300")).behaviors.First((Model a) => a.GetIl2CppType() == Il2CppType.Of<AttackModel>()).Clone().Cast<AttackModel>().weapons[0].projectile;
                                var cpocm = projectile.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<CreateProjectileOnContactModel>()).Cast<CreateProjectileOnContactModel>();
                                cpocm.projectile = am.weapons[j].projectile.CloneCast();
                                cpocm.emission = new ArcEmissionModel("ArcEmissionModel_", 8, 15, 360, null, false).Cast<EmissionModel>();
                                var csopcm = projectile.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<CreateSoundOnProjectileCollisionModel>());
                                var ceocm = projectile.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<CreateEffectOnContactModel>());
                                am.weapons[j].projectile.behaviors = am.weapons[0].projectile.behaviors.Add(cpocm, csopcm, ceocm);
                            }
                        }

                        am.range = 150;
                        beh[i] = am;
                    }
                    if (behavior.Is<AirUnitModel>(out var aum)) {
                        aum.display.guidRef = "BlackYellowAir";

                        beh[i] = aum;
                    }

                    beh[i] = behavior;
                }

                blackYellow.behaviors = beh;
            };
            recurring += tts => { };
            onLeave += () => { time = -1; };
            assetsToRead.Add(new("BlackYellowAir", "0cfd0fb38e23b89488959e32455d5bc5", RendererType.SKINNEDANDUNSKINNEDMESHRENDERER));
            assetsToRead.Add(new("BlackYellowBase", "6cbc51704a6befc40a2fe05a4c7a41b5", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("BlackYellowBullet", "ffa3be03eb9b2d24da77aeff09693b00", RendererType.SPRITERENDERER));
            assetsToRead.Add(new("BlackYellowLaser", "805de73c9d751734cb4cc61ffc9ed67a", RendererType.SPRITERENDERER));
            assetsToRead.Add(new("BlackYellowMissile", "299258962393e444a990a66c9aa9b619", RendererType.SPRITERENDERER));
        }
    }
}