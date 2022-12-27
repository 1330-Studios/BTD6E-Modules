namespace AdditionalTiers.Tasks.Towers;
internal class GoldenExperience : TowerTask
{
    public static TowerModel baseTwrMdl;
    private static TowerModel druid200;
    private static TowerModel supermonkey050;
    private static int time = -1;
    public GoldenExperience()
    {
        identifier = "Golden Experience";
        getTower = () => baseTwrMdl;
        baseTower = AddedTierName.THEGOLDEXPERIENCE;
        tower = AddedTierEnum.THEGOLDEXPERIENCE;
        requirements += tts => tts.tower.towerModel.baseId.Equals("DartMonkey") && tts.tower.towerModel.isParagon || tts.tower.towerModel.baseId.Equals("ParagonDartMonkey");
        onComplete += tts =>
        {
            if (time < 50)
            {
                time++;
                return;
            }
            TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
            tts.tower.worth = 0;
            tts.tower.UpdateRootModel(getTower());
            tts.tower.UpdatedModel(getTower());
            tts.TAdd(scale1: .01f);

            InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "This tower is special", "This tower upgrades a little bit differently. Every 500k pops you achieve with this tower, it will break through its limits and achieve new heights! Press 'OK' to explore this.");

            AbilityMenu.instance.TowerChanged(tts);
            AbilityMenu.instance.RebuildAbilities();
        };
        gameLoad += gm =>
        {
            druid200 = gm.towers.First(a => a.name.Equals("Druid-200")).CloneCast();
            supermonkey050 = gm.towers.First(a => a.name.Equals("SuperMonkey-050")).CloneCast();

            baseTwrMdl = gm.towers.First(a => a.name.Equals(baseTower)).CloneCast();
            baseTwrMdl.name = $"{identifier} Base";
            baseTwrMdl.SetDisplay("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T0.png");
            baseTwrMdl.SetIcons("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_Icon.png");

            baseTwrMdl.range += 5;
            baseTwrMdl.dontDisplayUpgrades = true;

            for (int i = 0; i < baseTwrMdl.behaviors.Length; i++)
            {
                if (baseTwrMdl.behaviors[i].Is<AttackModel>(out var am))
                {
                    am.behaviors.First(a => a.Is<DisplayModel>()).Cast<DisplayModel>().display = new() { guidRef = "AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T0.png " };
                    am.range += 5;

                    foreach (var weapon in am.weapons)
                    {
                        weapon.emission = new SingleEmissionModel("SEM_", null);
                        weapon.Rate = 0.5f;
                        weapon.projectile.ModifyDamageModel(new DamageChange() { multiply = true, damage = 20 });

                        foreach (var pbeh in weapon.projectile.behaviors)
                        {
                            if (pbeh.Is<CreateProjectileOnExhaustFractionModel>(out var cpoefm))
                            {
                                cpoefm.emission.Cast<ArcEmissionModel>().count = 7;
                            }
                        }
                    }
                }
            }
        };
        recurring += tts =>
        {
            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Base") && tts!.tower!.damageDealt >= 1_500_000)
            {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 1";
                changed.SetDisplay("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T1.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++)
                {
                    if (changed.behaviors[i].Is<AttackModel>(out var am))
                    {
                        am.behaviors.First(a => a.Is<DisplayModel>()).Cast<DisplayModel>().display = new() { guidRef = "AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T1.png " };
                        am.range += 5;

                        foreach (var weapon in am.weapons)
                        {
                            weapon.Rate = 0.4f;
                        }
                    }
                }

                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Up! Tier 1", "The process has started. This upgrade makes Golden Experience launch more juggernaut balls per second. Next tier at 2m pops.");
                return;
            }

            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Tier 1") && tts!.tower!.damageDealt >= 2_000_000)
            {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 2";
                changed.SetDisplay("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T2.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++)
                {
                    if (changed.behaviors[i].Is<AttackModel>(out var am))
                    {
                        am.behaviors.First(a => a.Is<DisplayModel>()).Cast<DisplayModel>().display = new() { guidRef = "AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T2.png " };
                        am.range += 5;

                        foreach (var weapon in am.weapons)
                        {
                            weapon.Rate = 0.3f;

                            weapon.projectile.ModifyDamageModel(new DamageChange() { multiply = true, damage = 2 });
                        }
                    }
                }

                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Up! Tier 2", "It's too late to stop it. Double damage overall. Faster fire rate. Next tier at 2.5m pops.");
                return;
            }

            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Tier 2") && tts!.tower!.damageDealt >= 2_500_000)
            {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 3";
                changed.SetDisplay("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T3.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++)
                {
                    if (changed.behaviors[i].Is<AttackModel>(out var am))
                    {
                        am.behaviors.First(a => a.Is<DisplayModel>()).Cast<DisplayModel>().display = new() { guidRef = "AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T3.png " };
                        am.range += 5;

                        foreach (var weapon in am.weapons)
                        {
                            weapon.Rate = 0.2f;
                            weapon.projectile.ModifyDamageModel(new DamageChange() { multiply = true, damage = 4 });

                            weapon.projectile.behaviors = weapon.projectile.behaviors.Add(new CreateProjectileOnIntervalModel("CPOIM_",
                                druid200.behaviors.First(a => a.Is<AttackModel>()).Cast<AttackModel>().weapons.First(a => a.name.Contains("Lightning", StringComparison.OrdinalIgnoreCase)).projectile,
                                new ArcEmissionModel("AEM_", 5, 0, 360, null, false), 5, true, 30, "first"));
                        }
                    }
                }

                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Up! Tier 3", "Let it consume you. Quadruple damage. Faster fire rate. Lightning sub-projectiles. Next tier at 3m pops.");
                return;
            }

            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Tier 3") && tts!.tower!.damageDealt >= 3_000_000)
            {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 4";
                changed.SetDisplay("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T4.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++)
                {
                    if (changed.behaviors[i].Is<AttackModel>(out var am))
                    {
                        am.behaviors.First(a => a.Is<DisplayModel>()).Cast<DisplayModel>().display = new() { guidRef = "AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T4.png " };
                        am.range += 5;

                        foreach (var weapon in am.weapons)
                        {
                            weapon.Rate = 0.2f;

                            weapon.projectile.SetDisplay("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_UProj.png");
                            weapon.projectile.ModifyDamageModel(new DamageChange() { multiply = true, damage = 5 });

                            var lightning = druid200.behaviors.First(a => a.Is<AttackModel>()).Cast<AttackModel>().weapons.First(a => a.name.Contains("Lightning", StringComparison.OrdinalIgnoreCase)).projectile.CloneCast();
                            lightning.ModifyDamageModel(new DamageChange() { set = true, damage = 0.25f });

                            foreach (var pbeh in weapon.projectile.behaviors)
                            {
                                if (pbeh.Is<CreateProjectileOnExhaustFractionModel>(out var cpoefm))
                                {
                                    cpoefm.projectile.SetDisplay("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_Proj.png");
                                    cpoefm.projectile.behaviors = cpoefm.projectile.behaviors.Add(new CreateProjectileOnIntervalModel("CPOIM_", lightning,
                                                                                                  new SingleEmissionModel("SEM_", null), 5, true, 25, "first"));
                                }
                            }
                        }
                    }
                }

                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Up! Tier 4", "It is complete, take it further though... Quintuple damage. Lightning sub-projectiles on the sub-projectiles. Next tier at 3.5m pops.");
                return;
            }

            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Tier 4") && tts!.tower!.damageDealt >= 3_500_000)
            {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 5";
                changed.SetDisplay("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T5.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++)
                {
                    if (changed.behaviors[i].Is<AttackModel>(out var am))
                    {
                        am.behaviors.First(a => a.Is<DisplayModel>()).Cast<DisplayModel>().display = new() { guidRef = "AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T5.png " };
                        am.range += 5;

                        foreach (var weapon in am.weapons)
                        {
                            weapon.Rate = 0.1f;

                            weapon.emission = new ParallelEmissionModel("PEM_", 3, 15, -15, false, null);

                            weapon.projectile.SetDisplay("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_UProj2.png");
                            weapon.projectile.ModifyDamageModel(new DamageChange() { multiply = true, damage = 10 });

                            foreach (var pbeh in weapon.projectile.behaviors)
                            {
                                if (pbeh.Is<CreateProjectileOnExhaustFractionModel>(out var cpoefm))
                                {
                                    cpoefm.projectile.SetDisplay("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_Proj2.png");
                                }
                            }
                        }
                    }
                }

                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Up! Tier 5", "The darkness took hold. 10x damage. Faster fire rate. 3 spiked balls launch at once. Next tier at 5 million pops.");
                return;
            }

            if (tts!.tower!.towerModel!.name.Equals($"{identifier} Tier 5") && tts!.tower!.damageDealt >= 5_000_000)
            {
                var changed = tts!.tower!.towerModel.CloneCast();
                changed.name = $"{identifier} Tier 6";
                changed.SetDisplay("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T6.png");

                changed.range += 5;

                for (int i = 0; i < changed.behaviors.Length; i++)
                {
                    if (changed.behaviors[i].Is<AttackModel>(out var am))
                    {
                        am.behaviors.First(a => a.Is<DisplayModel>()).Cast<DisplayModel>().display = new() { guidRef = "AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T6.png " };
                        am.range += 5;

                        foreach (var weapon in am.weapons)
                        {
                            weapon.Rate = 0.025f;

                            weapon.emission = new ParallelEmissionModel("PEM_", 5, 30, -30, false, null);

                            weapon.projectile.SetDisplay("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_UProj.png");
                            weapon.projectile.ModifyDamageModel(new DamageChange() { multiply = true, damage = 50 });
                            weapon.projectile.behaviors = weapon.projectile.behaviors.Add(new CreateEffectOnContactModel("CreateEffectOnContactModel_",
                                new EffectModel("Explosion", new() { guidRef = "6d84b13b7622d2744b8e8369565bc058" }, 1, 1)),
                                new DamageModifierForTagModel("DamageModifierForTagModel_", "Bad", 500, 0, false, true),
                                new DamageModifierForTagModel("DamageModifierForTagModel_", "Zomg", 300, 0, false, true),
                                new DamageModifierForTagModel("DamageModifierForTagModel_", "Ddt", 20, 0, false, true));

                            foreach (var pbeh in weapon.projectile.behaviors)
                            {
                                if (pbeh.Is<CreateProjectileOnExhaustFractionModel>(out var cpoefm))
                                {
                                    cpoefm.projectile.SetDisplay("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_Proj.png");
                                    cpoefm.emission.Cast<ArcEmissionModel>().count = 10;
                                    cpoefm.projectile.behaviors = cpoefm.projectile.behaviors.Add(new CreateEffectOnContactModel("CreateEffectOnContactModel_",
                                                                                    new EffectModel("Explosion", new() { guidRef = "6d84b13b7622d2744b8e8369565bc058" }, 1, 1)),
                                                                                    new DamageModifierForTagModel("DamageModifierForTagModel_", "Bad", 500, 0, false, true),
                                                                                    new DamageModifierForTagModel("DamageModifierForTagModel_", "Zomg", 300, 0, false, true),
                                                                                    new DamageModifierForTagModel("DamageModifierForTagModel_", "Ddt", 20, 0, false, true));
                                }
                            }
                        }
                    }
                }

                var abm = supermonkey050.behaviors.First(a => a.Is<AbilityModel>()).Cast<AbilityModel>();

                for (int j = 0; j < abm.behaviors.Length; j++)
                {
                    if (abm.behaviors[j].Is<CreateEffectOnAbilityModel>(out var ceoam))
                    {
                        ceoam.effectModel.assetId = new() { guidRef = "GoldenExperienceAbility" };
                    }
                    if (abm.behaviors[j].Is<ActivateAttackModel>(out var aam))
                    {
                        for (int k = 0; k < aam.attacks[0].weapons[0].projectile.behaviors.Length; k++)
                            if (aam.attacks[0].weapons[0].projectile.behaviors[k].Is<DamageModel>(out var damage))
                                damage.damage = 500_000_000;
                    }
                }

                abm.name = abm._name = abm.displayName = "Requiem";
                abm.icon = new() { guidRef = "Ui[AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_Icon.png]" };
                abm.cooldown /= 2;
                abm.cooldownFrames /= 2;

                changed.behaviors = changed.behaviors.Add(abm, new DisplayModel("DisplayModel_", new() { guidRef = "GoldenExperiencePlaceEffect" }, 0, new(), 1, false, 0));

                tts.tower.UpdateRootModel(changed);
                tts.tower.UpdatedModel(changed);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();

                InGame.instance.ShowEventPopup("ProgressiveTower", 38, true, "OK", null, 0, "", "Level Max! Tier 6", "This is requiem. 50x damage. Way faster fire rate. 5 spiked balls launch at once. 20x damage to DDTs. 300x damage to ZOMGs. 500x damage to BADs.");
                return;
            }
        };
        onLeave += () => time = -1;
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T0.png", "f6aa9eed583ceef44b813e221abc5b70", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T1.png", "f6aa9eed583ceef44b813e221abc5b70", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T2.png", "f6aa9eed583ceef44b813e221abc5b70", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T3.png", "f6aa9eed583ceef44b813e221abc5b70", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T4.png", "f6aa9eed583ceef44b813e221abc5b70", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T5.png", "f6aa9eed583ceef44b813e221abc5b70", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T6.png", "f6aa9eed583ceef44b813e221abc5b70", RendererType.SKINNEDMESHRENDERER));

        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T0.png ", "3227ca69bb352dc4c9c667712825b985", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T1.png ", "3227ca69bb352dc4c9c667712825b985", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T2.png ", "3227ca69bb352dc4c9c667712825b985", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T3.png ", "3227ca69bb352dc4c9c667712825b985", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T4.png ", "3227ca69bb352dc4c9c667712825b985", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T5.png ", "3227ca69bb352dc4c9c667712825b985", RendererType.SKINNEDMESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_T6.png ", "3227ca69bb352dc4c9c667712825b985", RendererType.SKINNEDMESHRENDERER));

        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_Proj.png", "72288b06ef230b644976478047ff0768", RendererType.MESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_Proj2.png", "72288b06ef230b644976478047ff0768", RendererType.MESHRENDERER));

        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_UProj.png", "655d4b5d0730e2949859a7fbeb3330fe", RendererType.MESHRENDERER));
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_UProj2.png", "655d4b5d0730e2949859a7fbeb3330fe", RendererType.MESHRENDERER));

        v2AssetStack.Add(new("GoldenExperienceAbility", "21f659bbb9e1d9441adf3239a773e224", RendererType.PARTICLESYSTEMRENDERER));
        v2AssetStack.Add(new("GoldenExperiencePlaceEffect", "21f659bbb9e1d9441adf3239a773e224", RendererType.PARTICLESYSTEMRENDERER));
    }
}