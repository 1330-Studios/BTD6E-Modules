namespace Ultra_Powers.PowerAdapters;
internal class UltraDartTime : IPowerAdapter {
    internal override void ModifyPower(ref PowerModel power) {
        if (!power.name.Equals("DartTime"))
            return;

        power.icon = "Ui[Ultra_Powers.Assets.UDartTime.png]".GetSpriteReference();

        foreach (var dtm in power.GetChildren<DartTimeModel>()) {
            dtm.bloonSpeed = 0.01f;
            dtm.towerAttackSpeed = 1;
            dtm.duration = 60;
        }
    }

    internal override void Setup(ref List<string> spriteAssets, ref List<(string, string, int)> rendererAssets) {
        spriteAssets.Add("Ultra_Powers.Assets.UDartTime.png");
    }
}