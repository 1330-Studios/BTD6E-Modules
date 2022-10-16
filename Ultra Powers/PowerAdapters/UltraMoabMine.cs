namespace Ultra_Powers.PowerAdapters;
internal class UltraMoabMine : IPowerAdapter {
    internal override void ModifyPower(ref PowerModel power) {
        if (!power.name.Equals("MoabMine"))
            return;

        power.icon = "Ui[Ultra_Powers.Assets.UMoabMineIcon.png]".GetSpriteReference();
        foreach (var mmm in power.GetChildren<MoabMineModel>()) {
            mmm.projectileModel.pierce = 5000;
            mmm.projectileModel.CapPierce(5000);
            mmm.projectileModel.display = new() { guidRef = "Ultra_Powers.Assets.UMoabMine.png" };
            for (int i = 0; i < mmm.projectileModel.behaviors.Length; i++) {
                if (mmm.projectileModel.behaviors[i].Is<DamageModel>(out var dm)) {
                    dm.damage = 50000;
                }
            }
        }
    }

    internal override void Setup(ref List<string> spriteAssets, ref List<(string, string, int)> rendererAssets) {
        spriteAssets.Add("Ultra_Powers.Assets.UMoabMineIcon.png");
        rendererAssets.Add(("Ultra_Powers.Assets.UMoabMine.png", "092c7228eb278d04e81fac7f87631e5f", 0));
    }
}