namespace Ultra_Powers.PowerAdapters;
internal class UltraBananaFarmer : IPowerAdapter {
    internal override void ModifyPower(ref PowerModel power) {
        if (!power.name.Equals("BananaFarmer"))
            return;

        var attack = UltraPowers.gameModel.towers[0].behaviors.First(a => a.Is<AttackModel>()).CloneCast<AttackModel>();
        attack.weapons[0].projectile.display = new() { guidRef = "595c7fb1d3705aa478d0d5171b74fb57" };

        power.icon = "Ui[Ultra_Powers.Assets.UBananaFarmerIcon.png]".GetSpriteReference();
        power.tower.SetDisplay("Ultra_Powers.Assets.UBananaFarmer.png");
        power.tower.range = 9999999;
        power.tower.towerSelectionMenuThemeId = "Default";
        for (int i = 0; i < power.tower.behaviors.Length; i++) {
            if (power.tower.behaviors[i].Is<CollectCashZoneModel>(out var cczm)) {
                cczm.attractRange = 9999999;
                cczm.speed = 5;
            }
        }

        power.tower.behaviors = power.tower.behaviors.Add(attack);
    }

    internal override void Setup(ref List<string> spriteAssets, ref List<(string, string, int)> rendererAssets) {
        spriteAssets.Add("Ultra_Powers.Assets.UBananaFarmerIcon.png");
        rendererAssets.Add(("Ultra_Powers.Assets.UBananaFarmer.png", "41f2963fea4f9eb46afaf47d60ade688", 1));
    }
}