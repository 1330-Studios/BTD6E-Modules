namespace AdditionalTiers.Tasks.Towers.Tier6s;
internal class PlanetWaves : TowerTask {
    public static TowerModel planetWaves;
    private static int time = -1;
    public PlanetWaves() {
        identifier = "Planet Waves";
        getTower = () => planetWaves;
        baseTower = AddedTierName.PLANETWAVES;
        tower = AddedTierEnum.PLANETWAVES;
        requirements += tts => tts.tower.towerModel.baseId.Equals("MonkeySub") && tts.tower.towerModel.tiers[0] == 5;
        onComplete += tts => {
            if (time < 50) {
                time++;
                return;
            }
            TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
            tts.tower.worth = 0;
            tts.tower.UpdateRootModel(planetWaves);
            tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
            AbilityMenu.instance.TowerChanged(tts);
            AbilityMenu.instance.RebuildAbilities();
        };
        gameLoad += gm => {
            planetWaves = gm.towers.First(a => a.name.Contains(AddedTierName.PLANETWAVES)).CloneCast();

            planetWaves.range = 125;
            planetWaves.cost = 0;
            planetWaves.name = "Planet Waves";
            planetWaves.baseId = "MonkeySub";
            planetWaves.display.guidRef = "PlanetWaves";
            planetWaves.dontDisplayUpgrades = true;
            planetWaves.portrait = new("PlanetWavesPortrait");
            planetWaves.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display.guidRef = "PlanetWaves";

            var beh = planetWaves.behaviors;
            for (var i = 0; i < beh.Length; i++) {
                var behavior = beh[i];
                if (behavior.Is<AttackModel>(out var am)) {
                    for (var j = 0; j < am.weapons.Length; j++) {
                        am.weapons[j].Rate = 0;
                        am.weapons[j].rate = 0;
                        am.weapons[j].rateFrames = 0;
                        am.weapons[j].projectile.ignorePierceExhaustion = true;
                        for (int k = 0; k < am.weapons[j].projectile.behaviors.Length; k++) {
                            if (am.weapons[j].projectile.behaviors[k].Is<DamageModel>(out var dm)) {
                                dm.damage = 1250;
                            }
                        }

                        am.weapons[j].projectile.behaviors = am.weapons[j].projectile.behaviors.Add(new DamageModifierForTagModel("DamageModifierForTagModel_", "Moabs", 3, 0, false, true));
                    }

                    am.range = planetWaves.range;
                    beh[i] = am;
                }

                if (behavior.Is<LinkProjectileRadiusToTowerRangeModel>(out var lprttrm)) {
                    lprttrm.baseTowerRange = planetWaves.range;

                    for (int j = 0; j < lprttrm.projectileModel.behaviors.Length; j++) {
                        if (lprttrm.projectileModel.behaviors[j].Is<DamageModel>(out var dm)) {
                            dm.damage = 1250;
                        }
                    }

                    beh[i] = lprttrm;
                }

                if (behavior.Is<SubmergeModel>(out var sm)) {
                    var sam = sm.submergeAttackModel.Cast<AttackModel>();
                    for (int j = 0; j < sam.weapons.Length; j++) {
                        sam.weapons[j].Rate = 0;
                        sam.weapons[j].rate = 0;
                        sam.weapons[j].rateFrames = 0;
                        sam.weapons[j].projectile.ignorePierceExhaustion = true;
                        for (int k = 0; k < sam.weapons[j].projectile.behaviors.Length; k++) {
                            if (sam.weapons[j].projectile.behaviors[k].Is<DamageModel>(out var dm)) {
                                dm.damage = 1250;
                            }
                        }

                        sam.weapons[j].projectile.behaviors = sam.weapons[j].projectile.behaviors.Add(new DamageModifierForTagModel("DamageModifierForTagModel_", "Moabs", 3, 0, false, true));
                    }
                    sm.submergeSpeed = 1;
                    sam.range = planetWaves.range;

                    beh[i] = sm;
                }

                beh[i] = behavior;
            }

            planetWaves.behaviors = beh;
        };
        recurring += tts => { };
        onLeave += () => { time = -1; };
        assetsToRead.Add(new("PlanetWaves", "1082b4e5556fa204399e09c8f302581d", RendererType.SKINNEDMESHRENDERER));
    }
}
