namespace AdditionalTiers.Tasks.Towers.Tier6s;

internal class GreenDay : TowerTask {
    public static TowerModel gd;
    private static int time = -1;
    public GreenDay() {
        identifier = "Green Day";
        getTower = () => gd;
        baseTower = AddedTierName.GREENDAY;
        tower = AddedTierEnum.GREENDAY;
        requirements += tts => tts.tower.towerModel.baseId.Equals("TackShooter") && tts.tower.towerModel.tiers[1] == 5;
        onComplete += tts => {
            if (time < 50) {
                time++;
                return;
            }
            TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
            tts.tower.worth = 0;
            tts.tower.UpdateRootModel(gd);
            tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
            AbilityMenu.instance.TowerChanged(tts);
            AbilityMenu.instance.RebuildAbilities();
        };
        gameLoad += gm => {
            gd = gm.towers.First(a => a.name.Contains(AddedTierName.GREENDAY)).CloneCast();

            gd.cost = 0;
            gd.name = "Green Day";
            gd.baseId = AddedTierName.GREENDAY.Split('-')[0];
            gd.SetDisplay("GreenDay");
            gd.dontDisplayUpgrades = true;
            gd.portrait = new("GreenDayPortrait");

            var beh = gd.behaviors;

            AttackModel activatedAttack = beh.First(a => a.Is<AbilityModel>(out _)).CloneCast<AbilityModel>().
            behaviors.First(a => a.Is<ActivateAttackModel>(out _)).Cast<ActivateAttackModel>().attacks[0];

            AttackModel clone = activatedAttack.CloneCast();
            clone.weapons[0].rate /= 2;

            AttackModel clone2 = activatedAttack.CloneCast();
            clone2.weapons[0].rate /= 2;

            activatedAttack.weapons[0].projectile.display.guidRef = "GreenDayProj";
            clone.weapons[0].projectile.display.guidRef = "GreenDayProj2";
            clone2.weapons[0].projectile.display.guidRef = "GreenDayProj3";

            clone.weapons[0].emission.Cast<ArcEmissionModel>().count = 8;
            clone2.weapons[0].emission.Cast<ArcEmissionModel>().count = 16;

            activatedAttack.weapons[0].projectile.behaviors.First(b => b.Is<DamageModel>(out _)).Cast<DamageModel>().damage *= 50;
            clone.weapons[0].projectile.behaviors.First(b => b.Is<DamageModel>(out _)).Cast<DamageModel>().damage *= 25;
            clone2.weapons[0].projectile.behaviors.First(b => b.Is<DamageModel>(out _)).Cast<DamageModel>().damage *= 10;

            activatedAttack.weapons[0].projectile.behaviors.Add(new DamageModifierForTagModel("DamageModifierForTagModel_", "Moabs", 10, 500, false, true));
            clone.weapons[0].projectile.behaviors.Add(new DamageModifierForTagModel("DamageModifierForTagModel_2", "Moabs", 5, 250, false, true));
            clone2.weapons[0].projectile.behaviors.Add(new DamageModifierForTagModel("DamageModifierForTagModel_3", "Moabs", 2.5f, 125, false, true));

            gd.behaviors = beh.Remove(a=>a.Is<AttackModel>(out _) || a.Is<AbilityModel>(out _)).Add(clone2, clone, activatedAttack, new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true));
        };
        recurring += _ => { };
        onLeave += () => time = -1;
        assetsToRead.Add(new("GreenDay", "880bd59f872f9894683c349834584137", RendererType.SKINNEDMESHRENDERER));
        assetsToRead.Add(new("GreenDayProj", "73c06263b85d1354da4cf582782051b1", RendererType.SPRITERENDERER));
        assetsToRead.Add(new("GreenDayProj2", "73c06263b85d1354da4cf582782051b1", RendererType.SPRITERENDERER));
        assetsToRead.Add(new("GreenDayProj3", "73c06263b85d1354da4cf582782051b1", RendererType.SPRITERENDERER));
    }
}