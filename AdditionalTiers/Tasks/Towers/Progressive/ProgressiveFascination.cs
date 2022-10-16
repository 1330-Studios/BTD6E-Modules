namespace AdditionalTiers.Tasks.Towers.Progressive;
internal class ProgressiveFascination : TowerTask {
    public static TowerModel baseTwrMdl;
    private static int time = -1;
    public ProgressiveFascination() {
        identifier = "Fascination";
        getTower = () => baseTwrMdl;
        baseTower = AddedTierName.FASCINATION;
        tower = AddedTierEnum.FASCINATION;
        requirements += tts => tts.tower.towerModel.baseId.Equals("BoomerangMonkey") && tts.tower.towerModel.tiers[1] == 5;
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
            baseTwrMdl.SetDisplay("AdditionalTiers.Resources.V2.Fascination.Fascination_T0.png");
            baseTwrMdl.SetIcons("AdditionalTiers.Resources.V2.Fascination.Fascination_ProjT0.png");

            baseTwrMdl.range += 5;
            baseTwrMdl.dontDisplayUpgrades = true;

            for (int i = 0; i < baseTwrMdl.behaviors.Length; i++) {
                if (baseTwrMdl.behaviors[i].Is<AttackModel>(out var am)) {
                    am.range += 5;
                    for (int j = 0; j < am.weapons.Length; j++) {
                        var weapon = am.weapons[j];

                        weapon.projectile.pierce = 5000000;
                        weapon.projectile.display.guidRef = "AdditionalTiers.Resources.V2.Fascination.Fascination_ProjT0.png";

                        am.weapons[j] = weapon;
                    }
                }
            }
        };
        recurring += tts => {
            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Base") && tts!.tower!.damageDealt >= 200000) {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 1";
                changed.SetDisplay("AdditionalTiers.Resources.V2.Fascination.Fascination_T1.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++) {
                    if (changed.behaviors[i].Is<AttackModel>(out var am)) {
                        am.range += 5;
                        for (int j = 0; j < am.weapons.Length; j++) {
                            var weapon = am.weapons[j];

                            weapon.emission = new ArcEmissionModel("AEM_", 2, 0, 35, null, false);
                            weapon.projectile.display.guidRef = "AdditionalTiers.Resources.V2.Fascination.Fascination_ProjT1.png";

                            am.weapons[j] = weapon;
                        }
                    }
                }

                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Up! Tier 1", "This upgrade makes Fascination shoot 2 shurikens at once. Next tier at 400 thousand pops.");
                return;
            }

            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Tier 1") && tts!.tower!.damageDealt >= 400000) {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 2";
                changed.SetDisplay("AdditionalTiers.Resources.V2.Fascination.Fascination_T2.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++) {
                    if (changed.behaviors[i].Is<AttackModel>(out var am)) {
                        am.range += 5;
                        for (int j = 0; j < am.weapons.Length; j++) {
                            var weapon = am.weapons[j];

                            weapon.Rate /= 2;
                            weapon.projectile.display.guidRef = "AdditionalTiers.Resources.V2.Fascination.Fascination_ProjT2.png";

                            weapon.projectile.behaviors = weapon.projectile.behaviors.Add(new DamageModifierForTagModel("DamageModifierForTagModel_", "Moabs", 4, 0, false, true),
                                new DamageModifierForTagModel("DamageModifierForTagModel_", "Ceramic", 2, 0, false, true));

                            am.weapons[j] = weapon;
                        }
                    }
                }

                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Up! Tier 2", "This upgrade makes Fascination shoot twice as fast. Double damage to Ceramic bloons and quadruple damage to MOAB class bloons. Next tier at 600 thousand pops.");
                return;
            }

            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Tier 2") && tts!.tower!.damageDealt >= 600000) {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 3";
                changed.SetDisplay("AdditionalTiers.Resources.V2.Fascination.Fascination_T3.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++) {
                    if (changed.behaviors[i].Is<AttackModel>(out var am)) {
                        am.range += 5;

                        for (int j = 0; j < am.weapons.Length; j++) {
                            var weapon = am.weapons[j];

                            weapon.Rate /= 3;
                            weapon.projectile.display.guidRef = "AdditionalTiers.Resources.V2.Fascination.Fascination_ProjT3.png";
                            weapon.projectile.scale *= 2;
                            weapon.projectile.radius *= 2;

                            for (var k = 0; k < weapon.projectile.behaviors.Length; k++)
                                if (weapon.projectile.behaviors[k].Is<DamageModel>(out var dm))
                                    dm.damage *= 2;

                            am.weapons[j] = weapon;
                        }
                    }
                }

                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Up! Tier 3", "This upgrade makes Fascination shoot 3 times as fast." +
                    " Overall double damage. 2 times larger hitbox for projectiles. Next tier at 800 thousand pops.");
                return;
            }

            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Tier 3") && tts!.tower!.damageDealt >= 800000) {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 4";
                changed.SetDisplay("AdditionalTiers.Resources.V2.Fascination.Fascination_T4.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++) {
                    if (changed.behaviors[i].Is<AttackModel>(out var am)) {
                        am.range += 5;

                        for (int j = 0; j < am.weapons.Length; j++) {
                            var weapon = am.weapons[j];

                            weapon.projectile.scale = 1.5f;
                            weapon.emission = new ArcEmissionModel("AEM_", 3, 0, 55, null, false);
                            weapon.projectile.display.guidRef = "AdditionalTiers.Resources.V2.Fascination.Fascination_ProjT4.png";

                            for (var k = 0; k < weapon.projectile.behaviors.Length; k++)
                                if (weapon.projectile.behaviors[k].Is<DamageModel>(out var dm))
                                    dm.damage *= 2;

                            weapon.projectile.behaviors = weapon.projectile.behaviors.Add(new DamageModifierForTagModel("DamageModifierForTagModel_", "Ddt", 4, 0, false, true));

                            am.weapons[j] = weapon;
                        }
                    }
                }

                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Up! Tier 4", "Fascination nears the limits of current technology. " +
                    "Double overall damage and Quadruple damage to DDTs. Fascination launches 3 glaives at once. Next tier at 1 million pops.");
                return;
            }

            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Tier 4") && tts!.tower!.damageDealt >= 1000000) {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 5";
                changed.SetDisplay("AdditionalTiers.Resources.V2.Fascination.Fascination_T5.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++) {
                    if (changed.behaviors[i].Is<AttackModel>(out var am)) {
                        am.range += 5;

                        for (int j = 0; j < am.weapons.Length; j++) {
                            var weapon = am.weapons[j];

                            weapon.emission = new ArcEmissionModel("AEM_", 5, 0, 75, null, false);
                            weapon.projectile.display.guidRef = "AdditionalTiers.Resources.V2.Fascination.Fascination_ProjT5.png";

                            for (var k = 0; k < weapon.projectile.behaviors.Length; k++)
                                if (weapon.projectile.behaviors[k].Is<DamageModel>(out var dm))
                                    dm.damage *= 2;
                                else if (weapon.projectile.behaviors[k].Is<FollowPathModel>(out var fpm))
                                    fpm.Speed *= 2;

                            am.weapons[j] = weapon;
                        }
                    }
                }

                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Up! Tier 5", "This upgrade makes Fascination launch 5 glaives at once! " +
                    "Double overall damage. Glaives move at twice the speed. Next tier at 1.2 million pops.");
                return;
            }

            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Tier 5") && tts!.tower!.damageDealt >= 1200000) {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 6";
                changed.SetDisplay("AdditionalTiers.Resources.V2.Fascination.Fascination_T6.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++) {
                    if (changed.behaviors[i].Is<AttackModel>(out var am)) {
                        am.range += 5;

                        for (int j = 0; j < am.weapons.Length; j++) {
                            var weapon = am.weapons[j];

                            weapon.Rate /= 4;
                            weapon.projectile.display.guidRef = "AdditionalTiers.Resources.V2.Fascination.Fascination_ProjT6.png";

                            for (var k = 0; k < weapon.projectile.behaviors.Length; k++)
                                if (weapon.projectile.behaviors[k].Is<DamageModel>(out var dm))
                                    dm.damage *= 4;

                            weapon.projectile.behaviors = weapon.projectile.behaviors.Add(new DamageModifierForTagModel("DamageModifierForTagModel_", "Bad", 10, 0, false, true));

                            am.weapons[j] = weapon;
                        }
                    }
                }

                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Up! Tier 6", "Fascination shoots glaives 4 times as fast. " +
                    "Quadrupled overall damage. BADs take 10 times more damage. Next tier at 2 million pops.");
                return;
            }

            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Tier 6") && tts!.tower!.damageDealt >= 2000000) {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 7";
                changed.SetDisplay("AdditionalTiers.Resources.V2.Fascination.Fascination_T7.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++) {
                    if (changed.behaviors[i].Is<AttackModel>(out var am)) {
                        am.range += 5;

                        for (int j = 0; j < am.weapons.Length; j++) {
                            var weapon = am.weapons[j];

                            weapon.Rate = 0.01f;
                            weapon.projectile.display.guidRef = "AdditionalTiers.Resources.V2.Fascination.Fascination_ProjT7.png";

                            for (var k = 0; k < weapon.projectile.behaviors.Length; k++)
                                if (weapon.projectile.behaviors[k].Is<DamageModel>(out var dm))
                                    dm.damage *= 75;

                            am.weapons[j] = weapon;
                        }
                    }
                }

                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Up! Tier 7", "This upgrade makes Fascination shoot as fast as possible! Damage is multiplied by 75! Next and last tier at 5 million pops.");
            }

            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Tier 7") && tts!.tower!.damageDealt >= 5000000) {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 8";
                changed.SetDisplay("AdditionalTiers.Resources.V2.Fascination.Fascination_T8.png");

                changed.range += 25;

                for (int i = 0; i < changed.behaviors.Length; i++) {
                    if (changed.behaviors[i].Is<AttackModel>(out var am)) {
                        am.range += 25;

                        for (int j = 0; j < am.weapons.Length; j++) {
                            var weapon = am.weapons[j];

                            weapon.projectile.scale = 1.5f;
                            weapon.projectile.radius *= 2;
                            weapon.projectile.display.guidRef = "AdditionalTiers.Resources.V2.Fascination.Fascination_ProjT8.png";

                            for (var k = 0; k < weapon.projectile.behaviors.Length; k++)
                                if (weapon.projectile.behaviors[k].Is<DamageModel>(out var dm))
                                    dm.damage *= 15;

                            weapon.projectile.behaviors = weapon.projectile.behaviors.Add(new CreateEffectOnContactModel("CreateEffectOnContactModel_",
                                new EffectModel("Explosion", new("6d84b13b7622d2744b8e8369565bc058"), 1, 1)), new PushBackModel("PushBackModel_", 1, "Moabs", 0.25f, 0.33f, 0.15f));

                            am.weapons[j] = weapon;
                        }
                    }
                }

                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Max! Tier 8", "Maximum Fascination. Damage is multiplied by 15! Projectiles explode on contact. Attacks push back moabs. Projectile hitbox size is doubled.");
            }
        };
        onLeave += () => time = -1;
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.Fascination.Fascination_T0.png", "6b4ef3869b1e66b45b7e3c983c47abd9", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.Fascination.Fascination_T1.png", "6b4ef3869b1e66b45b7e3c983c47abd9", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.Fascination.Fascination_T2.png", "6b4ef3869b1e66b45b7e3c983c47abd9", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.Fascination.Fascination_T3.png", "6b4ef3869b1e66b45b7e3c983c47abd9", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.Fascination.Fascination_T4.png", "6b4ef3869b1e66b45b7e3c983c47abd9", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.Fascination.Fascination_T5.png", "6b4ef3869b1e66b45b7e3c983c47abd9", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.Fascination.Fascination_T6.png", "6b4ef3869b1e66b45b7e3c983c47abd9", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.Fascination.Fascination_T7.png", "6b4ef3869b1e66b45b7e3c983c47abd9", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.Fascination.Fascination_T8.png", "6b4ef3869b1e66b45b7e3c983c47abd9", RendererType.SKINNEDMESHRENDERER));

        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.Fascination.Fascination_ProjT0.png", "23ee303384f0ac543ace7a301829ea88", RendererType.SPRITERENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.Fascination.Fascination_ProjT1.png", "23ee303384f0ac543ace7a301829ea88", RendererType.SPRITERENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.Fascination.Fascination_ProjT2.png", "23ee303384f0ac543ace7a301829ea88", RendererType.SPRITERENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.Fascination.Fascination_ProjT3.png", "23ee303384f0ac543ace7a301829ea88", RendererType.SPRITERENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.Fascination.Fascination_ProjT4.png", "23ee303384f0ac543ace7a301829ea88", RendererType.SPRITERENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.Fascination.Fascination_ProjT5.png", "23ee303384f0ac543ace7a301829ea88", RendererType.SPRITERENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.Fascination.Fascination_ProjT6.png", "23ee303384f0ac543ace7a301829ea88", RendererType.SPRITERENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.Fascination.Fascination_ProjT7.png", "23ee303384f0ac543ace7a301829ea88", RendererType.SPRITERENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.Fascination.Fascination_ProjT8.png", "23ee303384f0ac543ace7a301829ea88", RendererType.SPRITERENDERER));
    }
}