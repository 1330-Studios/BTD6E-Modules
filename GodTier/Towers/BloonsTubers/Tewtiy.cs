namespace GodlyTowers.Towers.BloonsTubers;

internal unsafe class Tewtiy : BloonsTuberBase {
    public new static string Name => "Tewtiy";
    public new static string Description => "COMO BLOON";
    public new static string CharactersBase => "BoomerangMonkey-005";
    public new static List<TowerModel> TowerModels = new();
    public new static List<UpgradeModel> UpgradeModels = new();

    public static AssetBundle TewtiyAssets;

    static Tewtiy() {
        TewtiyAssets = AssetBundle.LoadFromMemory(BloonsTuberAssets.TewtiyAssetBundle);
    }

    public new static (List<TowerModel>, List<UpgradeModel>, ShopTowerDetailsModel) InitializeTowers(ref GameModel model) {

        TowerModels = new() {
            GetBaseTower(ref model),
            GetT1(ref model),
            GetT2(ref model),
            GetT3(ref model),
            GetT4(ref model),
            GetT5(ref model),
            GetT6(ref model)
        };

        UpgradeModels = new() {
            new UpgradeModel("TewtiyBananaRicochet", 370, -1, new("TewtiyT1U"), 0, 0, 0, "", "Banana Ricochet"),
            new UpgradeModel("TewtiyLikeVid", 875, -1, new("TewtiyT2U"), 0, 1, 0, "", "Like The Video"),
            new UpgradeModel("TewtiyCommentVid", 2250, -1, new("TewtiyT3U"), 0, 2, 0, "", "Comment On The Video"),
            new UpgradeModel("TewtiySplosiveNanas", 7500, -1, new("TewtiyT4U"), 0, 3, 0, "", "\'Splosive Nanners"),
            new UpgradeModel("TewtiySubChannel", 20000, -1, new("TewtiyT5U"), 0, 4, 0, "", "Subscribe To The Channel"),
            new UpgradeModel("TewtiyLuminosity", 50000, -1, new("TewtiyT6U"), 1, 0, 0, "", "???")
        };

        model.towers = model.towers.Add(TowerModels);

        model.upgrades = model.upgrades.Add(UpgradeModels);

        foreach (var upgrade in UpgradeModels)
            model.upgradesByName.Add(upgrade.name, upgrade);

        model.towerSet = model.towerSet.Add(GetShopDetailsModel());

        return (TowerModels, UpgradeModels, GetShopDetailsModel());
    }

    public new static ShopTowerDetailsModel GetShopDetailsModel() {
        ShopTowerDetailsModel shop = new ShopTowerDetailsModel(Name, GlobalTowerIndex.Index, 5, 5, 5, -1, -1, null);

        if (!LocalizationManager.Instance.textTable.ContainsKey("Banana Ricochet Description"))
            LocalizationManager.Instance.textTable.Add("Banana Ricochet Description", "Banana go boioioioing");
        if (!LocalizationManager.Instance.textTable.ContainsKey("Like The Video Description"))
            LocalizationManager.Instance.textTable.Add("Like The Video Description", "Like the video. I'm waiting. LIKE THE VIDEO ALREADY!");
        if (!LocalizationManager.Instance.textTable.ContainsKey("Comment On The Video Description"))
            LocalizationManager.Instance.textTable.Add("Comment On The Video Description", "Comment on the video. If you liked the video you can do this too. I'll give you some time.");
        if (!LocalizationManager.Instance.textTable.ContainsKey("\'Splosive Nanners Description"))
            LocalizationManager.Instance.textTable.Add("\'Splosive Nanners Description", "Them nanners explodin\' again. Gee wiz. Alrighty, I guess we gotta throw em faster!");
        if (!LocalizationManager.Instance.textTable.ContainsKey("Subscribe To The Channel Description"))
            LocalizationManager.Instance.textTable.Add("Subscribe To The Channel Description", "You came this far, might as well subscribe. Theres more content like this uploaded daily! If you like this video, theres no doubt you'll like the others.");
        if (!LocalizationManager.Instance.textTable.ContainsKey("??? Description"))
            LocalizationManager.Instance.textTable.Add("??? Description", "Something big is coming...");

        return shop;
    }

    private static TowerModel GetBaseTower(ref GameModel model) {
        var tower = model.towers[0].CloneCast();
        tower.name = Name;
        tower.baseId = Name;
        tower.display = "TewtiyT0";
        tower.portrait = new("TewtiyPortrait");
        tower.icon = new("TewtiyPortrait");
        tower.upgrades = new UpgradePathModel[] { new("TewtiyBananaRicochet", "Tewtiy-100"), new("TewtiyLuminosity", "Tewtiy-510") };
        Parallel.ForEach(tower.behaviors, behavior => {
            if (behavior.Is<DisplayModel>(out var display)) {
                display.display = "TewtiyT0";
            }

            if (behavior.Is<AttackModel>(out var attack)) {
                foreach (var weapon in attack.weapons) {
                    weapon.projectile.display = "TewtiyBanana";
                    weapon.projectile.behaviors = weapon.projectile.behaviors.Add(new RotateModel("RotateModel_", -720.0f));
                }
            }
        });

        return tower;
    }

