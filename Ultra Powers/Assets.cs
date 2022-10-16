using Assets.Scripts;
using Assets.Scripts.Unity;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;

namespace Ultra_Powers;
internal static class Assets {
    internal static List<string> SpriteAssets = new();
    internal static List<(string, string, int)> RendererAssets = new();

    [HarmonyPatch(typeof(Factory.__c__DisplayClass21_0), nameof(Factory.__c__DisplayClass21_0._CreateAsync_b__0))]
    public sealed class DisplayFactory {
        [HarmonyPrefix]
        private static bool Prefix(Factory.__c__DisplayClass21_0 __instance, ref UnityDisplayNode prototype) {
            var factory = __instance.__4__this;
            var prefabReference = __instance.objectId;
            var guid = prefabReference.guidRef;
            var onComplete = __instance.onComplete;

            Transform transform = Game.instance.prototypeObjects.transform;

            var resourceManager = Addressables.Instance.ResourceManager;
            if (guid.Equals("UTotem_Particles")) {
                var baseRef = new PrefabReference() { guidRef = "faf5bc32286af5a4fb2920dbc6d59458" };

                factory.FindAndSetupPrototypeAsync(baseRef, new Action<UnityDisplayNode>(node => {
                    GameObject gameObject = Object.Instantiate(node.gameObject, transform);
                    gameObject.name = guid + " (Clone)";
                    factory.prototypeHandles[prefabReference] = resourceManager.CreateCompletedOperation(gameObject, "");
                    var udn = gameObject.GetComponent<UnityDisplayNode>();
                    udn.RecalculateGenericRenderers();

                    var ps = udn.transform.GetComponentsInChildren<ParticleSystem>();

                    for (int i = 0; i < ps.Length; i++)
                        ps[i].startColor = new Color32(0xEC, 0xAA, 0x52, 0xFF);

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

            if (prototype == null && RendererAssets.Any(a => a.Item1 == guid)) {
                var curAsset = RendererAssets.First(a => a.Item1 == guid);
                var baseRef = new PrefabReference() { guidRef = curAsset.Item2 };

                factory.FindAndSetupPrototypeAsync(baseRef, new Action<UnityDisplayNode>(node => {
                    GameObject gameObject = Object.Instantiate(node.gameObject, transform);
                    gameObject.name = curAsset.Item2 + " (Clone)";
                    factory.prototypeHandles[prefabReference] = resourceManager.CreateCompletedOperation(gameObject, "");
                    var udn = gameObject.GetComponent<UnityDisplayNode>();
                    udn.RecalculateGenericRenderers();

                    if (curAsset.Item3 != -1) {
                        udn.genericRenderers[curAsset.Item3].material.mainTexture = curAsset.Item1.GetEmbeddedResource().ToTexture();
                    } else {
                        for (int i = 0; i < udn.genericRenderers.Length; i++) {
                            udn.genericRenderers[i].material.mainTexture = curAsset.Item1.GetEmbeddedResource().ToTexture();
                        }
                    }
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
}