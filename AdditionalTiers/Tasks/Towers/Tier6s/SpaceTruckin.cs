namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class SpaceTruckin : TowerTask {
        public static TowerModel spaceTruckin;
        private static int time = -1;
        public SpaceTruckin() {
            identifier = "Space Truckin";
            getTower = () => spaceTruckin;
            baseTower = AddedTierName.SPACETRUCKIN;
            tower = AddedTierEnum.SPACETRUCKIN;
            requirements += tts => tts.tower.towerModel.baseId.Equals("MortarMonkey") && tts.tower.towerModel.tiers[0] == 5;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(spaceTruckin);
                tts.TAdd(true, 1.2f);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                spaceTruckin = gm.towers.First(a => a.name.Contains(AddedTierName.SPACETRUCKIN)).CloneCast();

                spaceTruckin.range = 150;
                spaceTruckin.cost = 0;
                spaceTruckin.name = "Space Truckin";
                spaceTruckin.baseId = "MortarMonkey";
                spaceTruckin.SetDisplay("SpaceTruckin");
                spaceTruckin.dontDisplayUpgrades = true;
                spaceTruckin.portrait = new("SpaceTruckinIcon");
                var beh = spaceTruckin.behaviors;
                for (var i = 0; i < beh.Length; i++) {
                    var behavior = beh[i];
                    if (behavior.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var am = behavior.Cast<AttackModel>();

                        am.behaviors = am.behaviors.Remove(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>());

                        for (var j = 0; j < am.weapons.Length; j++) {
                            am.weapons[j].projectile.pierce *= 5;
                            am.weapons[j].rate = 0;
                            am.weapons[j].rateFrames = 0;
                            am.weapons[j].emission.Cast<RandomTargetSpreadModel>().spread = 50;

                            am.weapons[j].projectile.AddDamageModel(DamageModelCreation.Full, 7500);

                            foreach (var pbeh in am.weapons[j].projectile.behaviors) {
                                if (pbeh.Is<CreateProjectileOnExhaustFractionModel>(out var cpoefm)) {
                                    cpoefm.projectile.ModifyDamageModel(new DamageChange() { multiply = true, damage = 50 });
                                }
                            }
                        }

                        am.weapons = am.weapons.Add(am.weapons[0].CloneCast(), am.weapons[0].CloneCast());

                        am.range = 150;
                        beh[i] = am;
                    }

                    beh[i] = behavior;
                }

                spaceTruckin.behaviors = beh.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true));
            };
            recurring += _ => { };
            onLeave += () => time = -1;
            assetsToRead.Add(new("SpaceTruckin", "944324f406a663f42877ec47edaa9da3", RendererType.SKINNEDMESHRENDERER));
        }
    }
}