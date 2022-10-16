namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class WhiteWedding : TowerTask {
        public static TowerModel whiteWedding;
        private static int time = -1;
        public WhiteWedding() {
            identifier = "White Wedding";
            getTower = () => whiteWedding;
            baseTower = AddedTierName.WHITEWEDDING;
            tower = AddedTierEnum.WHITEWEDDING;
            requirements += tts => tts.tower.towerModel.baseId.Equals("SuperMonkey") && tts.tower.towerModel.tiers[2] == 5;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(whiteWedding);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                whiteWedding = gm.towers.First(a => a.name.Contains(AddedTierName.WHITEWEDDING)).CloneCast();

                whiteWedding.range = 150;
                whiteWedding.cost = 0;
                whiteWedding.name = "White Wedding";
                whiteWedding.baseId = "SuperMonkey";
                whiteWedding.display.guidRef = "WhiteWedding";
                whiteWedding.dontDisplayUpgrades = true;
                whiteWedding.portrait = new("WhiteWeddingIcon");
                whiteWedding.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display.guidRef = "WhiteWedding";
                var beh = whiteWedding.behaviors;
                ProjectileModel proj = null;
                for (var i = 0; i < beh.Length; i++) {
                    var behavior = beh[i];
                    if (behavior.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var am = behavior.Cast<AttackModel>();

                        am.behaviors = am.behaviors.Remove(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>());

                        for (var j = 0; j < am.weapons.Length; j++) {
                            am.weapons[j].projectile.display.guidRef = "WhiteWeddingProjectile";
                            am.weapons[j].projectile.pierce *= 5;
                            am.weapons[j].projectile.scale *= 1.25f;
                            am.weapons[j].rate = 0;
                            am.weapons[j].rateFrames = 0;

                            proj = am.weapons[j].projectile.Clone().Cast<ProjectileModel>();
                            proj.display.guidRef = "WhiteWeddingOrbitProjectile";
                            proj.pierce *= 10;
                            proj.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DamageModel>()).Cast<DamageModel>().damage *= 5;
                            proj.behaviors = proj.behaviors.Remove(a => a.GetIl2CppType() == Il2CppType.Of<TravelStraitModel>());

                            am.weapons[j].behaviors = am.weapons[j].behaviors.Remove(a => a.GetIl2CppType() == Il2CppType.Of<EjectEffectModel>());
                        }

                        am.range = 150;
                        beh[i] = am;
                    }

                    beh[i] = behavior;
                }

                whiteWedding.behaviors = beh.Add(new OrbitModel("OrbitModel_", proj, 3, 25));
            };
            recurring += tts => { };
            onLeave += () => { time = -1; };
            assetsToRead.Add(new("WhiteWedding", "e6c683076381222438dfc733a602c157", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("WhiteWeddingProjectile", "ae8cebf807b15984daf0219b66f42897", RendererType.SPRITERENDERER));
            assetsToRead.Add(new("WhiteWeddingOrbitProjectile", "e23d594d3bf5af44c8b1e2445fe10a9e", RendererType.SPRITERENDERER));
        }
    }
}