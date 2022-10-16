namespace AdditionalTiers.Utils {
    public sealed class UpdateHelper {
        public static DateTime startTime = DateTime.MinValue;
        public static string modName = "";
        private static string modVersion = "";
        private static bool chanceGiven;

        public static void Init() {
            var mInf = Assembly.GetCallingAssembly().GetCustomAttribute<MelonInfoAttribute>();
            (modName, modVersion) = (mInf.Name, mInf.Version);
        }

        public static void OnGUI() {
            if (startTime == DateTime.MinValue)
                startTime = DateTime.Now;

            if (chanceGiven)
                return;
            if ((DateTime.Now - startTime).TotalSeconds > 5) {
                chanceGiven = true;
                return;
            }

            var top = Screen.height / 2 - 75;
            var left = Screen.width / 2 - 150;
            GUI.Box(new Rect(left, top, 300, 150), $"Update Tool for {modName} {(int)(5 - System.Math.Ceiling((DateTime.Now - startTime).TotalSeconds))}s");
            GUI.Label(new Rect(left + 15, top + 25, 300, 150), "Check for updates?");
            if (GUI.Button(new Rect(left + 15, top + 45, 80, 20), "Confirm") && UpdateAvailable()) Environment.Exit(0);

            bool UpdateAvailable() {
                try {
#pragma warning disable SYSLIB0014
                    dynamic json = JsonConvert.DeserializeObject<dynamic>(new WebClient().DownloadString("http://1330studios.com/btd6_info.json"));
#pragma warning restore SYSLIB0014
                    for (int i = 0; i < json.Count; i++)
                        if (json[i]["Name"].Equals(modName) && !json[i]["Versions"][0]["ReadableVersion"].Contains("(Testing)")) {
                            Process.Start(json[i]["Versions"][0]["DownloadLink"]);
                            return true;
                        }
                } catch (Exception ex) {
                    Logger13.Warn($"Issue resolving webapi json {ex.GetType().Name}");
                }
                return false;
            }
        }
    }
}
