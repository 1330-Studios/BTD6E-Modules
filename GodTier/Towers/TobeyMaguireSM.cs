namespace GodlyTowers.Towers {
    public sealed class TobeyMaguireSM {
        public static string name = "TMSM";

        public static UpgradeModel[] GetUpgrades() => new UpgradeModel[] {
            new("Wise Words of Encouragement", 500, 0, new("TMUpgrade1"), 0, 0, 0, "", "Wise Words of Encouragement"),
            new("True Perseverance", 3000, 0, new("TMUpgrade2"), 0, 1, 0, "", "True Perseverance"),
            new("Sacrifices", 5700, 0, new("TMUpgrade3"), 0, 2, 0, "", "Sacrifices"),
            new("Just The Basics", 20021, 0, new("TMUpgrade4"), 0, 3, 0, "", "Just The Basics"),
            new("Symbiote Suit", 31500, 0, new("TMSMPortrait2"), 0, 4, 0, "", "Symbiote Suit"),
        };

        public static (TowerModel, ShopTowerDetailsModel, TowerModel[], UpgradeModel[]) GetTower(GameModel gameModel) {
            var spidermanDetails = gameModel.towerSet[0].Clone().Cast<ShopTowerDetailsModel>();
            spidermanDetails.towerId = name;
            spidermanDetails.towerIndex = GlobalTowerIndex.Index;


            if (!LocalizationManager.Instance.textTable.ContainsKey("TMSM"))
                LocalizationManager.Instance.textTable.Add("TMSM", "Spider-Man (Earth 96283)");

            if (!LocalizationManager.Instance.textTable.ContainsKey("Wise Words of Encouragement Description"))
                LocalizationManager.Instance.textTable.Add("Wise Words of Encouragement Description", "“With Great Power Comes Great Responsibility.”");
            if (!LocalizationManager.Instance.textTable.ContainsKey("True Perseverance Description"))
                LocalizationManager.Instance.textTable.Add("True Perseverance Description", "“In spite of everything you’ve done for them, eventually they will hate you. Why bother?”");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Sacrifices Description"))
                LocalizationManager.Instance.textTable.Add("Sacrifices Description", "Sometimes, to do what's right, we have to be steady and give up the thing we want the most. Even our dreams.");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Just The Basics Description"))
                LocalizationManager.Instance.textTable.Add("Just The Basics Description", "Uncle Ben meant the world to us, but he wouldn't want us living one second with revenge in our hearts. It's like a poison.");
            if (!LocalizationManager.Instance.textTable.ContainsKey("Symbiote Suit Description"))
                LocalizationManager.Instance.textTable.Add("Symbiote Suit Description", "I know how It feels like, It feels good. The power, everything... but you'll lose yourself, let it go...");

            return (GetT0(gameModel), spidermanDetails, new[] { GetT0(gameModel), GetT1(gameModel), GetT2(gameModel), GetT3(gameModel), GetT4(gameModel), GetT5(gameModel) }, GetUpgrades());
        }

        public static unsafe TowerModel GetT0(GameModel gameModel) {
            var spiderman = gameModel.towers.First(a => a.name.Equals("DartMonkey")).Clone().Cast<TowerModel>();

            spiderman.name = name;
            spiderman.baseId = name;
            spiderman.display = "TM_Standard";
            spiderman.portrait = new("TMSMPortrait");
            spiderman.icon = new("TMSMPortrait");
            spiderman.towerSet = "Military";
            spiderman.emoteSpriteLarge = new("Marvel");
            spiderman.tier = 0;
            spiderman.tiers = new[] { 0, 0, 0 };
            spiderman.radius = 8;
            spiderman.range = 30;
            spiderman.cost = 725;
            spiderman.upgrades = new UpgradePathModel[] { new UpgradePathModel("Wise Words of Encouragement", name + "-100") };

            for (var i = 0; i < spiderman.behaviors.Count; i++) {
                var b = spiderman.behaviors[i];
                if (b.Is<AttackModel>(out var att)) {
                    foreach (var ab in att.behaviors) {
                        if (ab.Is<RotateToTargetModel>(out var rttm)) {
                            rttm.rotateOnlyOnThrow = false;
                        }
                    }

                    att.weapons[0].name = "spidermanpunch";
                    att.weapons[0].projectile.pierce = 1;
                    att.weapons[0].rate = 1.75f;
                    att.weapons[0].behaviors = Array.Empty<WeaponBehaviorModel>();
                    att.weapons[0].projectile.display = "";
                    att.weapons[0].projectile.radius = 2f;
                    foreach (var pb in att.weapons[0].projectile.behaviors) {
                        if (pb.Is<DamageModel>(out var dm))
                            dm.damage = 2;
                    }
                    att.range = 30;
                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Add(new CreateEffectOnContactModel("EffectModel_", new("9415386adf5c4455db6085efc30ceed6", "9415386adf5c4455db6085efc30ceed6",
                        1, 2, false, false, false, false, false, false, false)));
                    spiderman.behaviors[i] = att;
                }
                if (b.Is<DisplayModel>(out var display)) {
                    display.display = "TM_Standard";
                }
            }

            spiderman.behaviors = spiderman.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true));

            return spiderman;
        }

        public static unsafe TowerModel GetT1(GameModel gameModel) {
            var spiderman = GetT0(gameModel).Clone().Cast<TowerModel>();

            spiderman.name = name + "-100";
            spiderman.baseId = name;
            spiderman.display = "TM_Standard";
            spiderman.portrait = new("TMUpgrade1");
            spiderman.icon = new("TMUpgrade1");
            spiderman.towerSet = "Military";
            spiderman.emoteSpriteLarge = new("Movie");
            spiderman.tier = 1;
            spiderman.tiers = new[] { 1, 0, 0 };
            spiderman.range = 30;
            spiderman.appliedUpgrades = new[] { "Wise Words of Encouragement" };
            spiderman.upgrades = new UpgradePathModel[] { new UpgradePathModel("True Perseverance", name + "-200") };

            for (var i = 0; i < spiderman.behaviors.Count; i++) {
                var b = spiderman.behaviors[i];
                if (b.Is<AttackModel>(out var att)) {
                    att.weapons[0].rate = 1.675f;
                    att.weapons[0].projectile.pierce = 5;
                    att.weapons[0].projectile.radius = 1f;
                    foreach (var pb in att.weapons[0].projectile.behaviors) {
                        if (pb.Is<DamageModel>(out var dm))
                            dm.damage = 4;
                    }
                    
                    spiderman.behaviors[i] = att;
                }
                if (b.Is<DisplayModel>(out var display))
                    display.display = "TM_Standard";
            }

            return spiderman;
        }

        public static unsafe TowerModel GetT2(GameModel gameModel) {
            var spiderman = GetT1(gameModel).Clone().Cast<TowerModel>();

            spiderman.name = name + "-200";
            spiderman.baseId = name;
            spiderman.display = "TM_Standard";
            spiderman.portrait = new("TMUpgrade2");
            spiderman.icon = new("TMUpgrade2");
            spiderman.towerSet = "Military";
            spiderman.emoteSpriteLarge = new("Movie");
            spiderman.tier = 2;
            spiderman.tiers = new[] { 2, 0, 0 };
            spiderman.range = 35;
            spiderman.appliedUpgrades = new[] { "Wise Words of Encouragement", "True Perseverance" };
            spiderman.upgrades = new UpgradePathModel[] { new UpgradePathModel("Sacrifices", name + "-300") };

            for (var i = 0; i < spiderman.behaviors.Count; i++) {
                var b = spiderman.behaviors[i];
                if (b.Is<AttackModel>(out var att)) {
                    att.weapons[0].rate = 1.5f;
                    att.weapons[0].projectile.pierce = 10;
                    att.weapons[0].projectile.radius = 1.5f;
                    foreach (var pb in att.weapons[0].projectile.behaviors) {
                        if (pb.Is<DamageModel>(out var dm)) {
                            dm.damage = 10;
                            dm.immuneBloonProperties = BloonProperties.None;
                        }
                    }
                    att.range = 35;
                    spiderman.behaviors[i] = att;
                }
                if (b.Is<DisplayModel>(out var display))
                    display.display = "TM_Standard";
            }

            return spiderman;
        }

        public static unsafe TowerModel GetT3(GameModel gameModel) {
            var spiderman = GetT2(gameModel).Clone().Cast<TowerModel>();

            spiderman.name = name + "-300";
            spiderman.baseId = name;
            spiderman.display = "TM_Standard";
            spiderman.portrait = new("TMUpgrade3");
            spiderman.icon = new("TMUpgrade3");
            spiderman.towerSet = "Military";
            spiderman.emoteSpriteLarge = new("Movie");
            spiderman.tier = 3;
            spiderman.tiers = new[] { 3, 0, 0 };
            spiderman.range = 35;
            spiderman.appliedUpgrades = new[] { "Wise Words of Encouragement", "True Perseverance", "Sacrifices" };
            spiderman.upgrades = new UpgradePathModel[] { new UpgradePathModel("Just The Basics", name + "-300") };

            for (var i = 0; i < spiderman.behaviors.Count; i++) {
                var b = spiderman.behaviors[i];
                if (b.Is<AttackModel>(out var att)) {
                    att.weapons[0].rate = 1.33f;
                    att.weapons[0].projectile.radius = 2f;
                    foreach (var pb in att.weapons[0].projectile.behaviors) {
                        if (pb.Is<DamageModel>(out var dm))
                            dm.damage = 25;
                    }
                    var kb = new KnockbackModel("KnockbackModel_", 0.7f, 1f, 1.3f, 0.5f, "KnockbackKnockback");

                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Add(kb);

                    spiderman.behaviors[i] = att;
                }
                if (b.Is<DisplayModel>(out var display))
                    display.display = "TM_Standard";
            }

            return spiderman;
        }

        public static unsafe TowerModel GetT4(GameModel gameModel) {
            var spiderman = GetT3(gameModel).Clone().Cast<TowerModel>();

            spiderman.name = name + "-400";
            spiderman.baseId = name;
            spiderman.display = "TM_Standard";
            spiderman.portrait = new("TMUpgrade4");
            spiderman.icon = new("TMUpgrade4");
            spiderman.towerSet = "Military";
            spiderman.emoteSpriteLarge = new("Movie");
            spiderman.tier = 4;
            spiderman.tiers = new[] { 4, 0, 0 };
            spiderman.range = 40;
            spiderman.appliedUpgrades = new[] { "Wise Words of Encouragement", "True Perseverance", "Sacrifices", "Just The Basics" };
            spiderman.upgrades = new UpgradePathModel[] { new UpgradePathModel("Symbiote Suit", name + "-500") };

            for (var i = 0; i < spiderman.behaviors.Count; i++) {
                var b = spiderman.behaviors[i];
                if (b.Is<AttackModel>(out var att)) {
                    att.weapons[0].rate = 1.25f;
                    att.weapons[0].projectile.pierce = 50;
                    att.weapons[0].projectile.radius = 2.5f;
                    foreach (var pb in att.weapons[0].projectile.behaviors) {
                        if (pb.Is<DamageModel>(out var dm))
                            dm.damage = 50;
                    }
                    att.range = 40;
                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Add(new DamageModifierForTagModel("DMFTM_", "Ceramic", 1.5f, 50, false, true));
                    spiderman.behaviors[i] = att;
                }
                if (b.Is<DisplayModel>(out var display))
                    display.display = "TM_Standard";
            }

            return spiderman;
        }

        public static unsafe TowerModel GetT5(GameModel gameModel) {
            var spiderman = GetT4(gameModel).Clone().Cast<TowerModel>();

            GodTier.GodTier.CustomUpgrades.Add("Symbiote Suit", GodTier.GodTier.UpgradeBG.SymbioteSuit);

            spiderman.name = name + "-500";
            spiderman.baseId = name;
            spiderman.display = "TM_Symbiote";
            spiderman.portrait = new("TMSMPortrait2");
            spiderman.icon = new("TMSMPortrait2");
            spiderman.towerSet = "Military";
            spiderman.emoteSpriteLarge = new("Movie");
            spiderman.tier = 5;
            spiderman.tiers = new[] { 5, 0, 0 };
            spiderman.range = 50;
            spiderman.appliedUpgrades = new[] { "Wise Words of Encouragement", "True Perseverance", "Sacrifices", "Just The Basics", "Symbiote Suit" };
            spiderman.upgrades = Array.Empty<UpgradePathModel>();

            for (var i = 0; i < spiderman.behaviors.Count; i++) {
                var b = spiderman.behaviors[i];
                if (b.Is<AttackModel>(out var att)) {
                    att.weapons[0].rate = 0.8f;
                    att.weapons[0].projectile.pierce = 999;
                    att.weapons[0].projectile.ignorePierceExhaustion = true;
                    att.weapons[0].projectile.radius = 5;
                    att.weapons[0].projectile.display = "VenomProj";
                    foreach (var pb in att.weapons[0].projectile.behaviors) {
                        if (pb.Is<DamageModel>(out var dm))
                            dm.damage = 500;
                        if (pb.Is<TravelStraitModel>(out var tsm)) {
                            tsm.lifespan *= 5;
                            tsm.speed /= 2;
                        }
                    }
                    att.weapons[0].projectile.behaviors = att.weapons[0].projectile.behaviors.Add(new CreateEffectOnContactModel("EffectModel_", new("6d84b13b7622d2744b8e8369565bc058", "6d84b13b7622d2744b8e8369565bc058",
                        1, 2, false, false, false, false, false, false, false)), new DamageModifierForTagModel("DMFTM_", "Moabs", 5f, 100, false, true),
                        new RetargetOnContactModel("RetargetOnContactModel_", 50, 10, "First", 0.1f, true));
                    att.range = 50;
                    spiderman.behaviors[i] = att;
                }
                if (b.Is<DisplayModel>(out var display))
                    display.display = "TM_Symbiote";
            }

            return spiderman;
        }

        [HarmonyPatch(typeof(Factory), nameof(Factory.FindAndSetupPrototypeAsync))]
        public sealed class PrototypeUDN_Patch {
            public static Dictionary<string, UnityDisplayNode> protos = new();

            [HarmonyPrefix]
            public static bool Prefix(Factory __instance, string objectId, Il2CppSystem.Action<UnityDisplayNode> onComplete) {

                if (!protos.ContainsKey(objectId) && objectId.Equals("TM_Standard")) {
                    var udn = GetStandard(__instance.PrototypeRoot);
                    udn.name = "SpiderManStandard";
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }
                if (!protos.ContainsKey(objectId) && objectId.Equals("TM_Symbiote")) {
                    var udn = GetSymbiote(__instance.PrototypeRoot);
                    udn.name = "SpiderManSymbiote";
                    udn.RecalculateGenericRenderers();
                    udn.isSprite = false;
                    onComplete.Invoke(udn);
                    protos.Add(objectId, udn);
                    return false;
                }

                if (objectId.Equals("VenomProj")) {
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
                                    var text = Assets.LoadAsset("VenomProj").Cast<Texture2D>();
                                    smr.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new(0.5f, 0.5f), 5.4f);
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

        public static UnityDisplayNode GetStandard(Transform transform) {
            var udn = Object.Instantiate(Assets.LoadAsset("Tobey_Standard").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.transform.Rotate(new Vector3(-15, 0, 0));
            udn.gameObject.AddComponent<SetScale8>();
            return udn;
        }

        public static UnityDisplayNode GetSymbiote(Transform transform) {
            var udn = Object.Instantiate(Assets.LoadAsset("Tobey_Symbiote").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
            udn.Active = false;
            udn.transform.position = new(-3000, 0);
            udn.transform.Rotate(new Vector3(-15, 0, 0));
            udn.gameObject.AddComponent<SetScale8>();
            return udn;
        }


        public static void PlayAttack() => AudioFactory_CreateStartingSources.inst.PlaySoundFromUnity(null, "spidermanpunch", "FX", 1, 0.4f, 0, false);

        [HarmonyPatch(typeof(AudioFactory), nameof(AudioFactory.Start))]
        public sealed class AudioFactory_CreateStartingSources {
            public static AudioFactory inst;

            [HarmonyPostfix]
            public static void Prefix(AudioFactory __instance) {
                inst = __instance;
                if (!__instance.audioClips.ContainsKey("spidermanpunch"))
                    __instance.RegisterAudioClip("spidermanpunch", Assets.LoadAsset("punch").Cast<AudioClip>());
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
                if (reference != null && reference.guidRef.Equals("TMSMPortrait"))
                    try {
                        var b = Assets.LoadAsset(reference.guidRef);
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.Equals("TMSMPortrait2"))
                    try {
                        var b = Assets.LoadAsset(reference.guidRef);
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                if (reference != null && reference.guidRef.StartsWith("TMUpgrade"))
                    try {
                        var b = Assets.LoadAsset(reference.guidRef);
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
            }
        }

        [HarmonyPatch(typeof(Weapon), nameof(Weapon.SpawnDart))]
        public sealed class WI {
            private static readonly System.Random rand = new();

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
                    if (__instance.weaponModel.name.EndsWith("spidermanpunch")) {
                        __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().StopPlayback();
                        PlayPunchAnim(__instance.attack.tower, __instance.attack.target);
                        PlayAttack();
                    }
                } catch { }
            }

            // Messy code below :))))

            private static void PlayPunchAnim(Tower tower, Target target) {
                if (target?.bloon?.bloonModel?.isMoab ?? true) {
                    if (target?.bloon?.health - 1 > 0)
                        switch (rand.Next() % 3) {
                            case 0:
                                tower.Node.graphic.GetComponentInParent<Animator>().Play("Punch1");
                                break;
                            case 1:
                                tower.Node.graphic.GetComponentInParent<Animator>().Play("Punch2");
                                break;
                            case 2:
                                tower.Node.graphic.GetComponentInParent<Animator>().Play("Punch3");
                                break;
                        }
                    else if (rand.NextDouble() < 0.5)
                        tower.Node.graphic.GetComponentInParent<Animator>().Play($"Punch{rand.Next() % 9 + 1}");
                    else
                        tower.Node.graphic.GetComponentInParent<Animator>().Play("Punch1");
                } else {
                    switch (rand.Next() % 3) {
                        case 0:
                            if (target?.bloon?.health - 1 > 0)
                                switch (rand.Next() % 3) {
                                    case 0:
                                        tower.Node.graphic.GetComponentInParent<Animator>().Play("Punch1");
                                        break;
                                    case 1:
                                        tower.Node.graphic.GetComponentInParent<Animator>().Play("Punch2");
                                        break;
                                    case 2:
                                        tower.Node.graphic.GetComponentInParent<Animator>().Play("Punch3");
                                        break;
                                }
                            else
                                tower.Node.graphic.GetComponentInParent<Animator>().Play($"Punch{rand.Next() % 9 + 1}");
                            break;
                        case 1:
                            if (target?.bloon?.health - 1 > 0)
                                tower.Node.graphic.GetComponentInParent<Animator>().Play("Kick2");
                            else
                                tower.Node.graphic.GetComponentInParent<Animator>().Play($"Kick{rand.Next() % 3 + 1}");
                            break;
                        case 2:
                            if (target?.bloon?.health - 1 > 0)
                                tower.Node.graphic.GetComponentInParent<Animator>().Play("Sweep1");
                            else
                                tower.Node.graphic.GetComponentInParent<Animator>().Play($"Sweep{rand.Next() % 3 + 1}");
                            break;
                    }
                }
            }
        }
    }
}
