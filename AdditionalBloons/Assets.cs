using System.Diagnostics.CodeAnalysis;
using System.Drawing;

using AdditionalBloons.Resources;
using AdditionalBloons.Utils;

using Assets.Scripts.Unity;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;

using Color = UnityEngine.Color;
using Image = UnityEngine.UI.Image;

namespace AdditionalBloons.Tasks {
    public sealed class Assets {
        [HarmonyPatch(typeof(Factory.__c__DisplayClass21_0), nameof(Factory.__c__DisplayClass21_0._CreateAsync_b__0))]
        public sealed class NDisplayFactory {
            private static AssetBundle DOOMBUNDLE = AssetBundle.LoadFromMemory("AdditionalBloons.doom.bundle".GetEmbeddedResource());

            [HarmonyPrefix]
            private static bool Prefix(Factory.__c__DisplayClass21_0 __instance, ref UnityDisplayNode prototype) {
                var factory = __instance.__4__this;
                var prefabReference = __instance.objectId;
                var guid = prefabReference.guidRef;
                var onComplete = __instance.onComplete;

                if (prototype == null && guid.Equals("DOOM")) {
                    factory.FindAndSetupPrototypeAsync(new() { guidRef = "06bf915dea753ad43b772045caf1d906" }, new Action<UnityDisplayNode>(node => {
                        Transform transform = Game.instance.prototypeObjects.transform;

                        var orig = DOOMBUNDLE.LoadAsset("SupaDoom").Cast<GameObject>();
                        orig.transform.parent = transform;
                        GameObject gameObject = Object.Instantiate(orig, transform);
                        gameObject.name = guid + " (Clone)";

                        var resourceManager = Addressables.Instance.ResourceManager;
                        factory.prototypeHandles[prefabReference] = resourceManager.CreateCompletedOperation(gameObject, "");

                        var udn = gameObject.AddComponent<UnityDisplayNode>();
                        udn.transform.position = new(-3000, 10);

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
}