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

    [RegisterTypeInIl2Cpp]
    public class SetScale4 : MonoBehaviour {
        public SetScale4(IntPtr obj0) : base(obj0) { ClassInjector.DerivedConstructorBody(this); }

        public SetScale4() : base(ClassInjector.DerivedConstructorPointer<SetScale4>()) { }
        private void Update() {
            if (gameObject != null) {
                if (gameObject.transform.localScale.z < 25) {
                    gameObject.transform.localScale = new(25, 25, 25);
                }
            }
        }
    }
    [RegisterTypeInIl2Cpp]
    public class SetScale5 : MonoBehaviour {
        public SetScale5(IntPtr obj0) : base(obj0) { ClassInjector.DerivedConstructorBody(this); }

        public SetScale5() : base(ClassInjector.DerivedConstructorPointer<SetScale5>()) { }
        private void Update() {
            if (gameObject != null) {
                if (gameObject.transform.localScale.z < 30) {
                    gameObject.transform.localScale = new(30, 30, 30);
                }
            }
        }
    }
    [RegisterTypeInIl2Cpp]
    public class SetScale6 : MonoBehaviour {
        public SetScale6(IntPtr obj0) : base(obj0) { ClassInjector.DerivedConstructorBody(this); }

        public SetScale6() : base(ClassInjector.DerivedConstructorPointer<SetScale6>()) { }
        private void Start() {
            if (gameObject != null) {
                if (gameObject.transform.localScale.z < 8.5f) {
                    gameObject.transform.localScale = new(8.5f, 8.5f, 8.5f);
                }
            }
        }

        private void Update() {
            if (gameObject != null) {
                if (gameObject.transform.localScale.z < 8.5f) {
                    gameObject.transform.localScale = new(8.5f, 8.5f, 8.5f);
                }
            }
        }
    }
    [RegisterTypeInIl2Cpp]
    public class SetScale7 : MonoBehaviour {
        public SetScale7(IntPtr obj0) : base(obj0) { ClassInjector.DerivedConstructorBody(this); }

        public SetScale7() : base(ClassInjector.DerivedConstructorPointer<SetScale7>()) { }
        private void Start() {
            if (gameObject != null) {
                if (gameObject.transform.localScale.z != 0.5f) {
                    gameObject.transform.localScale = new(0.5f, 0.5f, 0.5f);
                }
            }
        }

        private void Update() {
            if (gameObject != null) {
                if (gameObject.transform.localScale.z != 0.5f) {
                    gameObject.transform.localScale = new(0.5f, 0.5f, 0.5f);
                }
            }
        }
    }
}
