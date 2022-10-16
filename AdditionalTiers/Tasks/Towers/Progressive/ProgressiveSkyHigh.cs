namespace AdditionalTiers.Tasks.Towers.Progressive;
internal class ProgressiveSkyHigh : TowerTask {
    public static TowerModel baseTwrMdl;
    private static int time = -1;
    public ProgressiveSkyHigh() {
        identifier = "Sky High";
        getTower = () => baseTwrMdl;
        baseTower = AddedTierName.SKYHIGH;
        tower = AddedTierEnum.SKYHIGH;
        requirements += tts => tts.tower.towerModel.baseId.Equals("DartMonkey") && tts.tower.towerModel.tiers[2] == 5;
        onComplete += tts => {
            if (time < 50) {
                time++;
                return;
            }
            TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
            tts.tower.worth = 0;
            tts.tower.UpdateRootModel(getTower());
            tts.tower.UpdatedModel(getTower());
            tts.TAdd(scale1: .01f);

            InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "This tower is special", "This tower upgrades a little bit differently. Every 200k pops you achieve with this tower, it will break through its limits and achieve new heights! Press 'OK' to explore this.");

            AbilityMenu.instance.TowerChanged(tts);
            AbilityMenu.instance.RebuildAbilities();
        };
        gameLoad += gm => {
            baseTwrMdl = gm.towers.First(a => a.name.Equals(baseTower)).CloneCast();
            baseTwrMdl.name = $"{identifier} Base";
            baseTwrMdl.SetDisplay("AdditionalTiers.Resources.V2.SkyHigh.SkyHigh_Base.png");
            baseTwrMdl.SetIcons("AdditionalTiers.Resources.V2.SkyHigh.SkyHighIcon.png");

            baseTwrMdl.range += 5;
            baseTwrMdl.dontDisplayUpgrades = true;

            for (int i = 0; i < baseTwrMdl.behaviors.Length; i++) {
                if (baseTwrMdl.behaviors[i].Is<AttackModel>(out var am)) {
                    am.range += 5;
                    for (int j = 0; j < am.weapons.Length; j++) {
                        var weapon = am.weapons[j];

                        weapon.projectile.pierce = 5000000;
                        weapon.projectile.display.guidRef = "AdditionalTiers.Resources.V2.SkyHigh.SkyHighProj_Base.png";

                        am.weapons[j] = weapon;
                    }
                }
            }
        };
        recurring += tts => {
            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Base") && tts!.tower!.damageDealt >= 200000) {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 1";
                changed.SetDisplay("AdditionalTiers.Resources.V2.SkyHigh.SkyHigh_T1.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++) {
                    if (changed.behaviors[i].Is<AttackModel>(out var am)) {
                        am.range += 5;
                        for (int j = 0; j < am.weapons.Length; j++) {
                            var weapon = am.weapons[j];

                            weapon.Rate *= 0.9f;
                            weapon.projectile.display.guidRef = "AdditionalTiers.Resources.V2.SkyHigh.SkyHighProj_T1.png";

                            am.weapons[j] = weapon;
                        }
                    }
                }

                /*var sim = tts.sim;
                var pos = new SV3(new Vector3(tts.position.x, -tts.position.z, tts.position.y));
                var input = sim.GetInputId();
                var newTower = tts.sim.simulation.towerManager.CreateTower(changed, pos, input, 2);
                newTower.damageDealt = tts.damageDealt;
                newTower.Rotation = tts.tower.Rotation;
                TransformationManager.VALUE.Replace(tts.tower, new(identifier, newTower.Id));
                InGame.instance.SellTower(tts);*/
                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Up! Tier 1", "This upgrade makes Sky High shoot faster, with laser stars.");
                return;
            }

            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Tier 1") && tts!.tower!.damageDealt >= 400000) {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 2";
                changed.SetDisplay("AdditionalTiers.Resources.V2.SkyHigh.SkyHigh_T2.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++) {
                    if (changed.behaviors[i].Is<AttackModel>(out var am)) {
                        am.range += 5;
                        for (int j = 0; j < am.weapons.Length; j++) {
                            var weapon = am.weapons[j];

                            weapon.emission = new RandomArcEmissionModel("RAEM_", 1, 0, 1, 15, 0, null);
                            weapon.Rate *= 0.8f;
                            weapon.projectile.display.guidRef = "AdditionalTiers.Resources.V2.SkyHigh.SkyHighProj_T2.png";

                            for (var k = 0; k < weapon.projectile.behaviors.Length; k++)
                                if (weapon.projectile.behaviors[k].Is<DamageModel>(out var dm))
                                    dm.damage *= 2;

                            am.weapons[j] = weapon;
                        }
                    }
                }

