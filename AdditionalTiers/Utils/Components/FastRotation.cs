namespace AdditionalTiers.Utils.Components {
    [RegisterTypeInIl2Cpp(false)]
    public class FastRotation : MonoBehaviour {
        public FastRotation(IntPtr obj0) : base(obj0) {
            ClassInjector.DerivedConstructorBody(this);
        }

        public FastRotation() : base(ClassInjector.DerivedConstructorPointer<FastRotation>()) { }


        public void Update() {
            transform.Rotate(0, 1f, 0);
        }
    }
}
