using Assets.Scripts;

namespace GodlyTowers.Towers {
    public sealed class Steve {
        public static string name = "Steve";

        public static UpgradeModel[] GetUpgrades() => new UpgradeModel[] {
            new UpgradeModel("Acquire Hardware", 64, 0, new("AcquireHardware"), 0, 0, 0, "", "Acquire Hardware"),
            new UpgradeModel("Take Aim", 640, 0, new("TakeAim"), 0, 1, 0, "", "Take Aim"),
            new UpgradeModel("Ol' Betsy", 1600, 0, new("OlBetsy"), 0, 2, 0, "", "Ol' Betsy"),
            new UpgradeModel("Enchanter", 6400, 0, new("Enchanter"), 0, 3, 0, "", "Enchanter"),
            new UpgradeModel("DIAMONDS!", 16000, 0, new("DIAMONDS"), 0, 4, 0, "", "DIAMONDS!")
        };

        public static (TowerModel, ShopTowerDetailsModel, TowerModel[], UpgradeModel[]) GetTower(GameModel gameModel) {
            var SteveDetails = gameModel.towerSet[0].Clone().Cast<ShopTowerDetailsModel>();
            SteveDetails.towerId = name;
            SteveDetails.towerIndex = GlobalTowerIndex.Index;

            if (!LocalizationManager.Instance.textTable.ContainsKey("Acquire Hardware Description"))
                LocalizationManager.Instance.textTable.Add("Acquire Hardware Description", "Smelt an iron ingot");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Take Aim Description"))
                LocalizationManager.Instance.textTable.Add("Take Aim Description", "Shoot something with an arrow");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Ol' Betsy Description"))
                LocalizationManager.Instance.textTable.Add("Ol' Betsy Description", "Shoot a crossbow");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Enchanter Description"))
                LocalizationManager.Instance.textTable.Add("Enchanter Description", "Enchant an item at an Enchantment Table");
            if (!LocalizationManager.Instance.textTable.ContainsKey("DIAMONDS! Description"))
                LocalizationManager.Instance.textTable.Add("DIAMONDS! Description", "Acquire diamonds");


            return (GetT0(gameModel), SteveDetails, new[] { GetT0(gameModel), GetT1(gameModel), GetT2(gameModel), GetT3(gameModel), GetT4(gameModel), GetT5(gameModel) }, GetUpgrades());
        }

