namespace BTD6E_Module_Helper;

internal static class ReferenceFixes {
    [HarmonyPatch(typeof(SpriteReference), MethodType.Constructor, typeof(string))]
    internal static class SpriteReference_Fixer {
        [HarmonyPrefix]
        internal static bool Fix(ref SpriteReference __instance, string guid) {
            __instance = new() { guidRef = guid };
            return false;
        }
    }
    [HarmonyPatch(typeof(TextureReference), MethodType.Constructor, typeof(string))]
    internal static class TextureReference_Fixer {
        [HarmonyPrefix]
        internal static bool Fix(ref TextureReference __instance, string guid) {
            __instance = new() { guidRef = guid };
            return false;
        }
    }
    [HarmonyPatch(typeof(AnimationClipReference), MethodType.Constructor, typeof(string))]
    internal static class AnimationClipReference_Fixer {
        [HarmonyPrefix]
        internal static bool Fix(ref AnimationClipReference __instance, string guid) {
            __instance = new() { guidRef = guid };
            return false;
        }
    }
    [HarmonyPatch(typeof(AudioClipReference), MethodType.Constructor, typeof(string))]
    internal static class AudioClipReference_Fixer {
        [HarmonyPrefix]
        internal static bool Fix(ref AudioClipReference __instance, string guid) {
            __instance = new() { guidRef = guid };
            return false;
        }
    }
    [HarmonyPatch(typeof(AudioSourceReference), MethodType.Constructor, typeof(string))]
    internal static class AudioSourceReference_Fixer {
        [HarmonyPrefix]
        internal static bool Fix(ref AudioSourceReference __instance, string guid) {
            __instance = new() { guidRef = guid };
            return false;
        }
    }
    [HarmonyPatch(typeof(PrefabReference), MethodType.Constructor, typeof(string))]
    internal static class PrefabReference_Fixer {
        [HarmonyPrefix]
        internal static bool Fix(ref PrefabReference __instance, string guid) {
            Logger13.Log("Called");
            __instance = new() { guidRef = guid };
            return false;
        }
    }
}