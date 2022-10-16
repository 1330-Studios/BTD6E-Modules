using Assets.Scripts.Models.Audio;
using Assets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Towers.Behaviors.Abilities;
using Assets.Scripts.Simulation.Towers.Projectiles;

namespace GodlyTowers.Towers {
    public sealed class Thanos {
        public static string name = "Thanos";

        public static UpgradeModel[] GetUpgrades() => new UpgradeModel[] {
            new("Space Stone", 700, 0, new("SpaceStone_U1"), 0, 0, 0, "", "Space Stone"),
            new("Reality Stone", 1270, 0, new("RealityStone_U2"), 0, 1, 0, "", "Reality Stone"),
            new("Soul Stone", 3600, 0, new("SoulStone_U3"), 0, 2, 0, "", "Soul Stone"),
            new("Time Stone", 9100, 0, new("TimeStone_U4"), 0, 3, 0, "", "Time Stone"),
            new("Mind Stone", 28500, 0, new("MindStone_U5"), 0, 4, 0, "", "Mind Stone")
        };

        public static (TowerModel, ShopTowerDetailsModel, TowerModel[], UpgradeModel[]) GetTower(GameModel gameModel) {
            var thanosDetails = gameModel.towerSet[0].Clone().Cast<ShopTowerDetailsModel>();
            thanosDetails.towerId = name;
            thanosDetails.towerIndex = GlobalTowerIndex.Index;

            if (!LocalizationManager.Instance.textTable.ContainsKey("Space Stone Description"))
                LocalizationManager.Instance.textTable.Add("Space Stone Description", "The Space Stone held dominion over the fabric of space being able to teleport its users anywhere in the universe.");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Reality Stone Description"))
                LocalizationManager.Instance.textTable.Add("Reality Stone Description", "The Reality Stone's power is such that it can affect reality on a universal scale. The stone is also nearly indestructible, being able to withstand blows from Mjølnir.");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Soul Stone Description"))
                LocalizationManager.Instance.textTable.Add("Soul Stone Description", "The Soul Stone has the ability to manipulate the soul, the essence that makes up an individual, and has the ability to resurrect and conjure the spiritual representation of the people who are dead.");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Time Stone Description"))
                LocalizationManager.Instance.textTable.Add("Time Stone Description", "The Time Stone has the ability to completely manipulate and control the flow of time on something as small as an apple to as large as the universe.");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Mind Stone Description"))
                LocalizationManager.Instance.textTable.Add("Mind Stone Description", "The Mind Stone's power can dominate the minds of others on contact, placing them under the control of the wielder and loyal to their commands indefinitely. ");

            return (GetT0(gameModel), thanosDetails, new[] { GetT0(gameModel), GetT1(gameModel), GetT2(gameModel), GetT3(gameModel), GetT4(gameModel), GetT5(gameModel) }, GetUpgrades());
        }

