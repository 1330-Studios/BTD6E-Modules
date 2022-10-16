
namespace GodlyTowers.Towers {
    public sealed class MiniPekka {
        public static string name = "MiniPekka";

        public static UpgradeModel[] GetUpgrades() => new UpgradeModel[] {
            new("Roman Specialty", 800, 0, new("MPTSM"), 0, 0, 0, "", "Roman Specialty"),
            new("European Recipies", 1330, 1, new("MPTSM"), 0, 0, 0, "", "European Recipies"),
            new("Vogue's Flip Flop", 4320, 2, new("MPTSM"), 0, 0, 0, "", "Vogue's Flip Flop"),
            new("Premade Pancake Mix", 12700, 3, new("MPTSM"), 0, 0, 0, "", "Premade Pancake Mix"),
            new("Marathon Mike", 42080, 4, new("MPTSM"), 0, 0, 0, "", "Marathon Mike")
        };

        public static (TowerModel, ShopTowerDetailsModel, TowerModel[], UpgradeModel[]) GetTower(GameModel gameModel) {
            var minipekkaDetails = gameModel.towerSet[0].Clone().Cast<ShopTowerDetailsModel>();
            minipekkaDetails.towerId = name;
            minipekkaDetails.towerIndex = GlobalTowerIndex.Index;


            if (!LocalizationManager.Instance.textTable.ContainsKey("MiniPekka"))
                LocalizationManager.Instance.textTable.Add("MiniPekka", "Mini P.E.K.K.A");

            if (!LocalizationManager.Instance.textTable.ContainsKey("Roman Specialty Description"))
                LocalizationManager.Instance.textTable.Add("Roman Specialty Description", "While some speculate that Tzi the Iceman ate einkorn wheat in the shape of an early flatbread, most culinary historians agree that the first pancake-like dish, known as Alita Dolcia (\"another sweet\" in Latin), was created by Romans in the first century CE using milk, flour, egg, and spices.");
            if (!LocalizationManager.Instance.textTable.ContainsKey("European Recipies Description"))
                LocalizationManager.Instance.textTable.Add("European Recipies Description", "Many European countries produced their own types of pancakes from scratch by the 15th century, utilizing a variety of ingredients such as wheat, buckwheat, wine or ale, and herbs and spices such as cloves, cinnamon, and nutmeg. Gains camo detection.");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Vogue's Flip Flop Description"))
                LocalizationManager.Instance.textTable.Add("Vogue's Flip Flop Description", "\"Pancakes are frankly difficult and not worth eating at all unless they are of paper thinness and succulent tenderness,\" Vogue wrote in 1935. They appear to have changed their tune in recent years, since they now provide a recipe for gluten - free chocolate banana pancakes on their website. ");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Premade Pancake Mix Description"))
                LocalizationManager.Instance.textTable.Add("Premade Pancake Mix Description", "The R. T. Davis Milling Company, which employed storyteller, cook, and missionary worker Nancy Green as a spokesman for its Aunt Jemima mix in 1890, created the world's first pancake mix. Gains true damage to all bloon types.");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Marathon Mike Description"))
                LocalizationManager.Instance.textTable.Add("Marathon Mike Description", "The flipping-a-pancake-while-running-a-marathon award goes to Dominic \"Mike\" Cuzzacrea, who completed a 1999 marathon at Niagara Falls in a time of 3 hours, 2 minutes, and 27 seconds—all while battling wind from the falls. Anyway, max Mini Pekka time!");

            return (GetT0(gameModel), minipekkaDetails, new[] { GetT0(gameModel), GetT1(gameModel), GetT2(gameModel), GetT3(gameModel), GetT4(gameModel), GetT5(gameModel) }, GetUpgrades());
        }

