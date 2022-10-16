using Assets.Scripts;
using Assets.Scripts.Simulation.Behaviors;
using Assets.Scripts.Unity;

using UnityEngine.AddressableAssets;
using UnityEngine.U2D;

namespace AdditionalTiers.Tasks {
    public sealed class Assets {
        [HideFromIl2Cpp]
        public static Dictionary<string, Il2CppSystem.Type> Types { get; set; } = new() {
            { "WhitesnakeProj", Il2CppType.Of<AnimatedEnergyTexture>() },
            { "WhitesnakePheonixProj", Il2CppType.Of<AnimatedFlameTexture>() },
            { "WhitesnakeDarkPheonixProj", Il2CppType.Of<AnimatedDarkFlameTexture>() }
        };
        [HideFromIl2Cpp]
        public static Dictionary<string, Action<UnityDisplayNode>> Actions { get; set; } = new() {
            {
                "GlaiveDominusSilver",
                udn => {
                    var light = udn.gameObject.AddComponent<Light>();
                    light.type = LightType.Directional;
                    light.renderMode = LightRenderMode.ForceVertex;

                    var assets = Particles;
                    var _obj = assets[1].Cast<GameObject>();
                    var obj = Object.Instantiate(_obj, udn.transform);
                    obj.SetActive(true);
                    var ps = obj.transform.GetComponentInChildren<ParticleSystem>();
                    ps.emissionRate = 15;
                    ps.transform.localScale = new(10, 10, 10);
                }
            },
            {
                "VitaminC",
                udn => {
                    var assets = ParticleSystems;
                    var _obj = assets[0].Cast<GameObject>();
                    var obj = Object.Instantiate(_obj, udn.transform);
                    obj.SetActive(true);
                    var ps = obj.transform.GetComponent<ParticleSystem>();
                    ps.transform.localScale = new(15, 15, 15);
                }
            },
            {
                "APMGold2",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.02f);
                    }
                }
            },
            {
                "MrRoboto",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.02f);
                    }
                    var assets = Particles;
                    var _obj = assets[0].Cast<GameObject>();
                    var obj = Object.Instantiate(_obj, udn.transform);
                    obj.SetActive(true);
                    var ps = obj.transform.GetComponentInChildren<ParticleSystem>();
                    ps.transform.localScale = new(10, 10, 10);
                }
            },
            {
                "MrRobotoAbility",
                udn => {
                    foreach (var ps in udn.transform.GetComponentsInChildren<ParticleSystem>())
                        ps.startColor = new Color(1f, 0.8f, 0);
                }
            },
            {
                "GlaiveDominusSilverOrbit",
                udn => {
                    var ps = udn.transform.GetComponentInChildren<ParticleSystem>();
                    ps.startColor = new Color(1, 0.25f, 0);
                    udn.gameObject.AddComponent<FastRotation>();
                }
            },
            {
                "GlaiveDominusSilverOrbit2",
                udn => {
                    var ps = udn.transform.GetComponentInChildren<ParticleSystem>();
                    ps.startColor = new Color(1, 1, 1);
                    for (int i = 0; i < 2; i++)
                        udn.gameObject.AddComponent<FastRotation>();
                }
            },
            {
                "GlaiveDominusSilverOrbit3",
                udn => {
                    var ps = udn.transform.GetComponentInChildren<ParticleSystem>();
                    ps.startColor = new Color(0.3f, 1, 0.3f);
                    for (int i = 0; i < 5; i++)
                        udn.gameObject.AddComponent<FastRotation>();
                }
            },
            {
                "GlaiveDominusSilverAbility",
                udn => {
                    var ps = udn.transform.GetComponentInChildren<ParticleSystem>();
                    ps.startColor = new Color(1f, 0, 0);
                }
            },
            {
                "VitaminCTotemParticles",
                udn => {
                    var ps = udn.transform.GetComponentsInChildren<ParticleSystem>();

                    for (int i = 0; i < ps.Length; i++)
                        ps[i].startColor = new Color(0, 1, 0);
                }
            },
            {
                "BTD4SunGod",
                udn => {
                    udn.transform.GetComponentInChildren<ParticleSystem>().gameObject.active = false;
                    udn.gameObject.AddComponent<SetHeight1>();
                }
            },
            {
                "BTD4SunGodV",
                udn => {
                    udn.transform.GetComponentInChildren<ParticleSystem>().gameObject.active = false;
                    udn.gameObject.AddComponent<SetHeight1>();
                }
            },
            {
                "DaftPunkProjectile",
                udn => {
                    var ps = udn.transform.GetComponentInChildren<ParticleSystem>();
                    ps.startColor = new Color(1, 0.25f, 0.25f);
                    var tr = udn.transform.GetComponentInChildren<TrailRenderer>();
                    tr.startColor = new Color(1f, 0.25f, 0.25f);
                    tr.endColor = new Color(0.4f, 0.1f, 0.1f);
                }
            },
            {
                "DaftPunkTurboProjectile",
                udn => {
                    var ps = udn.transform.GetComponentInChildren<ParticleSystem>();
                    ps.startColor = new Color(1, 0.25f, 0.25f);
                    var tr = udn.transform.GetComponentInChildren<TrailRenderer>();
                    tr.startColor = new Color(1f, 0.25f, 0.25f);
                    tr.endColor = new Color(0.4f, 0.1f, 0.1f);
                }
            },
            {
                "DaftPunkOrbit",
                udn => {
                    var ps = udn.transform.GetComponentInChildren<ParticleSystem>();
                    ps.startColor = new Color(1f, .1f, .1f);
                    for (int i = 0; i < 5; i++)
                        udn.gameObject.AddComponent<FastRotation>();
                }
            },
            {
                "FMTTM",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 1f);
                        udn.genericRenderers[i].material.SetColor("_OutlineColor", new Color32(177, 135, 255, 255));
                    }
                }
            },
            {
                "FMTTM2",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 1f);
                        udn.genericRenderers[i].material.SetColor("_OutlineColor", new Color32(177, 135, 255, 255));
                    }
                }
            },
            {
                "FMTTM3",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 1f);
                        udn.genericRenderers[i].material.SetColor("_OutlineColor", new Color32(177, 135, 255, 255));
                    }
                }
            },
            {
                "BurningDownTheHouse",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.05f);
                        udn.genericRenderers[i].material.SetColor("_OutlineColor", new Color32(0, 254, 254, 255));
                    }
                }
            },
            {
                "SheerHeartAttack",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.05f);
                        udn.genericRenderers[i].material.SetColor("_OutlineColor", new Color32(206, 141, 0, 255));
                    }
                }
            },
            {
                "AOBTD",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.02f);
                        udn.genericRenderers[i].material.SetColor("_OutlineColor", new Color32(162, 0, 255, 255));
                    }
                }
            },
            {
                "HeyYa",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++)
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 1);
                }
            },
            {
                "SpaceTruckin",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++)
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.02f);
                }
            },
            {
                "PlanetWaves",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++)
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.05f);
                }
            },
            {
                "GreenDay",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++)
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.05f);
                }
            },
            {
                "Dynamite",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++)
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.05f);
                }
            },
            {
                "TooCold",
                udn => {
                    var main = T6SpecificAssets.LoadAsset("TooColdMain");
                    var sub = T6SpecificAssets.LoadAsset("TooColdSub");
                    var @static = T6SpecificAssets.LoadAsset("TooColdStatic");

                    {
                        var obj = Object.Instantiate(main.Cast<GameObject>(), udn.transform);
                        obj.SetActive(true);
                        var ps = obj.transform.GetComponentInChildren<ParticleSystem>();
                        ps.transform.localScale = new(10, 0, 10);

                        var pos = ps.transform.position;

                        pos.y += 2.1f;

                        ps.transform.position = pos;
                    }
                    {
                        var obj = Object.Instantiate(sub.Cast<GameObject>(), udn.transform);
                        obj.SetActive(true);
                        var ps = obj.transform.GetComponentInChildren<ParticleSystem>();
                        ps.transform.localScale = new(10, 0, 10);

                        var pos = ps.transform.position;

                        pos.y += 2.2f;

                        ps.transform.position = pos;
                    }
                    {
                        var obj = Object.Instantiate(@static.Cast<GameObject>(), udn.transform);
                        obj.SetActive(true);
                        var ps = obj.transform.GetComponentInChildren<ParticleSystem>();
                        ps.transform.localScale = new(10, 0, 10);

                        var pos = ps.transform.position;

                        pos.y += 2.0f;

                        ps.transform.position = pos;
                    }

                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.005f);
                        udn.genericRenderers[i].material.SetColor("_OutlineColor", SpecialColors["TooCold"]);
                    }
                }
            },
            {
                "KillerQueen",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.5f);
                    }
                }
            },
            {
                "CrazyDiamond",
                udn => {
                    var main = T6SpecificAssets.LoadAsset("DSparkle");

                    var obj = Object.Instantiate(main.Cast<GameObject>(), udn.transform);
                    obj.SetActive(true);
                    var ps = obj.transform.GetComponentInChildren<ParticleSystem>();
                    ps.transform.localScale = new(10, 10, 10);
                }
            },
            {
                "LittleTalks",
                udn => {
                    for (int i = 0; i < udn.genericRenderers.Length; i++) {
                        udn.genericRenderers[i].material.SetFloat("_OutlineWidth", 0.025f);
                    }
                }
            },
            {
                "AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_Proj2.png",
                udn => {
                    var tr = udn.transform.GetComponentInChildren<TrailRenderer>();
                    tr.startColor = new Color(0.25f, 0.25f, 0.25f);
                    tr.endColor = new Color(0.1f, 0.1f, 0.1f);
                }
            },
            {
                "AdditionalTiers.Resources.V2.GoldenExperience.GoldenExperience_UProj2.png",
                udn => {
                    var tr = udn.transform.GetComponentInChildren<TrailRenderer>();
                    tr.startColor = new Color(0.25f, 0.25f, 0.25f);
                    tr.endColor = new Color(0.1f, 0.1f, 0.1f);
                }
            },
            {
                "GoldenExperienceAbility",
                udn => {
                    foreach (var ps in udn.transform.GetComponentsInChildren<ParticleSystem>()) {
                        ps.startColor = new Color(1f, 0.8f, 0);
                    }
                }
            },
            {
                "GoldenExperiencePlaceEffect",
                udn => {
                    foreach (var ps in udn.transform.GetComponentsInChildren<ParticleSystem>()) {
                        ps.loop = true;
                        ps.scalingMode = ParticleSystemScalingMode.Local;
                        ps.transform.localScale /= 2;
                        ps.startColor = new Color(1f, 0, 0.8f);
                    }
                }
            },
            {
                "AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T6.png",
                udn => {
                    foreach (var ps in udn.transform.GetComponentsInChildren<ParticleSystem>()) {
                        ps.startColor = new Color(0, 0, 0);
                    }
                }
            },
            {
                "AdditionalTiers.Resources.V2.KingCrimson.KingCrimson_T7.png",
                udn => {
                    foreach (var ps in udn.transform.GetComponentsInChildren<ParticleSystem>()) {
                        ps.startColor = new Color(0, 0, 0);
                    }
                }
            }
        };
        [HideFromIl2Cpp]
        public static Dictionary<string, Func<string, Sprite>> SpriteCreation { get; set; } = new() {
            { "ScaryMonstersProj", objectId => SpriteBuilder.createProjectile(CacheBuilder.Get(objectId), 10.8f) },
            { "GlaiveDominusSilverOrbit2", objectId => SpriteBuilder.createProjectile(CacheBuilder.Get(objectId), 10.8f) },
            { "VitaminCBlast", objectId => SpriteBuilder.createProjectile(CacheBuilder.Get(objectId), 7.6f) },
            { "BTD4SunGod", objectId => SpriteBuilder.createProjectile(CacheBuilder.Get(objectId), 43.2f, pivoty: 0.7f) },
            { "BTD4SunGodV", objectId => SpriteBuilder.createProjectile(CacheBuilder.Get(objectId), 43.2f, pivoty: 0.7f) },
            { "BlackYellowMissile", objectId => SpriteBuilder.createProjectile(CacheBuilder.Get(objectId), 10.8f) },
            { "BlackYellowBullet", objectId => SpriteBuilder.createProjectile(CacheBuilder.Get(objectId), 10.8f) },
            { "LittleTalksGS", objectId => SpriteBuilder.createProjectile(CacheBuilder.Get(objectId), 21.6f) },
            { "LittleTalksS", objectId => SpriteBuilder.createProjectile(CacheBuilder.Get(objectId), 10.8f) },
            { "LittleTalksPS", objectId => SpriteBuilder.createProjectile(CacheBuilder.Get(objectId), 10.8f) },
            { "LittleTalksPB", objectId => SpriteBuilder.createProjectile(CacheBuilder.Get(objectId), 10.8f) }
        };


        [HideFromIl2Cpp]
        public static Dictionary<string, Func<Object, bool>> SpecialShaderIndicies { get; set; } = new() {
            { "GlaiveDominusSilver", obj => obj.name.StartsWith("Unlit/Metallic") }
        };

        [HideFromIl2Cpp]
        public static Dictionary<string, Color> SpecialColors { get; set; } = new() {
            { "FMTTM", new Color32(177, 135, 255, 255) },
            { "FMTTM2", new Color32(177, 135, 255, 255) },
            { "FMTTM3", new Color32(177, 135, 255, 255) },
            { "BurningDownTheHouse", new Color32(0, 254, 254, 255) },
            { "SheerHeartAttack", new Color32(206, 141, 0, 255) },
            { "AOBTD", new Color32(162, 0, 255, 255) },
            { "SpaceTruckin", new Color32(255, 25, 25, 255) },
            { "TooCold", new Color32(55, 182, 237, 255) }
        };

        public static Color GetResetColor(DisplayBehavior beh) {
            if (beh?.displayModel?.display.guidRef != null && SpecialColors.ContainsKey(beh.displayModel.display.guidRef))
                return SpecialColors[beh.displayModel.display.guidRef];
            return Color.black;
        }

        private static AssetBundle _shader;

        public static AssetBundle ShaderBundle {
            get {
                if (_shader == null)
                    _shader = AssetBundle.LoadFromMemory(Images.shader);
                return _shader;
            }
        }

        private static AssetBundle _particle;

        public static AssetBundle ParticleBundle {
            get {
                if (_particle == null)
                    _particle = AssetBundle.LoadFromMemory(Images.particle);
                return _particle;
            }
        }

        private static AssetBundle _particlesystem;

        public static AssetBundle ParticleSystemBundle {
            get {
                if (_particlesystem == null)
                    _particlesystem = AssetBundle.LoadFromMemory(Images.particlesystem);
                return _particlesystem;
            }
        }

        private static AssetBundle _t6specificassets;

        public static AssetBundle T6SpecificAssets {
            get {
                if (_t6specificassets == null)
                    _t6specificassets = AssetBundle.LoadFromMemory(Images.t6);
                return _t6specificassets;
            }
        }

        private static Object[] _particles;
        public static Object[] Particles {
            get {
                if (_particles == null)
                    _particles = ParticleBundle.LoadAllAssets();
                return _particles;
            }
        }

        private static Object[] _particlesystems;
        public static Object[] ParticleSystems {
            get {
                if (_particlesystems == null)
                    _particlesystems = ParticleSystemBundle.LoadAllAssets();
                return _particlesystems;
            }
        }

        private static Object[] _shaderAssets;
        public static Object[] ShaderAssets {
            get {
                if (_shaderAssets == null)
                    _shaderAssets = ShaderBundle.LoadAllAssets();
                return _shaderAssets;
            }
        }

        private static AssetBundle richard = AssetBundle.LoadFromMemory("AdditionalTiers.Resources.Ninja.richard.bundle".GetEmbeddedResource());
        private static AssetBundle by = AssetBundle.LoadFromMemory("AdditionalTiers.Resources.B_Y.by.bundle".GetEmbeddedResource());

        [HarmonyPatch(typeof(Factory.__c__DisplayClass21_0), nameof(Factory.__c__DisplayClass21_0._CreateAsync_b__0))]
        public sealed class DisplayFactory {
            public static bool hasBeenBuilt;
            public static List<CombinationAssets> allCombinationAssetsKnown = new();
            public static Dictionary<string, UnityDisplayNode> cachedSRs = new();
            public static Dictionary<string, UnityDisplayNode> cachedRenderers = new(); // Too expensive to recalculate every time

            private static List<AssetInfo> allAssetsKnown = new();

            [HarmonyPrefix]
            private static bool Prefix(Factory.__c__DisplayClass21_0 __instance, ref UnityDisplayNode prototype) {
                var factory = __instance.__4__this;
                var prefabReference = __instance.objectId;
                var guid = prefabReference.guidRef;
                var onComplete = __instance.onComplete;

                if (cachedRenderers.ContainsKey(prefabReference.guidRef)) {
                    onComplete.Invoke(cachedRenderers[prefabReference.guidRef]);
                    return false;
                }
                if (cachedSRs.ContainsKey(prefabReference.guidRef)) {
                    onComplete.Invoke(cachedSRs[prefabReference.guidRef]);
                    return false;
                }

                Transform transform = Game.instance.prototypeObjects.transform;

                var resourceManager = Addressables.Instance.ResourceManager;

                if (guid.Contains(".Ninja.")) {

                    var nO = richard.LoadAsset(guid.Contains("Evil") ? "EVILBlev" : "RichardTylerNinjaFortniteBlevins").Cast<GameObject>();

                    nO.name = guid;

                    var ninja = Object.Instantiate(nO, factory.DisplayRoot).AddComponent<UnityDisplayNode>();
                    ninja.Active = false;
                    ninja.name = guid + " (Clone)";
                    ninja.gameObject.AddComponent<SetScaleN>();
                    ninja.RecalculateGenericRenderers();

                    factory.prototypeHandles[prefabReference] = resourceManager.CreateCompletedOperation(ninja.gameObject, "");

                    Vector3 nvector = new(Factory.kOffscreenPosition.x, 0f, 0f);
                    Quaternion nidentity = Quaternion.identity;
                    GameObject ngameObject2 = Object.Instantiate(ninja.gameObject, nvector, nidentity, factory.DisplayRoot);
                    ngameObject2.SetActive(true);
                    UnityDisplayNode ncomponent = ngameObject2.GetComponent<UnityDisplayNode>();
                    ncomponent.Create();
                    ncomponent.cloneOf = prefabReference;
                    factory.active.Add(ncomponent);
                    onComplete.Invoke(ncomponent);

                    return false;
                }

                if (guid.Contains(".B_Y")) {
                    var nO = by.LoadAsset("BAY").Cast<GameObject>();

                    nO.name = guid;

                    var bay = Object.Instantiate(nO, factory.DisplayRoot).AddComponent<UnityDisplayNode>();
                    bay.Active = false;
                    bay.name = guid + " (Clone)";
                    bay.gameObject.AddComponent<SetScaleBY>();
                    bay.RecalculateGenericRenderers();

                    factory.prototypeHandles[prefabReference] = resourceManager.CreateCompletedOperation(bay.gameObject, "");

                    Vector3 nvector = new(Factory.kOffscreenPosition.x, 0f, 0f);
                    Quaternion nidentity = Quaternion.identity;
                    GameObject ngameObject2 = Object.Instantiate(bay.gameObject, nvector, nidentity, factory.DisplayRoot);
                    ngameObject2.SetActive(true);
                    UnityDisplayNode ncomponent = ngameObject2.GetComponent<UnityDisplayNode>();
                    ncomponent.Create();
                    ncomponent.cloneOf = prefabReference;
                    factory.active.Add(ncomponent);
                    onComplete.Invoke(ncomponent);

                    return false;
                }

                if (prototype == null && allAssetsKnown.Any(a => a.CustomAssetName == guid)) {
                    var curAsset = allAssetsKnown.First(a => a.CustomAssetName == guid);
                    var baseRef = new PrefabReference() { guidRef = curAsset.BTDAssetName };

                    factory.FindAndSetupPrototypeAsync(baseRef, new Action<UnityDisplayNode>(node => {
                        GameObject gameObject = Object.Instantiate(node.gameObject, transform);
                        gameObject.name = curAsset.CustomAssetName + " (Clone)";
                        factory.prototypeHandles[prefabReference] = resourceManager.CreateCompletedOperation(gameObject, "");
                        var udn = gameObject.GetComponent<UnityDisplayNode>();
                        AssetCreation(curAsset, guid, udn);

                        Vector3 vector = new(Factory.kOffscreenPosition.x, 0f, 0f);
                        Quaternion identity = Quaternion.identity;
                        GameObject gameObject2 = Object.Instantiate(udn.gameObject, vector, identity, factory.DisplayRoot);
                        gameObject2.SetActive(true);
                        UnityDisplayNode component = gameObject2.GetComponent<UnityDisplayNode>();
                        component.Create();
                        component.cloneOf = prefabReference;
                        factory.active.Add(component);
                        onComplete.Invoke(component);
                    }));

                    return false;
                }

                if (guid.Equals("UpgradedText")) {
                    factory.FindAndSetupPrototypeAsync(new() { guidRef = "3dcdbc19136c60846ab944ada06695c0" }, new Action<UnityDisplayNode>(node => {
                        Transform transform = Game.instance.prototypeObjects.transform;

                        GameObject gameObject = Object.Instantiate(node.gameObject, transform);
                        gameObject.name = guid + " (Clone)";

                        var resourceManager = Addressables.Instance.ResourceManager;
                        factory.prototypeHandles[prefabReference] = resourceManager.CreateCompletedOperation(gameObject, "");
                        var udn = gameObject.GetComponent<UnityDisplayNode>();

                        udn.RecalculateGenericRenderers();
                        var nktmp = udn.GetComponentInChildren<NK_TextMeshPro>();
                        nktmp.m_fontColorGradient = new(Color.red, Color.red, new(255, 255, 0), Color.white);
                        nktmp.capitalize = false;
                        udn.RecalculateGenericRenderers();

                        Vector3 vector = new(Factory.kOffscreenPosition.x, 0f, 0f);
                        Quaternion identity = Quaternion.identity;
                        GameObject gameObject2 = Object.Instantiate(udn.gameObject, vector, identity, factory.DisplayRoot);
                        gameObject2.SetActive(true);
                        UnityDisplayNode component = gameObject2.GetComponent<UnityDisplayNode>();
                        component.Create();
                        component.cloneOf = prefabReference;
                        factory.active.Add(component);
                        onComplete.Invoke(component);
                    }));
                    return false;
                }

                if (guid.Equals("JackpotText")) {

                    factory.FindAndSetupPrototypeAsync(new() { guidRef = "3dcdbc19136c60846ab944ada06695c0" }, new Action<UnityDisplayNode>(node => {
                        Transform transform = Game.instance.prototypeObjects.transform;

                        GameObject gameObject = Object.Instantiate(node.gameObject, transform);
                        gameObject.name = guid + " (Clone)";

                        var resourceManager = Addressables.Instance.ResourceManager;
                        factory.prototypeHandles[prefabReference] = resourceManager.CreateCompletedOperation(gameObject, "");
                        var udn = gameObject.GetComponent<UnityDisplayNode>();

                        udn.RecalculateGenericRenderers();
                        var nktmp = udn.GetComponentInChildren<NK_TextMeshPro>();
                        nktmp.m_fontColorGradient = new(new(237, 171, 2), new(255, 200, 15), new(255, 255, 0), Color.white);
                        nktmp.capitalize = false;
                        udn.RecalculateGenericRenderers();

                        Vector3 vector = new(Factory.kOffscreenPosition.x, 0f, 0f);
                        Quaternion identity = Quaternion.identity;
                        GameObject gameObject2 = Object.Instantiate(udn.gameObject, vector, identity, factory.DisplayRoot);
                        gameObject2.SetActive(true);
                        UnityDisplayNode component = gameObject2.GetComponent<UnityDisplayNode>();
                        component.Create();
                        component.cloneOf = prefabReference;
                        factory.active.Add(component);
                        onComplete.Invoke(component);
                    }));
                    return false;
                }

                if (guid.Equals("UpgradedText")) {

                    factory.FindAndSetupPrototypeAsync(new() { guidRef = "3dcdbc19136c60846ab944ada06695c0" }, new Action<UnityDisplayNode>(node => {
                        Transform transform = Game.instance.prototypeObjects.transform;

                        GameObject gameObject = Object.Instantiate(node.gameObject, transform);
                        gameObject.name = guid + " (Clone)";

                        var resourceManager = Addressables.Instance.ResourceManager;
                        factory.prototypeHandles[prefabReference] = resourceManager.CreateCompletedOperation(gameObject, "");
                        var udn = gameObject.GetComponent<UnityDisplayNode>();

                        udn.RecalculateGenericRenderers();
                        var nktmp = udn.GetComponentInChildren<NK_TextMeshPro>();
                        nktmp.m_fontColorGradient = new(Color.green, Color.green, new(35, 255, 35), Color.white);
                        nktmp.capitalize = false;
                        udn.RecalculateGenericRenderers();

                        Vector3 vector = new(Factory.kOffscreenPosition.x, 0f, 0f);
                        Quaternion identity = Quaternion.identity;
                        GameObject gameObject2 = Object.Instantiate(udn.gameObject, vector, identity, factory.DisplayRoot);
                        gameObject2.SetActive(true);
                        UnityDisplayNode component = gameObject2.GetComponent<UnityDisplayNode>();
                        component.Create();
                        component.cloneOf = prefabReference;
                        factory.active.Add(component);
                        onComplete.Invoke(component);
                    }));
                    return false;
                }

                return true;
            }

            private static void AssetCreation(AssetInfo curAsset, string objectId, UnityDisplayNode udn, Il2CppSystem.Action<UnityDisplayNode> onComplete = null) {
                udn.RecalculateGenericRenderers();

                for (var i = 0; i < udn.genericRenderers.Length; i++) {
                    if (((curAsset.RendererType == RendererType.SKINNEDANDUNSKINNEDMESHRENDERER && udn.genericRenderers[i] is SkinnedMeshRenderer or MeshRenderer) ||
                        (curAsset.RendererType == RendererType.SKINNEDMESHRENDERER && udn.genericRenderers[i].Is<SkinnedMeshRenderer>()) ||
                        (curAsset.RendererType == RendererType.MESHRENDERER && udn.genericRenderers[i].Is<MeshRenderer>())) && !udn.genericRenderers[i].Is<SpriteRenderer>()) {
                        var renderer = udn.genericRenderers[i].Cast<Renderer>();
                        renderer.material.shader = ShaderAssets.First(a => a.name.StartsWith("Unlit/CelShading")).Cast<Shader>();
                        renderer.material.SetColor("_OutlineColor", Color.black);
                        renderer.material.mainTexture = CacheBuilder.Get(objectId);
                    } else if ((curAsset.RendererType is RendererType.SPRITERENDERER) && udn.genericRenderers[i].Is<SpriteRenderer>()) {
                        var spriteRenderer = udn.genericRenderers[i].Cast<SpriteRenderer>();
                        if (SpriteCreation.ContainsKey(objectId))
                            spriteRenderer.sprite = SpriteCreation[objectId](objectId);
                        else
                            spriteRenderer.sprite = SpriteBuilder.createProjectile(CacheBuilder.Get(objectId));
                    }
                }

                if (curAsset.RendererType == RendererType.SPRITERENDERER)
                    cachedSRs[objectId] = udn;

                if (Actions.ContainsKey(objectId))
                    Actions[objectId](udn);

                udn.RecalculateGenericRenderers();
            }

            [HideFromIl2Cpp]
            private static Dictionary<string, Texture2D> CombinedTextures { get; } = new();

            public static Texture2D CombineTextures(string name, Texture2D baseTex, Texture2D overlayTex) {
                if (CombinedTextures.ContainsKey(name) && CombinedTextures[name] != null)
                    return CombinedTextures[name];

                checked {
                    unchecked {
                        Texture2D texture = new(baseTex.width, baseTex.height);

                        var MainPixels = new Color[baseTex.width * baseTex.height];
                        var BaseTexturePixels = baseTex.GetPixels();
                        var OverlayPixels = overlayTex.GetPixels();

                        for (int i = 0; i < MainPixels.Length; i++) {
                            MainPixels[i] = BaseTexturePixels[i] + (OverlayPixels[i] * OverlayPixels[i].a);
                        }

                        texture.SetPixels(MainPixels);
                        texture.Apply();

                        CombinedTextures[name] = texture;

                        return texture;
                    }
                }
            }

            public static void Build() {
                for (var i = 0; i < AdditionalTiers.Towers.Length; i++) {
                    var assets = AdditionalTiers.Towers?[i]?.assetsToRead;
                    var assetsV2 = AdditionalTiers.Towers?[i]?.v2AssetStack;
                    var combinationAssets = AdditionalTiers.Towers?[i]?.assetsToCombine;
                    if (assets != null) {
                        foreach (var asset in assets) {
                            if (asset != null && !allAssetsKnown.Contains(asset))
                                allAssetsKnown.Add(asset);
                        }
                    }
                    if (assetsV2 != null) {
                        foreach (var asset in assetsV2) {
                            if (asset != null && !allAssetsKnown.Contains(asset))
                                allAssetsKnown.Add(asset);
                        }
                    }
                    if (combinationAssets != null) {
                        foreach (var asset in combinationAssets) {
                            if (!allCombinationAssetsKnown.Contains(asset))
                                allCombinationAssetsKnown.Add(asset);

                            CombineTextures(asset.TopLayerPath, CacheBuilder.Get(asset.BottomLayerPath), CacheBuilder.Get(asset.TopLayerPath));
                        }
                    }
                }

                hasBeenBuilt = true;
            }

            public static void Flush() {
                allAssetsKnown.Clear();
                allCombinationAssetsKnown.Clear();
            }
        }

        [HarmonyPatch(typeof(SpriteAtlas), nameof(SpriteAtlas.GetSprite))]
        internal static class SpriteAtlas_GetSprite {
            [HarmonyPrefix]
            private static bool Prefix(SpriteAtlas __instance, string name, ref Sprite __result) {
                if (__instance.name == "Ui") {
                    var resource = name.Trim().GetEmbeddedResource();
                    if (resource?.Length > 0) {
                        var texture = resource.ToTexture();
                        __result = Sprite.Create(texture, new(0, 0, texture.width, texture.height), new(), 10.2f);
                        __result.texture.mipMapBias = -1;
                        return false;
                    }
                }

                return true;
            }
        }

        public static class AnimatedAssets {
            private static readonly List<Sprite> _energySprites = new();

            public static List<Sprite> EnergySprites {
                get {
                    if (_energySprites == null || _energySprites.Count == 0) for (var index = 0; index < 64; index++) _energySprites.Add(SpriteBuilder.createProjectile(CacheBuilder.Get(string.Format("energy{0}", index))));
                    return _energySprites;
                }
            }
            private static readonly List<Sprite> _flameSprites = new();

            public static List<Sprite> FlameSprites {
                get {
                    if (_flameSprites == null || _flameSprites.Count == 0) for (var index = 0; index < 10; index++) _flameSprites.Add(SpriteBuilder.createProjectile(CacheBuilder.Get(string.Format("Flame{0}", index)), 7.6f, 0.5f, 0));
                    return _flameSprites;
                }
            }
            private static readonly List<Sprite> _darkFlameSprites = new();

            public static List<Sprite> DarkFlameSprites {
                get {
                    if (_darkFlameSprites == null || _darkFlameSprites.Count == 0) for (var index = 0; index < 10; index++) _darkFlameSprites.Add(SpriteBuilder.createProjectile(CacheBuilder.Get(string.Format("DarkFlame{0}", index)), 7.6f, 0.5f, 0));
                    return _darkFlameSprites;
                }
            }

            static AnimatedAssets() {
                _ = EnergySprites;
                _ = FlameSprites;
                _ = DarkFlameSprites;
            }
        }
    }
}