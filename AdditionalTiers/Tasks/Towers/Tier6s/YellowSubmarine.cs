namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class YellowSubmarine : TowerTask {
        public static TowerModel yellowSubmarine;
        private static int time = -1;
        public YellowSubmarine() {
            identifier = "Yellow Submarine";
            getTower = () => yellowSubmarine;
            baseTower = AddedTierName.YELLOWSUBMARINE;
            tower = AddedTierEnum.YELLOWSUBMARINE;
            requirements += tts => tts.tower.towerModel.baseId.Equals("MonkeySub") && tts.tower.towerModel.tiers[1] == 5;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(yellowSubmarine);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                yellowSubmarine = gm.towers.First(a => a.name.Contains("MonkeySub-052")).Clone()
                    .Cast<TowerModel>();

                yellowSubmarine.range = 150;
                yellowSubmarine.cost = 0;
                yellowSubmarine.name = "Yellow Submarine";
                yellowSubmarine.baseId = "MonkeySub";
                yellowSubmarine.display.guidRef = "YellowSubmarine";
                yellowSubmarine.dontDisplayUpgrades = true;
                yellowSubmarine.portrait = new("YellowSubmarineIcon");
                yellowSubmarine.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>())
                    .Cast<DisplayModel>().display.guidRef = "YellowSubmarine";
                var beh = yellowSubmarine.behaviors;
                ProjectileModel projectile = null;
                for (int i = 0; i < beh.Length; i++) {
                    if (beh[i].GetIl2CppType() == Il2CppType.Of<PreEmptiveStrikeLauncherModel>()) {
                        var am = beh[i].Cast<PreEmptiveStrikeLauncherModel>();
                        am.emissionModel = new ArcEmissionModel("ArcEmissionModel_", 3, 0, 72.5f, null, false).Cast<EmissionModel>();
                        am.projectileModel.behaviors = am.projectileModel.behaviors.Add(
                            new CreateProjectileOnContactModel("CreateProjectileOnContactModel_",
                                am.projectileModel.Clone().Cast<ProjectileModel>(),
                                am.emissionModel.Clone().Cast<EmissionModel>(), true, false, true));
                        projectile = am.projectileModel.Clone().Cast<ProjectileModel>();
                        beh[i] = am;
                    }

                    if (beh[i].GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var am = beh[i].Cast<AttackModel>();

                        am.range = 2000;
                        for (int j = 0; j < am.weapons.Length; j++) {
                            am.weapons[j].rate /= 5;
                            am.weapons[j].rateFrames /= 5;

                            am.weapons[j].projectile.behaviors = am.weapons[j].projectile.behaviors.Add(
                                new CreateProjectileOnContactModel("CreateProjectileOnContactModel_",
                                    am.weapons[j].projectile.Clone().Cast<ProjectileModel>(),
                                    am.weapons[j].emission.Clone().Cast<EmissionModel>(), false, false, false));

                            am.weapons[j].projectile.behaviors = am.weapons[j].projectile.behaviors.Add(
                                new CreateProjectileOnContactModel("CreateProjectileOnContactModel_", projectile,
                                    am.weapons[j].emission.Clone().Cast<EmissionModel>(), false, false, false));

                            for (int k = 0; k < am.weapons[j].projectile.behaviors.Length; k++) {
                                var bh = am.weapons[j].projectile.behaviors[k];

                                if (bh.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                                    var dm = bh.Cast<DamageModel>();

                                    dm.damage *= 3 * Globals.SixthTierDamageMulti;

                                    bh = dm;
                                }

                                am.weapons[j].projectile.behaviors[k] = bh;
                            }
                        }

                        beh[i] = am;
                    }

                    if (beh[i].GetIl2CppType() == Il2CppType.Of<AbilityModel>()) {
                        var am = beh[i].Cast<AbilityModel>();
                        am.icon = new("YellowSubmarineIcon");
                        var ab = am.behaviors;
                        for (int j = 0; j < ab.Length; j++) {
                            if (ab[j].GetIl2CppType() == Il2CppType.Of<ActivateAttackModel>()) {
                                var aam = ab[j].Cast<ActivateAttackModel>();

                                for (int k = 0; k < aam.attacks.Length; k++)
                                    for (int l = 0; l < aam.attacks[k].weapons.Length; l++) {
                                        aam.attacks[k].weapons[l].projectile.pierce *= 2000;
                                        aam.attacks[k].weapons[l].projectile.radius = 2000;
                                    }

                                ab[j] = aam;
                            }
                        }
                        beh[i] = am;
                    }
                }

                yellowSubmarine.behaviors = beh.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true));
            };
            recurring += tts => { };
            onLeave += () => { time = -1; };
            assetsToRead.Add(new("YellowSubmarine", "62ff4c3f34f9c3c4c9fce1ac3d122ee0", RendererType.SKINNEDMESHRENDERER));
        }
    }
}