        public static unsafe TowerModel GetT0(GameModel gameModel) {
            var minipekka = gameModel.towers.First(a => a.name.Equals("SniperMonkey")).Clone().Cast<TowerModel>();

            minipekka.name = name;
            minipekka.baseId = name;
            minipekka.display = "MiniPekka";
            minipekka.portrait = new("MPTSM");
            minipekka.icon = new("MPPortrait");
            minipekka.towerSet = "Magic";
            minipekka.emoteSpriteLarge = new("None");
            minipekka.radius = 8;
            minipekka.cost = 800;
            minipekka.range = 35;
            minipekka.mods = Array.Empty<ApplyModModel>();
            minipekka.tier = 0;
            minipekka.tiers = new[] { 0, 0, 0 };
            minipekka.upgrades = new UpgradePathModel[] { new UpgradePathModel("Roman Specialty", $"{name}-100") };

            for (var i = 0; i < minipekka.behaviors.Count; i++) {
                var b = minipekka.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();
                    att.weapons[0].rate *= 2;
                    att.weapons[0].name = "swing";
                    att.weapons[0].behaviors = Array.Empty<WeaponBehaviorModel>();
                    att.weapons[0].projectile.pierce = 1;

                    for (var j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        var pb = att.weapons[0].projectile.behaviors[j];
                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var dm = pb.Cast<DamageModel>();
                            dm.damage = 34.0f / 2;
                            att.weapons[0].projectile.behaviors[j] = dm;
                        }
                    }

                    att.range = 35;
                    minipekka.behaviors[i] = att;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "MiniPekka";
                }
            }


