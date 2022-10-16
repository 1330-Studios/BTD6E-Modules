using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CreateEffectOnExpireModel = Assets.Scripts.Models.Towers.Projectiles.Behaviors.CreateEffectOnExpireModel;

namespace AdditionalTiers.Tasks.Towers.Tier6s;
internal class Dynamite : TowerTask {
    public static TowerModel dynamite;
    private static int time = -1;
    public Dynamite() {
        identifier = "Dynamite";
        getTower = () => dynamite;
        baseTower = AddedTierName.DYNAMITE;
        tower = AddedTierEnum.DYNAMITE;
        requirements += tts => tts.tower.towerModel.baseId.Equals("BombShooter") && tts.tower.towerModel.tiers[0] == 5;
        onComplete += tts => {
            if (time < 50) {
                time++;
                return;
            }
            TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
            tts.tower.worth = 0;
            tts.tower.UpdateRootModel(dynamite);
            tts.TAdd(scale1: .25f);
            tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
            AbilityMenu.instance.TowerChanged(tts);
            AbilityMenu.instance.RebuildAbilities();
        };
        gameLoad += gm => {
            dynamite = gm.towers.First(a=>a.name.Equals(baseTower)).CloneCast();

            dynamite.range = 150;
            dynamite.cost = 0;
            dynamite.name = "Dynamite";
            dynamite.baseId = baseTower.Split('-')[0];
            dynamite.dontDisplayUpgrades = true;
            dynamite.SetDisplay("Dynamite");
            dynamite.portrait = new("DynamitePor");

            for (int i = 0; i < dynamite.behaviors.Length; i++) {
                if (dynamite.behaviors[i].Is<AttackModel>(out var am)) {
                    am.range = 150;

                    for (int j = 0; j < am.weapons.Length; j++) {
                        var weapon = am.weapons[j];

                        for (int k = 0; k < weapon.projectile.behaviors.Length; k++) {
                            if (weapon.projectile.behaviors[k].Is<CreateProjectileOnContactModel>(out var cpocm)) {
                                if (cpocm.projectile.id.Equals("Explosion")) {
                                    cpocm.projectile.ModifyDamageModel(new DamageChange() { set = true, damage = 50000, maxDamage = 50000 });
                                    cpocm.projectile.behaviors = cpocm.projectile.behaviors.Add(new DamageModifierForTagModel("DMFTM_OP", "Moabs", 500, 10000, false, true));
                                    cpocm.projectile.radius *= 5;
                                    cpocm.projectile.pierce = 99999999;
                                    cpocm.projectile.ignorePierceExhaustion = true;
                                } else {
                                    var FragOfFrag = cpocm.projectile.CloneCast();
                                    var FragOfFragOfFrag = cpocm.projectile.CloneCast();
                                    cpocm.projectile.ModifyDamageModel(new DamageChange() { set = true, damage = 5000, maxDamage = 5000 });
                                    cpocm.projectile.display.guidRef = "DynamiteFrag";
                                    cpocm.projectile.behaviors = cpocm.projectile.behaviors.Add(new CreateEffectOnExpireModel("CEOEM_", new("3e6bf36670555d4408d6a9ca2e531d85"), 2, false, false,
                                        new EffectModel("Explosion_", new("3e6bf36670555d4408d6a9ca2e531d85"), 1, 2)));
                                    cpocm.projectile.radius *= 5;
                                    cpocm.emission.Cast<ArcEmissionModel>().count = 24;

                                    FragOfFragOfFrag.ModifyDamageModel(new DamageChange() { set = true, damage = 250, maxDamage = 250 });
                                    FragOfFragOfFrag.display.guidRef = "DynamiteFragFragFrag";
                                    FragOfFragOfFrag.scale *= 1.25f;

                                    FragOfFrag.ModifyDamageModel(new DamageChange() { set = true, damage = 500, maxDamage = 500 });
                                    FragOfFrag.behaviors = FragOfFrag.behaviors.Add(new CreateProjectileOnExpireModel("CPOEM_", FragOfFragOfFrag, new ArcEmissionModel("AEM_", 16, 0, 360, null, false), false));

                                    cpocm.projectile.behaviors = cpocm.projectile.behaviors.Add(new CreateProjectileOnExpireModel("CPOCM_", FragOfFrag, new ArcEmissionModel("AEM_", 8, 0, 360, null, false), false));
                                }
                            }
                        }

                        weapon.projectile.display.guidRef = "DynamiteProj";
                        weapon.projectile.behaviors.First(a => a.Is<CreateEffectOnContactModel>()).Cast<CreateEffectOnContactModel>().effectModel.assetId.guidRef = "b1324f2f4c3809643b7ef1d8c112442a";
                        weapon.projectile.behaviors = weapon.projectile.behaviors.Add(new CreateEffectOnContactModel("CEOCM_", new EffectModel("EM_", new("e9ea81b200f5036498d38048b390f22f"), 1, 1)));

                        am.weapons[j] = weapon;
                    }
                }
            }

            dynamite.behaviors = dynamite.behaviors.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true).Cast<Model>());
        };
        recurring += _ => { };
        onLeave += () => time = -1;
        assetsToRead.Add(new("Dynamite", "1f04b9d0b4bbb354499b26d78f342bcb", RendererType.SKINNEDMESHRENDERER));
        assetsToRead.Add(new("DynamiteProj", "97d3634339d18c443853aecd4acbd157", RendererType.SPRITERENDERER));
        assetsToRead.Add(new("DynamiteFrag", "d15d481d5956b28449b8c87a578d8f07", RendererType.SPRITERENDERER));
        assetsToRead.Add(new("DynamiteFragFragFrag", "d15d481d5956b28449b8c87a578d8f07", RendererType.SPRITERENDERER));
    }
}