        public static unsafe TowerModel GetT0(GameModel gameModel) {
            var thanos = gameModel.towers.First(a => a.name.Equals("DartMonkey")).CloneCast();

            thanos.name = name;
            thanos.baseId = name;
            thanos.display = "ThanosT0";
            thanos.portrait = new("ThanosPortrait0");
            thanos.icon = new("ThanosPortrait0");
            thanos.towerSet = "Magic";
            thanos.emoteSpriteLarge = new("Marvel");
            thanos.tier = 0;
            thanos.tiers = new[] { 0, 0, 0 };
            thanos.radius = 8;
            thanos.range = 30;
            thanos.cost = 950;
            thanos.upgrades = new UpgradePathModel[] { new UpgradePathModel("Space Stone", name + "-100") };

            for (var i = 0; i < thanos.behaviors.Count; i++) {
                var b = thanos.behaviors[i];
                if (b.Is<AttackModel>(out var att)) {
                    foreach (var ab in att.behaviors) {
                        if (ab.Is<RotateToTargetModel>(out var rttm)) {
                            rttm.rotateOnlyOnThrow = false;
                        }
                    }

                    att.weapons[0].name = "thanospunch";
                    att.weapons[0].projectile.pierce = 1;
                    att.weapons[0].rate = 0.5f;
                    att.weapons[0].behaviors = Array.Empty<WeaponBehaviorModel>();
                    att.weapons[0].projectile.display = "";
                    att.weapons[0].projectile.radius = 2f;
                    foreach (var pb in att.weapons[0].projectile.behaviors) {
                        if (pb.Is<DamageModel>(out var dm)) {
                            dm.damage = 5;
                            dm.immuneBloonProperties = BloonProperties.None;
                        }
                        if (pb.Is<TravelStraitModel>(out var tsm))
                            tsm.lifespan /= 2;
                    }
                    att.range = 30;
                    thanos.behaviors[i] = att;
                }
                if (b.Is<DisplayModel>(out var display)) {
                    display.display = "ThanosT0";
                }
            }

            EffectModel darkshiftEffect = new EffectModel("ThanosPowerStoneEffect", "ThanosPowerStoneEffect", 1, 1, false, false, true, false, false, false, false);

            DamageUpModel dum = new DamageUpModel("DamageUpModel_", 500, 500, new AssetPathModel("", ""));
            CreateEffectOnAbilityModel ceoam = new("CreateEffectOnAbilityModel_", darkshiftEffect, false, false, false, false, false);

            AbilityModel ability = new AbilityModel("ThanosSpaceStoneWarp", "Power Stone Buff", "Using the power stone, thanos gains immense power boosts", 0, 0, new("PowerStone_U0"), 12, new Model[] { dum, ceoam },
                false, false, "", 0, 0, 99999, false, false);

            thanos.behaviors = thanos.behaviors.Add(ability);

            return thanos;
        }

        public static unsafe TowerModel GetT1(GameModel gameModel) {
            var thanos = GetT0(gameModel).CloneCast();

            thanos.name = name + "-100";
            thanos.baseId = name;
            thanos.display = "ThanosT1";
            thanos.portrait = new("ThanosPortrait1");
            thanos.tier = 1;
            thanos.tiers = new[] { 1, 0, 0 };
            thanos.range = 30;
            thanos.appliedUpgrades = new[] { "Space Stone" };
            thanos.upgrades = new UpgradePathModel[] { new UpgradePathModel("Reality Stone", name + "-200") };

            for (var i = 0; i < thanos.behaviors.Count; i++) {
                var b = thanos.behaviors[i];
                if (b.Is<AttackModel>(out var att)) {
                    att.weapons[0].projectile.pierce = 5;
                    att.weapons[0].projectile.radius = 3f;
                    foreach (var pb in att.weapons[0].projectile.behaviors) {
                        if (pb.Is<DamageModel>(out var dm))
                            dm.damage = 10;
                    }

                    thanos.behaviors[i] = att;
                }
                if (b.Is<DisplayModel>(out var display))
                    display.display = "ThanosT1";
            }

            SoundModel darkshiftSound = new SoundModel("84058c0c48eef6f448d0ec80a9daf0eb", "84058c0c48eef6f448d0ec80a9daf0eb");
            EffectModel darkshiftEffect = new EffectModel("ThanosSpaceStoneEffect", "ThanosSpaceStoneEffect", 1, 1, false, false, false, false, false, false, false);

            DarkshiftModel darkshift = new DarkshiftModel("ThanosDarkshift_", false, 500, "ThanosDarkshiftZone", darkshiftSound, darkshiftEffect, darkshiftEffect);

            AbilityModel ability = new AbilityModel("ThanosSpaceStoneWarp", "Space Stone Warping", "Using the space stone, thanos can travel anywhere instantly", 0, 0, new("SpaceStone_U1"), 15, new Model[] { darkshift },
                false, false, "Space Stone", 0, 0, 99999, false, false);

            thanos.behaviors = thanos.behaviors.Add(ability);

            return thanos;
        }

