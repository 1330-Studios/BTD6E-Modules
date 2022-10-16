using Assets.Scripts;

namespace AdditionalTiers.Tasks.Towers.Tier6s;
internal class LittleTalks : TowerTask {
    public static TowerModel littleTalks;
    private static int time = -1;
    public LittleTalks() {
        identifier = "Little Talks";
        getTower = () => littleTalks;
        baseTower = AddedTierName.LITTLETALKS;
        tower = AddedTierEnum.LITTLETALKS;
        requirements += tts => tts.tower.towerModel.baseId.Equals("MonkeyBuccaneer") && tts.tower.towerModel.isParagon || tts.tower.towerModel.baseId.Equals("ParagonMonkeyBuccaneer");
        onComplete += tts => {
            if (time < 50) {
                time++;
                return;
            }
            tts.tower.worth = 0;
            var sim = tts.sim;
            var pos = new SV3(new Vector3(tts.position.x, -tts.position.z, tts.position.y));
            var input = sim.GetInputId();

            sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);

            InGame.instance.SellTower(tts);

            var cTower = sim.simulation.towerManager.CreateTower(littleTalks, pos, input, ObjectId.FromData(0), ObjectId.Invalid);

            TransformationManager.VALUE.Add(new(identifier, cTower.Id.Id));

            AbilityMenu.instance.RebuildAbilities();
        };
        gameLoad += gm => {
            littleTalks = gm.towers.First(a => a.name.Equals(baseTower)).CloneCast();

            littleTalks.range = 201.2F;
            littleTalks.radius = 0;
            littleTalks.radiusSquared = 0;
            littleTalks.cost = 0;
            littleTalks.name = baseTower;
            littleTalks.baseId = baseTower.Split('-')[0];
            littleTalks.dontDisplayUpgrades = true;
            littleTalks.SetDisplay("LittleTalks");
            littleTalks.SetIcons("LittleTalksGS");
            littleTalks.areaTypes = new[] { AreaType.land, AreaType.ice, AreaType.unplaceable, AreaType.track, AreaType.water };

            for (int i = 0; i < littleTalks.behaviors.Length; i++) {
                if (littleTalks.behaviors[i].Is<AttackModel>(out var am)) {
                    am.range = 201.2F;

                    for (int j = 0; j < am.weapons.Length; j++) {
                        var weapon = am.weapons[j];

                        weapon.projectile.ignoreBlockers = true;
                        weapon.projectile.ignorePierceExhaustion = true;

                        if (weapon.emission.Is<ArcEmissionModel>(out var aem)) {
                            aem.count += 2;

                            weapon.Rate = 0.1f;
                        }

                        if (weapon.emission.Is<ParallelEmissionModel>(out var pem)) {
                            pem.count += 2;

                            weapon.projectile.scale *= 0.75f;
                        }

                        if (weapon.emission.Is<SingleEmissionModel>(out var sem)) {
                            var proj = weapon.projectile;

                            weapon.behaviors.First(a => a.Is<SubTowerFilterModel>()).Cast<SubTowerFilterModel>().maxNumberOfSubTowers = 20;

                            var createdTower = proj.behaviors.First(a => a.Is<CreateTowerModel>()).Cast<CreateTowerModel>();

                            foreach (var ctb in createdTower.tower.behaviors) {
                                if (ctb.Is<AirUnitModel>(out var aum)) {
                                    aum.display.guidRef = "LittleTalksPlane";
                                }

                                if (ctb.Is<AttackModel>(out var ctam)) {
                                    var ctwm = ctam.weapons[0];

                                    if (ctwm.emission.Is<SingleEmissionModel>(out var ctsem)) {
                                        ctwm.projectile.display.guidRef = "LittleTalksPS";
                                        ctwm.projectile.ModifyDamageModel(new DamageChange { multiply = true, damage = 2012 });
                                        ctwm.Rate = 0.1f;

                                        var clonedWeapon = ctwm.CloneCast();

                                        clonedWeapon.Rate = 1f;

                                        clonedWeapon.projectile.display.guidRef = "LittleTalksPB";
                                        clonedWeapon.projectile.ModifyDamageModel(new DamageChange { multiply = true, damage = 20120 });
                                        clonedWeapon.projectile.behaviors = clonedWeapon.projectile.behaviors.Add(new CreateEffectOnContactModel("CreateEffectOnContactModel_",
                                            new EffectModel("9e1d07331a0c6634bbe805787ac37e9b", new("9e1d07331a0c6634bbe805787ac37e9b"), 1, 2)),
                                            new CreateSoundOnProjectileCollisionModel("CreateSoundOnProjectileCollisionModel_",
                                            new SoundModel("SoundModel_ExplosionSound01", new("c3f64a62b08dc3e4eb885cc534d8362f")),
                                            new SoundModel("SoundModel_ExplosionSound02", new("644009e031551ad449a36fb19be7e80c")),
                                            new SoundModel("SoundModel_ExplosionSound03", new("bc84177cfa4a5e449b1e765818a66ca3")),
                                            new SoundModel("SoundModel_ExplosionSound04", new("902b4a026f80e504c9e49cef5acefb51")),
                                            new SoundModel("SoundModel_ExplosionSound05", new("a1b6610be487ea747981e9b59b4ef51c"))));

                                        ctam.weapons = ctam.weapons.Add(clonedWeapon);
                                    }

                                    ctam.weapons[0] = ctwm;
                                }
                            }

                            weapon.projectile = proj;
                        }

                        weapon.projectile.ModifyDamageModel(new() { multiply = true, damage = 2012 });
                        if (!string.IsNullOrEmpty(weapon.projectile.display.guidRef))
                            if (weapon.projectile.display.guidRef.Contains("93c92bb185de39d4abd1343eebb7ae2b", StringComparison.OrdinalIgnoreCase))
                                weapon.projectile.display.guidRef = "LittleTalksS";
                            else if (weapon.projectile.display.guidRef.Contains("f49296f9618935141be5d00de95b1908", StringComparison.OrdinalIgnoreCase))
                                weapon.projectile.display.guidRef = "LittleTalksGS";

                        am.weapons[j] = weapon;
                    }
                }
            }

            littleTalks.behaviors = littleTalks.behaviors.Remove(a => a.Is<ParagonTowerModel>()).Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true).Cast<Model>(),
                new DisplayModel("DM_", new("9ed0d0de732cabe48898f8dddb7023ca"), 0, new(), 1, true, 0), new DisplayModel("dm", new("Assets/Monkeys/Adora/Graphics/Effects/AdoraSunBeamPlacement.prefab"), 0, new(), 1, true, 0));
        };
        recurring += _ => { };
        onLeave += () => time = -1;
        assetsToRead.Add(new("LittleTalks", "e4514c91f322a3c419ffeed58b1947e6", RendererType.SKINNEDMESHRENDERER));
        assetsToRead.Add(new("LittleTalksS", "Assets/Monkeys/MonkeyBuccaneer/Graphics/Projectiles/CannonBallParagon.prefab", RendererType.SPRITERENDERER));
        assetsToRead.Add(new("LittleTalksGS", "Assets/Monkeys/MonkeyBuccaneer/Graphics/Projectiles/GrapeShotParagon.prefab", RendererType.SPRITERENDERER));
        assetsToRead.Add(new("LittleTalksPlane", "bafddc9e96223c849b29709ce057e33f", RendererType.MESHRENDERER));
        assetsToRead.Add(new("LittleTalksPS", "87973be1157b06042b5063eed04df650", RendererType.SPRITERENDERER));
        assetsToRead.Add(new("LittleTalksPB", "Assets/Monkeys/HeliPilot/Graphics/projectiles/ComancheBomb.prefab", RendererType.SPRITERENDERER));
    }
}