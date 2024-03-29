﻿namespace AdditionalTiers.Utils.Towers {
    public sealed class TaskRunner {

        [HarmonyPatch(typeof(InGame), nameof(InGame.Quit))]
        [HarmonyPatch(typeof(InGame), nameof(InGame.Restart))]
        private sealed class Reset {
            [HarmonyPostfix]
            private static void RunLeave() {
                var allAdditionalTiers = AdditionalTiers.Towers;
                for (var towerIndex = allAdditionalTiers.Length - 1; towerIndex >= 0; towerIndex--)
                    allAdditionalTiers[towerIndex].onLeave();
                TransformationManager.VALUE.Clear();
            }
        }
    }
}