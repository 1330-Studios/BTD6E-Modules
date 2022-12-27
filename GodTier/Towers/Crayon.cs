using GodlyTowers.Utils;

namespace GodlyTowers.Towers;
internal static class Crayon {
    public static string name = "Crayon";

    public static UpgradeModel[] GetUpgrades() {
        return new UpgradeModel[] {
            new UpgradeModel("Wild Watermelon", 2000, 0, "Crayon_tcred".GetSpriteReference(), 0, 0, 0, "", "Wild Watermelon"),
            new UpgradeModel("Laser Lemon", 3800, 0, "Crayon_tcyellow".GetSpriteReference(), 0, 1, 0, "", "Laser Lemon"),
            new UpgradeModel("Electric Lime", 8600, 0, "Crayon_tcgreen".GetSpriteReference(), 0, 2, 0, "", "Electric Lime"),
            new UpgradeModel("Royal Purple", 27500, 0, "Crayon_tcpurple".GetSpriteReference(), 0, 3, 0, "", "Royal Purple"),
            new UpgradeModel("Rainbow color, idk", 50000, 0, "Crayon_roygbiv".GetSpriteReference(), 0, 4, 0, "", "Rainbow color, idk")
        };
    }

    public static (TowerModel, ShopTowerDetailsModel, TowerModel[], UpgradeModel[]) GetTower(GameModel gameModel) {
        var godzillaDetails = gameModel.towerSet[0].Clone().Cast<ShopTowerDetailsModel>();
        godzillaDetails.towerId = name;
        godzillaDetails.towerIndex = GlobalTowerIndex.Index;

        LocalizationManager.Instance.textTable["Wild Watermelon Description"] = "Back in the year 1937, it cost 1 dollar for a round trip across the Golden Gate Bridge. That's almost $21 today!";
        LocalizationManager.Instance.textTable["Laser Lemon Description"] = "Ford Truck Month is going on all of March 2021! As long as you purchase your new Ford truck before March 31st 2021, you will be able to take advantage of the low interest rate specials and other time sensitive discounts.";
        LocalizationManager.Instance.textTable["Electric Lime Description"] = "Did you know that Spain has 35 million orange trees? They have that many and I can't even grow one.";
        LocalizationManager.Instance.textTable["Royal Purple Description"] = "I would've called this royal blue but that wouldn't make sense since this is purple.";
        LocalizationManager.Instance.textTable["Rainbow color, idk Description"] = "It's like a double rainbow all the way! I'm writing this at 1:28 PM CST 7/14/2022. Rapture, be pure. Take a tour through the sewer. Don't strain your brain, paint a train. You'll be singin' in the rain. Said don't stop to punk rock.";

        return (GetT0(gameModel), godzillaDetails, new[] { GetT0(gameModel), GetT1(gameModel), GetT2(gameModel), GetT3(gameModel), GetT4(gameModel), GetT5(gameModel) }, GetUpgrades());
    }

