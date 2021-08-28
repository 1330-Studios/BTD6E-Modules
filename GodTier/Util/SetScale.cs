﻿namespace GodlyTowers.Util {
    [RegisterTypeInIl2Cpp]
    public class SetScale : MonoBehaviour {
        public SetScale(IntPtr obj0) : base(obj0) { ClassInjector.DerivedConstructorBody(this); }

        public SetScale() : base(ClassInjector.DerivedConstructorPointer<SetScale>()) { }
        private void Update() {
            if (gameObject != null) {
                if (gameObject.transform.localScale.z < 24) {
                    gameObject.transform.localScale = new(24, 24, 24);
                }
            }
        }
    }

    [RegisterTypeInIl2Cpp]
    public class SetScale2 : MonoBehaviour {
        public SetScale2(IntPtr obj0) : base(obj0) { ClassInjector.DerivedConstructorBody(this); }

        public SetScale2() : base(ClassInjector.DerivedConstructorPointer<SetScale2>()) { }
        private void Update() {
            if (gameObject != null) {
                if (gameObject.transform.localScale.z < 17f) {
                    gameObject.transform.localScale = new(17, 17, 17);
                }
            }
        }
    }

    [RegisterTypeInIl2Cpp]
    public class SetScale3 : MonoBehaviour {
        public SetScale3(IntPtr obj0) : base(obj0) { ClassInjector.DerivedConstructorBody(this); }

        public SetScale3() : base(ClassInjector.DerivedConstructorPointer<SetScale3>()) { }
        private void Update() {
            if (gameObject != null) {
                if (gameObject.transform.localScale.z < 45) {
                    gameObject.transform.localScale = new(45, 45, 45);
                }
            }
        }
    }
}
