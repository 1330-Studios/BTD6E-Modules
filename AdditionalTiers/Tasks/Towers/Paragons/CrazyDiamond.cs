namespace AdditionalTiers.Tasks.Towers.Tier6s;
internal class CrazyDiamond : TowerTask {
    public static TowerModel crazyDiamond;
    private static int time = -1;
    public CrazyDiamond() {
        identifier = "Crazy Diamond";
        getTower = () => crazyDiamond;
        baseTower = AddedTierName.CRAZYDIAMOND;
        tower = AddedTierEnum.CRAZYDIAMOND;
        requirements += tts => tts.tower.towerModel.baseId.Equals("NinjaMonkey") && tts.tower.towerModel.isParagon || tts.tower.towerModel.baseId.Equals("ParagonNinjaMonkey");
        onComplete += tts => {
            if (time < 50) {
                time++;
                return;
            }
            TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
            tts.tower.worth = 0;
            tts.tower.UpdateRootModel(crazyDiamond);
            tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
            AbilityMenu.instance.TowerChanged(tts);
            AbilityMenu.instance.RebuildAbilities();
        };
        gameLoad += gm => {
            crazyDiamond = gm.towers.First(a => a.name.Equals(baseTower)).CloneCast();

            crazyDiamond.range = 197.5F;
            crazyDiamond.cost = 0;
            crazyDiamond.name = "CrazyDiamond";
            crazyDiamond.baseId = baseTower.Split('-')[0];
            crazyDiamond.dontDisplayUpgrades = true;
            crazyDiamond.SetDisplay("CrazyDiamond");
            crazyDiamond.SetIcons("CrazyDiamondPor");

            for (int i = 0; i < crazyDiamond.behaviors.Length; i++) {
                if (crazyDiamond.behaviors[i].Is<AttackModel>(out var am)) {
                    am.range = 197.5F;

                    for (int j = 0; j < am.weapons.Length; j++) {
                        var weapon = am.weapons[j];

                        weapon.Rate = 0;
                        weapon.emission = new AdoraEmissionModel("AEM_", 8, 360f / 8f, null);

                        weapon.projectile.ModifyDamageModel(new() { multiply = true, damage = 1975 });
                        if (!string.IsNullOrEmpty(weapon.projectile.display.guidRef))
                            if (weapon.projectile.display.guidRef.Contains("d2ba0805fb7500f4c979eee9478c3a07", StringComparison.OrdinalIgnoreCase))
                                weapon.projectile.display.guidRef = "CrazyDiamondShuriken";

                        am.weapons[j] = weapon;
                    }
                }
            }

            crazyDiamond.behaviors = crazyDiamond.behaviors.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true).Cast<Model>());
        };
        recurring += _ => { };
        onLeave += () => time = -1;
        assetsToRead.Add(new("CrazyDiamond", "Assets/Monkeys/NinjaMonkey/Graphics/Paragon - AscendedShadow/AscendedShadowLvl5.prefab", RendererType.SKINNEDMESHRENDERER));
        assetsToRead.Add(new("CrazyDiamondShuriken", "Assets/Monkeys/NinjaMonkey/Graphics/Projectiles/AscendedShadowShurikenLvl1.prefab", RendererType.SPRITERENDERER));
    }
}
