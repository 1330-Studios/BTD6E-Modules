namespace Ultra_Powers.PowerAdapters;
internal class UltraEnergisingTotem : IPowerAdapter {
    internal override void ModifyPower(ref PowerModel power) {
        if (!power.name.Equals("EnergisingTotem"))
            return;

        power.icon = power.tower.portrait = "Ui[Ultra_Powers.Assets.UEnergisingTotemIcon.png]".GetSpriteReference();
        power.tower.SetDisplay("Ultra_Powers.Assets.UEnergisingTotem.png");

        var totemBeh = power.tower.behaviors;

        for (int i = 0; i < totemBeh.Length; i++) {
            if (totemBeh[i].Is<EnergisingTotemBehaviorModel>(out var etbm))
                for (int j = 0; j < etbm.effectModels.Length; j++)
                    etbm.effectModels[j].assetId = new() { guidRef = "UTotem_Particles" };

            if (totemBeh[i].Is<RateSupportModel>(out var rsm))
                rsm.multiplier = 0.05f;
        }

        power.tower.behaviors = totemBeh.Add(new RangeSupportModel("RangeSupportModel_", true, 2, 0, "UETotem_Range", new(0), false, "UETotem_Range", "UETotem_Range"));
    }

    internal override void Setup(ref List<string> spriteAssets, ref List<(string, string, int)> rendererAssets) {
        spriteAssets.Add("Ultra_Powers.Assets.UEnergisingTotemIcon.png");
        rendererAssets.Add(("Ultra_Powers.Assets.UEnergisingTotem.png", "65bf98ead18ff0643b31acfd2736ce57", -1));
    }
}