        public static unsafe TowerModel GetT2(GameModel gameModel) {
            var thanos = GetT1(gameModel).CloneCast();

            thanos.name = name + "-200";
            thanos.baseId = name;
            thanos.display = "ThanosT2";
            thanos.portrait = new("ThanosPortrait2");
            thanos.tier = 2;
            thanos.tiers = new[] { 2, 0, 0 };
            thanos.range = 35;
            thanos.appliedUpgrades = new[] { "Space Stone", "Reality Stone" };
            thanos.upgrades = new UpgradePathModel[] { new UpgradePathModel("Soul Stone", name + "-300") };

            for (var i = 0; i < thanos.behaviors.Count; i++) {
                var b = thanos.behaviors[i];
                if (b.Is<AttackModel>(out var att)) {
                    att.weapons[0].rate = 0.4f;
                    att.weapons[0].projectile.pierce = 10;
                    att.weapons[0].projectile.radius = 4f;
                    foreach (var pb in att.weapons[0].projectile.behaviors) {
                        if (pb.Is<DamageModel>(out var dm)) {
                            dm.damage = 25;
                        }
                    }

                    DamageModifierForTagModel moabModifier = new("DMFTMM_", "Moabs", 2, 5, false, true);
                    DamageModifierForTagModel fortifiedModifier = new("DMFTMF_", "Fortified", 3, 10, false, true);

                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Add(moabModifier, fortifiedModifier);

                    att.range = 35;
                    thanos.behaviors[i] = att;
                }
                if (b.Is<DisplayModel>(out var display))
                    display.display = "ThanosT2";
            }

            var realityStoneEffect = new EffectModel("ThanosRealityStoneEffect", "ThanosRealityStoneEffect", 1, 1, false, false, false, false, false, false, false);
            var realityStoneCreateEffect = new CreateEffectOnAbilityModel("CreateEffectOnAbilityModel", realityStoneEffect, false, false, false, false, false);

            var realityStoneAbility = new AbilityModel("RealityStone", "RealityStone", "Thanos downgrades all bloons on screen", 0, 0, new("RealityStone_U2"), 16, new Model[] { realityStoneCreateEffect },
                false, false, "Reality Stone", 0, 0, 9999, false, false);

            thanos.behaviors = thanos.behaviors.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true), realityStoneAbility);

