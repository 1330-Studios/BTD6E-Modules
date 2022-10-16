namespace AdditionalTiers.Tasks.Towers.Tier6s;
internal class Barracuda : TowerTask {
    public static TowerModel barracuda;
    private static int time = -1;
    public Barracuda() {
        identifier = "Barracuda";
        getTower = () => barracuda;
        baseTower = AddedTierName.BARRACUDA;
        tower = AddedTierEnum.BARRACUDA;
        requirements += tts => tts.tower.towerModel.baseId.Equals(baseTower.Split('-')[0]) && tts.tower.towerModel.tiers[0] == 5;
        onComplete += tts => {
            if (time < 50) {
                time++;
                return;
            }
            TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
            tts.tower.worth = 0;
            tts.tower.UpdateRootModel(barracuda);
            tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
            AbilityMenu.instance.TowerChanged(tts);
            AbilityMenu.instance.RebuildAbilities();
        };
        gameLoad += gm => {
            barracuda = gm.towers.First(a => a.name.Equals(baseTower)).CloneCast();

            barracuda.range = 150;
            barracuda.cost = 0;
            barracuda.name = "Barracuda";
            barracuda.baseId = baseTower.Split('-')[0];
            barracuda.dontDisplayUpgrades = true;
            barracuda.SetDisplay("Barracuda");
            barracuda.portrait = "BarracudaIcon".GetSpriteReference();

            var etn = gm.towers.First(a => a.name.Equals("Etienne 20")).CloneCast();
            var drone = etn.behaviors.First(a => a.Is<DroneSupportModel>()).Cast<DroneSupportModel>();
            drone.count = 20;
            var att = etn.behaviors.Where(a => a.Is<AttackModel>());

            List<TowerCreateTowerModel> towers = new();

            var h1 = gm.towers.First(a => a.name.Equals("HeliPilot-500")).CloneCast();
            var a1 = gm.towers.First(a => a.name.Equals("MonkeyAce-500")).CloneCast();

            h1.behaviors.Remove(a => a.Is<DisplayModel>());
            a1.behaviors.Remove(a => a.Is<DisplayModel>());

            h1.display.guidRef = a1.display.guidRef = string.Empty;

            towers.AddRange(new TowerCreateTowerModel[] {
                new TowerCreateTowerModel("TowerCreateTowerModel_HeliPilot500", h1, true),
                new TowerCreateTowerModel("TowerCreateTowerModel_MonkeyAce500", a1, true)
            });

            for (int i = 0; i < barracuda.behaviors.Length; i++) {
                if (barracuda.behaviors[i].Is<AttackModel>(out var am)) {
                    if (am.name.Equals("AttackModel_Attack_", StringComparison.OrdinalIgnoreCase)) {
                        am.weapons[0].Rate = 0.01f;
                        var em = am.weapons[0].emission.Cast<ParallelEmissionModel>();
                        em.count = 4;
                        em.spreadLength = 5;
                        em.offsetStart = 2.5f;
                        am.weapons[0].emission = em;
                        am.weapons[0].projectile.SetDisplay("5737e26c93d5fc149ade2f7df1156c74");
                        am.weapons[0].projectile.pierce = 100;
                        am.weapons[0].projectile.ModifyDamageModel(new DamageChange() { multiply = true, damage = 60, cappedDamage = 60 });
                    }
                }
            }

            barracuda.behaviors = barracuda.behaviors.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true), drone).Add(att).Add(towers);
        };
        recurring += _ => { };
        onLeave += () => time = -1;
        assetsToRead.Add(new("Barracuda", "c987abcd0330f4c4b82309b905f36334", RendererType.SKINNEDMESHRENDERER));
    }
}
