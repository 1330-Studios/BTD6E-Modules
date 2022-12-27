namespace AdditionalTiers.Utils.Scene;
[RegisterTypeInIl2Cpp(false)]
public class CameraMotionManager : MonoBehaviour {
    public CameraMotionManager(IntPtr obj0) : base(obj0) => ClassInjector.DerivedConstructorBody(this);
    public CameraMotionManager() : base(ClassInjector.DerivedConstructorPointer<CameraMotionManager>()) { }

    public static CameraMotionManager instance;
    public static Camera cam;

    public Camera[] disable;

    public Vector3 target;

    public CameraMotion Action { get; private set; }

    public Action<CameraMotionManager> UpdateAction;

    public bool rest;

    private float prog;
    private bool operate;
    private Vector3 o_Position;
    private Quaternion o_Rotation;
    private float y_offset;
    private float y_rot_offset;

    // Start is called before the first frame update
    void Start() {
        var go = new GameObject("Camera_Container");
        cam = go.AddComponent<Camera>();
        cam.fieldOfView = 35;
        cam.clearFlags = CameraClearFlags.Skybox;
        cam.backgroundColor = new Color32(3, 207, 252, 255);
        cam.enabled = false;
        DontDestroyOnLoad(go);

        var camT = cam.gameObject.transform;

        o_Position = new Vector3(camT.position.x, camT.position.y, camT.position.z);
        o_Rotation = new Quaternion(camT.rotation.x, camT.rotation.y, camT.rotation.z, camT.rotation.w);
    }

    // Update is called once per frame
    void Update() {
        if (rest) {
            rest = false;
            return;
        }

        if (prog > 1) {
            Action = CameraMotion.None;
            prog = 0;
            operate = false;
            cam.enabled = false;
            foreach (var camera in disable) {
                camera.enabled = true;
            }
        }

        if (operate) {
            UpdateAction(this);

            var camT = cam.gameObject.transform;

            var pos = (target - (camT.forward * 50));
            pos.y = y_offset;
            camT.position = pos;

            switch (Action) {
                default: case CameraMotion.Pan: { UpdatePan(); break; }
                case CameraMotion.None: { break; }
            }
            prog += Time.deltaTime / 4f;
            cam.enabled = true;
            foreach (var camera in disable) {
                camera.enabled = false;
            }
        }
    }

    public void StartCameraMotion(CameraMotion motion, float yOffset = 10f, float yRotOffset = 0) {
        Action = motion;
        operate = true;
        prog = 0;
        y_offset = yOffset;
        y_rot_offset = yRotOffset;
        rest = true;

        var camT = cam.gameObject.transform;

        var pos = (target - (camT.forward * 50));
        pos.y = y_offset;
        camT.position = pos;
        camT.LookAt(target);
    }

    public void Reset() {
        Action = CameraMotion.None;
        operate = false;
        prog = 0;
        cam.gameObject.transform.position = o_Position;
        cam.gameObject.transform.rotation = o_Rotation;
    }
    void UpdatePan() {
        var eased = Easings.Interpolate(prog, 12);

        var rotEul = cam.gameObject.transform.rotation;
        cam.gameObject.transform.rotation = Quaternion.Euler(rotEul.x + 7.5f - eased * 15, (rotEul.y + y_rot_offset) - 7.5f + eased * 15, rotEul.z - 7.5f + eased * 15f);
    }
}

public enum CameraMotion {
    None,
    Pan
}