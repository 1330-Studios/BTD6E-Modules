namespace GodlyTowers.Towers {
    public sealed class Spider_Man {
        public static string name = "SpiderMan";

        public static UpgradeModel[] GetUpgrades() => new UpgradeModel[] {
            new("Iron Spider", 980, 0, new("SMPortrait1"), 0, 0, 0, "", "Iron Spider"),
            new("Upgraded Stark Suit", 2500, 0, new("SMPortrait2"), 0, 1, 0, "", "Upgraded Stark Suit"),
            new("Black and Gold Suit", 4575, 0, new("SMPortrait3"), 0, 2, 0, "", "Black and Gold Suit")
        };

        public static (TowerModel, ShopTowerDetailsModel, TowerModel[], UpgradeModel[]) GetTower(GameModel gameModel) {
            var spidermanDetails = gameModel.towerSet[0].Clone().Cast<ShopTowerDetailsModel>();
            spidermanDetails.towerId = name;
            spidermanDetails.towerIndex = GlobalTowerIndex.Index;


            if (!LocalizationManager.Instance.textTable.ContainsKey("SpiderMan"))
                LocalizationManager.Instance.textTable.Add("SpiderMan", "Spider-Man (Earth 199999)");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Iron Spider Description"))
                LocalizationManager.Instance.textTable.Add("Iron Spider Description", "New technology allows for faster web-slinging.");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Upgraded Stark Suit Description"))
                LocalizationManager.Instance.textTable.Add("Upgraded Stark Suit Description", "This suit has improved web-launching speed and will knock back bloons.");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Black and Gold Suit Description"))
                LocalizationManager.Instance.textTable.Add("Black and Gold Suit Description", "Electical currents flowing through the suit causes webs to contain large pulses of electricity.");

            return (GetT0(gameModel), spidermanDetails, new[] { GetT0(gameModel), GetT1(gameModel), GetT2(gameModel), GetT3(gameModel) }, GetUpgrades());
        }

        public static unsafe TowerModel GetT0(GameModel gameModel) {
            var spiderman = gameModel.towers[0].Clone().Cast<TowerModel>();

            spiderman.name = name;
            spiderman.baseId = name;
            spiderman.display = "SpiderMan";
            spiderman.portrait = new("SMPortrait");
            spiderman.icon = new("SMPortrait");
            spiderman.towerSet = "Military";
            spiderman.emoteSpriteLarge = new("Marvel");
            spiderman.radius = 8;
            spiderman.cost = 1250;
            spiderman.range = 75;
            spiderman.mods = Array.Empty<ApplyModModel>();
            spiderman.upgrades = new UpgradePathModel[] { new("Iron Spider", name + "-100") };

            var gluegunner = gameModel.towers.First(a => a.name.Equals("GlueGunner-203")).Clone().Cast<TowerModel>();
            var glueproj = gluegunner.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<AttackModel>()).Cast<AttackModel>().weapons[0].projectile;

            glueproj.filters = Array.Empty<FilterModel>();

            for (var i = 0; i < spiderman.behaviors.Count; i++) {
                var b = spiderman.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();
                    att.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<AttackFilterModel>()).Cast<AttackFilterModel>().filters = Array.Empty<FilterModel>();
                    att.weapons[0].name = "web";
                    att.weapons[0].animationOffset = 0.3f;
                    att.weapons[0].rate *= 2;
                    att.weapons[0].rateFrames *= 2;
                    att.weapons[0].projectile = glueproj;
                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Add(new DamageModel("DM_", 2, 2, true, false, true, BloonProperties.None, BloonProperties.None));
                    att.range = 75;


                    for (var j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        var pb = att.weapons[0].projectile.behaviors[j];
                        if (pb.GetIl2CppType() == Il2CppType.Of<AddBehaviorToBloonModel>()) {
                            var abtbm = pb.Cast<AddBehaviorToBloonModel>();
                            abtbm.behaviors[0].Cast<DamageOverTimeModel>().initialDelay = 0;
                            abtbm.behaviors[0].Cast<DamageOverTimeModel>().initialDelayFrames = 0;
                            abtbm.behaviors[0].Cast<DamageOverTimeModel>().interval /= 2;
                            abtbm.behaviors[0].Cast<DamageOverTimeModel>().intervalFrames /= 2;
                        }
                    }

                    spiderman.behaviors[i] = att;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "SpiderMan";
                }
            }

            spiderman.behaviors = spiderman.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true)); // spidey senses :))

            return spiderman;
        }

        public static unsafe TowerModel GetT1(GameModel gameModel) {
            var spiderman = GetT0(gameModel).Clone().Cast<TowerModel>();

            spiderman.name = name + "-100";
            spiderman.baseId = name;
            spiderman.display = "SpiderMan1";
            spiderman.portrait = new("SMPortrait1");
            spiderman.icon = new("SMPortrait1");
            spiderman.towerSet = "Military";
            spiderman.emoteSpriteLarge = new("Movie");
            spiderman.tier = 1;
            spiderman.tiers = new[] { 1, 0, 0 };
            spiderman.range = 100;
            spiderman.mods = Array.Empty<ApplyModModel>();
            spiderman.upgrades = new UpgradePathModel[] { new("Upgraded Stark Suit", name + "-200") };

            for (var i = 0; i < spiderman.behaviors.Count; i++) {
                var b = spiderman.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();
                    att.weapons[0].name = "web";
                    att.weapons[0].rate /= 1.8f;
                    att.weapons[0].emission = new ArcEmissionModel("AEM_", 3, 0, 45, null, false);
                    att.weapons[0].rateFrames = (int)(att.weapons[0].rateFrames / 1.8f);
                    att.range = 100;
                    for (var j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        var pb = att.weapons[0].projectile.behaviors[j];
                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var d = pb.Cast<DamageModel>();

                            d.damage = 4;
                            d.maxDamage = 4;
                        }
                        if (pb.GetIl2CppType() == Il2CppType.Of<AddBehaviorToBloonModel>()) {
                            var abtbm = pb.Cast<AddBehaviorToBloonModel>();
                            abtbm.behaviors[0].Cast<DamageOverTimeModel>().damage *= 2;
                            abtbm.behaviors[0].Cast<DamageOverTimeModel>().interval /= 2;
                            abtbm.behaviors[0].Cast<DamageOverTimeModel>().intervalFrames /= 2;
                        }
                    }
                    spiderman.behaviors[i] = att;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "SpiderMan1";
                }
            }

            return spiderman;
        }

        public static unsafe TowerModel GetT2(GameModel gameModel) {
            var spiderman = GetT1(gameModel).Clone().Cast<TowerModel>();

            spiderman.name = name + "-200";
            spiderman.baseId = name;
            spiderman.display = "SpiderMan2";
            spiderman.portrait = new("SMPortrait2");
            spiderman.icon = new("SMPortrait2");
            spiderman.towerSet = "Military";
            spiderman.emoteSpriteLarge = new("Movie");
            spiderman.tier = 2;
            spiderman.tiers = new[] { 2, 0, 0 };
            spiderman.range = 100;
            spiderman.mods = Array.Empty<ApplyModModel>();
            spiderman.upgrades = new UpgradePathModel[] { new("Black and Gold Suit", name + "-300") };

            for (var i = 0; i < spiderman.behaviors.Count; i++) {
                var b = spiderman.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();
                    att.weapons[0].name = "web";
                    att.range = 100;
                    for (var j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        var pb = att.weapons[0].projectile.behaviors[j];
                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var d = pb.Cast<DamageModel>();

                            d.damage = 15;
                            d.maxDamage = 15;
                        }
                        if (pb.GetIl2CppType() == Il2CppType.Of<AddBehaviorToBloonModel>()) {
                            var abtbm = pb.Cast<AddBehaviorToBloonModel>();
                            abtbm.behaviors[0].Cast<DamageOverTimeModel>().damage *= 5;
                            abtbm.behaviors[0].Cast<DamageOverTimeModel>().interval /= 4;
                            abtbm.behaviors[0].Cast<DamageOverTimeModel>().intervalFrames /= 4;
                        }
                        if (pb.GetIl2CppType() == Il2CppType.Of<TravelStraitModel>()) {
                            var tsm = pb.Cast<TravelStraitModel>();

                            tsm.speed *= 2;
                            tsm.speedFrames *= 2;
                        }
                    }
                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Add(new KnockbackModel("KBModel_", 0.5f, 1.25f, 1.7f, 0.5f, "KnockbackKnockback"));
                    spiderman.behaviors[i] = att;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "SpiderMan2";
                }
            }

            return spiderman;
        }

        public static unsafe TowerModel GetT3(GameModel gameModel) {
            var spiderman = GetT2(gameModel).Clone().Cast<TowerModel>();

            spiderman.name = name + "-300";
            spiderman.baseId = name;
            spiderman.display = "SpiderMan3";
            spiderman.portrait = new("SMPortrait3");
            spiderman.icon = new("SMPortrait3");
            spiderman.towerSet = "Military";
            spiderman.emoteSpriteLarge = new("Movie");
            spiderman.tier = 3;
            spiderman.tiers = new[] { 3, 0, 0 };
            spiderman.range = 150;
            spiderman.upgrades = Array.Empty<UpgradePathModel>();

            for (var i = 0; i < spiderman.behaviors.Count; i++) {
                var b = spiderman.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();
                    att.weapons[0].name = "web";
                    att.weapons[0].emission = new ArcEmissionModel("AEM_", 36, 0, 360, null, false);
                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Add(new CreateLightningEffectModel("CLEM_", 0.3f, new string[] {
                                    "548c26e4e668dac4a850a4c016916016", "ffed377b3e146f649b3e6d5767726a44", "c5e4bf0202becd0459c47b8184b4588f",
                                    "3e113b397a21a3a4687cf2ed0c436ec8", "c6c2049a0c01e8a4d9904db8c9b84ca0", "e9b2a3d6f0fe0e4419a423e4d2ebe6f6",
                                    "c8471dcde4c65fc459f7846c6a932a8c", "a73b565de9c31c14ebcd3317705ab17e", "bd23939e7362b8e40a3a39f595a2a1dc"
                                }, new float[] { 18, 18, 18, 50, 50, 50, 85, 85, 85 }),
                                new LightningModel("LM_", 5, new ArcEmissionModel("AEM_", 3, 0, 360, null, false), 360, 5));
                    att.range = 150;
                    for (var j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        var pb = att.weapons[0].projectile.behaviors[j];
                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var d = pb.Cast<DamageModel>();

                            d.damage = 30;
                            d.maxDamage = 30;
                        }
                        if (pb.GetIl2CppType() == Il2CppType.Of<AddBehaviorToBloonModel>()) {
                            var abtbm = pb.Cast<AddBehaviorToBloonModel>();
                            abtbm.behaviors[0].Cast<DamageOverTimeModel>().damage *= 10;
                            abtbm.behaviors[0].Cast<DamageOverTimeModel>().interval /= 10;
                            abtbm.behaviors[0].Cast<DamageOverTimeModel>().intervalFrames /= 10;
                        }
                    }
                    spiderman.behaviors[i] = att;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "SpiderMan3";
                }
            }

            return spiderman;
        }

        [HarmonyPatch(typeof(Factory), nameof(Factory.FindAndSetupPrototypeAsync))]
        public class PrototypeUDN_Patch {
            public static Dictionary<string, UnityDisplayNode> protos = new();

            [HarmonyPrefix]
            public static bool Prefix(Factory __instance, string objectId, Il2CppSystem.Action<UnityDisplayNode> onComplete) {
                if (!protos.ContainsKey(objectId) && objectId.Equals("SpiderMan3")) {
                    var udn = GetSpiderManBGS(__instance.PrototypeRoot);
                    udn.name = "SpiderManBGS";
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    for (int mI = 0; mI < 9; mI++) {
                        var m = Assets.LoadAsset($"BGS{mI + 1}").Cast<Material>();
                        udn.genericRenderers[mI].material = m ?? throw new Exception($"Cry :cry: {mI}");
                    }
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (!protos.ContainsKey(objectId) && objectId.Equals("SpiderMan2")) {
                    var udn = GetSpiderManFFH(__instance.PrototypeRoot);
                    udn.name = "SpiderManFFH";
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    for (int mI = 0; mI < 6; mI++) {
                        var m = Assets.LoadAsset("FFHM").Cast<Material>();
                        if (udn.genericRenderers[mI].material.name.Contains("Material37"))
                            m = Assets.LoadAsset("FFMH2").Cast<Material>();
                        udn.genericRenderers[mI].material = m ?? throw new Exception($"Cry :cry: {mI}");
                    }
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (!protos.ContainsKey(objectId) && objectId.Equals("SpiderMan1")) {
                    var udn = GetSpiderManIS(__instance.PrototypeRoot);
                    udn.name = "SpiderManIS";
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    for (int mI = 0; mI < 1; mI++) {
                        var m = Assets.LoadAsset("ISM").Cast<Material>();
                        udn.genericRenderers[mI].material = m ?? throw new Exception($"Cry :cry: {mI}");
                    }
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (!protos.ContainsKey(objectId) && objectId.Equals("SpiderMan")) {
                    var udn = GetSpiderManFFH(__instance.PrototypeRoot);
                    udn.name = "SpiderManCW";
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    for (int mI = 0; mI < 6; mI++) {
                        var m = Assets.LoadAsset("CWM").Cast<Material>();
                        if (udn.genericRenderers[mI].material.name.Contains("Material37"))
                            m = Assets.LoadAsset("FFMH2").Cast<Material>();
                        udn.genericRenderers[mI].material = m ?? throw new Exception($"Cry :cry: {mI}");
                    }
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

        private static AssetBundle __asset;

        public static AssetBundle Assets {
            get => __asset;
            set => __asset = value;
        }

        public static UnityDisplayNode GetSpiderManBGS(Transform transform) {
            var udn = Object.Instantiate(Assets.LoadAsset("SpiderManBGS").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.gameObject.AddComponent<SetScale>();
            return udn;
        }

        public static UnityDisplayNode GetSpiderManFFH(Transform transform) {
            var udn = Object.Instantiate(Assets.LoadAsset("SpiderManFFH").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.gameObject.AddComponent<SetScale2>();
            return udn;
        }

        public static UnityDisplayNode GetSpiderManIS(Transform transform) {
            var udn = Object.Instantiate(Assets.LoadAsset("SpiderManIS").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.gameObject.AddComponent<SetScale3>();
            return udn;
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
                if (reference != null && reference.guidRef.Equals("SMPortrait"))
                    try {
                        var text = LoadTextureFromBytes(Properties.Resources.SMPortrait).Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = LoadSprite(text);
                    } catch { }
                if (reference != null && reference.guidRef.Equals("SMPortrait1"))
                    try {
                        var text = LoadTextureFromBytes(Properties.Resources.SMPortrait1).Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = LoadSprite(text);
                    } catch { }
                if (reference != null && reference.guidRef.Equals("SMPortrait2"))
                    try {
                        var text = LoadTextureFromBytes(Properties.Resources.SMPortrait2).Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = LoadSprite(text);
                    } catch { }
                if (reference != null && reference.guidRef.Equals("SMPortrait3"))
                    try {
                        var text = LoadTextureFromBytes(Properties.Resources.SMPortrait3).Cast<Texture2D>();
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
        public sealed class WI {
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
                    if (__instance.weaponModel.name.EndsWith("web")) {
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().StopPlayback();
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().speed = 5f;
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("metarig|webshoot");
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("FFH|Web");
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("IS|web");
                        var wait = 2300f;
                        await Task.Run(async () => {
                            while (wait > 0) {
                                wait -= TimeManager.timeScaleWithoutNetwork + 1;
                                await Task.Delay(1);
                            }
                            __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("metarig|idle");
                            __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("FFH|Idle");
                            __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("IS|Idle");
                            return;
                        });
                    }
                } catch (Exception) { }
            }
        }
    }
}
