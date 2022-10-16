namespace PetTowers.Towers;
internal class GhostWolf : ITower<GhostWolf> {

    public override void Initialize(ref GameModel gameModel) {
        LocalizationManager.Instance.textTable["GhostWolf"] = "Ghost Wolf";

        LocalizationManager.Instance.textTable["Ghostly Shots Description"] = "Stronger spirits of the mountain are called upon. Double damage and True Damage.";
        LocalizationManager.Instance.textTable["Druidic Connections Description"] = "Druids in range attack 10% faster! Self attack speed +25%.";
        LocalizationManager.Instance.textTable["Inner Totem Description"] = "Slows nearby bloons by 30%! Double Damage.";
        LocalizationManager.Instance.textTable["Mountain Glacier Description"] = "Spawns freezing sentries. Calls upon 3 spirits at once.";
        LocalizationManager.Instance.textTable["Wolf of the Mountains Description"] = "All magic monkeys in range get +15 range, 0.5x rate, and +5 pierce! Quadruple Damage with 5x Damage to MOABs.";
    }

    public override TowerContainer GetTower(GameModel gameModel) {
        /*var tc = new TowerContainer();

        #region Details

        tc.shop = new ShopTowerDetailsModel("GhostWolf", 99999, 5, 5, 5, -1, -1, null);

        #endregion

        #region Upgrades

        var T1U = new UpgradeModel("GhostWolf_SuperShots", 770, 0, new("Assets/ResizedImages/UI/TrophyStoreIcons/Heroes/ObynWolfPetIcon.png"), 0, 0, 0, "", "Ghostly Shots");
        var T2U = new UpgradeModel("GhostWolf_DruidicConnections", 3000, 0, new("Assets/ResizedImages/UI/UpgradeIcons/ObynGreenfoot/BramblesAA.png"), 0, 1, 0, "", "Druidic Connections");
        var T3U = new UpgradeModel("GhostWolf_InnerTotem", 8560, 0, new("Assets/ResizedImages/MonkeyPortraits/Obyn/Totem.png"), 0, 2, 0, "", "Inner Totem");
        var T4U = new UpgradeModel("GhostWolf_MountainGlacier", 12750, 0, new("Assets/ResizedImages/UI/UpgradeIcons/ObynGreenfoot/WallOfTreesAA.png"), 0, 3, 0, "", "Mountain Glacier");
        var T5U = new UpgradeModel("GhostWolf_WOTM", 36000, 0, new("Assets/ResizedImages/UI/TrophyStoreIcons/Coop/ObynPeaceIcon.png"), 0, 4, 0, "", "Wolf of the Mountains");

        tc.upgrades.AddRange(new[] { T1U, T2U, T3U, T4U, T5U });

        #endregion

        #region TowerRefs

        var Obyn = gameModel.towers.First(a => a.name.Equals("ObynGreenfoot")).CloneCast();
        var NaturesWardTotem = gameModel.towers.First(a => a.name.Equals("NaturesWardTotem")).CloneCast();
        var Engi100 = gameModel.towers.First(a => a.name.Equals("EngineerMonkey-100")).CloneCast();

        #endregion

        #region T0

        var wolfT0 = gameModel.towers[0].CloneCast();

        wolfT0.SetDisplay("Assets/Monkeys/ObynGreenfoot/Graphics/Pets/Wolf/PetWolfDisplay.prefab");
        wolfT0.SetIcons("Assets/ResizedImages/UI/TrophyStoreIcons/Heroes/ObynWolfPetIcon.png");
        wolfT0.name = wolfT0.GetNameMod(wolfT0.baseId = "GhostWolf");
        wolfT0.towerSet = "Magic";
        wolfT0.upgrades = new UpgradePathModel[] { new UpgradePathModel("GhostWolf_SuperShots", "GhostWolf-100") };
        wolfT0.cost += 885;

        for (int i = 0; i < wolfT0.behaviors.Length; i++) {
            if (wolfT0.behaviors[i].Is<AttackModel>()) {
                var att = Obyn.behaviors.First(a => a.Is<AttackModel>()).CloneCast<AttackModel>();

                att.range = wolfT0.range;

                wolfT0.behaviors[i] = att;
            }
        }

        wolfT0.behaviors = wolfT0.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true));

        tc.towers.Add(wolfT0);

        #endregion

        #region T1

        var wolfT1 = wolfT0.CloneCast();
        wolfT1.tiers = new[] { wolfT1.tier = 1, 0, 0 };
        wolfT1.name = wolfT1.GetNameMod(wolfT1.baseId);
        wolfT1.upgrades = new UpgradePathModel[] { new UpgradePathModel("GhostWolf_DruidicConnections", "GhostWolf-200") };

        for (int i = 0; i < wolfT1.behaviors.Length; i++) {
            if (wolfT1.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.range = wolfT1.range += 10;
                attack.weapons[0].projectile.scale *= 1.5f;
                attack.weapons[0].projectile.pierce += 5;

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
        wolfT2.upgrades = new UpgradePathModel[] { new UpgradePathModel("GhostWolf_InnerTotem", "GhostWolf-300") };

        RateSupportModel rsm = new RateSupportModel("RateSupportModel_", .9f, true, "Obyn:NaturesWrath:PierceDruid", false, 1, new TowerFilterModel[] { new FilterInBaseTowerIdModel("FilterInBaseTowerIdModel_", new[] { "Druid" }) }, "NaturesWrathBuff", "BuffIconObyn");

        for (int i = 0; i < wolfT2.behaviors.Length; i++) {
            if (wolfT2.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.weapons[0].Rate *= .75f;
            }
        }

        wolfT2.behaviors = wolfT2.behaviors.Add(rsm);

        tc.towers.Add(wolfT2);

        #endregion

        #region T3

        var wolfT3 = wolfT2.CloneCast();
        wolfT3.tiers = new[] { wolfT3.tier = 3, 0, 0 };
        wolfT3.name = wolfT3.GetNameMod(wolfT3.baseId);
        wolfT3.upgrades = new UpgradePathModel[] { new UpgradePathModel("GhostWolf_MountainGlacier", "GhostWolf-400") };

        var slows = NaturesWardTotem.behaviors.Where(a => a.Is<SlowBloonsZoneModel>()).ToArray();

        for (int i = 0; i < wolfT3.behaviors.Length; i++) {
            if (wolfT3.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.range = wolfT3.range += 10;
                attack.weapons[0].projectile.pierce += 5;

                for (int j = 0; j < attack.weapons[0].projectile.behaviors.Length; j++) {
                    if (attack.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var damage)) {
                        damage.damage *= 2;
                        damage.CapDamage(damage.damage);
                    }
                }
            }
        }

        wolfT3.behaviors = wolfT3.behaviors.Add(slows);

        tc.towers.Add(wolfT3);

        #endregion

        #region T4

        var wolfT4 = wolfT3.CloneCast();
        wolfT4.tiers = new[] { wolfT4.tier = 4, 0, 0 };
        wolfT4.name = wolfT4.GetNameMod(wolfT4.baseId);
        wolfT4.upgrades = new UpgradePathModel[] { new UpgradePathModel("GhostWolf_WOTM", "GhostWolf-500") };

        for (int i = 0; i < wolfT4.behaviors.Length; i++) {
            if (wolfT4.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.range = wolfT4.range += 10;
                attack.weapons[0].projectile.pierce += 5;
                attack.weapons[0].emission = new AdoraEmissionModel("AdoraEmissionModel_", 3, 15, null);
            }
        }

        var spawner = Engi100.behaviors.First(a => a.name.Equals("AttackModel_Spawner_")).CloneCast<AttackModel>();

        spawner.weapons[0].projectile.display = "ea941ce796b2d81448d0342c0005b1ed";

        CreateTowerModel ctm = spawner.weapons[0].projectile.behaviors.First(a => a.Is<CreateTowerModel>()).Cast<CreateTowerModel>();
        ctm.tower = gameModel.towers.First(a => a.name.Equals("SentryCold")).CloneCast();

        wolfT4.behaviors = wolfT4.behaviors.Add(spawner);

        tc.towers.Add(wolfT4);

        #endregion

        #region T5

        var wolfT5 = wolfT4.CloneCast();
        wolfT5.tiers = new[] { wolfT5.tier = 5, 0, 0 };
        wolfT5.name = wolfT5.GetNameMod(wolfT5.baseId);
        wolfT5.upgrades = new UpgradePathModel[0];

        for (int i = 0; i < wolfT5.behaviors.Length; i++) {
            if (wolfT5.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.range = wolfT5.range += 15;
                attack.weapons[0].projectile.pierce += 5;

                for (int j = 0; j < attack.weapons[0].projectile.behaviors.Length; j++) {
                    if (attack.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var damage)) {
                        damage.damage *= 4;
                        damage.CapDamage(damage.damage);

                        attack.weapons[0].projectile.display = "Assets/Monkeys/ObynGreenfoot/Graphics/Projectiles/Mountain/MountainSpirit.prefab";
                    }
                }

                if (!attack.name.Contains("Spawner"))
                    attack.weapons[0].projectile.behaviors = attack.weapons[0].projectile.behaviors.Add(new DamageModifierForTagModel("DMFTM_", "Moabs", 5, 0, false, true));
            }
        }

        var spawner_2 = spawner.CloneCast();

        spawner_2.name = spawner_2.name.Replace("Spawner", "Spawner2");
        spawner_2.range = wolfT5.range;
        spawner_2.weapons[0].startInCooldown = true;
        spawner_2.weapons[0].customStartCooldown = spawner_2.weapons[0].Rate / 2f;

        wolfT5.behaviors = wolfT5.behaviors.Add(new PierceSupportModel("PierceSupportModel_", true, 5, "GhostWolfPierce", new TowerFilterModel[] { new FilterInSetModel("FISM_", new[] { "Magic" }) }, true, "", ""),
            new RateSupportModel("RateSupportModel__", .5f, true, "GhostWolfRate", true, 1, new TowerFilterModel[] { new FilterInSetModel("FISM_", new[] { "Magic" }) }, "", ""),
            new RangeSupportModel("RangeSupportModel_", true, 1, 15, "GhostWolfRange", new TowerFilterModel[] { new FilterInSetModel("FISM_", new[] { "Magic" }) }, true, "", ""),
            new DisplayModel("DM_Place1", "Assets/Monkeys/Adora/Graphics/Effects/AdoraTransformFXDark.prefab", 0, new(0, 0, 0), 1, true, 0),
            new DisplayModel("DM_Place2", "Assets/Monkeys/Adora/Graphics/Effects/AdoraSunBeamUpgradeLvl20.prefab", 0, new(0, 0, 0), 1, true, 0),
            spawner_2);

        tc.towers.Add(wolfT5);

        #endregion
        */
        return default; //TODO fix
    }
}