    private static TowerModel GetT1(ref GameModel model) {
        var tower = GetBaseTower(ref model);
        tower.name = Name + "-100";
        tower.display = "TewtiyT1";
        tower.tier = 1;
        tower.tiers = new[] { 1, 0, 0 };
        tower.upgrades = new UpgradePathModel[] { new("TewtiyLikeVid", "Tewtiy-200") };
        Parallel.ForEach(tower.behaviors, behavior => {
            if (behavior.Is<DisplayModel>(out var display)) {
                display.display = "TewtiyT1";
            }

            if (behavior.Is<AttackModel>(out var attack)) {
                foreach (var weapon in attack.weapons) {
                    weapon.projectile.behaviors = weapon.projectile.behaviors.Add(new TrackTargetWithinTimeModel("TrackTargetWithinTimeModel_", 9999999, true, false, 999, false, 9999999, false, 3.48f, true),
                        new KnockbackModel($"{weapon.projectile.name}_KnockbackModel_", 0.7f, 1f, 1.3f, 1, "KnockbackKnockback"));
                    foreach (var pbeh in weapon.projectile.behaviors) {
                        if (pbeh.Is<DamageModel>(out var damage)) {
                            damage.immuneBloonProperties = BloonProperties.None;
                            damage.damage++;
                        }
                    }
                }
            }
        });

        tower.behaviors = tower.behaviors.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true));

        return tower;
    }

    private static TowerModel GetT2(ref GameModel model) {
        var tower = GetT1(ref model);
        tower.name = Name + "-200";
        tower.display = "TewtiyT2";
        tower.tier = 2;
        tower.tiers = new[] { 2, 0, 0 };
        tower.upgrades = new UpgradePathModel[] { new("TewtiyCommentVid", "Tewtiy-300"), new("TewtiyLuminosity", "Tewtiy-510") };
        Parallel.ForEach(tower.behaviors, behavior => {
            if (behavior.Is<DisplayModel>(out var display)) {
                display.display = "TewtiyT2";
            }

            if (behavior.Is<AttackModel>(out var attack)) {
                foreach (var weapon in attack.weapons) {
                    weapon.rate -= 0.1f;
                    weapon.emission = new ArcEmissionModel("ArcEmissionModel_", 3, 0, 180, null, false);

                    foreach (var pbeh in weapon.projectile.behaviors) {
                        if (pbeh.Is<DamageModel>(out var damage)) {
                            damage.immuneBloonProperties = BloonProperties.None;
                            damage.damage++;
                        }
                    }
                }
            }
        });

        var transformationTower = model.towers.First(a => a.name.Contains("SuperMonkey-205")).CloneCast();

        var effect = new CreateEffectOnAbilityModel("CreateEffectOnAbilityModel_", new("EffectModel_", "b2a997cf912ef7e4494eee0b0645b976", 1, 4, false, false, false, false, false, false, false), false, false, false, false, false);
        var effectEnd = new CreateEffectOnAbilityEndModel("CreateEffectOnAbilityEndModel_", new("EffectModel_", "b2a997cf912ef7e4494eee0b0645b976", 1, 4, false, false, false, false, false, false, false), 10);

        var morph = new MorphTowerModel("MorphTowerModel_", true, 99999999, "TewtiyLikeVideo", 10, true, false, transformationTower, null,
            new("EffectModel_", "b2a997cf912ef7e4494eee0b0645b976", 1, 4, false, false, false, false, false, false, false), 5, 9999999999, 1, "", new("EffectModel_", "b2a997cf912ef7e4494eee0b0645b976", 1, 4, false, false, false, false, false, false, false),
            false, "");

        var switchDisplay = new SwitchDisplayModel("SwitchDisplayModel_", 10, true, "e6c683076381222438dfc733a602c157", new("EffectModel_", "b2a997cf912ef7e4494eee0b0645b976", 1, 4, false, false, false, false, false, false, false), true);
        var addRangs = new ActivateAttackModel("ActivateAttackModel_", 10, false,
            new AttackModel[] { transformationTower.behaviors.First(a => a.Is<AttackModel>(out _)).CloneCast<AttackModel>() }, false, true, false, true, false);

        var increaseRange = new IncreaseRangeModel("IncreaseRangeModel_", 599, 1, transformationTower.range - tower.range, true);

        var ability = new AbilityModel("LikeTheVideo", "Like The Video", "Like it please", 0, 0, new("TewtiyT2U"), 200,
            new Model[] { effect, effectEnd, morph, addRangs, switchDisplay, increaseRange }, false, false, "TewtiyLikeVid", 0, 0, 99999, false, false);

        tower.behaviors = tower.behaviors.Add(ability);

        return tower;
    }

    private static TowerModel GetT3(ref GameModel model) {
        var tower = GetT2(ref model);
        tower.name = Name + "-300";
        tower.display = "TewtiyT3";
        tower.tier = 3;
        tower.tiers = new[] { 3, 0, 0 };
        tower.upgrades = new UpgradePathModel[] { new("TewtiySplosiveNanas", "Tewtiy-400"), new("TewtiyLuminosity", "Tewtiy-510") };
        Parallel.ForEach(tower.behaviors, behavior => {
            if (behavior.Is<DisplayModel>(out var display)) {
                display.display = "TewtiyT3";
            }

            if (behavior.Is<AttackModel>(out var attack)) {
                attack.range += 25;
                foreach (var weapon in attack.weapons) {
                    foreach (var pbeh in weapon.projectile.behaviors) {
                        if (pbeh.Is<TravelStraitModel>(out var tsm)) {
                            tsm.lifespan++;
                            tsm.speed *= 1.25f;
                        }
                        if (pbeh.Is<DamageModel>(out var damage)) {
                            damage.immuneBloonProperties = BloonProperties.None;
                            damage.damage++;
                        }
                    }
                }
            }
        });
        tower.range += 25;

        var effect = new CreateEffectOnAbilityModel("CreateEffectOnAbilityModel_", new("EffectModel_", "7d20a90dc7159c2428d4c5a9ee4b7277", 1, 4, false, false, false, false, false, false, false), false, false, false, false, false);
        var effectEnd = new CreateEffectOnAbilityEndModel("CreateEffectOnAbilityEndModel_", new("EffectModel_", "7d20a90dc7159c2428d4c5a9ee4b7277", 1, 4, false, false, false, false, false, false, false), 30);

        var increaseSpeed = new TurboModel("TurboModel_", 30, 2, new AssetPathModel("TewtiyTurboBanana", "TewtiyTurboBanana"), 0, 0, false);

        var ability = new AbilityModel("CommentOnTheVideo", "Comment On The Video", "Comment on the video now!", 0, 0, new("TewtiyT3U"), 140,
            new Model[] { effect, effectEnd, increaseSpeed }, false, false, "TewtiyLikeVid", 0, 0, 99999, false, false);

        tower.behaviors = tower.behaviors.Add(ability);

        return tower;
    }

    private static TowerModel GetT4(ref GameModel model) {
        var tower = GetT3(ref model);
        tower.name = Name + "-400";
        tower.display = "TewtiyT4";
        tower.tier = 4;
        tower.tiers = new[] { 4, 0, 0 };
        tower.upgrades = new UpgradePathModel[] { new("TewtiySubChannel", "Tewtiy-500"), new("TewtiyLuminosity", "Tewtiy-510") };

        var bomb = model.towers.First(a => a.name.Contains("Bomb")).behaviors.First(a=>a.Is<AttackModel>(out _)).CloneCast<AttackModel>().weapons[0].projectile;

        Parallel.ForEach(tower.behaviors, behavior => {
            if (behavior.Is<DisplayModel>(out var display)) {
                display.display = "TewtiyT4";
            }

            if (behavior.Is<AttackModel>(out var attack)) {
                foreach (var weapon in attack.weapons) {
                    weapon.projectile.pierce = 5;
                    weapon.projectile.behaviors = weapon.projectile.behaviors.Add(bomb.behaviors.First(a => a.Is<CreateProjectileOnContactModel>(out _)).Clone(),
                        bomb.behaviors.First(a => a.Is<CreateEffectOnContactModel>(out _)).Clone(),
                        bomb.behaviors.First(a => a.Is<CreateSoundOnProjectileCollisionModel>(out _)).Clone());
                    weapon.projectile.display = "TewtiySplosiveNanner";

                    foreach (var pbeh in weapon.projectile.behaviors) {
                        if (pbeh.Is<DamageModel>(out var damage)) {
                            damage.immuneBloonProperties = BloonProperties.None;
                            damage.damage++;
                        }
                    }
                }
            }
        });

        return tower;
    }

    private static TowerModel GetT5(ref GameModel model) {
        var tower = GetT4(ref model);
        tower.name = Name + "-500";
        tower.display = "TewtiyT5";
        tower.tier = 5;
        tower.tiers = new[] { 5, 0, 0 };
        tower.upgrades = new UpgradePathModel[] { new("TewtiyLuminosity", "Tewtiy-510") };
        Parallel.ForEach(tower.behaviors, behavior => {
            if (behavior.Is<DisplayModel>(out var display)) {
                display.display = "TewtiyT5";
            }

            if (behavior.Is<AttackModel>(out var attack)) {
                attack.range += 50;

                foreach (var weapon in attack.weapons) {
                    weapon.rate = 0.05f;
                    weapon.projectile.pierce = 15;

                    foreach (var pbeh in weapon.projectile.behaviors) {
                        if (pbeh.Is<TravelStraitModel>(out var tsm)) {
                            tsm.lifespan++;
                            tsm.speed *= 1.25f;
                        }
                        if (pbeh.Is<DamageModel>(out var damage)) {
                            damage.immuneBloonProperties = BloonProperties.None;
                            damage.damage = 50;
                        }
                    }
                }
            }
        });

        tower.range += 50;

        var effect = new CreateEffectOnAbilityModel("CreateEffectOnAbilityModel_", new("EffectModel_", "7d20a90dc7159c2428d4c5a9ee4b7277", 1, 4, false, false, false, false, false, false, false), false, false, false, false, false);
        var effectEnd = new CreateEffectOnAbilityEndModel("CreateEffectOnAbilityEndModel_", new("EffectModel_", "7d20a90dc7159c2428d4c5a9ee4b7277", 1, 4, false, false, false, false, false, false, false), 15);

        var increaseDamage = new TurboModel("TurboModel_", 15, 2.5f, new AssetPathModel("TewtiySplosiveNanner", "TewtiySplosiveNanner"), 50, 0, false);

        var ability = new AbilityModel("SubscribeToTheChannel", "Subscribe To The Channel", "Subscribe To The Channel RIGHT THIS INSTANT!", 0, 0, new("TewtiyT5U"), 80,
            new Model[] { effect, effectEnd, increaseDamage }, false, false, "TewtiySubChannel", 0, 0, 99999, false, false);

        tower.behaviors = tower.behaviors.Add(ability);

        return tower;
    }



    private static TowerModel GetT6(ref GameModel model) {
        var tower = GetT5(ref model);
        tower.name = Name + "-510";
        tower.display = "TewtiyT6";
        tower.tier = 5;
        tower.tiers = new[] { 5, 1, 0 };
        tower.upgrades = Array.Empty<UpgradePathModel>();
        Parallel.ForEach(tower.behaviors, behavior => {
            if (behavior.Is<DisplayModel>(out var display)) {
                display.display = "TewtiyT6";
            }

            if (behavior.Is<AttackModel>(out var attack)) {
                attack.range += 50;

                foreach (var weapon in attack.weapons) {
                    weapon.rate = 0.001f;
                    weapon.projectile.pierce = 25;
                    weapon.emission = new AdoraEmissionModel("AEM_", 5, 30, null);
                    weapon.projectile.display = "TewtiyLBanana";
                    weapon.projectile.radius *= 2;

                    foreach (var pbeh in weapon.projectile.behaviors) {
                        if (pbeh.Is<TravelStraitModel>(out var tsm)) {
                            tsm.lifespan++;
                            tsm.speed *= 2;
                        }
                        if (pbeh.Is<DamageModel>(out var damage)) {
                            damage.immuneBloonProperties = BloonProperties.None;
                            damage.damage = 250;
                        }
                    }
                }
            }
        });

        tower.range += 50;

        var effect = new CreateEffectOnAbilityModel("CreateEffectOnAbilityModel_", new("EffectModel_", "7d20a90dc7159c2428d4c5a9ee4b7277", 1, 4, false, false, false, false, false, false, false), false, false, false, false, false);
        var effectEnd = new CreateEffectOnAbilityEndModel("CreateEffectOnAbilityEndModel_", new("EffectModel_", "7d20a90dc7159c2428d4c5a9ee4b7277", 1, 4, false, false, false, false, false, false, false), 15);

        var rangeUp = new IncreaseRangeModel("IncreaseRangeModel_", 899, 2, 50, true);
        var livesUp = new BonusLivesOnAbilityModel("BonusLivesOnAbilityModel_", 500);
        var switchDisp = new SwitchDisplayModel("SwitchDisplayModel_", 15, true, "TewtiyT6A", new("EffectModel_", "7d20a90dc7159c2428d4c5a9ee4b7277", 1, 4, false, false, false, false, false, false, false), true);
        var increaseSpeed = new TurboModel("TurboModel_", 15, 0, new AssetPathModel("TewtiyLBanana", "TewtiyLBanana"), 5000, 2, false);

        var ability = new AbilityModel("LuminosityMoment", "Luminosity Moment", "Lets   Get   This   BREAD", 0, 0, new("TewtiyLAbility"), 50,
            new Model[] { effect, effectEnd, rangeUp, livesUp, switchDisp, increaseSpeed }, false, false, "TewtiyLuminosity", 0, 0, 99999, false, false);

        tower.behaviors = tower.behaviors.Remove(a=>a.GetIl2CppType() == Il2CppType.Of<AbilityModel>()).Add(ability);

        return tower;
    }


    [HarmonyPatch(typeof(UpgradeButton), nameof(UpgradeButton.UpdateVisuals))]
    internal sealed class UpgradeButton_Update {
        [HarmonyPostfix]
        internal static void Fix(ref UpgradeButton __instance) {
            if (__instance == null) return;
            if (__instance.upgrade == null) return;
            if (__instance.upgrade.name == null) return;

            if (__instance?.tts.tower.towerModel.baseId.Equals("Tewtiy") == true && __instance.upgrade.name == "TewtiyLuminosity") {
                __instance.enabled = __instance.tts.tower.towerModel.tier == 5;
                __instance.transform.localScale = __instance.tts.tower.towerModel.tier == 5 ? new Vector3(1, 1, 1) : new Vector3(0, 0, 0);
            } else {
                __instance.enabled = true;
                __instance.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    [HarmonyPatch(typeof(Tower), nameof(Tower.OnPlace))]
    public static class Tower_OnPlace {
        [HarmonyPostfix]
        public static void Postfix(ref Tower __instance) {
            if (__instance?.towerModel?.baseId.Equals("Tewtiy") == true) {
                PlayPlacement();
            }
        }
    }

    public static void PlayPlacement() => AudioFactory_CreateStartingSources.inst.PlaySoundFromUnity(null, "placement_audio", "FX", 5, 1, 0, false);
    public static void PlayLike() => AudioFactory_CreateStartingSources.inst.PlaySoundFromUnity(null, "Like_the_video_audio", "FX", 5, 1, 0, false);
    public static void PlayComment() => AudioFactory_CreateStartingSources.inst.PlaySoundFromUnity(null, "Comment_on_the_video_audio", "FX", 5, 1, 0, false);
    public static void PlaySubscribe() => AudioFactory_CreateStartingSources.inst.PlaySoundFromUnity(null, "SAubscribe_to_channel_audio", "FX", 5, 1, 0, false);

    [HarmonyPatch(typeof(AudioFactory), nameof(AudioFactory.Start))]
    public static class AudioFactory_CreateStartingSources {
        public static AudioFactory inst;

        [HarmonyPostfix]
        public static void Prefix(AudioFactory __instance) {
            inst = __instance;
            if (!__instance.audioClips.ContainsKey("Comment_on_the_video_audio")) {
                var ac = TewtiyAssets.LoadAsset("Comment_on_the_video_audio").Cast<AudioClip>();
                __instance.RegisterAudioClip("Comment_on_the_video_audio", ac);
            }
            if (!__instance.audioClips.ContainsKey("Like_the_video_audio")) {
                var ac = TewtiyAssets.LoadAsset("Like_the_video_audio").Cast<AudioClip>();
                __instance.RegisterAudioClip("Like_the_video_audio", ac);
            }
            if (!__instance.audioClips.ContainsKey("placement_audio")) {
                var ac = TewtiyAssets.LoadAsset("placement").Cast<AudioClip>();
                __instance.RegisterAudioClip("placement_audio", ac);
            }
            if (!__instance.audioClips.ContainsKey("SAubscribe_to_channel_audio")) {
                var ac = TewtiyAssets.LoadAsset("SAubscribe_to_channel_audio").Cast<AudioClip>();
                __instance.RegisterAudioClip("SAubscribe_to_channel_audio", ac);
            }
        }
    }

    private static Color[] BaseTexturePixels;

    public static Texture2D LoadTextureFromBytes(byte[] FileData) {
        Texture2D Tex2D = new(2, 2);
        Tex2D.filterMode = FilterMode.Trilinear;
        if (ImageConversion.LoadImage(Tex2D, FileData)) return Tex2D;

        return null;
    }

    public static Texture2D CombineTextures(Texture2D baseTex, Texture2D overlayTex) {
        checked {
            unchecked {
                Texture2D texture = new(baseTex.width, baseTex.height);

                var MainPixels = new Color[baseTex.width * baseTex.height];
                if (BaseTexturePixels == null)
                    BaseTexturePixels = baseTex.GetPixels();
                var OverlayPixels = overlayTex.GetPixels();

                for (int i = 0; i < MainPixels.Length; i++)
                    MainPixels[i] = OverlayPixels[i].a < 1 ? BaseTexturePixels[i] + OverlayPixels[i] : OverlayPixels[i];

                texture.SetPixels(MainPixels);
                texture.Apply();

                return texture;
            }
        }
    }

    [HarmonyPatch(typeof(Factory), nameof(Factory.FindAndSetupPrototypeAsync))]
    public static class PrototypeUDN_Patch {
        public static Dictionary<string, UnityDisplayNode> protos = new();
        [HideFromIl2Cpp]
        public static Dictionary<string, Texture2D> Textures { get; } = new();

        private static bool hadError;

        public static void Init() {
            Textures.Clear();
            Textures.Add("TewtiyBase", LoadTextureFromBytes(BloonsTuberAssets.Tewtiy));
            Textures.Add("TewtiyT1", CombineTextures(LoadTextureFromBytes(BloonsTuberAssets.Tewtiy), LoadTextureFromBytes(BloonsTuberAssets.TewtiyT1Overlay)));
            Textures.Add("TewtiyT2", CombineTextures(LoadTextureFromBytes(BloonsTuberAssets.Tewtiy), LoadTextureFromBytes(BloonsTuberAssets.TewtiyT2Overlay)));
            Textures.Add("TewtiyT3", CombineTextures(LoadTextureFromBytes(BloonsTuberAssets.Tewtiy), LoadTextureFromBytes(BloonsTuberAssets.TewtiyT3Overlay)));
            Textures.Add("TewtiyT4", CombineTextures(LoadTextureFromBytes(BloonsTuberAssets.Tewtiy), LoadTextureFromBytes(BloonsTuberAssets.TewtiyT4Overlay)));
            Textures.Add("TewtiyT5", CombineTextures(LoadTextureFromBytes(BloonsTuberAssets.Tewtiy), LoadTextureFromBytes(BloonsTuberAssets.TewtiyT5Overlay)));
            Textures.Add("TewtiyT6", CombineTextures(LoadTextureFromBytes(BloonsTuberAssets.Tewtiy), LoadTextureFromBytes(BloonsTuberAssets.TewtiyT6Overlay)));
            Textures.Add("TewtiyT6A", CombineTextures(LoadTextureFromBytes(BloonsTuberAssets.Tewtiy), LoadTextureFromBytes(BloonsTuberAssets.TewtiyT6AOverlay)));

            do {
                hadError = false;

                foreach (var name in Textures.Keys)
                    GetTexture(name);

            } while (Textures["TewtiyT1"] == null || Textures["TewtiyT2"] == null || Textures["TewtiyT3"] == null
            || Textures["TewtiyT4"] == null || Textures["TewtiyT5"] == null || Textures["TewtiyT6"] == null ||
            Textures["TewtiyT6A"] == null || hadError);

            Logger13.Log("Generated all textures for \"Tewtiy\" tower.");
        }

        public static Texture2D GetTexture(string name) {
            var ret = Textures[name];

            if (ret == null || ret.GetPixel(1, 1) == Color.white) {
                hadError = true;

                Logger13.Warn($"{name} had to be regenerated!");

                switch (name) {
                    case "TewtiyBase":
                        Textures["TewtiyBase"] = LoadTextureFromBytes(BloonsTuberAssets.Tewtiy);
                        break;
                    case "TewtiyT1":
                        Textures["TewtiyT1"] = CombineTextures(LoadTextureFromBytes(BloonsTuberAssets.Tewtiy), LoadTextureFromBytes(BloonsTuberAssets.TewtiyT1Overlay));
                        break;
                    case "TewtiyT2":
                        Textures["TewtiyT2"] = CombineTextures(LoadTextureFromBytes(BloonsTuberAssets.Tewtiy), LoadTextureFromBytes(BloonsTuberAssets.TewtiyT2Overlay));
                        break;
                    case "TewtiyT3":
                        Textures["TewtiyT3"] = CombineTextures(LoadTextureFromBytes(BloonsTuberAssets.Tewtiy), LoadTextureFromBytes(BloonsTuberAssets.TewtiyT3Overlay));
                        break;
                    case "TewtiyT4":
                        Textures["TewtiyT4"] = CombineTextures(LoadTextureFromBytes(BloonsTuberAssets.Tewtiy), LoadTextureFromBytes(BloonsTuberAssets.TewtiyT4Overlay));
                        break;
                    case "TewtiyT5":
                        Textures["TewtiyT5"] = CombineTextures(LoadTextureFromBytes(BloonsTuberAssets.Tewtiy), LoadTextureFromBytes(BloonsTuberAssets.TewtiyT5Overlay));
                        break;
                    case "TewtiyT6":
                        Textures["TewtiyT6"] = CombineTextures(LoadTextureFromBytes(BloonsTuberAssets.Tewtiy), LoadTextureFromBytes(BloonsTuberAssets.TewtiyT6Overlay));
                        break;
                    case "TewtiyT6A":
                        Textures["TewtiyT6A"] = CombineTextures(LoadTextureFromBytes(BloonsTuberAssets.Tewtiy), LoadTextureFromBytes(BloonsTuberAssets.TewtiyT6AOverlay));
                        break;
                }

                ret = Textures[name];
            }

            return ret;
        }

        [HarmonyPrefix]
        public static bool Prefix(Factory __instance, string objectId, Il2CppSystem.Action<UnityDisplayNode> onComplete) {
            if (objectId.Equals("TewtiyT0")) {
                UnityDisplayNode udn = null;
                __instance.FindAndSetupPrototypeAsync("c73f298fe9c1187449a45ef0a7ae5fc2", new Action<UnityDisplayNode>(btdUdn => {
                    var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                    instance.name = objectId + "(Clone)";
                    instance.RecalculateGenericRenderers();
                    foreach (var r in instance.genericRenderers.Where(r => r.Is<SkinnedMeshRenderer>(out var smr))) {
                        r.material.mainTexture = GetTexture("TewtiyBase");
                    }

                    udn = instance;
                    onComplete.Invoke(udn);
                }));
                return false;
            }

            if (objectId.Equals("TewtiyT1")) {
                UnityDisplayNode udn = null;
                __instance.FindAndSetupPrototypeAsync("c73f298fe9c1187449a45ef0a7ae5fc2", new Action<UnityDisplayNode>(btdUdn => {
                    var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                    instance.name = objectId + "(Clone)";
                    instance.RecalculateGenericRenderers();
                    foreach (var r in instance.genericRenderers.Where(r => r.Is<SkinnedMeshRenderer>(out var smr))) {
                        r.material.mainTexture = GetTexture("TewtiyT1");
                    }

                    udn = instance;
                    onComplete.Invoke(udn);
                }));
                return false;
            }

            if (objectId.Equals("TewtiyT2")) {
                UnityDisplayNode udn = null;
                __instance.FindAndSetupPrototypeAsync("c73f298fe9c1187449a45ef0a7ae5fc2", new Action<UnityDisplayNode>(btdUdn => {
                    var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                    instance.name = objectId + "(Clone)";
                    instance.RecalculateGenericRenderers();
                    foreach (var r in instance.genericRenderers.Where(r => r.Is<SkinnedMeshRenderer>(out var smr))) {
                        r.material.mainTexture = GetTexture("TewtiyT2");
                    }

                    udn = instance;
                    onComplete.Invoke(udn);
                }));
                return false;
            }

            if (objectId.Equals("TewtiyT3")) {
                UnityDisplayNode udn = null;
                __instance.FindAndSetupPrototypeAsync("c73f298fe9c1187449a45ef0a7ae5fc2", new Action<UnityDisplayNode>(btdUdn => {
                    var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                    instance.name = objectId + "(Clone)";
                    instance.RecalculateGenericRenderers();
                    foreach (var r in instance.genericRenderers.Where(r => r.Is<SkinnedMeshRenderer>(out var smr))) {
                        r.material.mainTexture = GetTexture("TewtiyT3");
                    }

                    udn = instance;
                    onComplete.Invoke(udn);
                }));
                return false;
            }

            if (objectId.Equals("TewtiyT4")) {
                UnityDisplayNode udn = null;
                __instance.FindAndSetupPrototypeAsync("c73f298fe9c1187449a45ef0a7ae5fc2", new Action<UnityDisplayNode>(btdUdn => {
                    var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                    instance.name = objectId + "(Clone)";
                    instance.RecalculateGenericRenderers();
                    foreach (var r in instance.genericRenderers.Where(r => r.Is<SkinnedMeshRenderer>(out var smr))) {
                        r.material.mainTexture = GetTexture("TewtiyT4");
                    }

                    udn = instance;
                    onComplete.Invoke(udn);
                }));
                return false;
            }

            if (objectId.Equals("TewtiyT5")) {
                UnityDisplayNode udn = null;
                __instance.FindAndSetupPrototypeAsync("c73f298fe9c1187449a45ef0a7ae5fc2", new Action<UnityDisplayNode>(btdUdn => {
                    var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                    instance.name = objectId + "(Clone)";
                    instance.RecalculateGenericRenderers();
                    foreach (var r in instance.genericRenderers.Where(r => r.Is<SkinnedMeshRenderer>(out var smr))) {
                        r.material.mainTexture = GetTexture("TewtiyT5");
                    }

                    var _obj = TewtiyAssets.LoadAsset("TewtiyParticleSystem").Cast<GameObject>();
                    var obj = Object.Instantiate(_obj, instance.transform);
                    obj.SetActive(true);
                    var ps = obj.transform.GetComponentInChildren<ParticleSystem>();
                    ps.emissionRate = 15;
                    ps.transform.localScale = new(10, 10, 10);

                    udn = instance;
                    onComplete.Invoke(udn);
                }));
                return false;
            }

            if (objectId.Equals("TewtiyT6") || objectId.Equals("TewtiyT6A")) {
                UnityDisplayNode udn = null;
                __instance.FindAndSetupPrototypeAsync("c73f298fe9c1187449a45ef0a7ae5fc2", new Action<UnityDisplayNode>(btdUdn => {
                    var instance = Object.Instantiate(btdUdn, __instance.PrototypeRoot);
                    instance.name = objectId + "(Clone)";
                    instance.RecalculateGenericRenderers();
                    foreach (var r in instance.genericRenderers.Where(r => r.Is<SkinnedMeshRenderer>(out var smr))) {
                        r.material.mainTexture = GetTexture(objectId);
                    }

                    if (objectId.Equals("TewtiyT6A")) {
                        {
                            instance.transform.gameObject.AddComponent<SetScaleT>();
                            var _obj = TewtiyAssets.LoadAsset("LAPartSys").Cast<GameObject>();
                            var obj = Object.Instantiate(_obj, instance.transform);
                            obj.SetActive(true);
                            var ps = obj.transform.GetComponentInChildren<ParticleSystem>();
                            ps.transform.localScale = new(10, 10, 10);
                        }
                        {
                            var _obj = TewtiyAssets.LoadAsset("LAPartSysL").Cast<GameObject>();
                            var obj = Object.Instantiate(_obj, instance.transform);
                            obj.SetActive(true);
                            var ps = obj.transform.GetComponentInChildren<ParticleSystem>();
                            ps.transform.localScale = new(10, 10, 10);
                        }
                    } else {
                        {
                            var _obj = TewtiyAssets.LoadAsset("LPartSys").Cast<GameObject>();
                            var obj = Object.Instantiate(_obj, instance.transform);
                            obj.SetActive(true);
                            var ps = obj.transform.GetComponentInChildren<ParticleSystem>();
                            ps.transform.localScale = new(10, 10, 10);
                        }
                    }

                    udn = instance;
                    onComplete.Invoke(udn);
                }));
                return false;
            }

            if (objectId.Equals("TewtiyBanana")) {
                UnityDisplayNode udn = null;
                __instance.FindAndSetupPrototypeAsync("842be402795e7334cbc77d33b6746bff",
                    new Action<UnityDisplayNode>(oudn => {
                        var nudn = Object.Instantiate(oudn, __instance.PrototypeRoot);
                        nudn.name = objectId + "(Clone)";
                        nudn.isSprite = true;
                        nudn.RecalculateGenericRenderers();
                        for (var i = 0; i < nudn.genericRenderers.Length; i++) {
                            if (nudn.genericRenderers[i].GetIl2CppType() == Il2CppType.Of<SpriteRenderer>()) {
                                var smr = nudn.genericRenderers[i].Cast<SpriteRenderer>();
                                var text = LoadTextureFromBytes(BloonsTuberAssets.TewtiyBanana);
                                smr.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new(0.5f, 0.5f), 5.4f);
                            }
                        }
                        nudn.gameObject.AddComponent<Rotate>();

                        udn = nudn;
                        onComplete.Invoke(udn);
                    }));
                return false;
            }

            if (objectId.Equals("TewtiySplosiveNanner")) {
                UnityDisplayNode udn = null;
                __instance.FindAndSetupPrototypeAsync("842be402795e7334cbc77d33b6746bff",
                    new Action<UnityDisplayNode>(oudn => {
                        var nudn = Object.Instantiate(oudn, __instance.PrototypeRoot);
                        nudn.name = objectId + "(Clone)";
                        nudn.isSprite = true;
                        nudn.RecalculateGenericRenderers();
                        for (var i = 0; i < nudn.genericRenderers.Length; i++) {
                            if (nudn.genericRenderers[i].GetIl2CppType() == Il2CppType.Of<SpriteRenderer>()) {
                                var smr = nudn.genericRenderers[i].Cast<SpriteRenderer>();
                                var text = LoadTextureFromBytes(BloonsTuberAssets.TewtiySplosiveNanners);
                                smr.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new(0.5f, 0.5f), 5.4f);
                            }
                        }
                        nudn.gameObject.AddComponent<Rotate>();

                        udn = nudn;
                        onComplete.Invoke(udn);
                    }));
                return false;
            }

            if (objectId.Equals("TewtiyTurboBanana")) {
                UnityDisplayNode udn = null;
                __instance.FindAndSetupPrototypeAsync("842be402795e7334cbc77d33b6746bff",
                    new Action<UnityDisplayNode>(oudn => {
                        var nudn = Object.Instantiate(oudn, __instance.PrototypeRoot);
                        nudn.name = objectId + "(Clone)";
                        nudn.isSprite = true;
                        nudn.RecalculateGenericRenderers();
                        for (var i = 0; i < nudn.genericRenderers.Length; i++) {
                            if (nudn.genericRenderers[i].GetIl2CppType() == Il2CppType.Of<SpriteRenderer>()) {
                                var smr = nudn.genericRenderers[i].Cast<SpriteRenderer>();
                                var text = LoadTextureFromBytes(BloonsTuberAssets.TewtiyTurboBanana);
                                smr.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new(0.5f, 0.5f), 5.4f);
                            }
                        }
                        nudn.gameObject.AddComponent<Rotate>();

                        udn = nudn;
                        onComplete.Invoke(udn);
                    }));
                return false;
            }

            if (objectId.Equals("TewtiyLBanana")) {
                UnityDisplayNode udn = null;
                __instance.FindAndSetupPrototypeAsync("842be402795e7334cbc77d33b6746bff",
                    new Action<UnityDisplayNode>(oudn => {
                        var nudn = Object.Instantiate(oudn, __instance.PrototypeRoot);
                        nudn.name = objectId + "(Clone)";
                        nudn.isSprite = true;
                        nudn.RecalculateGenericRenderers();
                        for (var i = 0; i < nudn.genericRenderers.Length; i++) {
                            if (nudn.genericRenderers[i].GetIl2CppType() == Il2CppType.Of<SpriteRenderer>()) {
                                var smr = nudn.genericRenderers[i].Cast<SpriteRenderer>();
                                var text = LoadTextureFromBytes(BloonsTuberAssets.TewtiyLBanana);
                                smr.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new(0.5f, 0.5f), 5.4f);
                            }
                        }
                        nudn.gameObject.AddComponent<Rotate>();

                        var _obj = TewtiyAssets.LoadAsset("LPBanana").Cast<GameObject>();
                        var obj = Object.Instantiate(_obj, nudn.transform);
                        obj.SetActive(true);
                        var ps = obj.transform.GetComponentInChildren<ParticleSystem>();
                        ps.transform.localScale = new(10, 10, 10);

                        udn = nudn;
                        onComplete.Invoke(udn);
                    }));
                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(ResourceLoader), nameof(ResourceLoader.LoadSpriteFromSpriteReferenceAsync))]
    public sealed class ResourceLoader_Patch {
        [HarmonyPostfix]
        public static void Postfix(SpriteReference reference, ref Image image) {
            if (reference != null && reference.guidRef.Equals("TewtiyPortrait"))
                try {
                    var b = LoadTextureFromBytes(BloonsTuberAssets.TewtiyPortrait);
                    if (b != null) {
                        var text = b.Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                    }
                } catch { }
            if (reference != null && reference.guidRef.Equals("TewtiyT1U"))
                try {
                    var b = LoadTextureFromBytes(BloonsTuberAssets.TewtiyT1Upgrade);
                    if (b != null) {
                        var text = b.Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                    }
                } catch { }
            if (reference != null && reference.guidRef.Equals("TewtiyT2U"))
                try {
                    var b = LoadTextureFromBytes(BloonsTuberAssets.TewtiyT2Upgrade);
                    if (b != null) {
                        var text = b.Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                    }
                } catch { }
            if (reference != null && reference.guidRef.Equals("TewtiyT3U"))
                try {
                    var b = LoadTextureFromBytes(BloonsTuberAssets.TewtiyT3Upgrade);
                    if (b != null) {
                        var text = b.Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                    }
                } catch { }
            if (reference != null && reference.guidRef.Equals("TewtiyT4U"))
                try {
                    var b = LoadTextureFromBytes(BloonsTuberAssets.TewtiyT4Upgrade);
                    if (b != null) {
                        var text = b.Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                    }
                } catch { }
            if (reference != null && reference.guidRef.Equals("TewtiyT5U"))
                try {
                    var b = LoadTextureFromBytes(BloonsTuberAssets.TewtiyT5Upgrade);
                    if (b != null) {
                        var text = b.Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                    }
                } catch { }
            if (reference != null && reference.guidRef.Equals("TewtiyT6U"))
                try {
                    var b = LoadTextureFromBytes(BloonsTuberAssets.TewtiyT6Upgrade);
                    if (b != null) {
                        var text = b.Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                    }
                } catch { }
            if (reference != null && reference.guidRef.Equals("TewtiyLAbility"))
                try {
                    var b = LoadTextureFromBytes(BloonsTuberAssets.TewtiyLAbility);
                    if (b != null) {
                        var text = b.Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                    }
                } catch { }
        }
    }

    [HarmonyPatch(typeof(Ability), nameof(Ability.Activate))]
    public sealed class AA {
        [HarmonyPrefix]
        internal static bool Prefix(ref Ability __instance) {
            if (__instance?.abilityModel?.name?.EndsWith("LikeTheVideo") ?? true) {
                PlayLike();
            }

            if (__instance?.abilityModel?.name?.EndsWith("CommentOnTheVideo") ?? true) {
                PlayComment();
            }

            if (__instance?.abilityModel?.name?.EndsWith("SubscribeToTheChannel") ?? true) {
                PlaySubscribe();
            }

            return true;
        }
    }
}
