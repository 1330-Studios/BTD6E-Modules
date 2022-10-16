namespace Ultra_Powers.PowerAdapters;
internal class UltraTechBot : IPowerAdapter {
    internal override void ModifyPower(ref PowerModel power) {
        if (!power.name.Equals("TechBot"))
            return;

        power.icon = power.tower.icon = power.tower.portrait = "Ui[Ultra_Powers.Assets.UTechBotIcon.png]".GetSpriteReference();

        power.tower.range = 9999999;
        power.tower.isGlobalRange = true;

        var sm = UltraPowers.gameModel.towers.First(a => a.name.Equals("DartMonkey")).CloneCast().behaviors;

        var am = sm.First(a => a.Is<AttackModel>()).CloneCast<AttackModel>();

        am.range = 9999999;
        am.weapons[0].ejectX = 0;
        am.weapons[0].ejectZ = 15;
        am.weapons[0].ejectY = 20;

        am.weapons[0].emission = new ArcEmissionModel("AEM_", 500, 0, 75, null, false);
        am.weapons[0].Rate = 10;
        am.weapons[0].projectile.display = new() { guidRef = "0b7323ccbaace054ea5a4a579e24b473" };
        am.weapons[0].projectile.ignorePierceExhaustion = true;
        am.weapons[0].projectile.behaviors.First(a => a.Is<TravelStraitModel>()).Cast<TravelStraitModel>().Lifespan = 60;
        am.weapons[0].projectile.behaviors.First(a => a.Is<TravelStraitModel>()).Cast<TravelStraitModel>().Speed = 30;

        power.tower.behaviors = power.tower.behaviors.Add(am);
    }

    internal override void Setup(ref List<string> spriteAssets, ref List<(string, string, int)> rendererAssets) {
        spriteAssets.Add("Ultra_Powers.Assets.UTechBotIcon.png");
    }
}