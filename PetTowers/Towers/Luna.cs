using System.Linq;

namespace PetTowers.Towers;
internal class Luna : ITower<Luna> {

    public override void Initialize(ref GameModel gameModel) {
        LocalizationManager.Instance.textTable["Luna"] = "Luna";

        LocalizationManager.Instance.textTable["Laser Enriched Buckshots Description"] = "Take this, bloons!\n2x Damage, +5 Pierce, +10 Range";
        LocalizationManager.Instance.textTable["Laser Boomerangs Description"] = "Nananananananana Boomerang!\n2x Damage, +Bloon Tracking";
        LocalizationManager.Instance.textTable["Laser Beams! Description"] = "Faster and cooler than boomerangs.\n-Bloon Tracking, +5 Pierce, +10 Range, 2x Damage, 2x Speed";
        LocalizationManager.Instance.textTable["Larger Lasers Description"] = "They're like lasers, but larger.\n+5 Pierce, +10 Range, 2x Speed, 5x Damage";
        LocalizationManager.Instance.textTable["A Dark Secret Description"] = "I'm sorry you had to find out this way.\n+???";

        ResourceManager.RendererAssets.Add(("PetTowers.Resources.PetRoombaDiffuse.png", "Assets/Monkeys/Etienne/Graphics/Pets/Roomba/PetRoombaDisplay.prefab"));
        ResourceManager.RendererAssets.Add(("PetTowers.Resources.PetRoombaDiffuse4.png", "Assets/Monkeys/Etienne/Graphics/Pets/Roomba/PetRoombaDisplay.prefab"));
        ResourceManager.RendererAssets.Add(("PetTowers.Resources.PetRoombaDiffuse5.png", "Assets/Monkeys/Etienne/Graphics/Pets/Roomba/PetRoombaDisplay.prefab"));
    }

