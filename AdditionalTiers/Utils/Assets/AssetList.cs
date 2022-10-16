namespace AdditionalTiers.Utils.Assets {
    public sealed class AssetStack {
        private sealed class Asset {
            public Asset Next;
            public AssetInfo Data;

            public Asset(AssetInfo data) => (Next, Data) = (null, data);
        }

        private Asset head;
        
        public IEnumerator<AssetInfo> GetEnumerator() {
            while (head != null) {
                var ret = head.Data;
                head = head.Next;
                yield return ret;
            }
        }

        public void Add(AssetInfo asset) => head = new(asset) { Next = head };
    }
}