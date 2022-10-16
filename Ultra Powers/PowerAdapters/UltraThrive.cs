namespace Ultra_Powers.PowerAdapters;
internal class UltraThrive : IPowerAdapter {
    internal override void ModifyPower(ref PowerModel power) {
        if (!power.name.Equals("Thrive"))
            return;

        power.icon = "Ui[Ultra_Powers.Assets.UThriveIcon.png]".GetSpriteReference();
        foreach (var tm in power.GetChildren<ThriveModel>()) {
            tm.cashScale = 10;
            tm.increaseBloonWorthSimBehaviorModel.cashScale = 10;
        }
    }

    internal override void Setup(ref List<string> spriteAssets, ref List<(string, string, int)> rendererAssets) {
        spriteAssets.Add("Ultra_Powers.Assets.UThriveIcon.png");
    }
}