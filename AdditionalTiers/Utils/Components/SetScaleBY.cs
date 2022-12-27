namespace AdditionalTiers.Utils.Components;
[RegisterTypeInIl2Cpp(false)]
public class SetScaleBY : MonoBehaviour {
    public SetScaleBY(IntPtr obj0) : base(obj0) { ClassInjector.DerivedConstructorBody(this); }

    public SetScaleBY() : base(ClassInjector.DerivedConstructorPointer<SetScaleBY>()) { }
    private void Update() {
        if (gameObject != null && gameObject.transform.localScale.z != 100) {
            gameObject.transform.localScale = new(100, 100, 100);
        }
    }
}