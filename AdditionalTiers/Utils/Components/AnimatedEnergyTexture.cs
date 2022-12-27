namespace AdditionalTiers.Utils.Components {
    [RegisterTypeInIl2Cpp(false)]
    public class AnimatedEnergyTexture : MonoBehaviour {
        public AnimatedEnergyTexture(IntPtr obj0) : base(obj0) { ClassInjector.DerivedConstructorBody(this); }

        public AnimatedEnergyTexture() : base(ClassInjector.DerivedConstructorPointer<AnimatedEnergyTexture>()) { }

        private int _frame = 0;

        internal Sprite[] sprites = Tasks.Assets.AnimatedAssets.EnergySprites.ToArray();
        internal SpriteRenderer renderer;

        private void Start() => renderer = GetComponent<SpriteRenderer>();

        private void Update() {
            if (sprites.Length > 0 && renderer != null && Time.frameCount % (TimeManager.FastForwardActive ? 2 : 4) == 0) {
                renderer.sprite = sprites[_frame];
                _frame = (_frame + 1) % sprites.Length;
            }
        }
    }
}