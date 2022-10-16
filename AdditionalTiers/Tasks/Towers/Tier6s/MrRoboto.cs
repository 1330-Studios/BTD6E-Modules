namespace AdditionalTiers.Tasks.Towers.Tier6s;

public sealed class MrRoboto : TowerTask {
    public static TowerModel MRROBOTO;
    private static int time = -1;
    public MrRoboto() {
        identifier = "Mr. Roboto";
        getTower = () => MRROBOTO;
        baseTower = AddedTierName.MRROBOTO;
        tower = AddedTierEnum.MRROBOTO;
        requirements += tts => tts.tower.towerModel.baseId.Equals(AddedTierName.MRROBOTO.Split('-')[0]) && tts.tower.towerModel.tiers[1] == 5;
        onComplete += tts => {
            if (time < 50) {
                time++;
                return;
            }
            TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
            tts.tower.worth = 0;
            tts.tower.UpdateRootModel(MRROBOTO);
            for (int i = 0; i < tts.tower.attackBehaviorsInDependants.Count; i++) {
                try {
                    for (int i1 = 0; i1 < tts.tower.attackBehaviorsInDependants[i].entity.displayBehaviorCache.node.graphic.genericRenderers.Count; i1++)
                        tts.tower.attackBehaviorsInDependants[i].entity.displayBehaviorCache.SetScaleOffset(new(1.2f, 1.2f, 1.2f));
                } catch {
                    continue;
                }
            }
            tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
            AbilityMenu.instance.TowerChanged(tts);
            AbilityMenu.instance.RebuildAbilities();
        };
        gameLoad += gm => {
            MRROBOTO = gm.towers.First(a => a.name.Contains(AddedTierName.MRROBOTO)).CloneCast();

            MRROBOTO.range += 45;
            MRROBOTO.cost = 0;
            MRROBOTO.name = "Mr. Roboto";
            MRROBOTO.baseId = "SuperMonkey";
            MRROBOTO.SetDisplay("MrRoboto");
            MRROBOTO.dontDisplayUpgrades = true;
            MRROBOTO.portrait = new("MrRobotoIcon");
            var beh = MRROBOTO.behaviors;

            for (var i = 0; i < beh.Length; i++) {
                var behavior = beh[i];
                if (behavior.Is<AttackModel>(out var am)) {
                    for (int j = 0; j < am.behaviors.Length; j++)
                        if (am.behaviors[j].Is<DisplayModel>(out var dm))
                            if (dm.display.Equals("fdff998beaa71ee45977df86cfda6d96"))
                                dm.display.guidRef = "MrRobotoArms1";
                            else
                                dm.display.guidRef = "MrRobotoArms2";

                    if (am.name.Contains("Off")) {
                        am.weapons[0].projectile = gm.towers.First(t => t.name.Contains("HeliPilot-500")).CloneCast().behaviors.First(am => am.Is<AttackModel>(out _) && am.name.Contains("Missile")).Cast<AttackModel>().weapons[0].projectile;
                        for (int j = 0; j < am.weapons[0].projectile.behaviors.Length; j++)
                            if (am.weapons[0].projectile.behaviors[j].Is<CreateProjectileOnContactModel>(out var cpocm)) {
                                for (int k = 0; k < cpocm.projectile.behaviors.Length; k++)
                                    if (cpocm.projectile.behaviors[k].Is<DamageModel>(out var damage))
                                        damage.damage = 25000;

                                cpocm.projectile.ignorePierceExhaustion = true;
                            }
                        am.weapons[0].rate *= 25;
                        am.weapons[0].rateFrames *= 25;
                        am.weapons[0].projectile.display.guidRef = "MrRobotoRocket";
                    } else {
                        am.framesBeforeRetarget = 0;
                        am.weapons[0].rate = 0;
                        am.weapons[0].rateFrames = 0;
                        am.weapons[0].projectile.display.guidRef = "MrRobotoPlasma";
                        am.weapons[0].projectile.ignorePierceExhaustion = true;
                        am.weapons[0].emission = new ArcEmissionModel("ArcEmissionModel_", 5, 0, 75, null, false);
                    }

                    for (int j = 0; j < am.weapons[0].projectile.behaviors.Length; j++)
                        if (am.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var damage))
                            damage.damage *= 20;

                    am.weapons[0].behaviors = am.weapons[0].behaviors.Remove(m=>m.Is<CritMultiplierModel>(out _));

                    am.range += 45;
                }
                if (behavior.Is<AbilityModel>(out var abm)) {
                    for (int j = 0; j < abm.behaviors.Length; j++) {
                        if (abm.behaviors[j].Is<CreateEffectOnAbilityModel>(out var ceoam)) {
                            ceoam.effectModel.assetId.guidRef = "MrRobotoAbility";
                        }
                        if (abm.behaviors[j].Is<ActivateAttackModel>(out var aam)) {
                            for (int k = 0; k < aam.attacks[0].weapons[0].projectile.behaviors.Length; k++)
                                if (aam.attacks[0].weapons[0].projectile.behaviors[k].Is<DamageModel>(out var damage))
                                    damage.damage = 5000000;
                        }
                    }
                    abm.icon = new("MrRobotoAbilityIcon");
                    abm.cooldown /= 2;
                    abm.cooldownFrames /= 2;
                }
            }

            MRROBOTO.RebuildBehaviors(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true), new DisplayModel("dm", new("MrRobotoAbility"), 0, new(), 1, true, 0));
        };
        recurring += tts => { };
        onLeave += () => { time = -1; };
        assetsToRead.Add(new("MrRoboto", "78712e8e277f6264b8cf1e1844b5657d", RendererType.SKINNEDMESHRENDERER));
        assetsToRead.Add(new("MrRobotoArms1", "fdff998beaa71ee45977df86cfda6d96", RendererType.SKINNEDMESHRENDERER));
        assetsToRead.Add(new("MrRobotoArms2", "8a151c6c111ff5641882e51afc28c740", RendererType.SKINNEDMESHRENDERER));
        assetsToRead.Add(new("MrRobotoRocket", "9dccc16d26c1c8a45b129e2a8cbd17ba", RendererType.SPRITERENDERER));
        assetsToRead.Add(new("MrRobotoPlasma", "187bc7112ccbf6445afc2ef9173b4568", RendererType.SPRITERENDERER));
        assetsToRead.Add(new("MrRobotoAbility", "21f659bbb9e1d9441adf3239a773e224", RendererType.PARTICLESYSTEMRENDERER));
    }
}
