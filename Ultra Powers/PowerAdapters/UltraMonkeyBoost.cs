namespace Ultra_Powers.PowerAdapters;
internal class UltraMonkeyBoost : IPowerAdapter {
    internal override void ModifyPower(ref PowerModel power) {
        if (!power.name.Equals("MonkeyBoost"))
            return;

        power.icon = "Ui[Ultra_Powers.Assets.UMonkeyBoost.png]".GetSpriteReference();

        foreach (var mbm in power.GetChildren<MonkeyBoostModel>())
            mbm.rateScale = 0.001f;
    }

    internal override void Setup(ref List<string> spriteAssets, ref List<(string, string, int)> rendererAssets) {
        spriteAssets.Add("Ultra_Powers.Assets.UMonkeyBoost.png");
    }
}