namespace Ultra_Powers.PowerAdapters;
internal class UltraGlueTrap : IPowerAdapter {
    internal override void ModifyPower(ref PowerModel power) {
        if (!power.name.Equals("GlueTrap"))
            return;

        SlowModel glueSlow = UltraPowers.gameModel.towers.First(a => a.name.Equals("GlueGunner-005")).behaviors.First(a => a.Is<AttackModel>()).Cast<AttackModel>().weapons[0].projectile.behaviors.First(a=>a.Is<SlowModel>()).CloneCast<SlowModel>();

        power.icon = "Ui[Ultra_Powers.Assets.UGlueTrapIcon.png]".GetSpriteReference();
        foreach (var gtm in power.GetChildren<GlueTrapModel>()) {
            gtm.projectileModel.pierce = 30000;
            gtm.projectileModel.display = new() { guidRef = "Ultra_Powers.Assets.UGlueTrap.png" };
            gtm.projectileModel.filters = Array.Empty<FilterModel>();
            gtm.projectileModel.behaviors = gtm.projectileModel.behaviors.Remove(m => m.Is<ProjectileFilterModel>() || m.Is<CollideExtraPierceReductionModel>());
            for (var i = 0; i < gtm.projectileModel.behaviors.Count; i++)
                if (gtm.projectileModel.behaviors[i].Is<SlowModel>())
                    gtm.projectileModel.behaviors[i] = glueSlow;
        }
    }

    internal override void Setup(ref List<string> spriteAssets, ref List<(string, string, int)> rendererAssets) {
        spriteAssets.Add("Ultra_Powers.Assets.UGlueTrapIcon.png");
        rendererAssets.Add(("Ultra_Powers.Assets.UGlueTrap.png", "378f5d6aa5dbccb46954f2d6ced84b83",0));
    }
}