    public static TowerModel GetT0(GameModel gameModel) {
        var godzilla = gameModel.towers.First(a => a.baseId.Equals("HeliPilot")).Clone().Cast<TowerModel>();

        godzilla.name = name;
        godzilla.baseId = name;
        godzilla.SetDisplay("CrayonBox");
        godzilla.SetIcons("GodlyTowers.Resources.crayonPor.png");
        godzilla.towerSet = "Magic";
        godzilla.emoteSpriteLarge = new("None");
        godzilla.radius = 0;
        godzilla.cost = 750;
        godzilla.range = 35;
        godzilla.towerSize = TowerModel.TowerSize.medium;
        godzilla.footprint.ignoresPlacementCheck = true;
        godzilla.areaTypes = new(4);
        godzilla.areaTypes[0] = AreaType.ice;
        godzilla.areaTypes[1] = AreaType.land;
        godzilla.areaTypes[2] = AreaType.track;
        godzilla.areaTypes[3] = AreaType.water;
        godzilla.cachedThrowMarkerHeight = 10;
        godzilla.upgrades = new UpgradePathModel[] { new("Wild Watermelon", name + "-100") };

        for (int i = 0; i < godzilla.behaviors.Length; i++) {
            if (godzilla.behaviors[i].Is<AirUnitModel>(out var aum)) {
                aum.display = new() { guidRef= "BlueCrayon" };
                var hmm = aum.behaviors[0].Cast<HeliMovementModel>();
                hmm.tiltAngle = 0;
                hmm.brakeForce *= MathF.Pow(50, 3);
                hmm.slowdownRadiusMin = 2;
                hmm.slowdownRadiusMax = 15;
                hmm.strafeDistance = 5000;
                hmm.strafeDistanceSquared = MathF.Pow(5000, 2);
            }
            if (godzilla.behaviors[i].Is<AttackModel>(out var am)) {
                am.behaviors = new Model[] { new FollowTouchSettingModel("FTSM_", true, false) };
                am.fireWithoutTarget = true;
                am.weapons[0].emission = new EmissionWithOffsetsModel("EWOM_", new[] {
                    new ThrowMarkerOffsetModel("Middle", 0, 4, 0, 0) }, 1, false, null, 0);
                am.weapons[0].Rate = 0.025f;
                am.weapons[0].projectile.display = new() { guidRef = "CrayonBlueTrail" };
                am.weapons[0].projectile.radius /= 2;
                am.weapons[0].projectile.scale /= 3;
                am.weapons[0].projectile.scale *= 2;
                for (int j = 0; j < am.weapons[0].projectile.behaviors.Count; j++) {
                    if (am.weapons[0].projectile.behaviors[j].Is<TravelStraitModel>(out var tsm)) {
                        tsm.Speed = 0.05f;
                        tsm.Lifespan = 5;
                    }
                    if (am.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var dm)) {
                        dm.damage = .1f;
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
        godzilla.upgrades = new[] { new UpgradePathModel("Laser Lemon", name + "-200") };

        var creature = gameModel.towers.First(a => a.name.Equals("MortarMonkey-002")).behaviors.First(a => a.Is<AttackModel>()).Cast<AttackModel>().weapons[0].projectile.behaviors.First(a => a.Is<CreateProjectileOnExhaustFractionModel>()).CloneCast<CreateProjectileOnExhaustFractionModel>();

        foreach (var b in creature.projectile.behaviors) {
            if (b.Is<DamageModel>(out var dm)) {
                dm.damage = 1;
                dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
            }
            if (b.Is<AddBehaviorToBloonModel>(out var abtbm)) {
                abtbm.behaviors[0].Cast<DamageOverTimeModel>().damage = 0.1f;
            }
        }

        var create = new CreateProjectileOnContactModel("CreateProjectileOnContactModel", creature.projectile, creature.emission, false, false, false);

        for (int i = 0; i < godzilla.behaviors.Length; i++) {
            if (godzilla.behaviors[i].Is<AirUnitModel>(out var aum)) {
                aum.display = new() { guidRef = "RedCrayon" };
            }
            if (godzilla.behaviors[i].Is<AttackModel>(out var am)) {
                am.weapons[0].Rate = 0.02f;
                am.weapons[0].projectile.display = new() { guidRef = "CrayonRedTrail" };
                for (int j = 0; j < am.weapons[0].projectile.behaviors.Count; j++) {
                    if (am.weapons[0].projectile.behaviors[j].Is<TravelStraitModel>(out var tsm)) {
                        tsm.Lifespan *= 3;
                    }
                    if (am.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var dm)) {
                        dm.damage = 0.05f;
                        dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                    }
                }
                am.weapons[0].projectile.behaviors = am.weapons[0].projectile.behaviors.Add(create);
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
        godzilla.upgrades = new[] { new UpgradePathModel("Electric Lime", name + "-300") };

        var creature = gameModel.towers.First(a => a.name.Equals("BombShooter")).behaviors.First(a => a.Is<AttackModel>()).Cast<AttackModel>().weapons[0].projectile.behaviors.First(a => a.Is<CreateProjectileOnContactModel>());
        var creature2 = gameModel.towers.First(a => a.name.Equals("BombShooter")).behaviors.First(a => a.Is<AttackModel>()).Cast<AttackModel>().weapons[0].projectile.behaviors.First(a => a.Is<CreateEffectOnContactModel>());

        for (int i = 0; i < godzilla.behaviors.Length; i++) {
            if (godzilla.behaviors[i].Is<AirUnitModel>(out var aum)) {
                aum.display = new() { guidRef= "YellowCrayon" };
            }
            if (godzilla.behaviors[i].Is<AttackModel>(out var am)) {
                am.weapons[0].Rate = 0.0175f;
                am.weapons[0].projectile.display = new() { guidRef= "CrayonYellowTrail" };
                for (int j = 0; j < am.weapons[0].projectile.behaviors.Count; j++) {
                    if (am.weapons[0].projectile.behaviors[j].Is<TravelStraitModel>(out var tsm)) {
                        tsm.Lifespan *= 3;
                    }
                    if (am.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var dm)) {
                        dm.damage = 0.1f;
                        dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                    }
                }
                am.weapons[0].projectile.behaviors = am.weapons[0].projectile.behaviors.Add(creature, creature2);
            }
        }

        return godzilla;
    }

    public static TowerModel GetT3(GameModel gameModel) {
        var godzilla = GetT2(gameModel);

        godzilla.name = name + "-300";
        godzilla.baseId = name;
        godzilla.tier = 3;
        godzilla.tiers = new int[] { 3, 0, 0 };
        godzilla.upgrades = new[] { new UpgradePathModel("Royal Purple", name + "-400") };

        for (int i = 0; i < godzilla.behaviors.Length; i++) {
            if (godzilla.behaviors[i].Is<AirUnitModel>(out var aum)) {
                aum.display = new() { guidRef = "GreenCrayon" };
            }
            if (godzilla.behaviors[i].Is<AttackModel>(out var am)) {
                am.weapons[0].emission = new EmissionWithOffsetsModel("EWOM_", new[] {
                    new ThrowMarkerOffsetModel("Middle", 0, 4, 0, 0),
                    new ThrowMarkerOffsetModel("Left", 4.5f, 4, 0, 0),
                    new ThrowMarkerOffsetModel("Right", -4.5f, 4, 0, 0)
                }, 3, false, null, 0);
                am.weapons[0].Rate = 0.0125f;
                am.weapons[0].projectile.display = new() { guidRef = "CrayonGreenTrail" };
                for (int j = 0; j < am.weapons[0].projectile.behaviors.Count; j++) {
                    if (am.weapons[0].projectile.behaviors[j].Is<TravelStraitModel>(out var tsm)) {
                        tsm.Lifespan /= 3;
                    }
                    if (am.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var dm)) {
                        dm.damage = 0.15f;
                        dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                    }
                }
            }
        }

        godzilla.behaviors = godzilla.behaviors.Add(new OverrideCamoDetectionModel("OCDM_", true));

        return godzilla;
    }

    public static TowerModel GetT4(GameModel gameModel) {
        var godzilla = GetT3(gameModel);

        godzilla.name = name + "-400";
        godzilla.baseId = name;
        godzilla.tier = 4;
        godzilla.tiers = new int[] { 4, 0, 0 };
        godzilla.upgrades = new[] { new UpgradePathModel("Rainbow color, idk", name + "-500") };

        for (int i = 0; i < godzilla.behaviors.Length; i++) {
            if (godzilla.behaviors[i].Is<AirUnitModel>(out var aum)) {
                aum.display = new() { guidRef= "PurpleCrayon" };
            }
            if (godzilla.behaviors[i].Is<AttackModel>(out var am)) {
                am.weapons[0].emission = new EmissionWithOffsetsModel("EWOM_", new[] {
                    new ThrowMarkerOffsetModel("Middle", 0, 4, 0, 0),
                    new ThrowMarkerOffsetModel("Left", 4.5f, 4, 0, 0),
                    new ThrowMarkerOffsetModel("Right", -4.5f, 4, 0, 0),
                    new ThrowMarkerOffsetModel("Top", 0, 4, 4.5f, 0),
                    new ThrowMarkerOffsetModel("Bottom", 0, 4, -4.5f, 0)
                }, 5, false, null, 0);
                am.weapons[0].Rate = 0.0075f;
                am.weapons[0].projectile.display = new() { guidRef = "CrayonPurpleTrail" };
                for (int j = 0; j < am.weapons[0].projectile.behaviors.Count; j++) {
                    if (am.weapons[0].projectile.behaviors[j].Is<TravelStraitModel>(out var tsm)) {
                        tsm.Lifespan /= 3;
                    }
                    if (am.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var dm)) {
                        dm.damage = 0.25f;
                        dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                    }
                    if (am.weapons[0].projectile.behaviors[j].Is<CreateEffectOnContactModel>(out var effect)) {
                        effect.effectModel.assetId = new() { guidRef= "6d84b13b7622d2744b8e8369565bc058" };
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
        godzilla.upgrades = Array.Empty<UpgradePathModel>();

        for (int i = 0; i < godzilla.behaviors.Length; i++) {
            if (godzilla.behaviors[i].Is<AirUnitModel>(out var aum)) {
                aum.display = new() { guidRef= "RainbowCrayon" };
            }
            if (godzilla.behaviors[i].Is<AttackModel>(out var am)) {
                am.weapons[0].emission = new EmissionWithOffsetsModel("EWOM_", new[] {
                    new ThrowMarkerOffsetModel("Middle", 0, 4, 0, 0),
                    new ThrowMarkerOffsetModel("Left", 4.5f, 4, 0, 0),
                    new ThrowMarkerOffsetModel("Right", -4.5f, 4, 0, 0),
                    new ThrowMarkerOffsetModel("Top", 0, 4, 4.5f, 0),
                    new ThrowMarkerOffsetModel("Bottom", 0, 4, -4.5f, 0),
                    new ThrowMarkerOffsetModel("LTop", 4.5f, 4, 4.5f, 0),
                    new ThrowMarkerOffsetModel("LBottom", 4.5f, 4, -4.5f, 0),
                    new ThrowMarkerOffsetModel("RTop", -4.5f, 4, 4.5f, 0),
                    new ThrowMarkerOffsetModel("RBottom", -4.5f, 4, -4.5f, 0)
                }, 9, false, null, 0);
                am.weapons[0].Rate = 0.0001f;
                am.weapons[0].projectile.display = new() { guidRef= "CrayonRainbowTrail" };
                for (int j = 0; j < am.weapons[0].projectile.behaviors.Count; j++) {
                    if (am.weapons[0].projectile.behaviors[j].Is<TravelStraitModel>(out var tsm)) {
                        tsm.Lifespan /= 5f;
                    }
                    if (am.weapons[0].projectile.behaviors[j].Is<DamageModel>(out var dm)) {
                        dm.damage = 5f;
                        dm.immuneBloonProperties = dm.immuneBloonPropertiesOriginal = BloonProperties.None;
                    }
                    if (am.weapons[0].projectile.behaviors[j].Is<CreateEffectOnContactModel>(out var effect)) {
                        effect.effectModel.assetId = new() { guidRef = "6d84b13b7622d2744b8e8369565bc058" };
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
            if (!protos.ContainsKey(objectId) && objectId.Contains("CrayonBox")) {
                GameObject obj = Object.Instantiate(new GameObject(objectId + "(Clone)"), __instance.PrototypeRoot);
                var sr = obj.AddComponent<SpriteRenderer>();
                var tx = "GodlyTowers.Resources.box.png".GetEmbeddedResource().ToTexture();
                sr.sprite = Sprite.Create(tx, new(0, 0, tx.width, tx.height), new(0.5f, 0.5f), 40.5f, 0, SpriteMeshType.Tight);
                var udn = obj.AddComponent<UnityDisplayNode>();
                udn.transform.position = new(-3000, 10);

                udn.gameObject.AddComponent<MoveUp>();

                onComplete.Invoke(udn);
                protos.Add(objectId, udn);

                return false;
            }

            if (!protos.ContainsKey(objectId) && objectId.EndsWith("Crayon")) {
                var col = objectId.Replace("Crayon", "");
                var udn = GetCrayon(__instance.PrototypeRoot, col);
                udn.name = $"{col} Crayon";
                udn.RecalculateGenericRenderers();
                udn.isSprite = false;
                onComplete.Invoke(udn);
                protos.Add(objectId, udn);
                return false;
            }

            if (!protos.ContainsKey(objectId) && objectId.Contains("Crayon") && objectId.Contains("Trail")) {
                UnityDisplayNode udn = null;
                __instance.FindAndSetupPrototypeAsync("bdbeaa256e6c63b45829535831843376",
                    new Action<UnityDisplayNode>(oudn => {
                        var nudn = Object.Instantiate(oudn, __instance.PrototypeRoot);
                        nudn.name = objectId + "(Clone)";
                        nudn.isSprite = true;
                        nudn.RecalculateGenericRenderers();
                        var col = objectId.Replace("Crayon", "").Replace("Trail", "").ToLower();
                        for (var i = 0; i < nudn.genericRenderers.Length; i++) {
                            if (nudn.genericRenderers[i].GetIl2CppType() == Il2CppType.Of<SpriteRenderer>()) {
                                var smr = nudn.genericRenderers[i].Cast<SpriteRenderer>();
                                var text = Assets.LoadAsset(col).Cast<Texture2D>();
                                smr.sprite = Sprite.Create(text, new(0, 0, 32, 32), new(0.5f, 0.5f), 10.8f);
                                nudn.genericRenderers[i] = smr;
                            }
                        }

                        udn = nudn;
                        protos[objectId] = udn;
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

    public static UnityDisplayNode GetCrayon(Transform transform, string color) {
        var udn = Object.Instantiate(Assets.LoadAsset($"{color}Crayon").Cast<GameObject>(), transform).AddComponent<UnityDisplayNode>();
        udn.Active = false;
        udn.transform.position = new(-3000, 0);
        udn.gameObject.AddComponent<SetScaleCrayon>();
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
            if (reference != null && reference.guidRef.StartsWith("Crayon_")) {
                try {
                    var b = Assets.LoadAsset(reference.guidRef.Replace("Crayon_", ""));
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
