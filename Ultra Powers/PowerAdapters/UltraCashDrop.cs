namespace Ultra_Powers.PowerAdapters;
internal class UltraCashDrop : IPowerAdapter {
    internal override void ModifyPower(ref PowerModel power) {
        if (!power.name.Equals("CashDrop"))
            return;

        power.icon = "Ui[Ultra_Powers.Assets.UCrateIcon.png]".GetSpriteReference();
        foreach (var cdm in power.GetChildren<CashDropModel>()) {
            cdm.projectileModel.display = new() { guidRef = "Ultra_Powers.Assets.UCrate.png" };
            foreach (var cm in cdm.projectileModel.GetChildren<CashModel>()) {
                cm.maximum = cm.minimum = 1000000;
                cm.maximum += 500000;
            }
        }
    }

    internal override void Setup(ref List<string> spriteAssets, ref List<(string, string, int)> rendererAssets) {
        spriteAssets.Add("Ultra_Powers.Assets.UCrateIcon.png");
        rendererAssets.Add(("Ultra_Powers.Assets.UCrate.png", "c737ade5badc75d49b97ac44e123430c", 1));
    }
}