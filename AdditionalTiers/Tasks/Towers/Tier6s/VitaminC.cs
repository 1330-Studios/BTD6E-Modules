namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class VitaminC : TowerTask {
        public static TowerModel vitaminC;
        public static TowerModel totemVitaminC;
        private static int time = -1;
        public VitaminC() {
            identifier = "Vitamin C";
            getTower = () => vitaminC;
            baseTower = AddedTierName.VITAMINC;
            tower = AddedTierEnum.VITAMINC;
            requirements += tts => tts.tower.towerModel.baseId.Equals("SuperMonkey") && tts.tower.towerModel.tiers[0] == 5;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(vitaminC);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {

                totemVitaminC = gm.powers.First(a=>a.name.Contains("EnergisingTotem")).CloneCast().tower;
                totemVitaminC.SetDisplay("VitaminCTotem");
                totemVitaminC.ignoreTowerForSelection = true;

                var totemBeh = totemVitaminC.behaviors;

                for (int i = 0; i < totemVitaminC.behaviors.Length; i++) {
                    if (totemVitaminC.behaviors[i].Is<EnergisingTotemBehaviorModel>(out var etbm)) {
                        for (int j = 0; j < etbm.effectModels.Length; j++)
                            etbm.effectModels[j].assetId.guidRef = "VitaminCTotemParticles";
                    }
                    if (totemVitaminC.behaviors[i].Is<RateSupportModel>(out var rsm)) {
                        rsm.multiplier = 0.33f;
                    }
                }

                totemVitaminC.behaviors = totemBeh.Add(new TowerExpireModel("TEM_", 25, 1, true, true));

                var engi200spawner = gm.towers.First(a => a.name.Contains("EngineerMonkey-200")).CloneCast().behaviors.First(a => a.name.Contains("Spawner")).Cast<AttackModel>();
                engi200spawner.range = 100;
                engi200spawner.weapons[0].rate *= 3;
                engi200spawner.weapons[0].projectile.SetDisplay("VitaminCTotem");
                engi200spawner.weapons[0].projectile.behaviors.First(a => a.Is<CreateTowerModel>(out _)).Cast<CreateTowerModel>().tower = totemVitaminC;

                var spactory050abilityattack = gm.towers.First(a => a.name.Contains("SpikeFactory-250")).CloneCast().behaviors.First(a => a.Is<AbilityModel>(out _)).Cast<AbilityModel>().behaviors.First(a => a.Is<ActivateAttackModel>(out _)).Cast<ActivateAttackModel>().attacks[0];
                spactory050abilityattack.weapons[0].emission = new ArcEmissionModel("ArcEmissionModel_", 50, 0, 360, null, true);
                spactory050abilityattack.weapons[0].rate = 50;

                vitaminC = gm.towers.First(a => a.name.Contains(AddedTierName.VITAMINC)).CloneCast();

                vitaminC.range = 150;
                vitaminC.cost = 0;
                vitaminC.name = "Vitamin C";
                vitaminC.portrait = new("VitaminCIcon");
                vitaminC.baseId = "SuperMonkey";
                vitaminC.SetDisplay("");
                vitaminC.dontDisplayUpgrades = true;
                var beh = vitaminC.behaviors;
                for (var i = 0; i < beh.Length; i++) {
                    var behavior = beh[i];
                    if (behavior.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var am = behavior.Cast<AttackModel>();
                        am.behaviors.First(m => m.Is<DisplayModel>(out _)).Cast<DisplayModel>().display.guidRef = "VitaminC";

                        for (var j = 0; j < am.weapons.Length; j++) {
                            am.weapons[j].projectile.SetDisplay("VitaminCBlast");
                            am.weapons[j].emission = new AdoraEmissionModel("AdoraEmissionModel_", 3, 30, null);

                            for (int k = 0; k < am.weapons[j].projectile.behaviors.Length; k++) {
                                if (am.weapons[j].projectile.behaviors[k].Is<TravelStraitModel>(out var tsm)) {
                                    tsm.lifespan *= 1.5f;
                                }
                                if (am.weapons[j].projectile.behaviors[k].Is<WindModel>(out var wm)) {
                                    wm.chance = 0.25f;
                                    wm.affectMoab = true;
                                }
                            }
                            am.weapons[j].projectile.ModifyDamageModel(new DamageChange() { multiply = true, damage = 20000, immuneBloonProperties = BloonProperties.None });
                            am.weapons[j].projectile.behaviors = am.weapons[j].projectile.behaviors.Add(new AdoraTrackTargetModel("AdoraTrackTargetModel_", 9, 70, 360, 20, 1.5f, 5, 30));
                        }

                        beh[i] = am;
                    }

                    beh[i] = behavior;
                }

                vitaminC.behaviors = beh.Add(engi200spawner, spactory050abilityattack, new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true), new DisplayModel("dm", new("VitaminCTempleBase"), 0, new(), 1, true, 0));
            };
            recurring += tts => { };
            onLeave += () => { time = -1; };
            assetsToRead.Add(new("VitaminC", "45d40b8795fd36c4eadb7856f96a180c", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("VitaminCAvatar", "8f3b1daf26cefc34cbf78aa45210317b", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("VitaminCTotem", "65bf98ead18ff0643b31acfd2736ce57", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("VitaminCTotemParticles", "faf5bc32286af5a4fb2920dbc6d59458", RendererType.PARTICLESYSTEMRENDERER));
            assetsToRead.Add(new("VitaminCTempleBase", "e7bedca86ea05784fa030678549a9f79", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("VitaminCBlast", "dcd6cd8511c9a03458a32f42f860882c", RendererType.SPRITERENDERER));

        }
    }
}
