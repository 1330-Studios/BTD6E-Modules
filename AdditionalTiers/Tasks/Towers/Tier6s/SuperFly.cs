
using Assets.Scripts.Models.Towers.TowerFilters;

namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class SuperFly : TowerTask {
        public static TowerModel superFly;
        private static int time = -1;
        public SuperFly() {
            identifier = "Super Fly";
            getTower = () => superFly;
            baseTower = AddedTierName.SUPERFLY;
            tower = AddedTierEnum.SUPERFLY;
            requirements += tts => tts.tower.towerModel.baseId.Equals("MonkeyVillage") && tts.tower.towerModel.tiers[0] == 5;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(superFly);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                Globals.SixthTierPopCountMulti /= 10;
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                superFly = gm.towers.First(a => a.name.Contains("MonkeyVillage-520")).Clone()
                    .Cast<TowerModel>();

                superFly.range = 500;
                superFly.cost = 0;
                superFly.name = "Super Fly";
                superFly.baseId = "MonkeyVillage";
                superFly.display.guidRef = "SuperFly";
                superFly.dontDisplayUpgrades = true;
                superFly.portrait = new("SuperFlyIcon");
                superFly.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display.guidRef = "SuperFly";
                var beh = superFly.behaviors;
                for (var i = 0; i < beh.Length; i++) {
                    var behavior = beh[i];
                    if (behavior.GetIl2CppType() == Il2CppType.Of<PierceSupportModel>()) {
                        PierceSupportModel a = behavior.Cast<PierceSupportModel>();
                        a.pierce = 5;
                        a.filters = new Il2CppReferenceArray<TowerFilterModel>(0);
                        behavior = a;
                    }
                    if (behavior.GetIl2CppType() == Il2CppType.Of<ProjectileSpeedSupportModel>()) {
                        ProjectileSpeedSupportModel a = behavior.Cast<ProjectileSpeedSupportModel>();
                        a.multiplier = 0.5f;
                        a.filters = new Il2CppReferenceArray<TowerFilterModel>(0);
                        behavior = a;
                    }
                    if (behavior.GetIl2CppType() == Il2CppType.Of<RangeSupportModel>()) {
                        RangeSupportModel a = behavior.Cast<RangeSupportModel>();
                        // There's 2 RangeSupportModels
                        if (a.multiplier == .1f)
                            a.multiplier = .25f;
                        if (a.additive == 5)
                            a.additive = 10;
                        a.filters = new Il2CppReferenceArray<TowerFilterModel>(0);
                        behavior = a;
                    }
                    if (behavior.GetIl2CppType() == Il2CppType.Of<FreeUpgradeSupportModel>()) {
                        FreeUpgradeSupportModel a = behavior.Cast<FreeUpgradeSupportModel>();
                        a.upgrade = 3;
                        a.filters = new Il2CppReferenceArray<TowerFilterModel>(0);
                        behavior = a;
                    }
                    if (behavior.GetIl2CppType() == Il2CppType.Of<AbilityCooldownScaleSupportModel>()) {
                        AbilityCooldownScaleSupportModel a = behavior.Cast<AbilityCooldownScaleSupportModel>();
                        a.filters = new Il2CppReferenceArray<TowerFilterModel>(0);
                        behavior = a;
                    }

                    beh[i] = behavior;
                }

                superFly.behaviors = beh.Remove(a => a.GetIl2CppType() == Il2CppType.Of<AttackModel>());
            };
            recurring += tts => { };
            onLeave += () => { time = -1; Globals.Load(); };
            assetsToRead.Add(new("SuperFly", "06b880ab7e2941b4f9de3e132ba1e11e", RendererType.SKINNEDMESHRENDERER));
        }
    }
}