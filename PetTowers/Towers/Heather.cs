using Harmony;

namespace PetTowers.Towers;
internal class Heather : ITower<Heather> {

    public override void Initialize(ref GameModel gameModel) {
        ResourceManager.RendererAssets.Add(("PetTowers.Resources.Heather.ChickenDiffuse.png", "7ab0ed9280f77764daa48a449ce016a7"));
        ResourceManager.RendererAssets.Add(("PetTowers.Resources.Heather.Seeds.png", "842be402795e7334cbc77d33b6746bff"));
        ResourceManager.RendererAssets.Add(("PetTowers.Resources.Heather.MagneticSeeds.png", "842be402795e7334cbc77d33b6746bff"));
        ResourceManager.RendererAssets.Add(("PetTowers.Resources.Heather.FracturedSeeds.png", "d15d481d5956b28449b8c87a578d8f07"));
        ResourceManager.RendererAssets.Add(("PetTowers.Resources.Heather.ObsidianSeeds.png", "842be402795e7334cbc77d33b6746bff"));
        ResourceManager.RendererAssets.Add(("PetTowers.Resources.Heather.DiamondSeeds.png", "842be402795e7334cbc77d33b6746bff"));

        LocalizationManager.Instance.textTable.Add("Two Seeds Description", "Throw 2 seeds at once, not sure why one would throw seeds but here we are.");
        LocalizationManager.Instance.textTable.Add("Magnetic Seeds Description", "Put some magnets into seeds. Surely this is safe. Surely.");
        LocalizationManager.Instance.textTable.Add("Breaking Seeds Description", "These seeds have been broken. Better call Heather.");
        LocalizationManager.Instance.textTable.Add("Obsidian Seeds Description", "Obsidian from minecraft. Minecraft is a fun game.");
        LocalizationManager.Instance.textTable.Add("Supa dupa diamond seeds Description", "Diamonds also from minecraft. Oh also by now Heather is incredibly strong.");
    }

