namespace PetTowers.Towers;
internal class Mo : ITower<Mo> {

    public override void Initialize(ref GameModel gameModel) {

        LocalizationManager.Instance.textTable.Add("Much Faster Firing Description", "Fire faster. i think.");
        LocalizationManager.Instance.textTable.Add("Radar Services Description", "See stuff and things you wouldn't be able to normally.");
        LocalizationManager.Instance.textTable.Add("Bloontonium Dipped Bullets Description", "You know bloontonium from that one game? yeah we add that to this.");
        LocalizationManager.Instance.textTable.Add("Dog Support Air Description", "Who let the Grumman F-14 Tomcats out? Who, who, who, who, who?");
        LocalizationManager.Instance.textTable.Add("Bombs Bombs Bombs Description", "Bombs are cool. I like bombs. LEGALLY THIS IS A JOKE.");
    }

    public override TowerContainer GetTower(GameModel gameModel) {
        var tc = new TowerContainer();

        #region Details

        tc.shop = new ShopTowerDetailsModel("Mo", 99999, 5, 5, 5, -1, -1, null);

        #endregion

        #region Upgrades

        var T1U = new UpgradeModel("Mo_MuchFasterFiring", 890, 0, new() { guidRef = "Assets/ResizedImages/UI/UpgradeIcons/DartlingMonkey/FasterBarrelSpinUpgradeIcon.png" }, 0, 0, 0, "", "Much Faster Firing");
        var T2U = new UpgradeModel("Mo_RadarServices", 2150, 0, new() { guidRef = "Assets/ResizedImages/UI/UpgradeIcons/MonkeySub/SubmergeandSupportUpgradeIcon.png" }, 0, 1, 0, "", "Radar Services");
        var T3U = new UpgradeModel("Mo_BloontoniumDippedBullets", 3750, 0, new() { guidRef = "Assets/ResizedImages/UI/UpgradeIcons/MonkeySub/BloontoniumReactorUpgradeIcon.png" }, 0, 2, 0, "", "Bloontonium Dipped Bullets");
        var T4U = new UpgradeModel("Mo_DogSupportAir", 8880, 0, new() { guidRef = "Assets/ResizedImages/UI/UpgradeIcons/HeliPilot/ComancheDefenseUpgradeIcon.png" }, 0, 3, 0, "", "Dog Support Air");
        var T5U = new UpgradeModel("Mo_Bombs", 26400, 0, new() { guidRef = "Assets/ResizedImages/UI/UpgradeIcons/MonkeyAce/TsarBombaUpgradeIconAA.png" }, 0, 4, 0, "", "Bombs Bombs Bombs");

        tc.upgrades.AddRange(new[] { T1U, T2U, T3U, T4U, T5U });

        #endregion

        #region T0

        var kiwiT0 = gameModel.towers.First(a => a.baseId == "SniperMonkey").CloneCast();
        kiwiT0.SetDisplay("Assets/Monkeys/StrikerJones/Pets/GermanShepherd/GermanShepherd.prefab");
        kiwiT0.SetIcons("Assets/ResizedImages/UI/TrophyStoreIcons/Heroes/StrikerPetGermanShepherdIcon.png", true);

        kiwiT0.name = kiwiT0.GetNameMod("Mo");
        kiwiT0.baseId = "Mo";
        kiwiT0.towerSet = "Military";
        kiwiT0.upgrades = new[] { new UpgradePathModel("Mo_MuchFasterFiring", "Mo-100") };
        kiwiT0.cost += 215;
        kiwiT0.range = 25;

        for (int i = 0; i < kiwiT0.behaviors.Length; i++) {
            if (kiwiT0.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.sharedGridRange = 0;
                attack.range = kiwiT0.range;
            }
        }

        tc.towers.Add(kiwiT0);

        #endregion

        #region T1

        var kiwiT1 = kiwiT0.CloneCast();
        kiwiT1.tier = 1;
        kiwiT1.tiers = new[] { 1, 0, 0 };
        kiwiT1.name = kiwiT1.GetNameMod("Mo");
        kiwiT1.upgrades = new UpgradePathModel[] { new UpgradePathModel("Mo_RadarServices", "Mo-200") };

        for (int i = 0; i < kiwiT1.behaviors.Length; i++) {
            if (kiwiT1.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.range = kiwiT1.range;
                foreach (var weap in attack.weapons) {
                    weap.Rate /= 3;
                }
            }
        }

        kiwiT1.behaviors = kiwiT1.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true));

        tc.towers.Add(kiwiT1);

        #endregion

        #region T2

        var kiwiT2 = kiwiT1.CloneCast();
        kiwiT2.tier = 2;
        kiwiT2.tiers = new[] { 2, 0, 0 };
        kiwiT2.name = kiwiT2.GetNameMod("Mo");
        kiwiT2.upgrades = new UpgradePathModel[] { new UpgradePathModel("Mo_BloontoniumDippedBullets", "Mo-300") };
        kiwiT2.range = 50;

        for (int i = 0; i < kiwiT2.behaviors.Length; i++) {
            if (kiwiT2.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.range = kiwiT2.range;

                foreach (var weap in attack.weapons) {
                    weap.Rate /= 3;
                    weap.Rate *= 2;
                }
            }
        }

        var sub = gameModel.towers.First(a => a.name == "MonkeySub-300").CloneCast();

        kiwiT2.behaviors = kiwiT2.behaviors.Add(new DisplayModel("AOE", new() { guidRef = "dd1b6a5b8af6a4a4ab73864555d05c55" }, 0, new(0, 0, 0), 1, true, 0), sub.behaviors.First(a => a.Is<SubmergeModel>()).Cast<SubmergeModel>().submergeAttackModel);

        tc.towers.Add(kiwiT2);

        #endregion

        #region T3

        var kiwiT3 = kiwiT2.CloneCast();
        kiwiT3.tier = 3;
        kiwiT3.tiers = new[] { 3, 0, 0 };
        kiwiT3.name = kiwiT3.GetNameMod("Mo");
        kiwiT3.upgrades = new UpgradePathModel[] { new UpgradePathModel("Mo_DogSupportAir", "Mo-400") };
        kiwiT3.range += 15;

        for (int i = 0; i < kiwiT3.behaviors.Length; i++) {
            if (kiwiT3.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.range = kiwiT3.range;
                foreach (var weap in attack.weapons) {
                    weap.Rate /= 3;
                    weap.Rate *= 2;

                    foreach (var pb in weap.projectile.behaviors) {
                        if (pb.Is<DamageModel>(out var dm)) {
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            dm.damage *= 16;
                        }
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
        kiwiT4.name = kiwiT4.GetNameMod("Mo");
        kiwiT4.upgrades = new UpgradePathModel[] { new UpgradePathModel("Mo_Bombs", "Mo-500") };
        kiwiT4.range += 25;
        
        for (int i = 0; i < kiwiT4.behaviors.Length; i++) {
            if (kiwiT4.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.range = kiwiT4.range;

                foreach (var weap in attack.weapons) {
                    weap.Rate /= 3;
                    weap.Rate *= 2;

                    foreach (var pb in weap.projectile.behaviors) {
                        if (pb.Is<DamageModel>(out var dm)) {
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            dm.damage *= 2;
                        }
                    }
                }
            }
        }

        var sn = gameModel.towers.First(a => a.name == "SniperMonkey-050").CloneCast();

        kiwiT4.behaviors = kiwiT4.behaviors.Add(sn.behaviors.First(a => a.Is<AbilityModel>()));

        tc.towers.Add(kiwiT4);

        #endregion

        #region T5

        var kiwiT5 = kiwiT4.CloneCast();
        kiwiT5.tier = 5;
        kiwiT5.tiers = new[] { 5, 0, 0 };
        kiwiT5.name = kiwiT5.GetNameMod("Mo");
        kiwiT5.upgrades = Array.Empty<UpgradePathModel>();
        kiwiT5.range += 35;

        AttackModel t5Att = null;

        for (int i = 0; i < kiwiT5.behaviors.Length; i++) {
            if (kiwiT5.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.range = kiwiT5.range;

                foreach (var weap in attack.weapons) {
                    weap.Rate /= 3;
                    weap.Rate *= 2;

                    foreach (var pb in weap.projectile.behaviors) {
                        if (pb.Is<DamageModel>(out var dm)) {
                            dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            dm.damage *= 16;
                        }
                    }
                }
                t5Att = attack;
            }
        }

        var added = t5Att.CloneCast();

        added.targetProvider = new TargetLastModel("Last", true, false);

        var ace = gameModel.towers.First(a => a.name == "MonkeyAce-050").CloneCast();

        kiwiT5.behaviors = kiwiT5.behaviors.Add(ace.behaviors.First(a => a.Is<AbilityModel>()));

        kiwiT5.behaviors = kiwiT5.behaviors.Add(new DisplayModel("DM_Place1", new() { guidRef = "Assets/Monkeys/Adora/Graphics/Effects/AdoraTransformFXDark.prefab" }, 0, new(0, 0, 0), 1, true, 0),
            new DisplayModel("DM_Place2", new() { guidRef = "Assets/Monkeys/Adora/Graphics/Effects/AdoraSunBeamUpgradeLvl20.prefab" }, 0, new(0, 0, 0), 1, true, 0), added);

        tc.towers.Add(kiwiT5);

        #endregion

        return tc;
    }
}