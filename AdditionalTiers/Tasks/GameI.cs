namespace AdditionalTiers.Tasks {
    public sealed class GameI {
        //[HarmonyPatch(typeof(GameModel), nameof(GameModel))]
        public static class Loaded {
            public static GameModel Model { get; set; } = null!;

            //[HarmonyPostfix]
            public static void Postfix(ref GameModel __result) {
                Model = __result;

                if (AdditionalTiers.Towers is not null)
                    foreach (var tower in AdditionalTiers.Towers) {
                        var nextOne = tower?.identifier;
                        tower?.gameLoad(__result);
                    }
                else
                    Logger13.Error("AdditionalTiers.Towers is null! This is not good!");
            }
        }
    }
}