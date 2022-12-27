using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using AdditionalTiers.Tasks.Round8;
using AdditionalTiers.Tasks.Towers;

using Il2CppAssets.Scripts.Unity;

[assembly: MelonAuthorColor(255, 200, 200, 255)]
[assembly: MelonColor(255, 255, 75, 255)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
[assembly: MelonInfo(typeof(AdditionalTiers.AdditionalTiers), "Additional Tier Addon", "1.8", "1330 Studios LLC")]

namespace AdditionalTiers {
    public sealed class AdditionalTiers : MelonMod {
        public static TowerTask[] Towers;
        public static string Version;

        public override void OnInitializeMelon() {
            ErrorHandler.VALUE.Initialize();

            Version = MelonAssembly.Assembly.GetName().Version.ToString();

            List<TowerTask> towers = new() {
                new GoldenExperience(),
                new KingCrimson(),
                new Mystery(),
                new BAY(),
                new WhiteWedding(),
                new FortunateSon(),
                new KingOfDarkness(),
                new GlaiveCreator()
            };

            StringBuilder sb = new();

            sb.AppendLine("╔Towers Loaded:══════════════════════════════════════════════════════════════════════════════════════════════════╗");
            foreach (var tower in towers.OrderByDescending(a => (long)a.tower).OrderByDescending(a => a.baseTower.Contains("Paragon")))
                sb.AppendLine($"║\t{tower.baseTower.Split('-')[0] + "-" + tower.baseTower.Split('-')[1].Replace('2', '0').Replace('1', '0').Replace('0', 'x'),-20} - {$"\"{tower.identifier}\"",-20} @ {(long)tower.tower:n0} pops".PadRight(107) + "║");
            sb.AppendLine("╚════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");

            Towers = towers.ToArray();

            if (!MelonPreferences.HasEntry("Additional Tier Addon Config", "Tier 6 required pop count multiplier")) {
                MelonPreferences.CreateCategory("Additional Tier Addon Config", "Additional Tier Addon Config");
                MelonPreferences.CreateEntry("Additional Tier Addon Config", "Tier 6 required pop count multiplier", (float)1);
                MelonPreferences.CreateEntry("Additional Tier Addon Config", "Display Format", ADisplay.style);
            }

            Globals.Load();

            HarmonyInstance?.Patch(Method(typeof(Tower), nameof(Tower.Hilight)), postfix: new HarmonyMethod(Method(typeof(HighlightManager), nameof(HighlightManager.Highlight))));
            HarmonyInstance?.Patch(Method(typeof(Tower), nameof(Tower.UnHighlight)), postfix: new HarmonyMethod(Method(typeof(HighlightManager), nameof(HighlightManager.UnHighlight))));

            var o_OutputEncoding = Console.OutputEncoding;

            Console.OutputEncoding = Encoding.UTF8;
            
            LoggerInstance?.Msg(System.Drawing.Color.OrangeRed, $"Additional Tier Addon Loaded (v{Version}){Environment.NewLine}{sb}");

            Console.OutputEncoding = o_OutputEncoding;

            Logger13.Log("Success!");

            InternalVerification.Verify();

            CacheBuilder.Build();
            DisplayFactory.Build();
        }

        public override void OnApplicationQuit() {
            LoggerInstance?.Msg(System.Drawing.Color.MediumAquamarine, $"Last Win32 Error - {Marshal.GetLastWin32Error()}");
            DisplayFactory.Flush();
            CacheBuilder.Flush();
        }

        public override void OnGUI() => Watermark();

        private static bool loaded;

        public override void OnUpdate() {
            if (!loaded) {
                if (Game.instance is null || Game.instance.model is null)
                    return;

                var mdl = Game.instance.model;

                GameI.Loaded.Postfix(ref mdl);

                Game.instance.model = mdl;

                loaded = true;
            }

            if (!DisplayFactory.hasBeenBuilt)
                DisplayFactory.Build();

            if (InGame.instance == null || InGame.instance.bridge == null || InGame.instance.bridge.GetAllTowers() == null) {
                TransformationManager.VALUE.Clear();
                return;
            }

            UpgradeMenuManager.Update(InGame.instance);

            if (CameraMotionManager.instance == null) {
                var go = new GameObject("CameraMotionManager");
                var cmm = go.AddComponent<CameraMotionManager>();
                cmm.target = Vector3.zero;
                cmm.disable = Camera.allCameras;
                CameraMotionManager.instance = cmm;
            }
            if (OverlayManager.instance == null) {
                var go = new GameObject("OverlayManager");
                var om = go.AddComponent<OverlayManager>();
                OverlayManager.instance = om;
            }

            try {
                var allAdditionalTiers = Towers;
                for (var indexOfTowers = 0; indexOfTowers < InGame.instance?.bridge?.GetAllTowers().Count; indexOfTowers++) {
                    var towerToSimulation = InGame.instance?.bridge?.GetAllTowers()?.ToArray()?[indexOfTowers];
                    if (towerToSimulation?.destroyed == false) {
                        foreach (var addedTier in allAdditionalTiers) {
                            if (towerToSimulation != null && !addedTier.requirements(towerToSimulation)) continue;

                            var popsNeeded = (int)((int)addedTier.tower * Globals.SixthTierPopCountMulti);

                            if (popsNeeded <= towerToSimulation?.damageDealt) {
                                if (!TransformationManager.VALUE.Contains(towerToSimulation.tower))
                                    addedTier?.onComplete(towerToSimulation);
                                else if (TransformationManager.VALUE.Contains(towerToSimulation.tower)) addedTier.recurring(towerToSimulation);
                            } else if (towerToSimulation != null && !TransformationManager.VALUE.Contains(towerToSimulation.tower)) {
                                ADisplay.towerdata.Add((addedTier.identifier, towerToSimulation.damageDealt, popsNeeded));
                            }
                        }
                    }
                }
            } catch { }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Watermark() {
            var guiCol = GUI.color;
            GUI.color = new Color32(255, 255, 255, 50);
            var guiStyle = new GUIStyle {
                normal =
                {
                    textColor = Color.white
                }
            };
            GUI.Label(new Rect(10, Screen.height - 20, 100, 90), $"Additional Tiers Addon v{Version}", guiStyle);
            GUI.color = guiCol;
        }
    }
}