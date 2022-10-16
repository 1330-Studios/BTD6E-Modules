namespace AdditionalTiers.Tasks.Towers.Tier6s;
internal class KillerQueen : TowerTask {
    public static TowerModel killerqueen;
    private static int time = -1;
    public KillerQueen() {
        identifier = "Killer Queen";
        getTower = () => killerqueen;
        baseTower = AddedTierName.KILLERQUEEN;
        tower = AddedTierEnum.KILLERQUEEN;
        requirements += tts => tts.tower.towerModel.baseId.Equals(baseTower.Split('-')[0]) && tts.tower.towerModel.tiers[0] == 5;
        onComplete += tts => {
            if (time < 50) {
                time++;
                return;
            }
            TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
            tts.tower.worth = 0;
            tts.tower.UpdateRootModel(killerqueen);
            tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
            AbilityMenu.instance.TowerChanged(tts);
            AbilityMenu.instance.RebuildAbilities();
        };
        gameLoad += gm => {
            killerqueen = gm.towers.First(a => a.name.Equals(baseTower)).CloneCast();

            killerqueen.range = 150;
            killerqueen.cost = 0;
            killerqueen.name = "KillerQueen";
            killerqueen.baseId = baseTower.Split('-')[0];
            killerqueen.dontDisplayUpgrades = true;
            killerqueen.SetDisplay("KillerQueen");
            killerqueen.portrait = "KillerQueenPor".GetSpriteReference();

            var stealATM = gm.towers.First(a=>a.name.Equals("MonkeySub-500")).CloneCast().behaviors.First(a=>a.Is<SubmergeModel>()).Cast<SubmergeModel>().submergeAttackModel.Cast<AttackModel>();

            for (int i = 0; i < stealATM.weapons.Length; i++) {
                stealATM.weapons[i].Rate *= 0.1f;

                if (stealATM.weapons[i].projectile.behaviors.Any(a => a.Is<DamageModel>()))
                    stealATM.weapons[i].projectile.ModifyDamageModel(new DamageChange() { multiply = true, damage = 500, cappedDamage = 500 });
                else
                    stealATM.weapons[i].projectile.AddDamageModel(DamageModelCreation.Standard, 25, true, BloonProperties.None);
            }

            for (int i = 0; i < killerqueen.behaviors.Length; i++) {
                if (killerqueen.behaviors[i].Is<AttackModel>(out var am)) {
                    am.range = 2000;

                    for (int j = 0; j < am.weapons.Length; j++) {
                        var weapon = am.weapons[j];

                        weapon.Rate = 0.5f;

                        for (int k = 0; k < weapon.projectile.behaviors.Length; k++) {
                            if (weapon.projectile.behaviors[k].Is<CreateProjectileOnExhaustFractionModel>(out var cpoefm))
                                cpoefm.projectile.ModifyDamageModel(new DamageChange() { multiply = true, damage = 500, cappedDamage = 500 });
                            if (weapon.projectile.behaviors[k].Is<CreateProjectileOnExpireModel>(out var cpoem))
                                cpoem.projectile.ModifyDamageModel(new DamageChange() { multiply = true, damage = 500, cappedDamage = 500 });
                        }

                        am.weapons[j] = weapon;
                    }
                }
            }

            killerqueen.behaviors = killerqueen.behaviors.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true), stealATM);
        };
        recurring += _ => { };
        onLeave += () => time = -1;
        assetsToRead.Add(new("KillerQueen", "dadaf960f2e35974b91e441ed1203150", RendererType.SKINNEDMESHRENDERER));
    }
}