    public override TowerContainer GetTower(GameModel gameModel) {
        var tc = new TowerContainer();

        #region Details

        tc.shop = new ShopTowerDetailsModel("Heather", 99999, 5, 5, 5, -1, -1, null);

        #endregion

        #region Upgrades

        var T1U = new UpgradeModel("Heather_DoubleShot", 680, 0, new() { guidRef = "Ui[PetTowers.Resources.Heather.Seeds.png]" }, 0, 0, 0, "", "Two Seeds");
        var T2U = new UpgradeModel("Heather_Seeking", 1350, 0, new() { guidRef = "Assets/ResizedImages/UI/UpgradeIcons/MonkeyAce/NevaMissTargetingUpgradeIcon.png" }, 0, 1, 0, "", "Magnetic Seeds");
        var T3U = new UpgradeModel("Heather_Fracture", 3000, 0, new() { guidRef = "Assets/ResizedImages/UI/UpgradeIcons/BombShooter/FragBombsUpgradeIcon.png" }, 0, 2, 0, "", "Breaking Seeds");
        var T4U = new UpgradeModel("Heather_ObsidianSeeds", 5750, 0, new() { guidRef = "Ui[PetTowers.Resources.Heather.ObsidianSeeds.png]" }, 0, 3, 0, "", "Obsidian Seeds");
        var T5U = new UpgradeModel("Heather_DiamondSeeds", 12750, 0, new() { guidRef = "Ui[PetTowers.Resources.Heather.DiamondSeeds.png]" }, 0, 4, 0, "", "Supa dupa diamond seeds");

        tc.upgrades.AddRange(new[] { T1U, T2U, T3U, T4U, T5U });

        #endregion

        #region T0

        var kiwiT0 = gameModel.towers[0].CloneCast();
        kiwiT0.SetDisplay("PetTowers.Resources.Heather.ChickenDiffuse.png");
        kiwiT0.SetIcons("PetTowers.Resources.Heather.Chicken.png");

        kiwiT0.name = kiwiT0.GetNameMod(kiwiT0.baseId = "Heather");
        kiwiT0.towerSet = "Primary";
        kiwiT0.upgrades = new[] { new UpgradePathModel("Heather_DoubleShot", "Heather-100") };
        kiwiT0.cost += 250;

        for (int i = 0; i < kiwiT0.behaviors.Length; i++) {
            if (kiwiT0.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.weapons[0].projectile.display = new() { guidRef = "PetTowers.Resources.Heather.Seeds.png" };
                attack.weapons[0].emission = new RandomEmissionModel("REM_", 1, 10, 0, null, false, 0.8f, 1.2f, 0.01f, true);

                foreach (var pbeh in attack.weapons[0].projectile.behaviors) {
                    if (pbeh.Is<DamageModel>(out var dm)) {
                        dm.damage *= 2;
                    }
                    if (pbeh.Is<TravelStraitModel>(out var tsm)) {
                        tsm.Speed /= 2;
                    }
                }
            }
        }

        kiwiT0.behaviors = kiwiT0.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true));

        tc.towers.Add(kiwiT0);

        #endregion

        #region T1

        var kiwiT1 = kiwiT0.CloneCast();
        kiwiT1.tier = 1;
        kiwiT1.tiers = new[] { 1, 0, 0 };
        kiwiT1.name = kiwiT1.GetNameMod("Heather");
        kiwiT1.upgrades = new UpgradePathModel[] { new UpgradePathModel("Heather_Seeking", "Heather-200") };

        for (int i = 0; i < kiwiT1.behaviors.Length; i++) {
            if (kiwiT1.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.weapons[0].emission = new RandomEmissionModel("REM_", 2, 30, 0, null, false, 0.8f, 1.2f, 0.01f, true);
            }
        }

        tc.towers.Add(kiwiT1);

        #endregion

        #region T2

        var kiwiT2 = kiwiT1.CloneCast();
        kiwiT2.tier = 2;
        kiwiT2.tiers = new[] { 2, 0, 0 };
        kiwiT2.name = kiwiT2.GetNameMod("Heather");
        kiwiT2.upgrades = new UpgradePathModel[] { new UpgradePathModel("Heather_Fracture", "Heather-300") };
        kiwiT2.range += 35;

        for (int i = 0; i < kiwiT2.behaviors.Length; i++) {
            if (kiwiT2.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.range = kiwiT2.range;
                attack.weapons[0].projectile.display = new() { guidRef = "PetTowers.Resources.Heather.MagneticSeeds.png" };
                attack.weapons[0].projectile.behaviors = attack.weapons[0].projectile.behaviors.Add(new AdoraTrackTargetModel("ATTM_", 360, 1, 2, .1f, 2, 1, 360));

                foreach (var pbeh in attack.weapons[0].projectile.behaviors) {
                    if (pbeh.Is<DamageModel>(out var dm)) {
                        dm.damage *= 2;
                    }
                    if (pbeh.Is<TravelStraitModel>(out var tsm)) {
                        tsm.Lifespan *= 3;
                    }
                }
            }
        }

        tc.towers.Add(kiwiT2);

        #endregion

        #region T3

        var kiwiT3 = kiwiT2.CloneCast();
        kiwiT3.tier = 3;
        kiwiT3.tiers = new[] { 3, 0, 0 };
        kiwiT3.name = kiwiT3.GetNameMod("Heather");
        kiwiT3.upgrades = new UpgradePathModel[] { new UpgradePathModel("Heather_ObsidianSeeds", "Heather-400") };
        kiwiT3.range += 10;

        var bomb = gameModel.towers.First(a => a.name.Equals("BombShooter-002")).CloneCast().behaviors.First(a => a.Is<AttackModel>()).Cast<AttackModel>()
            .weapons[0].projectile.behaviors.First(a=>a.Is<CreateProjectileOnContactModel>()&&a.name.Contains("frag")).CloneCast<CreateProjectileOnContactModel>();

        bomb.projectile.display = new() { guidRef = "PetTowers.Resources.Heather.FracturedSeeds.png" };

        for (int i = 0; i < kiwiT3.behaviors.Length; i++) {
            if (kiwiT3.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.range = kiwiT3.range;

                attack.weapons[0].projectile.behaviors = attack.weapons[0].projectile.behaviors.Add(bomb);

                foreach (var pbeh in attack.weapons[0].projectile.behaviors) {
                    if (pbeh.Is<DamageModel>(out var dm)) {
                        dm.damage *= 5;
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
        kiwiT4.name = kiwiT4.GetNameMod("Heather");
        kiwiT4.upgrades = new UpgradePathModel[] { new UpgradePathModel("Heather_DiamondSeeds", "Heather-500") };
        kiwiT4.range += 5;

        for (int i = 0; i < kiwiT4.behaviors.Length; i++) {
            if (kiwiT4.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.range = kiwiT4.range;
                attack.weapons[0].projectile.display = new() { guidRef = "PetTowers.Resources.Heather.ObsidianSeeds.png" };

                foreach (var pbeh in attack.weapons[0].projectile.behaviors) {
                    if (pbeh.Is<DamageModel>(out var dm)) {
                        dm.damage *= 200;
                    }
                    if (pbeh.Is<TravelStraitModel>(out var tsm)) {
                        tsm.Lifespan *= 2;
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
        kiwiT5.name = kiwiT5.GetNameMod("Heather");
        kiwiT5.upgrades = Array.Empty<UpgradePathModel>();
        kiwiT5.range += 15;

        for (int i = 0; i < kiwiT5.behaviors.Length; i++) {
            if (kiwiT5.behaviors[i].Is<AttackModel>(out var attack)) {
                attack.range = kiwiT5.range;
                attack.weapons[0].projectile.display = new() { guidRef = "PetTowers.Resources.Heather.ObsidianSeeds.png" };
                attack.weapons[0].emission = new RandomEmissionModel("REM_", 5, 60, 0, null, false, 0.8f, 1.2f, 0.01f, true);

                attack.weapons[0].projectile.behaviors = attack.weapons[0].projectile.behaviors.Add(new CreateEffectOnContactModel("CEOCM_", new EffectModel("EM_", new() { guidRef = "Assets/Monkeys/Adora/Graphics/Effects/AdoraSunBeamPlacement.prefab" }, 1, 1)));

                foreach (var pbeh in attack.weapons[0].projectile.behaviors) {
                    if (pbeh.Is<DamageModel>(out var dm)) {
                        dm.damage *= 200;
                    }
                    if (pbeh.Is<TravelStraitModel>(out var tsm)) {
                        tsm.Speed *= 2;
                    }
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