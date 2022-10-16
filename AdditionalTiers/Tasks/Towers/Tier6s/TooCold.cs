namespace AdditionalTiers.Tasks.Towers.Tier6s;

internal class TooCold : TowerTask {
    public static TowerModel ic;
    private static int time = -1;
    public TooCold() {
        identifier = "Too Cold";
        getTower = () => ic;
        baseTower = AddedTierName.TOOCOLD;
        tower = AddedTierEnum.TOOCOLD;
        requirements += tts => tts.tower.towerModel.baseId.Equals("IceMonkey") && tts.tower.towerModel.tiers[0] == 5;
        onComplete += tts => {
            if (time < 50) {
                time++;
                return;
            }
            TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
            tts.tower.worth = 0;
            tts.tower.UpdateRootModel(ic);
            tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
            AbilityMenu.instance.TowerChanged(tts);
            AbilityMenu.instance.RebuildAbilities();
        };
        gameLoad += gm => {
            ic = gm.towers.First(a => a.name.Contains(AddedTierName.TOOCOLD)).CloneCast();

            ic.cost = 0;
            ic.name = "Too Cold";
            ic.baseId = AddedTierName.TOOCOLD.Split('-')[0];
            ic.SetDisplay("TooCold");
            ic.dontDisplayUpgrades = true;
            ic.portrait = new("TooColdPortrait");
            ic.range = 50;

            var beh = ic.behaviors;

            for (int i = 0; i < beh.Length; i++) {
                if (beh[i].Is<LinkProjectileRadiusToTowerRangeModel>(out var lprttrm))
                    lprttrm.baseTowerRange = ic.range;

                if (beh[i].Is<AttackModel>(out var am)) {
                    am.range = ic.range;

                    am.weapons[0].rate = 0;
                    am.weapons[0].animationOffset = 0;
                    am.weapons[0].projectile.ignorePierceExhaustion = true;
                    for (int j = 0; j < am.weapons[0].projectile.behaviors.Length; j++) {
                        if (am.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var dm)) {
                            dm.damage = 3000;
                            dm.immuneBloonProperties = BloonProperties.None;
                        }

                        if (am.weapons[0].projectile.behaviors[j].Is<FreezeModel>(out var fm)) {
                            fm.lifespan *= 2;
                            fm.damageModel.damage = 500;
                        }

                        if (am.weapons[0].projectile.behaviors[j].Is<AddBonusDamagePerHitToBloonModel>(out var abdphtbm)) {
                            abdphtbm.lifespan *= 5;
                            abdphtbm.perHitDamageAddition = 1000;
                        }
                    }

                    for (int j = 0; j < am.weapons[0].behaviors.Length; j++) {
                        if (am.weapons[0].behaviors[j].Is<EjectEffectModel>(out var eef)) {
                            eef.effectModel.scale *= 3;
                        }
                    }
                }
            }

            ic.behaviors = beh.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true));
        };
        recurring += _ => { };
        onLeave += () => time = -1;
        assetsToRead.Add(new("TooCold", "0939a7e98392a7148927794c841b288e", RendererType.SKINNEDMESHRENDERER));
    }
}