        public static unsafe TowerModel GetT0(GameModel gameModel) {
            var Steve = gameModel.towers[0].Clone().Cast<TowerModel>();

            Steve.name = name;
            Steve.baseId = name;
            Steve.display = "Steve0";
            Steve.portrait = new("StevePortraitT0");
            Steve.icon = new("StevePortraitT0");
            Steve.towerSet = "Primary";
            Steve.emoteSpriteLarge = new("Game");
            Steve.radius = 8;
            Steve.cost = 800;
            Steve.range = 35;
            Steve.mods = Array.Empty<ApplyModModel>();
            Steve.upgrades = new UpgradePathModel[] { new("Acquire Hardware", name + "-100") };

            for (var i = 0; i < Steve.behaviors.Count; i++) {
                var b = Steve.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = gameModel.towers.First(a => a.name.Contains("Sauda 20")).behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<AttackModel>()).Clone().Cast<AttackModel>();
                    att.range = 35;
                    att.weapons[0].name = "stevehitw";
                    att.weapons[0].rate = 1.6f;
                    att.weapons[0].projectile.pierce *= 5;
                    att.weapons[0].projectile.radius *= 2;
                    att.weapons[0].projectile.ignorePierceExhaustion = true;
                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Remove(a => a.GetIl2CppType() == Il2CppType.Of<AddBehaviorToBloonModel>());
                    for (var j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        var pb = att.weapons[0].projectile.behaviors[j];

                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var d = pb.Cast<DamageModel>();

                            d.damage = 4;

                            pb = d;
                        }
                    }
                    Steve.behaviors[i] = att;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "Steve0";
                    b = display;
                }
            }

            var link = gameModel.towers.First(a => a.name.Contains("Sauda 20")).behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<LinkProjectileRadiusToTowerRangeModel>()).Clone().Cast<LinkProjectileRadiusToTowerRangeModel>();
            link.projectileModel.behaviors = link.projectileModel.behaviors.Remove(a => a.GetIl2CppType() == Il2CppType.Of<AddBehaviorToBloonModel>());
            link.baseTowerRange = 35;

            Steve.behaviors = Steve.behaviors.Add(link, new OverrideCamoDetectionModel("OCDM_", true));

            return Steve;
        }

        public static unsafe TowerModel GetT1(GameModel gameModel) {
            var Steve = GetT0(gameModel).Clone().Cast<TowerModel>();

            Steve.name = name + "-100";
            Steve.baseId = name;
            Steve.display = "Steve1";
            Steve.portrait = new("StevePortraitT1");
            Steve.icon = new("StevePortraitT1");
            Steve.towerSet = "Primary";
            Steve.emoteSpriteLarge = new("Game");
            Steve.tier = 1;
            Steve.tiers = new[] { 1, 0, 0 };
            Steve.range = 35;
            Steve.mods = Array.Empty<ApplyModModel>();
            Steve.upgrades = new UpgradePathModel[] { new("Take Aim", name + "-200") };

            for (var i = 0; i < Steve.behaviors.Count; i++) {
                var b = Steve.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();
                    att.weapons[0].name = "stevehitw";
                    att.weapons[0].projectile.radius *= 1.25f;
                    for (var j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        var pb = att.weapons[0].projectile.behaviors[j];

                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var d = pb.Cast<DamageModel>();

                            d.damage = 6;

                            pb = d;
                        }

                        att.weapons[0].projectile.behaviors[j] = pb;
                    }
                    Steve.behaviors[i] = att;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "Steve1";
                }
            }

            return Steve;
        }

        public static unsafe TowerModel GetT2(GameModel gameModel) {
            var Steve = GetT1(gameModel).Clone().Cast<TowerModel>();

            Steve.name = name + "-200";
            Steve.baseId = name;
            Steve.display = "Steve2";
            Steve.portrait = new("StevePortraitT2");
            Steve.icon = new("StevePortraitT2");
            Steve.towerSet = "Primary";
            Steve.emoteSpriteLarge = new("Game");
            Steve.tier = 2;
            Steve.tiers = new[] { 2, 0, 0 };
            Steve.range = 50;
            Steve.upgrades = new UpgradePathModel[] { new("Ol' Betsy", name + "-300") };

            var dartAM = gameModel.towers[0].behaviors.First(a=>a.Is<AttackModel>(out _)).CloneCast<AttackModel>();

            for (int i = 0; i < Steve.behaviors.Count; i++) {
                if (Steve.behaviors[i].Is<AttackModel>(out _)) {
                    dartAM.range = 50;
                    dartAM.weapons[0].rate = 1.333333333333333f;
                    dartAM.weapons[0].name = "stevehita";
                    dartAM.weapons[0].projectile.display = "StArrow";
                    for (var j = 0; j < dartAM.weapons[0].projectile.behaviors.Length; j++) {
                        var pb = dartAM.weapons[0].projectile.behaviors[j];

                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var d = pb.Cast<DamageModel>();

                            d.damage = 11*2;

                            pb = d;
                        }

                        dartAM.weapons[0].projectile.behaviors[j] = pb;
                    }
                    Steve.behaviors[i] = dartAM;
                }

                if (Steve.behaviors[i].Is<DisplayModel>(out var dm)) {
                    dm.display = "Steve2";
                }
            }

            return Steve;
        }

        public static unsafe TowerModel GetT3(GameModel gameModel) {
            var Steve = GetT2(gameModel).Clone().Cast<TowerModel>();

            Steve.name = name + "-300";
            Steve.baseId = name;
            Steve.display = "Steve3";
            Steve.portrait = new("StevePortraitT3");
            Steve.icon = new("StevePortraitT3");
            Steve.towerSet = "Primary";
            Steve.emoteSpriteLarge = new("Game");
            Steve.tier = 3;
            Steve.tiers = new[] { 3, 0, 0 };
            Steve.range = 50;
            Steve.upgrades = new UpgradePathModel[] { new("Enchanter", name + "-400") };

            for (var i = 0; i < Steve.behaviors.Count; i++) {
                var b = Steve.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();

                    att.weapons[0].name = "stevehitb";
                    att.weapons[0].rate = 1.25f;

                    for (var k = 0; k < att.weapons[0].projectile.behaviors.Length; k++) {
                        var pb = att.weapons[0].projectile.behaviors[k];

                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var d = pb.Cast<DamageModel>();

                            d.damage = 18*3;
                        }
                    }

                    Steve.behaviors[i] = att;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "Steve3";
                }
            }

            return Steve;
        }

        public static unsafe TowerModel GetT4(GameModel gameModel) {
            var Steve = GetT3(gameModel).Clone().Cast<TowerModel>();

            Steve.name = name + "-400";
            Steve.baseId = name;
            Steve.display = "Steve4";
            Steve.portrait = new("StevePortraitT4");
            Steve.icon = new("StevePortraitT4");
            Steve.towerSet = "Primary";
            Steve.emoteSpriteLarge = new("Game");
            Steve.tier = 4;
            Steve.tiers = new[] { 4, 0, 0 };
            Steve.range = 75;
            Steve.upgrades = new UpgradePathModel[] { new UpgradePathModel("DIAMONDS!", name + "-500") };

            var add = GetT1(gameModel).behaviors.First(a=>a.Is<AttackModel>(out _)).CloneCast<AttackModel>();

            add.range = 20;

            var dartAM = gameModel.towers.First(a => a.name.Contains("DartMonkey-030")).behaviors.First(a => a.Is<AttackModel>(out _)).CloneCast<AttackModel>();

            for (var i = 0; i < Steve.behaviors.Count; i++) {

                if (Steve.behaviors[i].Is<AttackModel>(out _)) {
                    Steve.behaviors[i] = dartAM;

                    dartAM.range = 75;
                    dartAM.weapons[0].name = "stevehitb";
                    dartAM.weapons[0].projectile.display = "StArrow";
                    dartAM.weapons[0].rate = 1.25f;

                    for (var k = 0; k < dartAM.weapons[0].projectile.behaviors.Length; k++) {
                        var pb = dartAM.weapons[0].projectile.behaviors[k];

                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var d = pb.Cast<DamageModel>();

                            d.damage = 18*4;
                            d.immuneBloonProperties = BloonProperties.None;
                        }

                        if (pb.GetIl2CppType() == Il2CppType.Of<TravelStraitModel>()) {
                            var tsm = pb.Cast<TravelStraitModel>();

                            tsm.lifespan *= 2;
                            tsm.speed *= 1.5f;
                        }
                    }

                    Steve.behaviors[i] = dartAM;
                }

                if (Steve.behaviors[i].Is<DisplayModel>(out var dm)) {
                    dm.display = "Steve4";
                }
            }

            Steve.behaviors = Steve.behaviors.Add(add);

            return Steve;
        }



        public static unsafe TowerModel GetT5(GameModel gameModel) {
            var Steve = gameModel.towers[0].Clone().Cast<TowerModel>();

            Steve.name = name + "-500";
            Steve.baseId = name;
            Steve.display = "Steve5";
            Steve.portrait = new("StevePortraitT5");
            Steve.icon = new("StevePortraitT5");
            Steve.towerSet = "Primary";
            Steve.emoteSpriteLarge = new("Game");
            Steve.radius = 8;
            Steve.cost = 800;
            Steve.range = 85;
            Steve.mods = Array.Empty<ApplyModModel>();
            Steve.upgrades = Array.Empty<UpgradePathModel>();

            var bomb = gameModel.towers.First(a => a.name.Contains("Bomb")).behaviors.First(a => a.Is<AttackModel>(out _)).CloneCast<AttackModel>().weapons[0].projectile;
            var mortar = gameModel.towers.First(a => a.name.Contains("MortarMonkey-002")).behaviors.First(a => a.Is<AttackModel>(out _)).CloneCast<AttackModel>().weapons[0].projectile;

            var add = GetT4(gameModel).behaviors.First(a => a.Is<AttackModel>(out _)).CloneCast<AttackModel>();

            add.range = 85;

            for (var j = 0; j < add.weapons[0].projectile.behaviors.Length; j++) {
                var pb = add.weapons[0].projectile.behaviors[j];

                if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                    var d = pb.Cast<DamageModel>();

                    d.damage = 75;

                    pb = d;
                }
            }

            add.weapons[0].projectile.behaviors = add.weapons[0].projectile.behaviors.Add(bomb.behaviors.First(a => a.Is<CreateProjectileOnContactModel>(out _)).Clone(),
                        bomb.behaviors.First(a => a.Is<CreateEffectOnContactModel>(out _)).Clone(),
                        bomb.behaviors.First(a => a.Is<CreateSoundOnProjectileCollisionModel>(out _)).Clone(),
                        mortar.behaviors.First(a => a.Is<CreateProjectileOnExhaustFractionModel>(out _)).Clone());

            for (var i = 0; i < Steve.behaviors.Count; i++) {
                var b = Steve.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = gameModel.towers.First(a => a.name.Contains("Sauda 20")).behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<AttackModel>()).Clone().Cast<AttackModel>();
                    att.range = 40;
                    att.weapons[0].name = "stevehitl";
                    att.weapons[0].rate = 0.7f;
                    att.weapons[0].projectile.ignorePierceExhaustion = true;
                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Remove(a => a.GetIl2CppType() == Il2CppType.Of<AddBehaviorToBloonModel>());
                    for (var j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        var pb = att.weapons[0].projectile.behaviors[j];

                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var d = pb.Cast<DamageModel>();

                            d.damage = 125;

                            pb = d;
                        }
                    }
                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Add(bomb.behaviors.First(a => a.Is<CreateProjectileOnContactModel>(out _)).Clone(),
                        bomb.behaviors.First(a => a.Is<CreateEffectOnContactModel>(out _)).Clone(),
                        bomb.behaviors.First(a => a.Is<CreateSoundOnProjectileCollisionModel>(out _)).Clone(),
                        mortar.behaviors.First(a => a.Is<CreateProjectileOnExhaustFractionModel>(out _)).Clone(),
                        new WindModel("WindModel_", 25, 75, 0.5f, true, null, 0).Clone());
                    Steve.behaviors[i] = att;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "Steve5";
                    b = display;
                }

                if (b.Is<LinkProjectileRadiusToTowerRangeModel>(out var lprttrm)) {
                    lprttrm.baseTowerRange = Steve.range;
                }
            }

            Steve.behaviors = Steve.behaviors.Add(add);

            return Steve;
        }

        [HarmonyPatch(typeof(Factory), nameof(Factory.FindAndSetupPrototypeAsync))]
        public static class PrototypeUDN_Patch {
            public static Dictionary<string, UnityDisplayNode> protos = new();

            [HarmonyPrefix]
            public static bool Prefix(Factory __instance, string objectId, Il2CppSystem.Action<UnityDisplayNode> onComplete) {
                if (!protos.ContainsKey(objectId) && objectId.StartsWith("Steve")) {
                    var udn = GetSteve(__instance.PrototypeRoot, int.Parse(objectId.Replace("Steve", "")));
                    udn.name = "Steve";
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }

                if (objectId.Equals("StArrow")) {
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
                                    var text = Assets.LoadAsset("arrow").Cast<Texture2D>();
                                    smr.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new(0.5f, 0.5f), 2.7f / 2f);
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

        public static AssetBundle Assets { get; set; }

        public static UnityDisplayNode GetSteve(Transform transform, int tier) {
            var udn = Object.Instantiate(Assets.LoadAsset($"SteveT{tier}").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.gameObject.AddComponent<SetScale10>();
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
                if (reference != null && reference.guidRef.Equals("AcquireHardware"))
                    try {
                        var b = Assets.LoadAsset("AcquireHardware");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            text.filterMode = FilterMode.Point;
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.Equals("TakeAim"))
                    try {
                        var b = Assets.LoadAsset("TakeAim");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            text.filterMode = FilterMode.Point;
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.Equals("OlBetsy"))
                    try {
                        var b = Assets.LoadAsset("OlBetsy");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            text.filterMode = FilterMode.Point;
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.Equals("Enchanter"))
                    try {
                        var b = Assets.LoadAsset("Enchanter");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            text.filterMode = FilterMode.Point;
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.Equals("DIAMONDS"))
                    try {
                        var b = Assets.LoadAsset("DIAMONDS");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            text.filterMode = FilterMode.Point;
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.StartsWith("StevePortraitT"))
                    try {
                        var b = Assets.LoadAsset(reference.guidRef.Replace("StevePortrait", ""));
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            text.filterMode = FilterMode.Point;
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
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
            private static readonly Dictionary<ObjectId, float> remaining = new();

            [HarmonyPrefix]
            public static bool Prefix_SwitchWeapons(ref Weapon __instance) {
                if (__instance == null) return true;
                if (__instance.weaponModel == null) return true;
                if (__instance.weaponModel.name == null) return true;
                if (__instance.attack == null) return true;
                if (__instance.attack.tower == null) return true;
                if (__instance.attack.tower.Node == null) return true;
                if (__instance.attack.tower.Node.graphic == null) return true;

                try {
                    if (__instance.weaponModel.name.EndsWith("stevehitw")) {
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("WSword").gameObject.SetActive(true);
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("DSword").gameObject.SetActive(false);
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("Bow").gameObject.SetActive(false);
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("Cbow").gameObject.SetActive(false);
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().StopPlayback();
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("Swing");
                    }
                } catch (Exception) { }

                try {
                    if (__instance.weaponModel.name.EndsWith("stevehitl")) {
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("WSword").gameObject.SetActive(false);
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("DSword").gameObject.SetActive(true);
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("Bow").gameObject.SetActive(false);
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("Cbow").gameObject.SetActive(false);
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().StopPlayback();
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("Swing");
                    }
                } catch (Exception) { }

                try {
                    if (__instance.weaponModel.name.EndsWith("stevehita")) {
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("WSword").gameObject.SetActive(false);
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("DSword").gameObject.SetActive(false);
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("Bow").gameObject.SetActive(true);
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("Cbow").gameObject.SetActive(false);
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().StopPlayback();
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("Bow");
                    }
                } catch (Exception) { }

                try {
                    if (__instance.weaponModel.name.EndsWith("stevehitb")) {
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("WSword").gameObject.SetActive(false);
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("DSword").gameObject.SetActive(false);
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("Bow").gameObject.SetActive(false);
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("Cbow").gameObject.SetActive(true);
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().StopPlayback();
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("Bow");
                    }
                } catch (Exception) { }

                return true;
            }

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
                    if (__instance.weaponModel.name.EndsWith("stevehitw")) {
                        if (remaining.ContainsKey(__instance.attack.tower.Id))
                            remaining.Remove(__instance.attack.tower.Id);
                        remaining.Add(__instance.attack.tower.Id, 1111);
                        await Task.Run(async () => {
                            while (remaining.ContainsKey(__instance.attack.tower.Id) && remaining[__instance.attack.tower.Id] > 0) {
                                remaining[__instance.attack.tower.Id] -= TimeManager.timeScaleWithoutNetwork + 1;
                                await Task.Delay(1);
                            }
                        });
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("Idle");
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("WSword").gameObject.SetActive(false);
                    }
                } catch (Exception) { }

                try {
                    if (__instance.weaponModel.name.EndsWith("stevehitl")) {
                        if (remaining.ContainsKey(__instance.attack.tower.Id))
                            remaining.Remove(__instance.attack.tower.Id);
                        remaining.Add(__instance.attack.tower.Id, 1111);
                        await Task.Run(async () => {
                            while (remaining.ContainsKey(__instance.attack.tower.Id) && remaining[__instance.attack.tower.Id] > 0) {
                                remaining[__instance.attack.tower.Id] -= TimeManager.timeScaleWithoutNetwork + 1;
                                await Task.Delay(1);
                            }
                        });
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("Idle");
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("DSword").gameObject.SetActive(false);
                    }
                } catch (Exception) { }

                try {
                    if (__instance.weaponModel.name.EndsWith("stevehita")) {
                        if (remaining.ContainsKey(__instance.attack.tower.Id))
                            remaining.Remove(__instance.attack.tower.Id);
                        remaining.Add(__instance.attack.tower.Id, 1111);
                        await Task.Run(async () => {
                            while (remaining.ContainsKey(__instance.attack.tower.Id) && remaining[__instance.attack.tower.Id] > 0) {
                                remaining[__instance.attack.tower.Id] -= TimeManager.timeScaleWithoutNetwork + 1;
                                await Task.Delay(1);
                            }
                        });
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("Idle");
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("Bow").gameObject.SetActive(false);
                    }
                } catch (Exception) { }

                try {
                    if (__instance.weaponModel.name.EndsWith("stevehitb")) {
                        if (remaining.ContainsKey(__instance.attack.tower.Id))
                            remaining.Remove(__instance.attack.tower.Id);
                        remaining.Add(__instance.attack.tower.Id, 1111);
                        await Task.Run(async () => {
                            while (remaining.ContainsKey(__instance.attack.tower.Id) && remaining[__instance.attack.tower.Id] > 0) {
                                remaining[__instance.attack.tower.Id] -= TimeManager.timeScaleWithoutNetwork + 1;
                                await Task.Delay(1);
                            }
                        });
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("Idle");
                        __instance.attack.tower.Node.graphic.transform.Find("Steve").Find("Principal").Find("Peito").Find("Braco.R").Find("Cbow").gameObject.SetActive(false);
                    }
                } catch (Exception) { }
            }
        }
    }
}
