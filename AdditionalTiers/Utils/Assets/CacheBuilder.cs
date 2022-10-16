using System.Collections;
using System.Globalization;

namespace AdditionalTiers.Utils.Assets {
    public static class CacheBuilder {
        private static readonly Dictionary<string, string> built = new();
        private static readonly Dictionary<string, byte[]> builtBytes = new();

        public static void Build() {
            foreach (DictionaryEntry v in Images.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true)) {
                if (v.Key is string id && v.Value is byte[] bytes) {
                    built.Add(id, Convert.ToBase64String(bytes));
                }
            }
        }

        public static Texture2D Get(string key) {
            if (key.Contains(".V2."))
                return key.Trim().GetEmbeddedResource().ToTexture();

            if (builtBytes.ContainsKey(key)) {
                return LoadTextureFromBytes(builtBytes[key]);
            }

            var bytes = Convert.FromBase64String(built[key]);
            builtBytes.Add(key, bytes);
            return LoadTextureFromBytes(Convert.FromBase64String(built[key]));
        }

        public static void Flush() => built.Clear();

        private static Texture2D LoadTextureFromBytes(byte[] FileData) {
            Texture2D Tex2D = new Texture2D(2, 2);
            if (ImageConversion.LoadImage(Tex2D, FileData)) return Tex2D;

            return null;
        }
    }
}