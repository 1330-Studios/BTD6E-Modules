namespace BTD6E_Module_Helper {
    internal static class TextureExtensions {
        internal static Texture2D ToTexture(this byte[] bytes) {
            Texture2D Tex2D = new Texture2D(2, 2);
            return ImageConversion.LoadImage(Tex2D, bytes) ? Tex2D : null;
        }
    }
}
