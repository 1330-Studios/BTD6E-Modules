namespace PetTowers.Towers {
    public sealed class Kiwi : ITower<Kiwi> {

        public override void Initialize(ref GameModel gameModel) {
            LocalizationManager.Instance.textTable.Add("KiwiNinja", "Kiwi Ninja");

            LocalizationManager.Instance.textTable.Add("Advanced Aiming Shurikens Description", "Shurikens seek their targets.");
            LocalizationManager.Instance.textTable.Add("Exploding Shurikens Description", "Shurikens explode on contact.");
            LocalizationManager.Instance.textTable.Add("Quad Shots Description", "Throw out 4 shurikens at once!");
            LocalizationManager.Instance.textTable.Add("Grandmaster Kiwi Description", "This bird is one of legends, the one true savior against the bloons. Godspeed, kiwi, godspeed.");

        }

        public override TowerContainer GetTower(GameModel gameModel) {
            var tc = new TowerContainer();

            #region Details

            tc.shop = new ShopTowerDetailsModel("KiwiNinja", 99999, 5, 5, 5, -1, -1, null);

            #endregion

            #region Upgrades

            var T1U = new UpgradeModel("KiwiNinja_DoubleShot", 720, 0, new() { guidRef = "Assets/ResizedImages/UI/UpgradeIcons/NinjaMonkey/DoubleShotUpgradeIcon.png" }, 0, 0, 0, "", "Double Shot") { name = "KiwiNinja_DoubleShot", cost = 720, xpCost = 0, icon = new() { guidRef = "Assets/ResizedImages/UI/UpgradeIcons/NinjaMonkey/DoubleShotUpgradeIcon.png" }, path = 0, locked = 0, tier = 0, confirmation = "", localizedNameOverride = "Double Shot" };
            var T2U = new UpgradeModel("KiwiNinja_Seeking", 1500, 0, new() { guidRef = "Assets/ResizedImages/UI/UpgradeIcons/NinjaMonkey/SeekingShurikenUpgradeIcon.png" }, 0, 1, 0, "", "Advanced Aiming Shurikens") { name = "KiwiNinja_Seeking", cost = 1500, xpCost = 0, icon = new() { guidRef = "Assets/ResizedImages/UI/UpgradeIcons/NinjaMonkey/SeekingShurikenUpgradeIcon.png" }, path = 0, locked = 0, tier = 1, confirmation = "", localizedNameOverride = "Advanced Aiming Shurikens" };
            var T3U = new UpgradeModel("KiwiNinja_Explosive", 2650, 0, new() { guidRef = "Assets/ResizedImages/UI/UpgradeIcons/BombShooter/RecursiveClusterUpgradeIcon.png" }, 0, 2, 0, "", "Exploding Shurikens") { name = "KiwiNinja_Explosive", cost = 2650, xpCost = 0, icon = new() { guidRef = "Assets/ResizedImages/UI/UpgradeIcons/BombShooter/RecursiveClusterUpgradeIcon.png" }, path = 0, locked = 0, tier = 2, confirmation = "", localizedNameOverride = "Exploding Shurikens" };
            var T4U = new UpgradeModel("KiwiNinja_QuadShot", 7600, 0, new() { guidRef = "Assets/ResizedImages/UI/UpgradeIcons/HeliPilot/QuadDartsUpgradeIcon.png" }, 0, 3, 0, "", "Quad Shots") { name = "KiwiNinja_QuadShot", cost = 7600, xpCost = 0, icon = new() { guidRef = "Assets/ResizedImages/UI/UpgradeIcons/HeliPilot/QuadDartsUpgradeIcon.png" }, path = 0, locked = 0, tier = 3, confirmation = "", localizedNameOverride = "Quad Shots" };
            var T5U = new UpgradeModel("KiwiNinja_Grandmaster", 15600, 0, new() { guidRef = "Assets/ResizedImages/UI/UpgradeIcons/NinjaMonkey/GrandmasterNinjaUpgradeIcon.png" }, 0, 4, 0, "", "Grandmaster Kiwi") { name = "KiwiNinja_Grandmaster", cost = 15600, xpCost = 0, icon = new() { guidRef = "Assets/ResizedImages/UI/UpgradeIcons/NinjaMonkey/GrandmasterNinjaUpgradeIcon.png" }, path = 0, locked = 0, tier = 4, confirmation = "", localizedNameOverride = "Grandmaster Kiwi" };

            tc.upgrades.AddRange(new[] { T1U, T2U, T3U, T4U, T5U });

            #endregion

            #region T0

            var kiwiT0 = gameModel.towers[0].CloneCast();
            kiwiT0.SetDisplay("Assets/Monkeys/NinjaMonkey/Graphics/Pets/Kiwi/NinjaMonkeyKiwiDisplay.prefab");
            kiwiT0.SetIcons("Assets/ResizedImages/UI/TrophyStoreIcons/Tower/NinjaMonkeyPetKiwiIcon.png", true);

            kiwiT0.name = kiwiT0.GetNameMod("KiwiNinja");
            kiwiT0.baseId = "KiwiNinja";
            kiwiT0.towerSet = "Magic";
            kiwiT0.upgrades = new[] { new UpgradePathModel("KiwiNinja_DoubleShot", "KiwiNinja-100") };
            kiwiT0.cost += 250;

            for (int i = 0; i < kiwiT0.behaviors.Length; i++) {
                if (kiwiT0.behaviors[i].Is<AttackModel>(out var attack)) {
                    attack.weapons[0].emission = new RandomArcEmissionModel("RandomArcEmissionModel_", 1, 0, 1, 10, 0, null);
                    attack.weapons[0].projectile.display = new() { guidRef = "Assets/Monkeys/NinjaMonkey/Graphics/Projectiles/Shuriken.prefab" };
                }
            }

            kiwiT0.behaviors = kiwiT0.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true));

            tc.towers.Add(kiwiT0);

            #endregion

            #region T1

            var kiwiT1 = kiwiT0.CloneCast();
            kiwiT1.tier = 1;
            kiwiT1.tiers = new[] { 1, 0, 0 };
            kiwiT1.name = kiwiT1.GetNameMod("KiwiNinja");
            kiwiT1.upgrades = new UpgradePathModel[] { new UpgradePathModel("KiwiNinja_Seeking", "KiwiNinja-200") };

            for (int i = 0; i < kiwiT1.behaviors.Length; i++) {
                if (kiwiT1.behaviors[i].Is<AttackModel>(out var attack)) {
                    attack.weapons[0].emission.Cast<RandomArcEmissionModel>().count++;
                    attack.weapons[0].emission.Cast<RandomArcEmissionModel>().angle = 15;
                    attack.weapons[0].Rate *= 0.95f;

                    for (int j = 0; j < attack.weapons[0].projectile.behaviors.Length; j++) {
                        if (attack.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var dm)) {
                            dm.damage++;
                            dm.maxDamage = 4;
                        }
                    }
                }
            }

            tc.towers.Add(kiwiT1);

            #endregion

            #region T2

            var kiwiT2 = kiwiT1.CloneCast();
            kiwiT2.tier = 2;
            kiwiT2.tiers = new[] { 2, 0, 0 };
            kiwiT2.name = kiwiT2.GetNameMod("KiwiNinja");
            kiwiT2.upgrades = new UpgradePathModel[] { new UpgradePathModel("KiwiNinja_Explosive", "KiwiNinja-300") };
            kiwiT2.range += 35;

            for (int i = 0; i < kiwiT2.behaviors.Length; i++) {
                if (kiwiT2.behaviors[i].Is<AttackModel>(out var attack)) {
                    attack.range += 35;
                    attack.weapons[0].projectile.behaviors = attack.weapons[0].projectile.behaviors.Add(new RotateModel("RotateModel_", 720),
                        new TrackTargetWithinTimeModel("TrackTargetWithinTimeModel_", 9999999, true, false, 144, false, 9999999, false, 3.47999978f, true));
                }
            }

            tc.towers.Add(kiwiT2);

            #endregion

            #region T3

            var kiwiT3 = kiwiT2.CloneCast();
            kiwiT3.tier = 3;
            kiwiT3.tiers = new[] { 3, 0, 0 };
            kiwiT3.name = kiwiT3.GetNameMod("KiwiNinja");
            kiwiT3.upgrades = new UpgradePathModel[] { new UpgradePathModel("KiwiNinja_QuadShot", "KiwiNinja-400") };
            kiwiT3.range += 10;

            var bombShooterProj = gameModel.towers.First(a => a.name.StartsWith("BombShooter")).CloneCast().behaviors.First(a => a.Is<AttackModel>()).Cast<AttackModel>().weapons[0].projectile;

            var cpocm = bombShooterProj.behaviors.First(a => a.Is<CreateProjectileOnContactModel>()).Cast<CreateProjectileOnContactModel>();
            var ceocm = bombShooterProj.behaviors.First(a => a.Is<CreateEffectOnContactModel>()).Cast<CreateEffectOnContactModel>();

            for (int i = 0; i < kiwiT3.behaviors.Length; i++) {
                if (kiwiT3.behaviors[i].Is<AttackModel>(out var attack)) {
                    attack.range += 10;
                    attack.weapons[0].projectile.display = new() { guidRef = "Assets/Monkeys/NinjaMonkey/Graphics/Projectiles/SharpShuriken.prefab" };
                    attack.weapons[0].projectile.behaviors = attack.weapons[0].projectile.behaviors.Add(cpocm, ceocm);

                    for (int j = 0; j < attack.weapons[0].projectile.behaviors.Length; j++) {
                        if (attack.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var dm)) {
                            dm.immuneBloonProperties = BloonProperties.None;
                            dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                        }
                    }
                }
            }

            tc.towers.Add(kiwiT3);

            #endregion

            #region T4

            var kiwiT4 = kiwiT3.CloneCast();
            kiwiT4.tier = 4;
            kiwiT4.tiers = new[] { 4, 0, 0 };
            kiwiT4.name = kiwiT4.GetNameMod("KiwiNinja");
            kiwiT4.upgrades = new UpgradePathModel[] { new UpgradePathModel("KiwiNinja_Grandmaster", "KiwiNinja-500") };
            kiwiT4.range += 5;

            for (int i = 0; i < kiwiT4.behaviors.Length; i++) {
                if (kiwiT4.behaviors[i].Is<AttackModel>(out var attack)) {
                    attack.range += 5;
                    attack.weapons[0].Rate *= 0.75f;
                    attack.weapons[0].projectile.display = new() { guidRef =  "Assets/Monkeys/NinjaMonkey/Graphics/Projectiles/SharpShuriken.prefab" };
                    attack.weapons[0].emission.Cast<RandomArcEmissionModel>().count = 4;
                    attack.weapons[0].emission.Cast<RandomArcEmissionModel>().angle = 30;

                    for (int j = 0; j < attack.weapons[0].projectile.behaviors.Length; j++) {
                        if (attack.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var dm)) {
                            dm.damage = 12;
                            dm.maxDamage = 16;
                        }

                        if (attack.weapons[0].projectile.behaviors[j].Is<CreateProjectileOnContactModel>(out var create)) {
                            for (int k = 0; k < create.projectile.behaviors.Length; k++) {
                                if (create.projectile.behaviors[k].Is<DamageModel>(out var cdm)) {
                                    cdm.damage = 20;
                                    cdm.maxDamage = 20;
                                }
                            }

                            create.projectile.behaviors = create.projectile.behaviors.Add(new DamageModifierForTagModel("DamageModifierForTagModel_Moabs", "Moabs", 3, 5, false, true));
                        }
                    }
                }
            }

            tc.towers.Add(kiwiT4);

            #endregion

            #region T5

            var kiwiT5 = kiwiT4.CloneCast();
            kiwiT5.tier = 5;
            kiwiT5.tiers = new[] { 5, 0, 0 };
            kiwiT5.name = kiwiT5.GetNameMod("KiwiNinja");
            kiwiT5.upgrades = Array.Empty<UpgradePathModel>();
            kiwiT5.range += 15;

            for (int i = 0; i < kiwiT5.behaviors.Length; i++) {
                if (kiwiT5.behaviors[i].Is<AttackModel>(out var attack)) {
                    attack.range += 15;
                    attack.weapons[0].Rate *= 0.75f;
                    attack.weapons[0].projectile.display = new() { guidRef = "Assets/Monkeys/NinjaMonkey/Graphics/Projectiles/AscendedShadowShurikenLvl5.prefab" };
                    attack.weapons[0].projectile.radius *= 2;
                    attack.weapons[0].emission.Cast<RandomArcEmissionModel>().count = 24;
                    attack.weapons[0].emission.Cast<RandomArcEmissionModel>().angle = 360;

                    for (int j = 0; j < attack.weapons[0].projectile.behaviors.Length; j++) {
                        if (attack.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var dm)) {
                            dm.damage = 20;
                            dm.maxDamage = 25;
                        }

                        if (attack.weapons[0].projectile.behaviors[j].Is<TravelStraitModel>(out var tsm)) {
                            tsm.Lifespan *= 3;
                            tsm.Speed *= 1.5f;
                        }

                        if (attack.weapons[0].projectile.behaviors[j].Is<CreateProjectileOnContactModel>(out var create)) {
                            for (int k = 0; k < create.projectile.behaviors.Length; k++) {
                                if (create.projectile.behaviors[k].Is<DamageModel>(out var cdm)) {
                                    cdm.damage = 50;
                                    cdm.maxDamage = 50;
                                }
                            }

                            create.projectile.radius *= 3;
                            create.projectile.behaviors = create.projectile.behaviors.Add(new DamageModifierForTagModel("DamageModifierForTagModel_Bad", "Bad", 5, 50, false, true));
                            create.projectile.behaviors = create.projectile.behaviors.Add(new DamageModifierForTagModel("DamageModifierForTagModel_Ddt", "Ddt", 100, 10000, false, true));
                        }

                        if (attack.weapons[0].projectile.behaviors[j].Is<CreateEffectOnContactModel>(out var createEffect))
                            createEffect.effectModel.assetId = new() { guidRef = "Assets/Monkeys/MortarMonkey/Graphics/Effects/ShellShockExplosionXXL.prefab" };
                    }
                }
            }

            kiwiT5.behaviors = kiwiT5.behaviors.Add(new DisplayModel("DM_Place1", new() { guidRef = "Assets/Monkeys/Adora/Graphics/Effects/AdoraTransformFXDark.prefab" }, 0, new(0, 0, 0), 1, true, 0),
                new DisplayModel("DM_Place2", new() { guidRef = "Assets/Monkeys/Adora/Graphics/Effects/AdoraSunBeamUpgradeLvl20.prefab" }, 0, new(0, 0, 0), 1, true, 0));
            
            tc.towers.Add(kiwiT5);

            #endregion

            return tc;
        }
    }
}