                /*var sim = tts.sim;
                var pos = new SV3(new Vector3(tts.position.x, -tts.position.z, tts.position.y));
                var input = sim.GetInputId();
                var newTower = tts.sim.simulation.towerManager.CreateTower(changed, pos, input, 2);
                newTower.damageDealt = tts.damageDealt;
                newTower.Rotation = tts.tower.Rotation;
                TransformationManager.VALUE.Replace(tts.tower, new(identifier, newTower.Id));
                InGame.instance.SellTower(tts);*/
                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Up! Tier 2", "This upgrade makes Sky High shoot even faster. This also doubles damage.");
                return;
            }

            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Tier 2") && tts!.tower!.damageDealt >= 600000) {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 3";
                changed.SetDisplay("AdditionalTiers.Resources.V2.SkyHigh.SkyHigh_T3.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++) {
                    if (changed.behaviors[i].Is<AttackModel>(out var am)) {
                        am.range += 5;

                        for (int j = 0; j < am.weapons.Length; j++) {
                            var weapon = am.weapons[j];

                            weapon.emission = new RandomArcEmissionModel("RAEM_", 1, 0, 1, 25, 0, null);
                            weapon.Rate *= 0.75f;
                            weapon.projectile.display.guidRef = "AdditionalTiers.Resources.V2.SkyHigh.SkyHighProj_T3.png";

                            for (var k = 0; k < weapon.behaviors.Length; k++)
                                if (weapon.behaviors[k].Is<CritMultiplierModel>(out var cmm)) {
                                    cmm.damage = 100;
                                    cmm.lower = 10;
                                    cmm.upper = 10;
                                }

                            for (var k = 0; k < weapon.projectile.behaviors.Length; k++)
                                if (weapon.projectile.behaviors[k].Is<DamageModel>(out var dm))
                                    dm.damage *= 4;

                            am.weapons[j] = weapon;
                        }
                    }
                }

                /*var sim = tts.sim;
                var pos = new SV3(new Vector3(tts.position.x, -tts.position.z, tts.position.y));
                var input = sim.GetInputId();
                var newTower = tts.sim.simulation.towerManager.CreateTower(changed, pos, input, 2);
                newTower.damageDealt = tts.damageDealt;
                newTower.Rotation = tts.tower.Rotation;
                TransformationManager.VALUE.Replace(tts.tower, new(identifier, newTower.Id));
                InGame.instance.SellTower(tts);*/
                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Up! Tier 3", "This upgrade makes Sky High shoot even faster, yet again." +
                    " This also quadruples damage. Crits deal 100x more damage.");
                return;
            }

            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Tier 3") && tts!.tower!.damageDealt >= 800000) {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 4";
                changed.SetDisplay("AdditionalTiers.Resources.V2.SkyHigh.SkyHigh_T4.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++) {
                    if (changed.behaviors[i].Is<AttackModel>(out var am)) {
                        am.range += 5;

                        for (int j = 0; j < am.weapons.Length; j++) {
                            var weapon = am.weapons[j];

                            weapon.emission = new RandomArcEmissionModel("RAEM_", 3, 0, 1, 15, 0, null);
                            weapon.Rate *= 0.75f;
                            weapon.projectile.display.guidRef = "AdditionalTiers.Resources.V2.SkyHigh.SkyHighProj_T4.png";

                            for (var k = 0; k < weapon.projectile.behaviors.Length; k++)
                                if (weapon.projectile.behaviors[k].Is<DamageModel>(out var dm))
                                    dm.damage *= 4;

                            am.weapons[j] = weapon;
                        }
                    }
                }

                /*var sim = tts.sim;
                var pos = new SV3(new Vector3(tts.position.x, -tts.position.z, tts.position.y));
                var input = sim.GetInputId();
                var newTower = tts.sim.simulation.towerManager.CreateTower(changed, pos, input, 2);
                newTower.damageDealt = tts.damageDealt;
                newTower.Rotation = tts.tower.Rotation;
                TransformationManager.VALUE.Replace(tts.tower, new(identifier, newTower.Id));
                InGame.instance.SellTower(tts);*/
                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Up! Tier 4", "This upgrade makes Sky High shoot even faster... again. " +
                    "This also quadruples damage, again. Sky High shoots out 3 projectiles at once!");
                return;
            }

            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Tier 4") && tts!.tower!.damageDealt >= 1000000) {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 5";
                changed.SetDisplay("AdditionalTiers.Resources.V2.SkyHigh.SkyHigh_T5.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++) {
                    if (changed.behaviors[i].Is<AttackModel>(out var am)) {
                        am.range += 5;

                        for (int j = 0; j < am.weapons.Length; j++) {
                            var weapon = am.weapons[j];

                            weapon.Rate *= 0.8f;
                            weapon.projectile.display.guidRef = "AdditionalTiers.Resources.V2.SkyHigh.SkyHighProj_T5.png";

                            for (var k = 0; k < weapon.projectile.behaviors.Length; k++)
                                if (weapon.projectile.behaviors[k].Is<DamageModel>(out var dm))
                                    dm.damage *= 4;
                                else if (weapon.projectile.behaviors[k].Is<TravelStraitModel>(out var tsm))
                                    tsm.Speed *= 2;

                            am.weapons[j] = weapon;
                        }
                    }
                }

                /*var sim = tts.sim;
                var pos = new SV3(new Vector3(tts.position.x, -tts.position.z, tts.position.y));
                var input = sim.GetInputId();
                var newTower = tts.sim.simulation.towerManager.CreateTower(changed, pos, input, 2);
                newTower.damageDealt = tts.damageDealt;
                newTower.Rotation = tts.tower.Rotation;
                TransformationManager.VALUE.Replace(tts.tower, new(identifier, newTower.Id));
                InGame.instance.SellTower(tts);*/
                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Up! Tier 5", "This upgrade makes Sky High shoot faster... again... " +
                    "This also quadruples damage... again. Projectiles move twice as fast.");
                return;
            }

            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Tier 5") && tts!.tower!.damageDealt >= 1200000) {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 6";
                changed.SetDisplay("AdditionalTiers.Resources.V2.SkyHigh.SkyHigh_T6.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++) {
                    if (changed.behaviors[i].Is<AttackModel>(out var am)) {
                        am.range += 5;

                        for (int j = 0; j < am.weapons.Length; j++) {
                            var weapon = am.weapons[j];

                            weapon.Rate *= 0.8f;
                            weapon.projectile.display.guidRef = "AdditionalTiers.Resources.V2.SkyHigh.SkyHighProj_T6.png";

                            for (var k = 0; k < weapon.projectile.behaviors.Length; k++)
                                if (weapon.projectile.behaviors[k].Is<DamageModel>(out var dm))
                                    dm.damage *= 4;

                            weapon.projectile.behaviors = weapon.projectile.behaviors.Add(new AdoraTrackTargetModel("AdoraTrackTargetModel_", 9, 70, 360, 20, 1.5f, 5, 30));

                            am.weapons[j] = weapon;
                        }
                    }
                }

                /*var sim = tts.sim;
                var pos = new SV3(new Vector3(tts.position.x, -tts.position.z, tts.position.y));
                var input = sim.GetInputId();
                var newTower = tts.sim.simulation.towerManager.CreateTower(changed, pos, input, 2);
                newTower.damageDealt = tts.damageDealt;
                newTower.Rotation = tts.tower.Rotation;
                TransformationManager.VALUE.Replace(tts.tower, new(identifier, newTower.Id));
                InGame.instance.SellTower(tts);*/
                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Up! Tier 6", "This upgrade makes Sky High shoot faster... again... yeah... " +
                    "This also quadruples damage... again... Projectiles seek out bloons. To unlock the next and last upgrade, reach 2 million pops!");
                return;
            }

            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Tier 6") && tts!.tower!.damageDealt >= 2000000) {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 7";
                changed.SetDisplay("AdditionalTiers.Resources.V2.SkyHigh.SkyHigh_T7.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++) {
                    if (changed.behaviors[i].Is<AttackModel>(out var am)) {
                        am.range += 5;

                        for (int j = 0; j < am.weapons.Length; j++) {
                            var weapon = am.weapons[j];

                            weapon.emission = new RandomArcEmissionModel("RAEM_", 5, 0, 1, 10, 0, null);
                            weapon.Rate = 0.01f;
                            weapon.projectile.display.guidRef = "AdditionalTiers.Resources.V2.SkyHigh.SkyHighProj_T7.png";

                            for (var k = 0; k < weapon.projectile.behaviors.Length; k++)
                                if (weapon.projectile.behaviors[k].Is<DamageModel>(out var dm))
                                    dm.damage *= 50;

                            weapon.projectile.behaviors = weapon.projectile.behaviors.Add(new CreateEffectOnContactModel("CreateEffectOnContactModel_",
                                new EffectModel("Explosion", new("6d84b13b7622d2744b8e8369565bc058"), 1, 1)),
                                new DamageModifierForTagModel("DamageModifierForTagModel_", "Moabs", 4, 0, false, true), new PushBackModel("PushBackModel_", 1, "Moabs", 0.25f, 0.33f, 0.15f));

                            am.weapons[j] = weapon;
                        }
                    }
                }

                changed.behaviors = changed.behaviors.Add(new PerRoundCashBonusTowerModel("PRCBTM_", 50000, 1, 10, new("CashText"), true));

                /*var sim = tts.sim;
                var pos = new SV3(new Vector3(tts.position.x, -tts.position.z, tts.position.y));
                var input = sim.GetInputId();
                var newTower = tts.sim.simulation.towerManager.CreateTower(changed, pos, input, 2);
                newTower.damageDealt = tts.damageDealt;
                newTower.Rotation = tts.tower.Rotation;
                TransformationManager.VALUE.Replace(tts.tower, new(identifier, newTower.Id));
                InGame.instance.SellTower(tts);*/
                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Max! Tier 7", "This upgrade makes Sky High shoot as fast as possible! " +
                    "Damage is multiplied by 50! 5 projectiles are sent out at a time. Projectiles explode on contact. MOAB class bloons take 4x damage. Sky High gains extra " +
                    "cash at the end of the round. Attacks push back moabs.");
                return;
            }
        };
        onLeave += () => time = -1;
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.SkyHigh.SkyHigh_Base.png", "f7a1b5c14ded01146b80bd7121f3fcd7", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.SkyHigh.SkyHigh_T1.png", "f7a1b5c14ded01146b80bd7121f3fcd7", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.SkyHigh.SkyHigh_T2.png", "f7a1b5c14ded01146b80bd7121f3fcd7", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.SkyHigh.SkyHigh_T3.png", "f7a1b5c14ded01146b80bd7121f3fcd7", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.SkyHigh.SkyHigh_T4.png", "f7a1b5c14ded01146b80bd7121f3fcd7", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.SkyHigh.SkyHigh_T5.png", "f7a1b5c14ded01146b80bd7121f3fcd7", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.SkyHigh.SkyHigh_T6.png", "f7a1b5c14ded01146b80bd7121f3fcd7", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.SkyHigh.SkyHigh_T7.png", "f7a1b5c14ded01146b80bd7121f3fcd7", RendererType.SKINNEDMESHRENDERER));

        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.SkyHigh.SkyHighProj_Base.png", "ae8cebf807b15984daf0219b66f42897", RendererType.SPRITERENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.SkyHigh.SkyHighProj_T1.png", "ae8cebf807b15984daf0219b66f42897", RendererType.SPRITERENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.SkyHigh.SkyHighProj_T2.png", "ae8cebf807b15984daf0219b66f42897", RendererType.SPRITERENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.SkyHigh.SkyHighProj_T3.png", "ae8cebf807b15984daf0219b66f42897", RendererType.SPRITERENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.SkyHigh.SkyHighProj_T4.png", "ae8cebf807b15984daf0219b66f42897", RendererType.SPRITERENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.SkyHigh.SkyHighProj_T5.png", "ae8cebf807b15984daf0219b66f42897", RendererType.SPRITERENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.SkyHigh.SkyHighProj_T6.png", "ae8cebf807b15984daf0219b66f42897", RendererType.SPRITERENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.SkyHigh.SkyHighProj_T7.png", "ae8cebf807b15984daf0219b66f42897", RendererType.SPRITERENDERER));
    }
}