using Assets.Scripts.Unity;

using UnityEngine.AddressableAssets;
using UnityEngine.U2D;

namespace PetTowers;
internal static class ResourceManager {
    internal static List<(string, string)> RendererAssets = new();

    [HarmonyPatch(typeof(Factory.__c__DisplayClass21_0), nameof(Factory.__c__DisplayClass21_0._CreateAsync_b__0))]
    public sealed class DisplayFactory {

        [HarmonyPrefix]
        private static bool Prefix(Factory.__c__DisplayClass21_0 __instance) {
            var factory = __instance.__4__this;
            var prefabReference = __instance.objectId;
            var guid = prefabReference.guidRef;
            var onComplete = __instance.onComplete;

            foreach (var curAsset in RendererAssets) {
                if (guid.Equals(curAsset.Item1)) {
                    factory.FindAndSetupPrototypeAsync(new() { guidRef = curAsset.Item2 }, new Action<UnityDisplayNode>(node => {
                        Transform transform = Game.instance.prototypeObjects.transform;
                        GameObject gameObject = Object.Instantiate(node.gameObject, transform);
                        gameObject.name = guid + " (Clone)";

                        var resourceManager = Addressables.Instance.ResourceManager;
                        factory.prototypeHandles[prefabReference] = resourceManager.CreateCompletedOperation(gameObject, "");
                        var udn = gameObject.GetComponent<UnityDisplayNode>();

                        udn.RecalculateGenericRenderers();

                        foreach (var renderer in udn.genericRenderers) {
                            if (renderer.Is<SkinnedMeshRenderer>()) {
                                renderer.material.mainTexture = curAsset.Item1.GetEmbeddedResource().ToTexture();
                            } else
                            if (renderer.Is<SpriteRenderer>(out var sr) && !udn.genericRenderers.Any(a=>a.Is<MeshRenderer>()||a.Is<SkinnedMeshRenderer>())) {
                                var texture = curAsset.Item1.GetEmbeddedResource().ToTexture();
                                texture.mipMapBias = -1;
                                sr.sprite = Sprite.Create(texture, new(0, 0, texture.width, texture.height), new(.5f, .5f), 5.6f);
                                renderer.material.mainTexture = curAsset.Item1.GetEmbeddedResource().ToTexture();
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
