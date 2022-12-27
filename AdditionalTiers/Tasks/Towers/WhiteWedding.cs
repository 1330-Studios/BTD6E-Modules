namespace AdditionalTiers.Tasks.Towers;
internal class WhiteWedding : TowerTask
{ // What a name
    public static TowerModel baseTwrMdl;
    private static int time = -1;
    public WhiteWedding()
    {
        identifier = "White Wedding";
        getTower = () => baseTwrMdl;
        baseTower = AddedTierName.WHITEWEDDING;
        tower = AddedTierEnum.WHITEWEDDING;
        requirements += tts => tts.tower.towerModel.baseId.Equals("SuperMonkey") && tts.tower.towerModel.tiers[2] == 5;
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
            tts.TAdd(scale1: 0);

            AbilityMenu.instance.TowerChanged(tts);
            AbilityMenu.instance.RebuildAbilities();
        };
        gameLoad += gm =>
        {
            baseTwrMdl = gm.towers.First(a => a.name.Equals(baseTower)).CloneCast();
            baseTwrMdl.name = $"{identifier} Base";
            baseTwrMdl.SetDisplay("AdditionalTiers.Resources.V2.WhiteWedding.WhiteWedding_Base.png");
            baseTwrMdl.SetIcons("AdditionalTiers.Resources.V2.WhiteWedding.WhiteWeddingIcon.png");

            baseTwrMdl.range += 5;
            baseTwrMdl.dontDisplayUpgrades = true;

            for (int i = 0; i < baseTwrMdl.behaviors.Length; i++)
            {
                if (baseTwrMdl.behaviors[i].Is<AttackModel>(out var am))
                {
                    am.range += 5;
                    foreach (var weap in am.weapons)
                    {
                        weap.Rate = 0.05f;
                        foreach (var wb in weap.behaviors)
                        {
                            if (wb.Is<EjectEffectModel>(out var eem))
                            {
                                eem.effectModel.assetId = new() { guidRef = "AdditionalTiers.Resources.V2.WhiteWedding.WhiteWeddingHandBlur.png" };
                            }
                        }
                        foreach (var pb in weap.projectile.behaviors)
                        {
                            if (pb.Is<DamageModel>(out var dm))
                            {
                                dm.damage = 16;
                                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                            }
                        }
                    }
                }
            }

            baseTwrMdl.behaviors = baseTwrMdl.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true));

            UpgradeMenuManager.towers[baseTwrMdl.name] = new UMM_Tower {
                CurrentUpgrade = 0,
                Name = identifier,
                TowerModel = baseTwrMdl,
                TowerType = "GROUND",
                UpgradeCost = 750_000,
                Portrait = "AdditionalTiers.Resources.V2.WhiteWedding.WhiteWeddingIcon.png",
                CurrentSPA = 0.05,
                CurrentDamage = 16,
                NextSPA = -0.05,
                NextDamage = 48,
                NextRange = 5,
                Extra = "Extra Rangs",
                MaxUpgrade = false,
                NextUpgradeName = $"{identifier} T1"
            };

            var t1 = baseTwrMdl.CloneCast();
            t1.name = $"{identifier} T1";
            t1.range += 25;
            foreach (var beh in t1.behaviors)
            {
                if (beh.Is<AttackModel>(out var am))
                {
                    am.range += 25;
                    foreach (var weap in am.weapons)
                    {
                        weap.Rate /= 2;
                        weap.emission = new AdoraEmissionModel("AEM_", 8, 20, null);
                        foreach (var pb in weap.projectile.behaviors)
                        {
                            if (pb.Is<DamageModel>(out var dm))
                            {
                                dm.damage = 64;
                            }
                        }
                        weap.projectile.behaviors = weap.projectile.behaviors.Add(new AdoraTrackTargetModel("ATTM_", 9, 70, 360, 20, 3, 50, 30));
                    }
                }
            }

            UpgradeMenuManager.towers[t1.name] = new UMM_Tower {
                CurrentUpgrade = 1,
                Name = identifier,
                TowerModel = t1,
                TowerType = "GROUND",
                UpgradeCost = 2_500_000,
                Portrait = "AdditionalTiers.Resources.V2.WhiteWedding.WhiteWeddingIcon.png",
                CurrentSPA = 0.025,
                CurrentDamage = 64,
                NextSPA = -0.04,
                NextDamage = 192,
                NextRange = 0,
                Extra = "False Swipe",
                MaxUpgrade = false,
                NextUpgradeName = $"{identifier} T2"
            };

            var t2 = t1.CloneCast();
            t2.name = $"{identifier} T2";
            t2.range += 25;
            foreach (var beh in t2.behaviors)
            {
                if (beh.Is<AttackModel>(out var am))
                {
                    am.range += 25;
                    foreach (var weap in am.weapons)
                    {
                        weap.Rate = 0.0125f;
                        weap.emission = new AdoraEmissionModel("AEM_", 16, 15, null);
                        foreach (var pb in weap.projectile.behaviors)
                        {
                            if (pb.Is<DamageModel>(out var dm))
                            {
                                dm.damage = 256;
                            }
                        }
                    }
                }
            }

            var abm = gm.towers.First(a => a.name.Equals("SuperMonkey-050")).CloneCast().behaviors.First(a => a.Is<AbilityModel>()).Cast<AbilityModel>();

            abm.name = "FalseSwipe";
            abm.icon = new() { guidRef = "Ui[AdditionalTiers.Resources.V2.WhiteWedding.FalseSwipe.png]" };
            abm.cooldown /= 2;
            abm.cooldownFrames /= 2;
            abm.behaviors = new Model[] { new CreateEffectOnAbilityModel("FalseSwipeEffect", new EffectModel("FalseSwipe", new() { guidRef = "6d84b13b7622d2744b8e8369565bc058" }, 1, 1), false, true, false, false, false),
            new CreateSoundOnAbilityModel("CreateSoundOnAbilityModel_", new("FalseSwipeSound", new() {guidRef = "cf5b5e74df29fcf47a7d48b559114a99"}), null, null)};

            t2.behaviors = t2.behaviors.Add(abm);

            UpgradeMenuManager.towers[t2.name] = new UMM_Tower {
                CurrentUpgrade = 2,
                Name = identifier,
                TowerModel = t2,
                TowerType = "GROUND",
                UpgradeCost = 5_000_000,
                Portrait = "AdditionalTiers.Resources.V2.WhiteWedding.WhiteWeddingIcon.png",
                CurrentSPA = 0.0125,
                CurrentDamage = 256,
                NextSPA = 0,
                NextDamage = 0,
                NextRange = 0,
                Extra = "",
                MaxUpgrade = true,
                NextUpgradeName = ""
            };
        };
        recurring += _ => { };
        onLeave += () => time = -1;
        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.WhiteWedding.WhiteWedding_Base.png", "e6c683076381222438dfc733a602c157", RendererType.SKINNEDMESHRENDERER));

        v2AssetStack.Add(new("AdditionalTiers.Resources.V2.WhiteWedding.WhiteWeddingHandBlur.png", "0c9b475b471b56649bbc09f244eba526", RendererType.PARTICLESYSTEMRENDERER));
    }

    [HarmonyPatch(typeof(Ability), nameof(Ability.Activate))]
    public sealed class AA
    {
        [HarmonyPrefix]
        internal static unsafe bool Prefix(ref Ability __instance)
        {
            if (__instance?.abilityModel?.name?.EndsWith("FalseSwipe") ?? true)
            {
                foreach (var bloon in InGame.instance.bridge.GetAllBloons())
                {
                    if (bloon != null && !bloon.Def.name.Contains("TestBloon"))
                    {
                        __instance.tower.damageDealt += 400000;
                        bloon.GetSimBloon().SetHealth(Math.Max(bloon.GetSimBloon().Health - 400000, 1));
                    }
                }
            }

            return true;
        }
    }
}
