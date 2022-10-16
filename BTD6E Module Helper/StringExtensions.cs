namespace BTD6E_Module_Helper {
    internal static class StringExtensions {
        public static Dictionary<string, SpriteReference> Sprites = new();
        public static Dictionary<string, byte[]> Resources = new();

        public static byte[] GetEmbeddedResource(this string path) {
            if (Resources.ContainsKey(path))
                return Resources[path];

            using Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            byte[] array = new byte[(manifestResourceStream?.Length) ?? 0L];
            manifestResourceStream?.Read(array, 0, array.Length);
            Resources[path] = array;
            return array;
        }

        public static SpriteReference GetSpriteReference(this string name) {
            if (Sprites.ContainsKey(name))
                return Sprites[name];

            var sr = new SpriteReference() { guidRef = name };
            Sprites[name] = sr;
            return sr;
        }
    }
}
