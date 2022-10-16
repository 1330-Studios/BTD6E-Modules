using GodlyTowers.Utils;

namespace GodlyTowers.Towers;
internal static class LightningMcQueen {
    public static string name = "LightningMcQueen";

    public static UpgradeModel[] GetUpgrades() {
        return new UpgradeModel[] {
            new UpgradeModel("More Victories", 1200, 0, "McQueen_vicroy".GetSpriteReference(), 0, 0, 0, "", "More Victories"),
            new UpgradeModel("New & Improved", 3500, 0, "McQueen_newandimproved".GetSpriteReference(), 0, 1, 0, "", "New & Improved"),
            new UpgradeModel("Holy Driver", 7500, 0, "McQueen_holydriver".GetSpriteReference(), 0, 2, 0, "", "Holy Driver"),
            new UpgradeModel("Hi Ryan", 22000, 0, "McQueen_hiryan".GetSpriteReference(), 0, 3, 0, "", "Hi Ryan"),
            new UpgradeModel("He is speed", 48500, 0, "McQueen_iamspeed".GetSpriteReference(), 0, 4, 0, "", "He is speed")
        };
    }

    public static (TowerModel, ShopTowerDetailsModel, TowerModel[], UpgradeModel[]) GetTower(GameModel gameModel) {
        var godzillaDetails = gameModel.towerSet[0].Clone().Cast<ShopTowerDetailsModel>();
        godzillaDetails.towerId = name;
        godzillaDetails.towerIndex = GlobalTowerIndex.Index;

        LocalizationManager.Instance.textTable["LightningMcQueen"] = "Lightning McQueen";

        LocalizationManager.Instance.textTable["More Victories Description"] = "More speed. More power.";
        LocalizationManager.Instance.textTable["New & Improved Description"] = "More damage! Also like and subscribe!";
        LocalizationManager.Instance.textTable["Holy Driver Description"] = "You've been down too long in the midnight sea oh, what's becoming of me?";
        LocalizationManager.Instance.textTable["Hi Ryan Description"] = "How you doin' today?";
        LocalizationManager.Instance.textTable["He is speed Description"] = "He is speed.";

        return (GetT0(gameModel), godzillaDetails, new[] { GetT0(gameModel), GetT1(gameModel), GetT2(gameModel), GetT3(gameModel), GetT4(gameModel), GetT5(gameModel) }, GetUpgrades());
    }

    public static TowerModel GetT0(GameModel gameModel) {
        var godzilla = gameModel.towers.First(a => a.baseId.Equals("HeliPilot")).Clone().Cast<TowerModel>();

        godzilla.name = name;
        godzilla.baseId = name;
        godzilla.SetDisplay("McQueenPad");
        godzilla.portrait = "GodlyTowers.Resources.McQueen.png".GetSpriteReference();
        godzilla.icon = "GodlyTowers.Resources.McQueen.png".GetSpriteReference();
        godzilla.towerSet = "Magic";
        godzilla.emoteSpriteLarge = new("Movie");
        godzilla.radius = 0;
        godzilla.cost = 2750;
        godzilla.range = 35;
        godzilla.towerSize = TowerModel.TowerSize.medium;
        godzilla.footprint.ignoresPlacementCheck = true;
        godzilla.areaTypes = new(4);
        godzilla.areaTypes[0] = AreaType.ice;
        godzilla.areaTypes[1] = AreaType.land;
        godzilla.areaTypes[2] = AreaType.track;
        godzilla.areaTypes[3] = AreaType.water;
        godzilla.cachedThrowMarkerHeight = 10;
        godzilla.upgrades = new UpgradePathModel[] { new("More Victories", name + "-100") };

        for (int i = 0; i < godzilla.behaviors.Length; i++) {
            if (godzilla.behaviors[i].Is<AirUnitModel>(out var aum)) {
                aum.display = "McQueen";
                var hmm = aum.behaviors[0].Cast<HeliMovementModel>();
                hmm.tiltAngle = 0;
                hmm.maxSpeed *= 2;
                hmm.slowdownRadiusMin = 0;
                hmm.slowdownRadiusMax = 5;
                hmm.strafeDistance = 1;
                hmm.strafeDistanceSquared = 1;
            }
            if (godzilla.behaviors[i].Is<AttackModel>(out var am)) {
                am.behaviors = new Model[] { new FollowTouchSettingModel("FTSM_", true, false), new RotateToTargetAirUnitModel("RTTAUM_", false) };
                am.fireWithoutTarget = true;
                am.weapons[0].emission = new EmissionWithOffsetsModel("EWOM_", new[] {
                    new ThrowMarkerOffsetModel("UFarLeft", 6.75f, 16, 4, 0),
                    new ThrowMarkerOffsetModel("ULeft", 4.5f, 16, 4, 0),
                    new ThrowMarkerOffsetModel("UInnerLeft", 2.25f, 16, 4, 0),
                    new ThrowMarkerOffsetModel("UMiddle", 0, 16, 4, 0),
                    new ThrowMarkerOffsetModel("UFarRight", -6.75f, 16, 4, 0),
                    new ThrowMarkerOffsetModel("URight", -4.5f, 16, 4, 0),
                    new ThrowMarkerOffsetModel("UInnerRight", -2.25f, 16, 4, 0),

                    new ThrowMarkerOffsetModel("BFarLeft", 6.75f, -16, 4, 0),
                    new ThrowMarkerOffsetModel("BLeft", 4.5f, -16, 4, 0),
                    new ThrowMarkerOffsetModel("BInnerLeft", 2.25f, -16, 4, 0),
                    new ThrowMarkerOffsetModel("BMiddle", 0, -16, 4, 0),
                    new ThrowMarkerOffsetModel("BFarRight", -6.75f, -16, 4, 0),
                    new ThrowMarkerOffsetModel("BRight", -4.5f, -16, 4, 0),
                    new ThrowMarkerOffsetModel("BInnerRight", -2.25f, -16, 4, 0) }, 14, false, null, 0);
                am.weapons[0].Rate = 0.33333334f / 2f;
                am.weapons[0].projectile.display = "";
                am.weapons[0].projectile.radius /= 2;
                for (int j = 0; j < am.weapons[0].projectile.behaviors.Count; j++) {
                    if (am.weapons[0].projectile.behaviors[j].Is<TravelStraitModel>(out var tsm)) {
                        tsm.Speed = 0.25f;
                        tsm.Lifespan = 0.15f;
                    }
                    if (am.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var dm)) {
                        dm.damage = 1.5f;
                    }
                }
            }
        }

