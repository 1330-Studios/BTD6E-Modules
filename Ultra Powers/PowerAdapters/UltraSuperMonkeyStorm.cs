namespace Ultra_Powers.PowerAdapters;
internal class UltraSuperMonkeyStorm : IPowerAdapter {
    internal override void ModifyPower(ref PowerModel power) {
        if (!power.name.Equals("SuperMonkeyStorm"))
            return;

        power.icon = "Ui[Ultra_Powers.Assets.USuperMonkeyStormIcon.png]".GetSpriteReference();
        foreach (var dam in power.GetChildren<DamageAllModel>()) {
            dam.amount *= 10;
        }
    }

    internal override void Setup(ref List<string> spriteAssets, ref List<(string, string, int)> rendererAssets) {
        spriteAssets.Add("Ultra_Powers.Assets.USuperMonkeyStormIcon.png");
    }
}