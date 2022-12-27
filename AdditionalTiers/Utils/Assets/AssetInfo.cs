namespace AdditionalTiers.Utils.Assets {
    [Serializable]
    public record AssetInfo {
        public string CustomAssetName { get => _customassetname; set => _customassetname = value; }

        public string BTDAssetName { get => _btdassetname; set => _btdassetname = value; }

        public RendererType RendererType { get => _renderertype; set => _renderertype = value; }

        private string _customassetname = String.Empty;
        private string _btdassetname = String.Empty;
        private RendererType _renderertype = RendererType.UNSET;

        public AssetInfo(string customAsset, string btdAsset, RendererType type) {
            _customassetname = customAsset;
            _btdassetname = btdAsset;
            _renderertype = type;
        }
    }
    // I dont wanna put this elsewhere.
    public enum RendererType {
        SKINNEDMESHRENDERER,
        MESHRENDERER,
        SKINNEDANDUNSKINNEDMESHRENDERER,
        SPRITERENDERER,
        PARTICLESYSTEMRENDERER,
        ANYRENDERER,
        UNSET
    }
}