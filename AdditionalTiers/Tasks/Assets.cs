using Il2CppAssets.Scripts.Simulation.Behaviors;
using Il2CppAssets.Scripts.Unity;

using Il2CppTMPro;

using UnityEngine;
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
        };


        [HideFromIl2Cpp]
        public static Dictionary<string, Func<Object, bool>> SpecialShaderIndicies { get; set; } = new() {
        };

        [HideFromIl2Cpp]
        public static Dictionary<string, Color> SpecialColors { get; set; } = new() {
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
                    _shader = AssetBundle.LoadFromMemory("AdditionalTiers.Resources.shader.bundle".GetEmbeddedResource());
                return _shader;
            }
        }

        private static AssetBundle _particle;

        public static AssetBundle ParticleBundle {
            get {
                if (_particle == null)
                    _particle = AssetBundle.LoadFromMemory("AdditionalTiers.Resources.particle.bundle".GetEmbeddedResource());
                return _particle;
            }
        }

        private static AssetBundle _particlesystem;

        public static AssetBundle ParticleSystemBundle {
            get {
                if (_particlesystem == null)
                    _particlesystem = AssetBundle.LoadFromMemory("AdditionalTiers.Resources.particlesystem.bundle".GetEmbeddedResource());
                return _particlesystem;
            }
        }

        private static AssetBundle _t6specificassets;

        public static AssetBundle T6SpecificAssets {
            get {
                if (_t6specificassets == null)
                    _t6specificassets = AssetBundle.LoadFromMemory("AdditionalTiers.Resources.t6.bundle".GetEmbeddedResource());
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

        public static AssetBundle Round8 = AssetBundle.LoadFromMemory("AdditionalTiers.Resources.round8".GetEmbeddedResource());

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

                if (guid.StartsWith("Round8_")) {
                    var round8Asset = Round8.LoadAsset(guid.Replace("Round8_", "").Split('#')[0]).Cast<GameObject>();
                    round8Asset.name = guid.Replace("Round8_", "").Split('#')[0];

                    var round8AssetInstance = Object.Instantiate(round8Asset, factory.DisplayRoot).AddComponent<UnityDisplayNode>();
                    round8AssetInstance.Active = false;
                    round8AssetInstance.name = guid.Replace("Round8_", "").Split('#')[0] + " (Clone)";
                    var scale = round8AssetInstance.gameObject.AddComponent<Round8Scale>();
                    scale.Scale = float.Parse(guid.Replace("Round8_", "").Split('#')[1]);
                    round8AssetInstance.RecalculateGenericRenderers();

                    factory.prototypeHandles[prefabReference] = resourceManager.CreateCompletedOperation(round8AssetInstance.gameObject, $"COULDN'T COMPLETE OPERATION FOR ROUND 8 ASSET {guid.Normalize()}");

                    Vector3 nvector = new(Factory.kOffscreenPosition.x, 0f, 0f);
                    Quaternion nidentity = Quaternion.identity;
                    GameObject ngameObject2 = Object.Instantiate(round8AssetInstance.gameObject, nvector, nidentity, factory.DisplayRoot);
                    ngameObject2.SetActive(true);
                    UnityDisplayNode ncomponent = ngameObject2.GetComponent<UnityDisplayNode>();
                    var ncomponentscale = ngameObject2.GetComponent<Round8Scale>();
                    ncomponentscale.Scale = float.Parse(guid.Replace("Round8_", "").Split('#')[1]);
                    ncomponent.Create();
                    ncomponent.cloneOf = prefabReference;
                    factory.active.Add(ncomponent);
                    onComplete.Invoke(ncomponent);

                    return false;
                }

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
                        var nktmp = udn.GetComponentInChildren<TextMeshPro>();
                        nktmp.m_fontColorGradient = new(Color.red, Color.red, new(255, 255, 0), Color.white);
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
                        var nktmp = udn.GetComponentInChildren<TextMeshPro>();
                        nktmp.m_fontColorGradient = new(new(237, 171, 2), new(255, 200, 15), new(255, 255, 0), Color.white);
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
                        var nktmp = udn.GetComponentInChildren<TextMeshPro>();
                        nktmp.m_fontColorGradient = new(Color.green, Color.green, new(35, 255, 35), Color.white);
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
                    if (curAsset.RendererType == RendererType.ANYRENDERER && udn.genericRenderers[i].GetIl2CppType() != Il2CppType.Of<ParticleSystemRenderer>()) {
                        var renderer = udn.genericRenderers[i].Cast<Renderer>();
                        renderer.material.shader = ShaderAssets.First(a => a.name.StartsWith("Unlit/CelShading")).Cast<Shader>();
                        renderer.material.SetColor("_OutlineColor", Color.black);
                        renderer.material.mainTexture = CacheBuilder.Get(objectId);
                    } else if (curAsset.RendererType == RendererType.PARTICLESYSTEMRENDERER) {
                        var renderer = udn.genericRenderers[i].Cast<Renderer>();
                        renderer.material.mainTexture = CacheBuilder.Get(objectId);
                    } else if (((curAsset.RendererType == RendererType.SKINNEDANDUNSKINNEDMESHRENDERER && udn.genericRenderers[i] is SkinnedMeshRenderer or MeshRenderer) ||
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
                    if (name.StartsWith("Round8_")) {
                        var assetName = name.Trim().Replace("Round8_", "");
                        var r8T = Round8.LoadAsset(assetName).Cast<Texture2D>();
                        __result = Sprite.Create(r8T, new(0, 0, r8T.width, r8T.height), new(), 10.2f);
                        __result.texture.requestedMipmapLevel = -1;
                        return false;
                    }
                    
                    var resource = name.Trim().GetEmbeddedResource();
                    if (resource?.Length > 0) {
                        var texture = resource.ToTexture();
                        __result = Sprite.Create(texture, new(0, 0, texture.width, texture.height), new(), 10.2f);
                        __result.texture.requestedMipmapLevel = -1;
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