    public override TowerContainer GetTower(GameModel gameModel) {
        /*var tc = new TowerContainer();

        #region Details

        tc.shop = new ShopTowerDetailsModel("Luna", 99999, 5, 5, 5, -1, -1, null);

        #endregion

        #region Upgrades

        var T1U = new UpgradeModel("Luna_Buckshot", 800, 0, new("Assets/ResizedImages/UI/UpgradeIcons/DartlingMonkey/BuckshotUpgradeIcon.png"), 0, 0, 0, "", "Laser Enriched Buckshots");
        var T2U = new UpgradeModel("Luna_LaserRangs", 2750, 0, new("Assets/ResizedImages/UI/UpgradeIcons/DartlingMonkey/LaserShockUpgradeIcon.png"), 0, 1, 0, "", "Laser Boomerangs");
        var T3U = new UpgradeModel("Luna_LaserShots", 6675, 0, new("Assets/ResizedImages/UI/UpgradeIcons/SuperMonkey/LaserBlastUpgradeIcon.png"), 0, 2, 0, "", "Laser Beams!");
        var T4U = new UpgradeModel("Luna_LargerLasers", 11000, 0, new("Assets/ResizedImages/UI/UpgradeIcons/DartlingMonkey/LaserCannonUpgradeIcon.png"), 0, 3, 0, "", "Larger Lasers");
        var T5U = new UpgradeModel("Luna_DarkSecret", 33750, 0, new("Assets/ResizedImages/AchievementIcons/HiddenIcon.png"), 0, 4, 0, "", "A Dark Secret");

        tc.upgrades.AddRange(new[] { T1U, T2U, T3U, T4U, T5U });

        #endregion

        #region T0

        var wolfT0 = gameModel.towers[0].CloneCast();

        wolfT0.SetDisplay("PetTowers.Resources.PetRoombaDiffuse.png");
        wolfT0.SetIcons("PetTowers.Resources.EtienneRoombaPetIcon.png");
        wolfT0.name = wolfT0.GetNameMod(wolfT0.baseId = "Luna");
        wolfT0.towerSet = "Magic";
        wolfT0.upgrades = new UpgradePathModel[] { new UpgradePathModel("Luna_Buckshot", "Luna-100") };
        wolfT0.cost += 485;

        for (int i = 0; i < wolfT0.behaviors.Length; i++) {
            if (wolfT0.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.range = wolfT0.range;
                attack.weapons[0].emission = new RandomArcEmissionModel("RAEM_", 8, 0, 50, 20, 0, null);
                attack.weapons[0].projectile.display = "Assets/Monkeys/DartlingGunner/Graphics/Projectiles/LaserShockBuckshot.prefab";

                wolfT0.behaviors[i] = attack;
            }
        }

        wolfT0.behaviors = wolfT0.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true));

        tc.towers.Add(wolfT0);

        #endregion

        #region T1

        var wolfT1 = wolfT0.CloneCast();
        wolfT1.tiers = new[] { wolfT1.tier = 1, 0, 0 };
        wolfT1.name = wolfT1.GetNameMod(wolfT1.baseId);
        wolfT1.upgrades = new UpgradePathModel[] { new UpgradePathModel("Luna_LaserRangs", "Luna-200") };

        for (int i = 0; i < wolfT1.behaviors.Length; i++) {
            if (wolfT1.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.range = wolfT1.range += 10;
                attack.weapons[0].projectile.scale *= 1.25f;
                attack.weapons[0].projectile.pierce += 5;
                attack.weapons[0].Rate = 0.7f;
                attack.weapons[0].projectile.display = "Assets/Monkeys/DartlingGunner/Graphics/Projectiles/LaserShockBuckshotStronger.prefab";

                for (int j = 0; j < attack.weapons[0].projectile.behaviors.Length; j++) {
                    if (attack.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var damage)) {
                        damage.damage *= 2;
                        damage.CapDamage(damage.damage);
                        damage.immuneBloonProperties = BloonProperties.None;
                    }
                }
            }
        }

        tc.towers.Add(wolfT1);

        #endregion

        #region T2

        var wolfT2 = wolfT1.CloneCast();
        wolfT2.tiers = new[] { wolfT2.tier = 2, 0, 0 };
        wolfT2.name = wolfT2.GetNameMod(wolfT2.baseId);
        wolfT2.upgrades = new UpgradePathModel[] { new UpgradePathModel("Luna_LaserShots", "Luna-300") };

        for (int i = 0; i < wolfT2.behaviors.Length; i++) {
            if (wolfT2.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.weapons[0].Rate = 0.125f;
                attack.weapons[0].emission = new SingleEmissionModel("SEM_", null);
                attack.weapons[0].projectile.display = "Assets/Monkeys/SuperMonkey/Graphics/Projectiles/LaserBoomerangDarkChampion.prefab";
                attack.weapons[0].projectile.behaviors = attack.weapons[0].projectile.behaviors.Add(new TrackTargetModel("TTM", 75, true, true, 999999999999, false, 9999999999999, false, true));

                for (int j = 0; j < attack.weapons[0].projectile.behaviors.Length; j++) {
                    if (attack.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var damage)) {
                        damage.CapDamage(damage.damage *= 2);
                    }
                }
            }
        }

        tc.towers.Add(wolfT2);

        #endregion

        #region T3

        var wolfT3 = wolfT2.CloneCast();
        wolfT3.tiers = new[] { wolfT3.tier = 3, 0, 0 };
        wolfT3.name = wolfT3.GetNameMod(wolfT3.baseId);
        wolfT3.upgrades = new UpgradePathModel[] { new UpgradePathModel("Luna_LargerLasers", "Luna-400") };

        for (int i = 0; i < wolfT3.behaviors.Length; i++) {
            if (wolfT3.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.range = wolfT3.range += 10;
                attack.weapons[0].projectile.pierce += 5;
                attack.weapons[0].Rate = 0.33333f;
                attack.weapons[0].emission = new ArcEmissionModel("AEM_", 2, 0, 25, null, false);
                attack.weapons[0].projectile.display = "Assets/Monkeys/HeliPilot/Graphics/projectiles/ApacheLaserPulse.prefab";

                attack.weapons[0].projectile.behaviors = attack.weapons[0].projectile.behaviors.Remove(a => a.Is<TrackTargetModel>());

                for (int j = 0; j < attack.weapons[0].projectile.behaviors.Length; j++) {
                    if (attack.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var damage)) {
                        damage.damage *= 2;
                        damage.CapDamage(damage.damage);
                    }
                    if (attack.weapons[0].projectile.behaviors[j].Is<TravelStraitModel>(out var tsm)) {
                        tsm.Speed *= 2;
                        tsm.Lifespan /= 2;
                    }
                }
            }
        }

        tc.towers.Add(wolfT3);

        #endregion

        #region T4

        var wolfT4 = wolfT3.CloneCast();
        wolfT4.tiers = new[] { wolfT4.tier = 4, 0, 0 };
        wolfT4.name = wolfT4.GetNameMod(wolfT4.baseId);
        wolfT4.upgrades = new UpgradePathModel[] { new UpgradePathModel("Luna_DarkSecret", "Luna-500") };
        wolfT4.SetDisplay("PetTowers.Resources.PetRoombaDiffuse4.png");

        for (int i = 0; i < wolfT4.behaviors.Length; i++) {
            if (wolfT4.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.range = wolfT4.range += 10;
                attack.weapons[0].projectile.pierce += 5;
                attack.weapons[0].Rate = 0.25f;
                attack.weapons[0].emission = new ArcEmissionModel("AEM_", 3, 0, 45, null, false);
                attack.weapons[0].projectile.display = "Assets/Monkeys/DartlingGunner/Graphics/Projectiles/LaserCannonPowerful.prefab";

                for (int j = 0; j < attack.weapons[0].projectile.behaviors.Length; j++) {
                    if (attack.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var damage)) {
                        damage.damage *= 5;
                        damage.CapDamage(damage.damage);
                    }
                    if (attack.weapons[0].projectile.behaviors[j].Is<TravelStraitModel>(out var tsm)) {
                        tsm.Speed *= 2;
                        tsm.Lifespan /= 2;
                    }
                }
            }
        }

        wolfT4.behaviors = wolfT4.behaviors.Add(gameModel.towers.First(a => a.name.Equals("MonkeyBuccaneer-040")).behaviors.First(a => a.Is<AbilityModel>()));

        tc.towers.Add(wolfT4);

        #endregion

        #region T5

        var wolfT5 = wolfT4.CloneCast();
        wolfT5.tiers = new[] { wolfT5.tier = 5, 0, 0 };
        wolfT5.name = wolfT5.GetNameMod(wolfT5.baseId);
        wolfT5.upgrades = Array.Empty<UpgradePathModel>();
        wolfT5.SetDisplay("PetTowers.Resources.PetRoombaDiffuse5.png");

        for (int i = 0; i < wolfT5.behaviors.Length; i++) {
            if (wolfT5.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.range = wolfT5.range += 15;
                attack.weapons[0].projectile.pierce += 5;
                attack.weapons[0].Rate = 0.05f;
                attack.weapons[0].emission = new ArcEmissionModel("AEM_", 4, 0, 60, null, false);
                attack.weapons[0].projectile.display = "Assets/Monkeys/SuperMonkey/Graphics/Projectiles/BlastSunAvatar555.prefab";

                for (int j = 0; j < attack.weapons[0].projectile.behaviors.Length; j++) {
                    if (attack.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var damage)) {
                        damage.damage *= 14;
                        damage.CapDamage(damage.damage);
                    }
                    if (attack.weapons[0].projectile.behaviors[j].Is<TravelStraitModel>(out var tsm)) {
                        tsm.Speed /= 4;
                        tsm.Lifespan *= 4;
                    }
                }

                attack.weapons[0].projectile.behaviors = attack.weapons[0].projectile.behaviors.Add(new DamageModifierForTagModel("DMFTM_", "Moabs", 5, 5, false, true),
                    new DamageModifierForTagModel("DMFTM_", "Bad", 50, 50, false, true));
            }
        }

        wolfT5.behaviors = wolfT5.behaviors.Remove(a=>a.Is<AbilityModel>()).Add(new DisplayModel("DM_Place1", "Assets/Monkeys/Adora/Graphics/Effects/AdoraTransformFXDark.prefab", 0, new(0, 0, 0), 1, true, 0),
            new DisplayModel("DM_Place2", "Assets/Monkeys/Adora/Graphics/Effects/AdoraSunBeamUpgradeLvl20.prefab", 0, new(0, 0, 0), 1, true, 0), gameModel.towers.First(a => a.name.Equals("MonkeyBuccaneer-050")).behaviors.First(a => a.Is<AbilityModel>()));

        tc.towers.Add(wolfT5);

        #endregion
        */
        return default; //TODO fix
    }
}
