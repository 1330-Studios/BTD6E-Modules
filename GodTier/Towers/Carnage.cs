namespace GodlyTowers.Towers {
    public sealed class Carnage {
        public static string name = "Carnage";

        public static UpgradeModel[] GetUpgrades() => new UpgradeModel[] {
            new("Spike Shooter", 980, 0, new("CarnagePortrait"), 0, 0, 0, "", "Spike Shooter"),
            new("Knulls Empowerment", 1280, 0, new("CarnagePortrait"), 0, 1, 0, "", "Knull's Empowerment"),
            new("Grendel Power", 4700, 0, new("CarnagePortrait"), 0, 2, 0, "", "Grendel Power"),
            new("Maximum", 15666, 0, new("MaxCarnagePortrait"), 0, 3, 0, "", "Maximum")
        };

        public static (TowerModel, ShopTowerDetailsModel, TowerModel[], UpgradeModel[]) GetTower(GameModel gameModel) {
            var carnageDetails = gameModel.towerSet[0].Clone().Cast<ShopTowerDetailsModel>();
            carnageDetails.towerId = name;
            carnageDetails.towerIndex = GlobalTowerIndex.Index;

            if (!LocalizationManager.Instance.textTable.ContainsKey("Carnage"))
                LocalizationManager.Instance.textTable.Add("Carnage", "Carnage (Earth 616)");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Spike Shooter Description"))
                LocalizationManager.Instance.textTable.Add("Spike Shooter Description", "Carnage starts using spikes as weapons.");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Knull's Empowerment Description"))
                LocalizationManager.Instance.textTable.Add("Knull's Empowerment Description", "Deepens the link between Carnage and dark elder god Knull.");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Grendel Power Description"))
                LocalizationManager.Instance.textTable.Add("Grendel Power Description", "There turns out to be more of the Grendel left than originally thought. A more complete link is established.");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Maximum Description"))
                LocalizationManager.Instance.textTable.Add("Maximum Description", "Let there be Carnage.");

            return (GetT0(gameModel), carnageDetails, new[] { GetT0(gameModel), GetT1(gameModel), GetT2(gameModel), GetT3(gameModel), GetT4(gameModel) }, GetUpgrades());
        }

        public static unsafe TowerModel GetT0(GameModel gameModel) {
            var carnage = gameModel.towers[0].Clone().Cast<TowerModel>();

            carnage.name = name;
            carnage.baseId = name;
            carnage.display = "Carnage";
            carnage.portrait = new("CarnagePortrait");
            carnage.icon = new("CarnagePortrait");
            carnage.towerSet = "Military";
            carnage.emoteSpriteLarge = new("Marvel");
            carnage.radius = 8;
            carnage.cost = 800;
            carnage.range = 35;
            carnage.mods = Array.Empty<ApplyModModel>();
            carnage.upgrades = new UpgradePathModel[] { new("Spike Shooter", name + "-100") };

            for (var i = 0; i < carnage.behaviors.Count; i++) {
                var b = carnage.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = gameModel.towers.First(a => a.name.Contains("Sauda 20")).behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<AttackModel>()).Clone().Cast<AttackModel>();
                    att.range = 35;
                    att.weapons[0].name = "cswipe";
                    att.weapons[0].rate = 0.95f;
                    att.weapons[0].projectile.pierce *= 5;
                    att.weapons[0].projectile.radius *= 2;
                    att.weapons[0].projectile.ignorePierceExhaustion = true; 
                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Remove(a => a.GetIl2CppType() == Il2CppType.Of<AddBehaviorToBloonModel>());
                    for (var j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        var pb = att.weapons[0].projectile.behaviors[j];

                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var d = pb.Cast<DamageModel>();

                            d.damage = 3;
                            d.maxDamage = 3;

                            pb = d;
                        }
                    }
                    carnage.behaviors[i] = att;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "Carnage";
                    b = display;
                }
            }

            var link = gameModel.towers.First(a => a.name.Contains("Sauda 20")).behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<LinkProjectileRadiusToTowerRangeModel>()).Clone().Cast<LinkProjectileRadiusToTowerRangeModel>();
            link.projectileModel.behaviors = link.projectileModel.behaviors.Remove(a => a.GetIl2CppType() == Il2CppType.Of<AddBehaviorToBloonModel>());
            link.baseTowerRange = 35;
            
            carnage.behaviors = carnage.behaviors.Add(link, new OverrideCamoDetectionModel("OCDM_", true));

            return carnage;
        }

        public static unsafe TowerModel GetT1(GameModel gameModel) {
            var carnage = GetT0(gameModel).Clone().Cast<TowerModel>();

            carnage.name = name + "-100";
            carnage.baseId = name;
            carnage.display = "Carnage";
            carnage.portrait = new("CarnagePortrait");
            carnage.icon = new("CarnagePortrait");
            carnage.towerSet = "Military";
            carnage.emoteSpriteLarge = new("Movie");
            carnage.tier = 1;
            carnage.tiers = new[] { 1, 0, 0 };
            carnage.range = 50;
            carnage.mods = Array.Empty<ApplyModModel>();
            carnage.upgrades = new UpgradePathModel[] { new("Knulls Empowerment", name + "-200") };

            for (var i = 0; i < carnage.behaviors.Count; i++) {
                var b = carnage.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();
                    att.range = 50;

                    var dartatt = gameModel.towers[0].behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<AttackModel>()).Clone().Cast<AttackModel>();
                    var dartweap = dartatt.weapons[0];
                    dartweap.name = "cspikes";
                    dartweap.rate = 3.25f;
                    dartweap.emission = new ArcEmissionModel("ArcEmissionModel_", 3, 0, 30, null, false);
                    dartweap.projectile.display = "CarnageSpike";

                    att.weapons = att.weapons.Add(dartweap);
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "Carnage";
                }
                
                if (b.GetIl2CppType() == Il2CppType.Of<LinkProjectileRadiusToTowerRangeModel>()) {
                    var link = b.Cast<LinkProjectileRadiusToTowerRangeModel>();
                    link.baseTowerRange = 50;
                }
            }

            return carnage;
        }

        public static unsafe TowerModel GetT2(GameModel gameModel) {
            var carnage = GetT1(gameModel).Clone().Cast<TowerModel>();

            carnage.name = name + "-200";
            carnage.baseId = name;
            carnage.display = "Carnage";
            carnage.portrait = new("CarnagePortrait");
            carnage.icon = new("CarnagePortrait");
            carnage.towerSet = "Military";
            carnage.emoteSpriteLarge = new("Movie");
            carnage.tier = 2;
            carnage.tiers = new[] { 2, 0, 0 };
            carnage.range = 50;
            carnage.upgrades = new UpgradePathModel[] { new("Grendel Power", name + "-300") };

            AbilityModel abm = gameModel.towers.FirstOrDefault(pat => pat.name.Contains("Pat") && pat.tier == 20).behaviors.FirstOrDefault(ab => ab.name.Contains("Rally")).Clone().Cast<AbilityModel>();
            abm.icon = new("Knull");
            abm.name = "Knulls Empowerment";
            abm.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<CreateEffectOnAbilityModel>()).Cast<CreateEffectOnAbilityModel>().effectModel =
            new("EM_", "a73a10bf47571424080d9e2b33bb2045", 1.1f, 9999999.0f, false, false, false, false, false, false, false);
            ActivateTowerDamageSupportZoneModel atdszm = abm.behaviors.FirstOrDefault(AA => AA.name.Contains("ActivateTowerDamageSupportZoneModel")).Cast<ActivateTowerDamageSupportZoneModel>();
            atdszm.damageIncrease = 50;
            atdszm.range = 5;
            atdszm.canEffectThisTower = true;
            atdszm.buffIconName = "";
            atdszm.buffLocsName = "";
            atdszm.showBuffIcon = false;

            carnage.behaviors = carnage.behaviors.Add(abm);

            return carnage;
        }

        public static unsafe TowerModel GetT3(GameModel gameModel) {
            var carnage = GetT2(gameModel).Clone().Cast<TowerModel>();

            carnage.name = name + "-300";
            carnage.baseId = name;
            carnage.display = "CarnageLG";
            carnage.portrait = new("CarnagePortrait");
            carnage.icon = new("CarnagePortrait");
            carnage.towerSet = "Military";
            carnage.emoteSpriteLarge = new("Movie");
            carnage.tier = 3;
            carnage.tiers = new[] { 3, 0, 0 };
            carnage.range = 50;
            carnage.upgrades = new UpgradePathModel[] { new("Maximum", name + "-400") };

            for (var i = 0; i < carnage.behaviors.Count; i++) {
                var b = carnage.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();

                    att.weapons[1].emission = new ArcEmissionModel("ArcEmissionModel_", 9, 0, 90, null, false);
                    for (int j = 0; j < att.weapons.Length; j++) {
                        att.weapons[j].projectile.pierce += 5;
                        att.weapons[j].rate -= 0.2f;

                        for (var k = 0; k < att.weapons[j].projectile.behaviors.Length; k++) {
                            var pb = att.weapons[j].projectile.behaviors[k];

                            if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                                var d = pb.Cast<DamageModel>();

                                d.damage *= 10;
                                d.maxDamage *= 10;
                            }
                        }
                    }
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "CarnageLG";
                }
            }

            return carnage;
        }

        public static unsafe TowerModel GetT4(GameModel gameModel) {
            var carnage = GetT3(gameModel).Clone().Cast<TowerModel>();

            carnage.name = name + "-400";
            carnage.baseId = name;
            carnage.display = "CarnageMax";
            carnage.portrait = new("MaxCarnagePortrait");
            carnage.icon = new("MaxCarnagePortrait");
            carnage.towerSet = "Military";
            carnage.emoteSpriteLarge = new("Movie");
            carnage.tier = 4;
            carnage.tiers = new[] { 4, 0, 0 };
            carnage.range = 66;
            carnage.upgrades = Array.Empty<UpgradePathModel>();

            for (var i = 0; i < carnage.behaviors.Count; i++) {
                var b = carnage.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();

                    att.weapons[1].emission = new ArcEmissionModel("ArcEmissionModel_", 15, 0, 90, null, false);
                    att.weapons[1].projectile.display = "MaxCarnageSpikes";
                    for (int j = 0; j < att.weapons.Length; j++) {
                        att.weapons[j].projectile.pierce += 5;
                        att.weapons[j].rate -= 0.35f;
                        att.range = 66;

                        for (var k = 0; k < att.weapons[j].projectile.behaviors.Length; k++) {
                            var pb = att.weapons[j].projectile.behaviors[k];

                            if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                                var d = pb.Cast<DamageModel>();

                                d.damage *= 6.66f;
                                d.maxDamage *= 6.66f;
                            }
                        }
                    }
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "CarnageMax";
                }

                if (b.GetIl2CppType() == Il2CppType.Of<LinkProjectileRadiusToTowerRangeModel>()) {
                    var link = b.Cast<LinkProjectileRadiusToTowerRangeModel>();
                    link.baseTowerRange = 66;
                }
            }

            AbilityModel abm = carnage.behaviors.First(a=>a.GetIl2CppType() == Il2CppType.Of<AbilityModel>()).Clone().Cast<AbilityModel>();
            abm.icon = new("Knull");
            abm.name = "Knulls Empowerment";
            ActivateTowerDamageSupportZoneModel atdszm = abm.behaviors.FirstOrDefault(AA => AA.name.Contains("ActivateTowerDamageSupportZoneModel")).Cast<ActivateTowerDamageSupportZoneModel>();
            atdszm.damageIncrease = 750;
            atdszm.range = 5;
            atdszm.canEffectThisTower = true;
            atdszm.buffIconName = "";
            atdszm.buffLocsName = "";
            atdszm.showBuffIcon = false;

            return carnage;
        }

        [HarmonyPatch(typeof(Factory), nameof(Factory.FindAndSetupPrototypeAsync))]
        public class PrototypeUDN_Patch {
            public static Dictionary<string, UnityDisplayNode> protos = new();

            [HarmonyPrefix]
            public static bool Prefix(Factory __instance, string objectId, Il2CppSystem.Action<UnityDisplayNode> onComplete) {
                if (!protos.ContainsKey(objectId) && objectId.Equals("CarnageMax")) {
                    var udn = GetCarnageMax(__instance.PrototypeRoot);
                    udn.name = "CarnageMax";
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (!protos.ContainsKey(objectId) && objectId.Equals("Carnage")) {
                    var udn = GetCarnage(__instance.PrototypeRoot);
                    udn.name = "Carnage";
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (!protos.ContainsKey(objectId) && objectId.Equals("CarnageLG")) {
                    var udn = GetCarnageLG(__instance.PrototypeRoot);
                    udn.name = "CarnageLG";
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }

                if (objectId.Equals("CarnageSpike")) {
                    UnityDisplayNode udn = null;
                    __instance.FindAndSetupPrototypeAsync("bdbeaa256e6c63b45829535831843376",
                        new Action<UnityDisplayNode>(oudn => {
                            var nudn = Object.Instantiate(oudn, __instance.PrototypeRoot);
                            nudn.name = objectId + "(Clone)";
                            nudn.isSprite = true;
                            nudn.RecalculateGenericRenderers();
                            for (var i = 0; i < nudn.genericRenderers.Length; i++) {
                                if (nudn.genericRenderers[i].GetIl2CppType() == Il2CppType.Of<SpriteRenderer>()) {
                                    var smr = nudn.genericRenderers[i].Cast<SpriteRenderer>();
                                    var text = Assets.LoadAsset("CarnageSpike").Cast<Texture2D>();
                                    smr.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new(0.5f, 0.5f), 22.5f);
                                    nudn.genericRenderers[i] = smr;
                                }
                            }

                            udn = nudn;
                            onComplete.Invoke(udn);
                        }));
                    return false;
                }

                if (objectId.Equals("MaxCarnageSpikes")) {
                    UnityDisplayNode udn = null;
                    __instance.FindAndSetupPrototypeAsync("bdbeaa256e6c63b45829535831843376",
                        new Action<UnityDisplayNode>(oudn => {
                            var nudn = Object.Instantiate(oudn, __instance.PrototypeRoot);
                            nudn.name = objectId + "(Clone)";
                            nudn.isSprite = true;
                            nudn.RecalculateGenericRenderers();
                            for (var i = 0; i < nudn.genericRenderers.Length; i++) {
                                if (nudn.genericRenderers[i].GetIl2CppType() == Il2CppType.Of<SpriteRenderer>()) {
                                    var smr = nudn.genericRenderers[i].Cast<SpriteRenderer>();
                                    var text = Assets.LoadAsset("MaxCarnageSpikes").Cast<Texture2D>();
                                    smr.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new(0.5f, 0.5f), 22.5f);
                                    nudn.genericRenderers[i] = smr;
                                }
                            }

                            udn = nudn;
                            onComplete.Invoke(udn);
                        }));
                    return false;
                }

                if (protos.ContainsKey(objectId)) {
                    onComplete.Invoke(protos[objectId]);
                    return false;
                }

                return true;
            }
        }

        private static AssetBundle __asset;

        public static AssetBundle Assets {
            get => __asset;
            set => __asset = value;
        }

        public static UnityDisplayNode GetCarnageMax(Transform transform) {
            var udn = Object.Instantiate(Assets.LoadAsset("Carnage_Max").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.gameObject.AddComponent<SetScale4>();
            return udn;
        }

        public static UnityDisplayNode GetCarnage(Transform transform) {
            var udn = Object.Instantiate(Assets.LoadAsset("Carnage").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.gameObject.AddComponent<SetScale4>();
            return udn;
        }

        public static UnityDisplayNode GetCarnageLG(Transform transform) {
            var udn = Object.Instantiate(Assets.LoadAsset("Carnage").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.gameObject.AddComponent<SetScale5>();
            return udn;
        }

        [HarmonyPatch(typeof(Factory), nameof(Factory.ProtoFlush))]
        public class PrototypeFlushUDN_Patch {
            [HarmonyPostfix]
            public static void Postfix() {
                foreach (var proto in PrototypeUDN_Patch.protos.Values)
                    Object.Destroy(proto.gameObject);
                PrototypeUDN_Patch.protos.Clear();
            }
        }


        [HarmonyPatch(typeof(ResourceLoader), nameof(ResourceLoader.LoadSpriteFromSpriteReferenceAsync))]
        public class ResourceLoader_Patch {
            [HarmonyPostfix]
            public static void Postfix(SpriteReference reference, ref Image image) {
                if (reference != null && reference.guidRef.Equals("CarnagePortrait"))
                    try {
                        var text = LoadTextureFromBytes(Properties.Resources.CarnagePortrait).Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = LoadSprite(text);
                    } catch { }
                if (reference != null && reference.guidRef.Equals("MaxCarnagePortrait"))
                    try {
                        var text = LoadTextureFromBytes(Properties.Resources.MaxCarnagePortrait).Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = LoadSprite(text);
                    } catch { }
                if (reference != null && reference.guidRef.Equals("Knull"))
                    try {
                        var text = LoadTextureFromBytes(Properties.Resources.Knull).Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = LoadSprite(text);
                    } catch { }
            }

            private static Texture2D LoadTextureFromBytes(byte[] FileData) {
                Texture2D Tex2D = new(2, 2);
                if (ImageConversion.LoadImage(Tex2D, FileData)) return Tex2D;

                return null;
            }

            private static Sprite LoadSprite(Texture2D text) {
                return Sprite.Create(text, new(0, 0, text.width, text.height), new());
            }
        }

        [HarmonyPatch(typeof(Weapon), nameof(Weapon.SpawnDart))]
        public static class WI {
            private static readonly Dictionary<int, float> remaining = new();

            [HarmonyPostfix]
            public static void Postfix(ref Weapon __instance) => RunAnimations(__instance);

            private static async Task RunAnimations(Weapon __instance) {
                if (__instance == null) return;
                if (__instance.weaponModel == null) return;
                if (__instance.weaponModel.name == null) return;
                if (__instance.attack == null) return;
                if (__instance.attack.tower == null) return;
                if (__instance.attack.tower.Node == null) return;
                if (__instance.attack.tower.Node.graphic == null) return;

                try {
                    if (__instance.weaponModel.name.EndsWith("cswipe")) {
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().StopPlayback();
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().speed = 2f;
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("SWIPE");
                        if (remaining.ContainsKey(__instance.attack.tower.Id.Id))
                            remaining.Remove(__instance.attack.tower.Id.Id);
                        remaining.Add(__instance.attack.tower.Id.Id, 1111);
                        await Task.Run(async () => {
                            while (remaining.ContainsKey(__instance.attack.tower.Id.Id) && remaining[__instance.attack.tower.Id.Id] > 0) {
                                remaining[__instance.attack.tower.Id.Id] -= TimeManager.timeScaleWithoutNetwork + 1;
                                await Task.Delay(1);
                            }
                            return;
                        });
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("IDLE");
                    }
                    if (__instance.weaponModel.name.EndsWith("cspikes")) {
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().StopPlayback();
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().speed = 2f;
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("SPIKES");
                        if (remaining.ContainsKey(__instance.attack.tower.Id.Id))
                            remaining.Remove(__instance.attack.tower.Id.Id);
                        remaining.Add(__instance.attack.tower.Id.Id, 1111);
                        await Task.Run(async () => {
                            while (remaining.ContainsKey(__instance.attack.tower.Id.Id) && remaining[__instance.attack.tower.Id.Id] > 0) {
                                remaining[__instance.attack.tower.Id.Id] -= TimeManager.timeScaleWithoutNetwork + 1;
                                await Task.Delay(1);
                            }
                            return;
                        });
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("IDLE");
                    }
                } catch (Exception) { }
            }
        }
    }
}
