namespace Ultra_Powers.PowerAdapters;
internal class UltraCamoDetector : IPowerAdapter {
    internal override void ModifyPower(ref PowerModel power) {
        if (!power.name.Equals("CamoTrap"))
            return;

        power.icon = "Ui[Ultra_Powers.Assets.UCamoTrapIcon.png]".GetSpriteReference();

        foreach (var ctm in power.GetChildren<CamoTrapModel>()) {
            ctm.projectileModel.pierce = 50000;
            ctm.projectileModel.scale = 2;
            ctm.projectileModel.radius = 1000000;
            ctm.projectileModel.filters = Array.Empty<FilterModel>();

            foreach (var beh in ctm.projectileModel.behaviors) {
                if (beh.Is<ProjectileFilterModel>(out var pfm)) {
                    var filter = pfm.filters[1].Cast<FilterWithTagModel>();
                    filter.camoTag = filter.growTag = filter.fortifiedTag = true;
                    pfm.filters[1] = filter;
                }

                if (beh.Is<RemoveBloonModifiersModel>(out var rbmm)) {
                    rbmm.cleanseCamo = rbmm.cleanseFortified = rbmm.cleanseRegen = true;
                }
            }
        }
    }

    internal override void Setup(ref List<string> spriteAssets, ref List<(string, string, int)> rendererAssets) {
        spriteAssets.Add("Ultra_Powers.Assets.UCamoTrapIcon.png");
    }
}