        return godzilla;
    }

    public static TowerModel GetT1(GameModel gameModel) {
        var godzilla = GetT0(gameModel);

        godzilla.name = name + "-100";
        godzilla.tier = 1;
        godzilla.tiers = new int[] { 1, 0, 0 };
        godzilla.portrait = "McQueen_vicroy".GetSpriteReference();
        godzilla.upgrades = new[] { new UpgradePathModel("New & Improved", name + "-200") };

        for (int i = 0; i < godzilla.behaviors.Length; i++) {
            if (godzilla.behaviors[i].Is<AirUnitModel>(out var aum)) {
                var hmm = aum.behaviors[0].Cast<HeliMovementModel>();
                hmm.maxSpeed *= 2;
            }
            if (godzilla.behaviors[i].Is<AttackModel>(out var am)) {
                am.weapons[0].Rate /= 2;
            }
        }

        return godzilla;
    }

    public static TowerModel GetT2(GameModel gameModel) {
        var godzilla = GetT1(gameModel);

        godzilla.name = name + "-200";
        godzilla.baseId = name;
        godzilla.tier = 2;
        godzilla.tiers = new int[] { 2, 0, 0 };
        godzilla.portrait = "McQueen_newandimproved".GetSpriteReference();
        godzilla.upgrades = new[] { new UpgradePathModel("Holy Driver", name + "-300") };

        for (int i = 0; i < godzilla.behaviors.Length; i++) {
            if (godzilla.behaviors[i].Is<AirUnitModel>(out var aum)) {
                aum.display = "McQueenR1";
            }
            if (godzilla.behaviors[i].Is<AttackModel>(out var am)) {
                for (int j = 0; j < am.weapons[0].projectile.behaviors.Length; j++) {
                    if (am.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var damage)) {
                        damage.damage *= 5;
                        damage.immuneBloonProperties = BloonProperties.None;
                        damage.immuneBloonPropertiesOriginal = BloonProperties.None;
                    }
                }
            }
        }

        godzilla.behaviors = godzilla.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true));

        return godzilla;
    }

    public static TowerModel GetT3(GameModel gameModel) {
        var godzilla = GetT2(gameModel);

        godzilla.name = name + "-300";
        godzilla.baseId = name;
        godzilla.tier = 3;
        godzilla.tiers = new int[] { 3, 0, 0 };
        godzilla.portrait = "McQueen_holydriver".GetSpriteReference();
        godzilla.upgrades = new[] { new UpgradePathModel("Hi Ryan", name + "-400") };

        var wm = godzilla.CloneCast().behaviors.First(a => a.Is<AttackModel>()).Cast<AttackModel>().weapons[0].CloneCast();

        var MortarMonkey_002 = gameModel.towers.First(a => a.name.Equals("MortarMonkey-002")).CloneCast().behaviors.First(a => a.Is<AttackModel>()).Cast<AttackModel>();

        var cpoefm = MortarMonkey_002.weapons[0].projectile.behaviors.First(a => a.Is<CreateProjectileOnExhaustFractionModel>()).Cast<CreateProjectileOnExhaustFractionModel>();

        wm.Rate = 0.05f;
        wm.projectile.display = "McQueenTrack";
        wm.emission = new EmissionWithOffsetsModel("EWOM_", new[] {
                    new ThrowMarkerOffsetModel("BLeft", 4.5f, -16, 4, 0),
                    new ThrowMarkerOffsetModel("BRight", -4.5f, -16, 4, 0)
                }, 2, false, null, 0);
        wm.projectile.behaviors = wm.projectile.behaviors.Add(new CreateProjectileOnContactModel("CPOCM_", cpoefm.projectile, cpoefm.emission, false, false, false));

        for (int j = 0; j < wm.projectile.behaviors.Length; j++) {
            if (wm.projectile.behaviors[j].Is<TravelStraitModel>(out var tsm)) {
                tsm.Lifespan = 5;
                tsm.Speed = 0.01f;
            }
        }

        for (int i = 0; i < godzilla.behaviors.Length; i++) {
            if (godzilla.behaviors[i].Is<AirUnitModel>(out var aum)) {
                aum.display = "McQueen";
            }
            if (godzilla.behaviors[i].Is<AttackModel>(out var am)) {
                for (int j = 0; j < am.weapons[0].projectile.behaviors.Length; j++) {
                    if (am.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var damage)) {
                        damage.damage *= 2;
                    }
                }

                am.behaviors = am.behaviors.Add(new PursuitSettingModel("PursuitSettingModel_", true, 0.5f, false));

                am.weapons = am.weapons.Add(wm);
            }
        }

        return godzilla;
    }

    public static TowerModel GetT4(GameModel gameModel) {
        var godzilla = GetT3(gameModel);

        godzilla.name = name + "-400";
        godzilla.baseId = name;
        godzilla.tier = 4;
        godzilla.tiers = new int[] { 4, 0, 0 };
        godzilla.portrait = "McQueen_hiryan".GetSpriteReference();
        godzilla.upgrades = new[] { new UpgradePathModel("He is speed", name + "-500") };

        for (int i = 0; i < godzilla.behaviors.Length; i++) {
            if (godzilla.behaviors[i].Is<AirUnitModel>(out var aum)) {
                aum.display = "McQueenR2";
                var hmm = aum.behaviors[0].Cast<HeliMovementModel>();
                hmm.rotationSpeed *= 2;
            }
            if (godzilla.behaviors[i].Is<AttackModel>(out var am)) {

                am.weapons[0].Rate /= 2;
                am.weapons[1].Rate /= 2;

                for (int j = 0; j < am.weapons[0].projectile.behaviors.Length; j++) {
                    if (am.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var damage)) {
                        damage.damage *= 2;
                    }
                }
            }
        }

        return godzilla;
    }

    public static TowerModel GetT5(GameModel gameModel) {
        var godzilla = GetT4(gameModel);

        godzilla.name = name + "-500";
        godzilla.baseId = name;
        godzilla.tier = 5;
        godzilla.tiers = new int[] { 5, 0, 0 };
        godzilla.portrait = "McQueen_iamspeed".GetSpriteReference();
        godzilla.upgrades = Array.Empty<UpgradePathModel>();

        for (int i = 0; i < godzilla.behaviors.Length; i++) {
            if (godzilla.behaviors[i].Is<AirUnitModel>(out var aum)) {
                aum.display = "McQueenL";
                var hmm = aum.behaviors[0].Cast<HeliMovementModel>();
                hmm.rotationSpeed *= 2;
                hmm.maxSpeed *= 5;
            }
            if (godzilla.behaviors[i].Is<AttackModel>(out var am)) {

                am.weapons[0].Rate /= 4;
                am.weapons[1].Rate /= 4;

                for (int j = 0; j < am.weapons[0].projectile.behaviors.Length; j++) {
                    if (am.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var damage)) {
                        damage.damage *= 5;
                    }
                }
            }
        }

        return godzilla;
    }

    [HarmonyPatch(typeof(Factory), nameof(Factory.FindAndSetupPrototypeAsync))]
    public sealed class PrototypeUDN_Patch {
        public static Dictionary<string, UnityDisplayNode> protos = new();

        [HarmonyPrefix]
        public static bool Prefix(Factory __instance, string objectId, Il2CppSystem.Action<UnityDisplayNode> onComplete) {
            if (!protos.ContainsKey(objectId) && objectId.Equals("McQueen")) {
                var udn = GetMcQueen_Standard(__instance.PrototypeRoot);
                udn.name = "McQueen Standard";
                udn.RecalculateGenericRenderers();
                udn.isSprite = false;
                onComplete.Invoke(udn);
                protos.Add(objectId, udn);
                return false;
            }
            if (!protos.ContainsKey(objectId) && objectId.Equals("McQueenR1")) {
                var udn = GetMcQueen_Rev1(__instance.PrototypeRoot);
                udn.name = "McQueen Rev1";
                udn.RecalculateGenericRenderers();
                udn.isSprite = false;
                onComplete.Invoke(udn);
                protos.Add(objectId, udn);
                return false;
            }
            if (!protos.ContainsKey(objectId) && objectId.Equals("McQueenR2")) {
                var udn = GetMcQueen_Rev2(__instance.PrototypeRoot);
                udn.name = "McQueen Rev2";
                udn.RecalculateGenericRenderers();
                udn.isSprite = false;
                onComplete.Invoke(udn);
                protos.Add(objectId, udn);
                return false;
            }
            if (!protos.ContainsKey(objectId) && objectId.Equals("McQueenL")) {
                var udn = GetMcQueen_Large(__instance.PrototypeRoot);
                udn.name = "McQueen Large";
                udn.RecalculateGenericRenderers();
                udn.isSprite = false;
                onComplete.Invoke(udn);
                protos.Add(objectId, udn);
                return false;
            }

            if (!protos.ContainsKey(objectId) && objectId.Contains("McQueenPad")) {
                GameObject obj = Object.Instantiate(new GameObject(objectId + "(Clone)"), __instance.PrototypeRoot);
                var sr = obj.AddComponent<SpriteRenderer>();
                var tx = "GodlyTowers.Resources.vicroy.png".GetEmbeddedResource().ToTexture();
                sr.sprite = Sprite.Create(tx, new(0, 0, tx.width, tx.height), new(0.5f, 0.5f), 40.5f, 0, SpriteMeshType.Tight);
                var udn = obj.AddComponent<UnityDisplayNode>();
                udn.transform.position = new(-3000, 10);

                udn.gameObject.AddComponent<MoveUp>();

                onComplete.Invoke(udn);
                protos.Add(objectId, udn);

                return false;
            }

            if (objectId.Equals("McQueenTrack")) {
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
                                var text = Assets.LoadAsset("tracks").Cast<Texture2D>();
                                smr.sprite = Sprite.Create(text, new(0, 0, 16, 16), new(0.5f, 0.5f), 5.4f);
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

    public static UnityDisplayNode GetMcQueen_Standard(Transform transform) {
        var udn = Object.Instantiate(Assets.LoadAsset("McQueen").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
        udn.Active = false;
        udn.transform.position = new(-3000, 0);
        udn.gameObject.AddComponent<SetScaleMQ>();
        return udn;
    }

    public static UnityDisplayNode GetMcQueen_Rev1(Transform transform) {
        var udn = Object.Instantiate(Assets.LoadAsset("Rev1").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
        udn.Active = false;
        udn.transform.position = new(-3000, 0);
        udn.gameObject.AddComponent<SetScaleMQ>();
        return udn;
    }

    public static UnityDisplayNode GetMcQueen_Rev2(Transform transform) {
        var udn = Object.Instantiate(Assets.LoadAsset("Rev2").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
        udn.Active = false;
        udn.transform.position = new(-3000, 0);
        udn.gameObject.AddComponent<SetScaleMQ>();
        return udn;
    }

    public static UnityDisplayNode GetMcQueen_Large(Transform transform) {
        var udn = Object.Instantiate(Assets.LoadAsset("McQueen").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
        udn.Active = false;
        udn.transform.position = new(-3000, 0);
        udn.gameObject.AddComponent<SetScaleMQL>();
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
            if (reference != null) {
                if (reference.guidRef.Equals("McQueen_vicroy")) {
                    try {
                        var b = Assets.LoadAsset("vicroy");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                }
                if (reference.guidRef.Equals("McQueen_newandimproved")) {
                    try {
                        var b = Assets.LoadAsset("newandimproved");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                }
                if (reference.guidRef.Equals("McQueen_iamspeed")) {
                    try {
                        var b = Assets.LoadAsset("iamspeed");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                }
                if (reference.guidRef.Equals("McQueen_holydriver")) {
                    try {
                        var b = Assets.LoadAsset("holydriver");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                }
                if (reference.guidRef.Equals("McQueen_hiryan")) {
                    try {
                        var b = Assets.LoadAsset("hiryan");
                        if (b != null) {
                            var text = b.Cast<Texture2D>();
                            image.canvasRenderer.SetTexture(text);
                            image.sprite = Sprite.Create(text, new(0, 0, text.width, text.height), new());
                        }
                    } catch { }
                }
            }
        }
    }
}
