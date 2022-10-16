namespace AdditionalTiers.Utils.Components;
[RegisterTypeInIl2Cpp]
public class SetScaleN : MonoBehaviour {
    public SetScaleN(IntPtr obj0) : base(obj0) { ClassInjector.DerivedConstructorBody(this); }

    public SetScaleN() : base(ClassInjector.DerivedConstructorPointer<SetScaleN>()) { }
    private void Update() {
        if (gameObject != null && gameObject.transform.localScale.z != 25) {
            gameObject.transform.localScale = new(25, 25, 25);
        }
    }
}