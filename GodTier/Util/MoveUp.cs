namespace GodlyTowers.Utils {
    [RegisterTypeInIl2Cpp]
    public class MoveUp : MonoBehaviour {
        public MoveUp(IntPtr obj0) : base(obj0) { ClassInjector.DerivedConstructorBody(this); }

        public MoveUp() : base(ClassInjector.DerivedConstructorPointer<MoveUp>()) { }

        public void Update() {
            if (gameObject.transform.position.y != 10) {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 10, gameObject.transform.position.z);
            }
        }
    }
}