            return minipekka;
        }

        public static unsafe TowerModel GetT1(GameModel gameModel) {
            var minipekka = GetT0(gameModel);

            minipekka.name = $"{name}-100";
            minipekka.baseId = name;
            minipekka.display = "MiniPekka1";
            minipekka.portrait = new("MPTSM");
            minipekka.icon = new("MPPortrait");
            minipekka.towerSet = "Magic";
            minipekka.emoteSpriteLarge = new("None");
            minipekka.radius = 8;
            minipekka.cost = 800;
            minipekka.range = 35;
            minipekka.mods = Array.Empty<ApplyModModel>();
            minipekka.tier = 1;
            minipekka.tiers = new[] { 1, 0, 0 };
            minipekka.upgrades = new UpgradePathModel[] { new UpgradePathModel("European Recipies", $"{name}-200") };

            for (var i = 0; i < minipekka.behaviors.Count; i++) {
                var b = minipekka.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();
                    att.weapons[0].name = "swing";
                    att.weapons[0].behaviors = Array.Empty<WeaponBehaviorModel>();

                    for (var j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        var pb = att.weapons[0].projectile.behaviors[j];
                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var dm = pb.Cast<DamageModel>();
                            dm.damage = 41.1f;
                            att.weapons[0].projectile.behaviors[j] = dm;
                        }
                    }

                    att.range = 35;
                    minipekka.behaviors[i] = att;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "MiniPekka1";
                }
            }


            return minipekka;
        }

        public static unsafe TowerModel GetT2(GameModel gameModel) {
            var minipekka = GetT1(gameModel);

            minipekka.name = $"{name}-200";
            minipekka.baseId = name;
            minipekka.display = "MiniPekka2";
            minipekka.portrait = new("MPTSM");
            minipekka.icon = new("MPPortrait");
            minipekka.towerSet = "Magic";
            minipekka.emoteSpriteLarge = new("None");
            minipekka.radius = 8;
            minipekka.cost = 800;
            minipekka.range = 35;
            minipekka.mods = Array.Empty<ApplyModModel>();
            minipekka.tier = 2;
            minipekka.tiers = new[] { 2, 0, 0 };
            minipekka.upgrades = new UpgradePathModel[] { new UpgradePathModel("Vogue's Flip Flop", $"{name}-300") };

            for (var i = 0; i < minipekka.behaviors.Count; i++) {
                var b = minipekka.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();
                    att.weapons[0].name = "swing";
                    att.weapons[0].behaviors = Array.Empty<WeaponBehaviorModel>();

                    for (var j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        var pb = att.weapons[0].projectile.behaviors[j];
                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var dm = pb.Cast<DamageModel>();
                            dm.damage = 54.4f;
                            att.weapons[0].projectile.behaviors[j] = dm;
                        }
                    }

                    att.range = 35;
                    minipekka.behaviors[i] = att;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "MiniPekka2";
                }
            }

            minipekka.behaviors = minipekka.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true));

            return minipekka;
        }

        public static unsafe TowerModel GetT3(GameModel gameModel) {
            var minipekka = GetT2(gameModel);

            minipekka.name = $"{name}-300";
            minipekka.baseId = name;
            minipekka.display = "MiniPekka3";
            minipekka.portrait = new("MPTSM");
            minipekka.icon = new("MPPortrait");
            minipekka.towerSet = "Magic";
            minipekka.emoteSpriteLarge = new("None");
            minipekka.radius = 8;
            minipekka.cost = 800;
            minipekka.range = 35;
            minipekka.mods = Array.Empty<ApplyModModel>();
            minipekka.tier = 3;
            minipekka.tiers = new[] { 3, 0, 0 };
            minipekka.upgrades = new UpgradePathModel[] { new UpgradePathModel("Premade Pancake Mix", $"{name}-400") };

            for (var i = 0; i < minipekka.behaviors.Count; i++) {
                var b = minipekka.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();
                    att.weapons[0].name = "swing";
                    att.weapons[0].behaviors = Array.Empty<WeaponBehaviorModel>();
                    att.weapons[0].projectile.filters = Array.Empty<FilterModel>();
                    att.weapons[0].projectile.pierce += 5;

                    for (var j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        var pb = att.weapons[0].projectile.behaviors[j];
                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var dm = pb.Cast<DamageModel>();
                            dm.damage = 65.6f;
                            att.weapons[0].projectile.behaviors[j] = dm;
                        }

                        if (pb.GetIl2CppType() == Il2CppType.Of<ProjectileFilterModel>()) {
                            var pfm = pb.Cast<ProjectileFilterModel>();
                            pfm.filters = Array.Empty<FilterModel>();
                            att.weapons[0].projectile.behaviors[j] = pfm;
                        }
                    }

                    att.range = 35;
                    minipekka.behaviors[i] = att;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "MiniPekka3";
                }
            }


            return minipekka;
        }

        public static unsafe TowerModel GetT4(GameModel gameModel) {
            var minipekka = GetT3(gameModel);

            minipekka.name = $"{name}-400";
            minipekka.baseId = name;
            minipekka.display = "MiniPekka4";
            minipekka.portrait = new("MPTSM");
            minipekka.icon = new("MPPortrait");
            minipekka.towerSet = "Magic";
            minipekka.emoteSpriteLarge = new("None");
            minipekka.radius = 8;
            minipekka.cost = 800;
            minipekka.range = 35;
            minipekka.mods = Array.Empty<ApplyModModel>();
            minipekka.tier = 4;
            minipekka.tiers = new[] { 4, 0, 0 };
            minipekka.upgrades = new UpgradePathModel[] { new UpgradePathModel("Marathon Mike", $"{name}-500") };

            for (var i = 0; i < minipekka.behaviors.Count; i++) {
                var b = minipekka.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();
                    att.weapons[0].rate /= 1.5f;
                    att.weapons[0].name = "swing";
                    att.weapons[0].behaviors = Array.Empty<WeaponBehaviorModel>();
                    att.weapons[0].projectile.pierce += 5;

                    for (var j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        var pb = att.weapons[0].projectile.behaviors[j];
                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var dm = pb.Cast<DamageModel>();
                            dm.damage = 79.2f;
                            dm.immuneBloonProperties = BloonProperties.None;
                            att.weapons[0].projectile.behaviors[j] = dm;
                        }
                    }

                    att.range = 35;
                    minipekka.behaviors[i] = att;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "MiniPekka4";
                }
            }


            return minipekka;
        }

        public static unsafe TowerModel GetT5(GameModel gameModel) {
            var minipekka = GetT4(gameModel);

            minipekka.name = $"{name}-500";
            minipekka.baseId = name;
            minipekka.display = "MiniPekka5";
            minipekka.portrait = new("MPTSM");
            minipekka.icon = new("MPPortrait");
            minipekka.towerSet = "Magic";
            minipekka.emoteSpriteLarge = new("None");
            minipekka.radius = 8;
            minipekka.cost = 800;
            minipekka.range = 40;
            minipekka.mods = Array.Empty<ApplyModModel>();
            minipekka.tier = 5;
            minipekka.tiers = new[] { 5, 0, 0 };
            minipekka.upgrades = Array.Empty<UpgradePathModel>();

            for (var i = 0; i < minipekka.behaviors.Count; i++) {
                var b = minipekka.behaviors[i];
                if (b.GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                    var att = b.Cast<AttackModel>();
                    att.weapons[0].rate /= 1.5f;
                    att.weapons[0].name = "swing";
                    att.weapons[0].behaviors = Array.Empty<WeaponBehaviorModel>();
                    att.weapons[0].projectile.pierce += 50;
                    att.weapons[0].projectile.radius = 30;

                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Add(
                            new CreateEffectOnContactModel("CreateEffectOnContactModel_",
                                new("6d84b13b7622d2744b8e8369565bc058", "6d84b13b7622d2744b8e8369565bc058", 1, 2, false, false, false, false, false, false, false))
                        );

                    for (var j = 0; j < att.weapons[0].projectile.behaviors.Length; j++) {
                        var pb = att.weapons[0].projectile.behaviors[j];
                        if (pb.GetIl2CppType() == Il2CppType.Of<DamageModel>()) {
                            var dm = pb.Cast<DamageModel>();
                            dm.damage = 1804;
                            att.weapons[0].projectile.behaviors[j] = dm;
                        }

                        att.weapons[0].projectile.behaviors[j] = pb;
                    }

                    att.range = 40;
                    minipekka.behaviors[i] = att;
                }

                if (b.GetIl2CppType() == Il2CppType.Of<DisplayModel>()) {
                    var display = b.Cast<DisplayModel>();
                    display.display = "MiniPekka5";
                }
            }

            GodTier.GodTier.CustomUpgrades.Add("Marathon Mike", GodTier.GodTier.UpgradeBG.MiniPekka);

            return minipekka;
        }

        [HarmonyPatch(typeof(Factory), nameof(Factory.FindAndSetupPrototypeAsync))]
        public class PrototypeUDN_Patch {
            public static Dictionary<string, UnityDisplayNode> protos = new();

            [HarmonyPrefix]
            public static bool Prefix(Factory __instance, string objectId, Il2CppSystem.Action<UnityDisplayNode> onComplete) {
                if (!protos.ContainsKey(objectId) && objectId.Equals("MiniPekka")) {
                    var udn = GetMiniPekka(__instance.PrototypeRoot);
                    udn.name = "MiniPekka";
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (!protos.ContainsKey(objectId) && objectId.Equals("MiniPekka1")) {
                    var udn = GetMiniPekka1(__instance.PrototypeRoot);
                    udn.name = "MiniPekka1";
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (!protos.ContainsKey(objectId) && objectId.Equals("MiniPekka2")) {
                    var udn = GetMiniPekka2(__instance.PrototypeRoot);
                    udn.name = "MiniPekka2";
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (!protos.ContainsKey(objectId) && objectId.Equals("MiniPekka3")) {
                    var udn = GetMiniPekka3(__instance.PrototypeRoot);
                    udn.name = "MiniPekka3";
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (!protos.ContainsKey(objectId) && objectId.Equals("MiniPekka4")) {
                    var udn = GetMiniPekka4(__instance.PrototypeRoot);
                    udn.name = "MiniPekka4";
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (!protos.ContainsKey(objectId) && objectId.Equals("MiniPekka5")) {
                    var udn = GetMiniPekka5(__instance.PrototypeRoot);
                    udn.name = "MiniPekka5";
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

        public static UnityDisplayNode GetMiniPekka(Transform transform) {
            var udn = Object.Instantiate(Assets.LoadAsset("minipekka_base").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.gameObject.AddComponent<SetScale6>();
            return udn;
        }
        public static UnityDisplayNode GetMiniPekka1(Transform transform) {
            var udn = Object.Instantiate(Assets.LoadAsset("minipekka_1").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.gameObject.AddComponent<SetScale6>();
            return udn;
        }
        public static UnityDisplayNode GetMiniPekka2(Transform transform) {
            var udn = Object.Instantiate(Assets.LoadAsset("minipekka_2").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.gameObject.AddComponent<SetScale6>();
            return udn;
        }
        public static UnityDisplayNode GetMiniPekka3(Transform transform) {
            var udn = Object.Instantiate(Assets.LoadAsset("minipekka_3").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.gameObject.AddComponent<SetScale6>();
            return udn;
        }
        public static UnityDisplayNode GetMiniPekka4(Transform transform) {
            var udn = Object.Instantiate(Assets.LoadAsset("minipekka_4").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.gameObject.AddComponent<SetScale6>();
            return udn;
        }
        public static UnityDisplayNode GetMiniPekka5(Transform transform) {
            var udn = Object.Instantiate(Assets.LoadAsset("minipekka_5").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.gameObject.AddComponent<SetScale6>();
            return udn;
        }

        [HarmonyPatch(typeof(TowerToSimulation), nameof(TowerToSimulation.Upgrade))]
        public class TowerToSimulation_Upgrade {
            [HarmonyPostfix]
            public static void Postfix(TowerToSimulation __instance) {
                if (__instance?.tower?.towerModel?.baseId == name)
                    PlayPancake();
            }
        }

        [HarmonyPatch(typeof(Tower), nameof(Tower.OnPlace))]
        public class Tower_OnPlace {
            [HarmonyPostfix]
            public static void Postfix(Tower __instance) {
                if (__instance?.towerModel?.baseId == name)
                    PlayPancake();
            }
        }

        [HarmonyPatch(typeof(UnityToSimulation), nameof(UnityToSimulation.SellTower))]
        public class UnityToSimulation_SellTower {
            [HarmonyPrefix]
            public static void Prefix(UnityToSimulation __instance, int id) {
                if (__instance.GetTower(id) != null && __instance.GetTower(id)?.tower?.towerModel?.baseId == name)
                    PlaySell();
            }
        }

        public static void PlayPancake() => AudioFactory_CreateStartingSources.inst.PlaySoundFromUnity(null, "mini_pekka_deploy_end_06", "FX", 1, 1, 0, false);
        public static void PlayAttack() => AudioFactory_CreateStartingSources.inst.PlaySoundFromUnity(null, "mini_pekka_atk_12", "FX", 5, 1, 0, false);
        public static void PlaySell() => AudioFactory_CreateStartingSources.inst.PlaySoundFromUnity(null, "mini_pekka_hit_03", "FX", 5, 1, 0, false);

        [HarmonyPatch(typeof(AudioFactory), nameof(AudioFactory.Start))]
        public class AudioFactory_CreateStartingSources {
            public static AudioFactory inst;

            [HarmonyPostfix]
            public static void Prefix(AudioFactory __instance) {
                inst = __instance;
                if (!__instance.audioClips.ContainsKey("mini_pekka_deploy_end_06")) {
                    var ac = Assets.LoadAsset("mini_pekka_deploy_end_06").Cast<AudioClip>();
                    __instance.RegisterAudioClip("mini_pekka_deploy_end_06", ac);
                }
                if (!__instance.audioClips.ContainsKey("mini_pekka_atk_12")) {
                    var ac = Assets.LoadAsset("mini_pekka_atk_12").Cast<AudioClip>();
                    __instance.RegisterAudioClip("mini_pekka_atk_12", ac);
                }
                if (!__instance.audioClips.ContainsKey("mini_pekka_hit_03")) {
                    var ac = Assets.LoadAsset("mini_pekka_hit_03").Cast<AudioClip>();
                    __instance.RegisterAudioClip("mini_pekka_hit_03", ac);
                }
            }
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
                if (reference != null && reference.guidRef.Equals("MPPortrait"))
                    try {
                        var text = LoadTextureFromBytes(Properties.Resources.MPPortrait).Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = LoadSprite(text);
                    } catch { }
                if (reference != null && reference.guidRef.Equals("MPTSM"))
                    try {
                        var text = LoadTextureFromBytes(Properties.Resources.MPTSM).Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = LoadSprite(text);
                    } catch { }
                if (reference != null && reference.guidRef.Equals("MiniPekkaUBG"))
                    try {
                        var text = LoadTextureFromBytes(Properties.Resources.MPUBG);
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
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
                    if (__instance.weaponModel.name.EndsWith("swing")) {
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().StopPlayback();
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("swing");
                        //PlayAttack();
                        var wait = 1200f;
                        await Task.Run(async () => {
                            while (wait > 0) {
                                wait -= TimeManager.timeScaleWithoutNetwork + 1;
                                await Task.Delay(1);
                            }
                            __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("start");
                            return;
                        });
                    }
                } catch (Exception) { }
            }
        }
    }
}
