namespace AdditionalTiers.Utils.Assets;
public struct CombinationAssets {
    public string TopLayerPath;
    public string BottomLayerPath;
    public string AssetDisplay;

    public CombinationAssets(string topLayerPath, string bottomLayerPath, string assetDisplay) {
        TopLayerPath = topLayerPath ?? throw new ArgumentNullException(nameof(topLayerPath));
        BottomLayerPath = bottomLayerPath ?? throw new ArgumentNullException(nameof(bottomLayerPath));
        AssetDisplay = assetDisplay ?? throw new ArgumentNullException(nameof(assetDisplay));
    }
}