            return thanos;
        }

        public static unsafe TowerModel GetT3(GameModel gameModel) {
            var thanos = GetT2(gameModel).CloneCast();

            thanos.name = name + "-300";
            thanos.baseId = name;
            thanos.display = "ThanosT3";
            thanos.portrait = new("ThanosPortrait3");
            thanos.tier = 3;
            thanos.tiers = new[] { 3, 0, 0 };
            thanos.range = 35;
            thanos.towerSelectionMenuThemeId = "UnpoppedArmy";
            thanos.appliedUpgrades = new[] { "Space Stone", "Reality Stone", "Soul Stone" };
            thanos.upgrades = new UpgradePathModel[] { new UpgradePathModel("Time Stone", name + "-300") };

            for (var i = 0; i < thanos.behaviors.Count; i++) {
                var b = thanos.behaviors[i];
                if (b.Is<DisplayModel>(out var display))
                    display.display = "ThanosT3";
            }

            var necromancer = gameModel.towers.FirstOrDefault(tower => tower.name.Contains("WizardMonkey-025")).CloneCast();

            var necromancerAttack = necromancer.behaviors.FirstOrDefault(behavior => behavior.name.Contains("Necromancer_")).CloneCast();
            var necromancerZone = necromancer.behaviors.FirstOrDefault(behavior => behavior.name.Contains("NecromancerZoneModel_")).CloneCast();

            var soulStoneEffect = new EffectModel("ThanosSoulStoneEffect", "ThanosSoulStoneEffect", 1, 1, false, false, false, false, false, false, false);
            var soulStoneCreateEffect = new CreateEffectOnAbilityModel("CreateEffectOnAbilityModel", soulStoneEffect, false, false, false, false, false);

            var soulStoneAbility = new AbilityModel("SoulStone", "SoulStone", "Thanos absorbs a random bloon on screen adding it to the total health", 0, 0, new("SoulStone_U3"), 20, new Model[] { soulStoneCreateEffect },
                false, false, "Soul Stone", 0, 0, 9999, false, false);

            thanos.behaviors = thanos.behaviors.Add(necromancerAttack, necromancerZone, soulStoneAbility);

            return thanos;
        }

        public static unsafe TowerModel GetT4(GameModel gameModel) {
            var thanos = GetT3(gameModel).CloneCast();

            thanos.name = name + "-400";
            thanos.baseId = name;
            thanos.display = "ThanosT4";
            thanos.portrait = new("ThanosPortrait4");
            thanos.tier = 4;
            thanos.tiers = new[] { 4, 0, 0 };
            thanos.range = 40;
            thanos.appliedUpgrades = new[] { "Space Stone", "Reality Stone", "Soul Stone", "Time Stone" };
            thanos.upgrades = new UpgradePathModel[] { new UpgradePathModel("Mind Stone", name + "-500") };

            bool appliedBefore = false;

            for (var i = 0; i < thanos.behaviors.Count; i++) {
                var b = thanos.behaviors[i];
                if (b.Is<AttackModel>(out var att)) {
                    att.weapons[0].projectile.pierce = 50;
                    att.weapons[0].projectile.radius = 5f;
                    foreach (var pb in att.weapons[0].projectile.behaviors) {
                        if (pb.Is<DamageModel>(out var dm))
                            dm.damage = 50;
                    }

                    if (!appliedBefore) {
                        WindModel wind = new WindModel("WindModel_", 25, 75, 0.5f, true, null, 0);

                        att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Add(wind);

                        appliedBefore = true;
                    }

                    att.range = 40;
                    thanos.behaviors[i] = att;
                }
                if (b.Is<DisplayModel>(out var display))
                    display.display = "ThanosT4";
            }

            var timeStoneEffect = new EffectModel("ThanosTimeStoneEffect", "ThanosTimeStoneEffect", 1, 1, false, false, false, false, false, false, false);
            var timeStoneCreateEffect = new CreateEffectOnAbilityModel("CreateEffectOnAbilityModel", timeStoneEffect, false, false, false, false, false);

            var timeStoneAbility = new AbilityModel("TimeStone", "TimeStone", "Thanos uses the time stone to progress the rounds further by 1.", 0, 0, new("TimeStone_U4"), 12, new Model[] { timeStoneCreateEffect },
                false, false, "Time Stone", 0, 0, 9999, false, false);

            thanos.behaviors = thanos.behaviors.Add(timeStoneAbility);

            return thanos;
        }

        public static unsafe TowerModel GetT5(GameModel gameModel) {
            var thanos = GetT4(gameModel).CloneCast();

            thanos.name = name + "-500";
            thanos.baseId = name;
            thanos.display = "ThanosT5";
            thanos.portrait = new("ThanosPortrait5");
            thanos.tier = 5;
            thanos.tiers = new[] { 5, 0, 0 };
            thanos.range = 50;
            thanos.appliedUpgrades = new[] { "Space Stone", "Reality Stone", "Soul Stone", "Time Stone", "Mind Stone" };
            thanos.upgrades = Array.Empty<UpgradePathModel>();

            GodTier.GodTier.CustomUpgrades.Add("Mind Stone", GodTier.GodTier.UpgradeBG.MindStone);

            for (var i = 0; i < thanos.behaviors.Count; i++) {
                var b = thanos.behaviors[i];
                if (b.Is<AttackModel>(out var att)) {
                    att.weapons[0].rate = 0.35f;
                    att.weapons[0].projectile.pierce = 999;
                    att.weapons[0].projectile.ignorePierceExhaustion = true;
                    att.weapons[0].projectile.radius = 10;
                    foreach (var pb in att.weapons[0].projectile.behaviors) {
                        if (pb.Is<DamageModel>(out var dm))
                            dm.damage = 1000;
                    }
                    att.range = 50;
                    thanos.behaviors[i] = att;
                }
                if (b.Is<DisplayModel>(out var display))
                    display.display = "ThanosT5";
            }

            var psi = gameModel.towers.FirstOrDefault(tower => tower.name.Contains("Psi 20")).CloneCast();
            var psiAbility = psi.behaviors.First(m => m.GetIl2CppType() == Il2CppType.Of<AbilityModel>()).CloneCast<AbilityModel>();
            var psiAbilityAttack = psiAbility.behaviors.First(m => m.GetIl2CppType() == Il2CppType.Of<ActivateAttackModel>()).CloneCast<ActivateAttackModel>();

            psiAbilityAttack.attacks[0].weapons[0].projectile.radius = 9999999f;
            psiAbilityAttack.attacks[0].weapons[0].projectile.ignorePierceExhaustion = true;

            var mindStoneEffect = new EffectModel("ThanosMindStoneEffect", "ThanosMindStoneEffect", 1, 1, false, false, false, false, false, false, false);
            var mindStoneCreateEffect = new CreateEffectOnAbilityModel("CreateEffectOnAbilityModel", mindStoneEffect, false, false, false, false, false);

            var mindStoneAbility = new AbilityModel("MindStone", "MindStone", "Using the mind stone thanos stalls the bloons with confusion", 0, 0, new("MindStone_U5"), 13, new Model[] { mindStoneCreateEffect, psiAbilityAttack },
                false, false, "Mind Stone", 0, 0, 9999, false, false);

            var techterror = gameModel.towers.FirstOrDefault(tower => tower.name.Contains("SuperMonkey-250")).CloneCast();
            var techterrorAbility = techterror.behaviors.First(m => m.GetIl2CppType() == Il2CppType.Of<AbilityModel>()).CloneCast<AbilityModel>();
            var techterrorAbilityAttack = techterrorAbility.behaviors.First(m => m.GetIl2CppType() == Il2CppType.Of<ActivateAttackModel>()).CloneCast<ActivateAttackModel>();

            techterrorAbilityAttack.attacks[0].weapons[0].name = "thanossnap";
            techterrorAbilityAttack.attacks[0].weapons[0].projectile.radius = 9999999f;
            techterrorAbilityAttack.attacks[0].weapons[0].projectile.ignorePierceExhaustion = true;

            var gauntletAbility = new AbilityModel("ThanosSnap", "Thanos' Snap", "Perfectly balanced, as all things should be.", 0, 0, new("Thanos_GauntletAbility"), 50, new Model[] {techterrorAbilityAttack},
                false, false, "Mind Stone", 0, 0, 9999, false, false);

            thanos.behaviors = thanos.behaviors.Add(mindStoneAbility, gauntletAbility);

            return thanos;
        }

        [HarmonyPatch(typeof(Factory), nameof(Factory.FindAndSetupPrototypeAsync))]
        public sealed class PrototypeUDN_Patch {
            public static Dictionary<string, UnityDisplayNode> protos = new();

            [HarmonyPrefix]
            public static bool Prefix(Factory __instance, string objectId, Il2CppSystem.Action<UnityDisplayNode> onComplete) {

                if (objectId.Equals("ThanosPowerStoneEffect")) {
                    UnityDisplayNode udn = null;
                    __instance.FindAndSetupPrototypeAsync("2a7cf871777b123409e5cb2ac9f02162",
                        new Action<UnityDisplayNode>(
                            btdUdn => {
                                var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                                instance.name = objectId + "(Clone)";
                                instance.RecalculateGenericRenderers();
                                var pss = instance.transform.GetComponents<ParticleSystem>();
                                var pscs = instance.transform.GetComponentsInChildren<ParticleSystem>();
                                var color = new Color(157 / 255f, 0 / 255f, 255 / 255f, 255 / 255f);
                                foreach (var ps in pss)
                                    ps.startColor = color;
                                foreach (var ps in pscs)
                                    ps.startColor = color;
                                udn = instance;
                                onComplete.Invoke(udn);
                            }));
                }

                if (objectId.Equals("ThanosSpaceStoneEffect")) {
                    UnityDisplayNode udn = null;
                    __instance.FindAndSetupPrototypeAsync("2a7cf871777b123409e5cb2ac9f02162",
                        new Action<UnityDisplayNode>(
                            btdUdn => {
                                var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                                instance.name = objectId + "(Clone)";
                                instance.RecalculateGenericRenderers();
                                var pss = instance.transform.GetComponents<ParticleSystem>();
                                var pscs = instance.transform.GetComponentsInChildren<ParticleSystem>();
                                var color = new Color(0 / 255f, 229 / 255f, 255 / 255f, 255 / 255f);
                                foreach (var ps in pss)
                                    ps.startColor = color;
                                foreach (var ps in pscs)
                                    ps.startColor = color;
                                udn = instance;
                                onComplete.Invoke(udn);
                            }));
                }

                if (objectId.Equals("ThanosRealityStoneEffect")) {
                    UnityDisplayNode udn = null;
                    __instance.FindAndSetupPrototypeAsync("2a7cf871777b123409e5cb2ac9f02162",
                        new Action<UnityDisplayNode>(
                            btdUdn => {
                                var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                                instance.name = objectId + "(Clone)";
                                instance.RecalculateGenericRenderers();
                                var pss = instance.transform.GetComponents<ParticleSystem>();
                                var pscs = instance.transform.GetComponentsInChildren<ParticleSystem>();
                                var color = new Color(255 / 255f, 20 / 255f, 20 / 255f, 255 / 255f);
                                foreach (var ps in pss)
                                    ps.startColor = color;
                                foreach (var ps in pscs)
                                    ps.startColor = color;
                                udn = instance;
                                onComplete.Invoke(udn);
                            }));
                }
                
                if (objectId.Equals("ThanosSoulStoneEffect")) {
                    UnityDisplayNode udn = null;
                    __instance.FindAndSetupPrototypeAsync("2a7cf871777b123409e5cb2ac9f02162",
                        new Action<UnityDisplayNode>(
                            btdUdn => {
                                var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                                instance.name = objectId + "(Clone)";
                                instance.RecalculateGenericRenderers();
                                var pss = instance.transform.GetComponents<ParticleSystem>();
                                var pscs = instance.transform.GetComponentsInChildren<ParticleSystem>();
                                var color = new Color(255 / 255f, 128 / 255f, 0 / 255f, 255 / 255f);
                                foreach (var ps in pss)
                                    ps.startColor = color;
                                foreach (var ps in pscs)
                                    ps.startColor = color;
                                udn = instance;
                                onComplete.Invoke(udn);
                            }));
                }
                
                if (objectId.Equals("ThanosTimeStoneEffect")) {
                    UnityDisplayNode udn = null;
                    __instance.FindAndSetupPrototypeAsync("2a7cf871777b123409e5cb2ac9f02162",
                        new Action<UnityDisplayNode>(
                            btdUdn => {
                                var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                                instance.name = objectId + "(Clone)";
                                instance.RecalculateGenericRenderers();
                                var pss = instance.transform.GetComponents<ParticleSystem>();
                                var pscs = instance.transform.GetComponentsInChildren<ParticleSystem>();
                                var color = new Color(0 / 255f, 255 / 255f, 40 / 255f, 255 / 255f);
                                foreach (var ps in pss)
                                    ps.startColor = color;
                                foreach (var ps in pscs)
                                    ps.startColor = color;
                                udn = instance;
                                onComplete.Invoke(udn);
                            }));
                }

                if (objectId.Equals("ThanosMindStoneEffect")) {
                    UnityDisplayNode udn = null;
                    __instance.FindAndSetupPrototypeAsync("2a7cf871777b123409e5cb2ac9f02162",
                        new Action<UnityDisplayNode>(
                            btdUdn => {
                                var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                                instance.name = objectId + "(Clone)";
                                instance.RecalculateGenericRenderers();
                                var pss = instance.transform.GetComponents<ParticleSystem>();
                                var pscs = instance.transform.GetComponentsInChildren<ParticleSystem>();
                                var color = new Color(255 / 255f, 255 / 255f, 0 / 255f, 255 / 255f);
                                foreach (var ps in pss)
                                    ps.startColor = color;
                                foreach (var ps in pscs)
                                    ps.startColor = color;
                                udn = instance;
                                onComplete.Invoke(udn);
                            }));
                }

                if (objectId.Equals("ThanosDarkshiftZone")) {
                    UnityDisplayNode udn = null;
                    __instance.FindAndSetupPrototypeAsync("0b51091b90e723c4f9f1caaa123e2b8f",
                        new Action<UnityDisplayNode>(
                            btdUdn => {
                                var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                                instance.name = objectId + "(Clone)";
                                instance.RecalculateGenericRenderers();
                                var pss = instance.transform.GetComponents<ParticleSystem>();
                                var pscs = instance.transform.GetComponentsInChildren<ParticleSystem>();
                                var color = new Color(0 / 255f, 229 / 255f, 255 / 255f, 255 / 255f);
                                foreach (var ps in pss)
                                    ps.startColor = color;
                                foreach (var ps in pscs)
                                    ps.startColor = color;
                                udn = instance;
                                onComplete.Invoke(udn);
                            }));
                }

                if (!protos.ContainsKey(objectId) && objectId.Equals("ThanosT0")) {
                    var udn = GetThanos(__instance.PrototypeRoot, 0);
                    udn.name = objectId;
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (!protos.ContainsKey(objectId) && objectId.Equals("ThanosT1")) {
                    var udn = GetThanos(__instance.PrototypeRoot, 1);
                    udn.name = objectId;
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (!protos.ContainsKey(objectId) && objectId.Equals("ThanosT2")) {
                    var udn = GetThanos(__instance.PrototypeRoot, 2);
                    udn.name = objectId;
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (!protos.ContainsKey(objectId) && objectId.Equals("ThanosT3")) {
                    var udn = GetThanos(__instance.PrototypeRoot, 3);
                    udn.name = objectId;
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (!protos.ContainsKey(objectId) && objectId.Equals("ThanosT4")) {
                    var udn = GetThanos(__instance.PrototypeRoot, 4);
                    udn.name = objectId;
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (!protos.ContainsKey(objectId) && objectId.Equals("ThanosT5")) {
                    var udn = GetThanos(__instance.PrototypeRoot, 5);
                    udn.name = objectId;
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }

                if (protos.ContainsKey(objectId)) {
                    onComplete.Invoke(protos[objectId]);
                    return false;
                }

                return true;
            }
        }

        public static AssetBundle Assets { get; set; }

        public static UnityDisplayNode GetThanos(Transform transform, int tier) {
            var udn = Object.Instantiate(Assets.LoadAsset($"ThanosT{tier}").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.transform.Rotate(new Vector3(-15, 0, 0));
            udn.gameObject.AddComponent<SetScale9>();
            if (tier == 5)
                udn.gameObject.AddComponent<ChromaColor>();
            return udn;
        }


        public static void PlaySnap() => AudioFactory_CreateStartingSources.inst.PlaySoundFromUnity(null, "thanos_snap", "FX", 1, 0.4f, 0, false);

        [HarmonyPatch(typeof(AudioFactory), nameof(AudioFactory.Start))]
        public sealed class AudioFactory_CreateStartingSources {
            public static AudioFactory inst;

            [HarmonyPostfix]
            public static void Prefix(AudioFactory __instance) {
                inst = __instance;
                if (!__instance.audioClips.ContainsKey("thanos_snap"))
                    __instance.RegisterAudioClip("thanos_snap", Assets.LoadAsset("Snap").Cast<AudioClip>());
            }
        }

        [HarmonyPatch(typeof(Factory), nameof(Factory.ProtoFlush))]
        public sealed class PrototypeFlushUDN_Patch {
            [HarmonyPostfix]
            public static void Postfix() {
                foreach (var proto in PrototypeUDN_Patch.protos.Values)
                    Object.Destroy(proto.gameObject);
                PrototypeUDN_Patch.protos.Clear();
            }
        }

        [HarmonyPatch(typeof(ResourceLoader), nameof(ResourceLoader.LoadSpriteFromSpriteReferenceAsync))]
        public sealed class ResourceLoader_Patch {
            [HarmonyPostfix]
            public static void Postfix(SpriteReference reference, ref Image image) {
                if (reference != null && reference.guidRef.StartsWith("ThanosPortrait")) {
                    try {
                        var b = Assets.LoadAsset("Portrait" + reference.guidRef.Replace("ThanosPortrait", ""));
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                }
                
                if (reference != null && reference.guidRef.Equals("PowerStone_U0")) {
                    try {
                        var b = Assets.LoadAsset(reference.guidRef);
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                }
                if (reference != null && reference.guidRef.Equals("SpaceStone_U1")) {
                    try {
                        var b = Assets.LoadAsset(reference.guidRef);
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                }
                if (reference != null && reference.guidRef.Equals("RealityStone_U2")) {
                    try {
                        var b = Assets.LoadAsset(reference.guidRef);
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                }
                if (reference != null && reference.guidRef.Equals("SoulStone_U3")) {
                    try {
                        var b = Assets.LoadAsset(reference.guidRef);
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                }
                if (reference != null && reference.guidRef.Equals("TimeStone_U4")) {
                    try {
                        var b = Assets.LoadAsset(reference.guidRef);
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                }
                if (reference != null && reference.guidRef.Equals("MindStone_U5")) {
                    try {
                        var b = Assets.LoadAsset(reference.guidRef);
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                }
                if (reference != null && reference.guidRef.Equals("Thanos_GauntletAbility")) {
                    try {
                        var b = Assets.LoadAsset("Gauntlet");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                }
            }
        }
        
        [HarmonyPatch(typeof(Weapon), nameof(Weapon.SpawnDart))]
        public sealed class WI {

            [HarmonyPostfix]
            public static void Postfix(ref Weapon __instance) {
                if (__instance == null) return;
                if (__instance.weaponModel == null) return;
                if (__instance.weaponModel.name == null) return;
                if (__instance.attack == null) return;
                if (__instance.attack.tower == null) return;
                if (__instance.attack.tower.Node == null) return;
                if (__instance.attack.tower.Node.graphic == null) return;

                try {
                    if (__instance.weaponModel.name.EndsWith("thanospunch")) {
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().StopPlayback();
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("Punch");
                    }
                    if (__instance.weaponModel.name.EndsWith("thanossnap")) {
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().StopPlayback();
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("Snap");
                        PlaySnap();
                    }
                } catch { }
            }
        }

        [HarmonyPatch(typeof(Bloon), nameof(Bloon.Damage))]
        public sealed class BD {
            private static bool should;

            [HarmonyPrefix]
            internal static bool Prefix(ref float totalAmount, Projectile projectile) {
                if (projectile?.Weapon?.weaponModel?.name?.EndsWith("thanossnap") == true) {
                    if (should)
                        totalAmount = 0;
                    else
                        totalAmount = 10000000f;

                    should = !should;
                }

                return true;
            }
        }

        [HarmonyPatch(typeof(Ability), nameof(Ability.Activate))]
        public sealed class AA {
            [HarmonyPrefix]
            internal static bool Prefix(ref Ability __instance) {
                if (__instance?.abilityModel?.name?.EndsWith("TimeStone") ?? true) {
                    __instance.Sim.StartRaceRound();
                    __instance.Sim.map.spawner.currentRound.Write(__instance.Sim.map.spawner.currentRound.ValueInt + 1);
                }

                if (__instance?.abilityModel?.name?.EndsWith("SoulStone") ?? true) {
                    var closestBloon = __instance.Sim.bloonManager.GetClosestTarget(500, __instance.tower.Position.data);
                    if (closestBloon != null) {
                        var health = closestBloon.Health;
                        closestBloon.Degrade(true, __instance.tower, true);
                        __instance.Sim.SetHealth(__instance.Sim.Health + health);
                        __instance.Sim.MaxHealth = 1000000;
                    }
                }

                if (__instance?.abilityModel?.name?.EndsWith("RealityStone") ?? true) {
                    foreach (var bloon in InGame.instance.bridge.GetAllBloons()) {
                        if (bloon != null) {
                            int id = bloon.id;

                            var bloonSim = InGame.instance.bridge.GetBloonFromId(id);
                            bloonSim.Damage(1000, null, false, false, true, __instance.tower);
                        }
                    }
                }

                return true;
            }
        }
    }
}
