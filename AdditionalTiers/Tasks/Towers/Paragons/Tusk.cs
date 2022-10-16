

namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class Tusk : TowerTask {
        public static TowerModel gds;
        private static int time = -1;
        public Tusk() {
            identifier = "Tusk";
            getTower = () => gds;
            baseTower = AddedTierName.TUSK;
            tower = AddedTierEnum.TUSK;
            requirements += tts => tts.tower.towerModel.baseId.Equals("BoomerangMonkey") && tts.tower.towerModel.isParagon || tts.tower.towerModel.baseId.Equals("ParagonBoomerangMonkey");
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(gds);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                gds = gm.towers.First(a => a.name.Contains(AddedTierName.TUSK)).CloneCast();

                gds.range += 50;
                gds.cost = 0;
                gds.dontDisplayUpgrades = true;
                gds.portrait = new("GlaiveDominusSilverIcon");
                gds.name = identifier;
                gds.SetDisplay("GlaiveDominusSilver");
                var beh = gds.behaviors;

                var mortarMonkeyCrt = gm.towers.First(a => a.name.Contains("MortarMonkey-002")).behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<AttackModel>()).CloneCast<AttackModel>().weapons[0].projectile.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<CreateProjectileOnExhaustFractionModel>()).Cast<CreateProjectileOnExhaustFractionModel>();
                List<OrbitModel> orbits = new();
                for (int i = 0; i < beh.Length; i++) {
                    if (beh[i].Is<OrbitModel>(out var orbitModel)) {
                        orbits = orbitModel.CloneOrbit(3, 5, 15, 25);
                        orbits[1].projectile.display.guidRef = "GlaiveDominusSilverOrbit3";
                        orbits[1].projectile.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display.guidRef = "GlaiveDominusSilverOrbit3";
                        orbits[1].projectile.AddDamageModel(DamageModelCreation.Full, 250);
                        orbits[0].projectile.display.guidRef = "GlaiveDominusSilverOrbit2";
                        orbits[0].projectile.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display.guidRef = "GlaiveDominusSilverOrbit2";
                        orbits[0].projectile.AddDamageModel(DamageModelCreation.Full, 100);
                        orbits[2].projectile.display.guidRef = "GlaiveDominusSilverOrbit";
                        orbits[2].projectile.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display.guidRef = "GlaiveDominusSilverOrbit";
                        orbits[2].projectile.AddDamageModel(DamageModelCreation.Full, 25);
                    }
                    if (beh[i].Is<AttackModel>(out var attackModel)) {
                        attackModel.weapons[0].projectile.display.guidRef = "358f76f3f21b8f9439f3cccd23c9b5ff";
                        attackModel.weapons[0].emission = new ArcEmissionModel("arcEmissionModel_", 3, 0, 30, null, false);
                        attackModel.weapons[0].projectile.behaviors = attackModel.weapons[0].projectile.behaviors.Add(mortarMonkeyCrt);
                        attackModel.weapons[0].projectile.ignorePierceExhaustion = true;
                        attackModel.range += 50;

                        attackModel.weapons[0].rate = 0;
                        attackModel.weapons[0].rateFrames = 0;

                        var projBeh = attackModel.weapons[0].projectile.behaviors;
                        for (int j = 0; j < projBeh.Length; j++) {
                            if (projBeh[j].Is<DamageModel>(out var damageModel)) {

                                projBeh[j] = damageModel;
                            }
                            if (projBeh[j].Is<RetargetOnContactModel>(out var retargetOnContactModel)) {
                                retargetOnContactModel.maxBounces *= 5;

                                projBeh[j] = retargetOnContactModel;
                            }
                        }
                    }
                }

                AbilityModel abm = gm.towers.FirstOrDefault(pat => pat.name.Contains("Pat") && pat.tier == 20).behaviors.FirstOrDefault(ab => ab.name.Contains("Rally")).Clone().Cast<AbilityModel>();
                abm.icon = new("GlaiveDominusSilverOrbit3");
                abm.name = "GlaiveDominusSilverBuff";
                abm.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<CreateEffectOnAbilityModel>()).Cast<CreateEffectOnAbilityModel>().effectModel =
                new("EM_", new("GlaiveDominusSilverAbility"), 1.1f, 9999999.0f, false, false, false, false, false, false, false);
                ActivateTowerDamageSupportZoneModel atdszm = abm.behaviors.FirstOrDefault(AA => AA.name.Contains("ActivateTowerDamageSupportZoneModel")).Cast<ActivateTowerDamageSupportZoneModel>();
                atdszm.damageIncrease = 50;
                atdszm.canEffectThisTower = true;
                atdszm.buffIconName = "GlaiveDominusSilverBuff";
                atdszm.buffLocsName = "GlaiveDominusSilverOrbit3";
                atdszm.showBuffIcon = true;
                atdszm.isGlobal = true;

                gm.buffIndicatorModels = gm.buffIndicatorModels.Add(new BuffIndicatorModel("bim_", "GlaiveDominusSilverBuff", "GlaiveDominusSilverOrbit3"));
                gds.RebuildBehaviorsA(a => a.Is<ParagonTowerModel>(out _) || a.Is<OrbitModel>(out _), abm, orbits[0], orbits[1], orbits[2], new DisplayModel("dm", new("Assets/Monkeys/Adora/Graphics/Effects/AdoraSunBeamPlacement.prefab"), 0, new(), 1, true, 0));
            };
            recurring += tts => { };
            onLeave += () => { time = -1; };
            assetsToRead.Add(new("GlaiveDominusSilver", "2ef4918ce220ca04d87a9afc8290a0c4", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("GlaiveDominusSilverOrbit", "0a990350818c21e4bb5039a00c8a4977", RendererType.SPRITERENDERER));
            assetsToRead.Add(new("GlaiveDominusSilverOrbit2", "0a990350818c21e4bb5039a00c8a4977", RendererType.SPRITERENDERER));
            assetsToRead.Add(new("GlaiveDominusSilverOrbit3", "0a990350818c21e4bb5039a00c8a4977", RendererType.SPRITERENDERER));
            assetsToRead.Add(new("GlaiveDominusSilverAbility", "a73a10bf47571424080d9e2b33bb2045", RendererType.PARTICLESYSTEMRENDERER));
        }
    }
}