namespace Ultra_Powers.PowerAdapters;
internal class UltraRoadSpikes : IPowerAdapter {
    internal override void ModifyPower(ref PowerModel power) {
        if (!power.name.Equals("RoadSpikes"))
            return;

        power.icon = "Ui[Ultra_Powers.Assets.URoadSpikesIcon.png]".GetSpriteReference();

        var spactory = UltraPowers.gameModel.towers.First(a => a.name.Equals("SpikeFactory-501")).behaviors.First(a => a.Is<AttackModel>()).CloneCast<AttackModel>().weapons[0].projectile;

        spactory.behaviors = spactory.behaviors.Remove(a => a.Is<ArriveAtTargetModel>() || a.Is<HeightOffsetProjectileModel>() || a.Is<ScaleProjectileModel>() || a.Is<CreateProjectileOnExpireModel>());

        foreach (var rsm in power.GetChildren<RoadSpikesModel>())
            rsm.projectileModel = spactory;
    }

    internal override void Setup(ref List<string> spriteAssets, ref List<(string, string, int)> rendererAssets) {
        spriteAssets.Add("Ultra_Powers.Assets.URoadSpikesIcon.png");
    }
}