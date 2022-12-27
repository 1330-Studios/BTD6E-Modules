namespace AdditionalTiers.Utils.Scene;
[RegisterTypeInIl2Cpp(false)]
public class OverlayManager : MonoBehaviour {
    public OverlayManager(IntPtr obj0) : base(obj0) => ClassInjector.DerivedConstructorBody(this);
    public OverlayManager() : base(ClassInjector.DerivedConstructorPointer<OverlayManager>()) { }

    public static OverlayManager instance;

    private Canvas canvas;
    private Text text;
    public static Font font = AssetBundle.LoadFromMemory("AdditionalTiers.Utils.Scene.camera_export".GetEmbeddedResource()).LoadAsset("Assets/LuckiestGuy-Regular.ttf").Cast<Font>();

    private float prog;

    public string towerName = string.Empty;
    public bool rest;
    public bool operate;

    void Start() {
        instance = this;

        var c_Object = new GameObject("CanvasO");
        canvas = c_Object.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        var t_Object = new GameObject("TextO");
        t_Object.transform.parent = c_Object.transform;
        text = t_Object.AddComponent<Text>();
        text.text = towerName;
        text.rectTransform.anchoredPosition = new Vector3(850, 350, 0);
        text.font = font;
        text.color = Color.black;
        text.alignment = TextAnchor.MiddleCenter;
        text.rectTransform.sizeDelta = new Vector2(500, 500);
        text.fontSize = 75;
    }

    void Update() {
        if (rest) {
            rest = false;
            return;
        }

        if (operate) {
            if (prog > 3) {
                text.rectTransform.anchoredPosition = new Vector3(850, 350, 0);
                operate = false;
            }
            
            if (prog > 2.5f) {
                var _xP = Easings.Interpolate((prog - 2.5f) * 2f, 10);
                var _yP = Easings.Interpolate((prog - 2.5f) * 2f, 10);

                var _x = Mathf.Lerp(-450, 850, _xP);
                var _y = Mathf.Lerp(-230, 350, _yP);

                text.rectTransform.anchoredPosition = new Vector3(Mathf.Clamp(850, -450, _x), Mathf.Clamp(350, -230, _y), 0);
                prog += Time.deltaTime;
                return;
            }

            if (prog > 1) {
                text.rectTransform.anchoredPosition = new Vector3(-450, -230, 0);
                prog += Time.deltaTime;
                return;
            }

            var xP = Easings.Interpolate(prog, 0);
            var yP = Easings.Interpolate(prog, 11);

            var x = Mathf.Lerp(850, -450, xP);
            var y = Mathf.Lerp(350, -230, yP);

            text.rectTransform.anchoredPosition = new Vector3(Mathf.Clamp(850, -450, x), Mathf.Clamp(350, -230, y), 0);

            prog += Time.deltaTime;
        }
    }

    public void StartOverlay() {
        operate = true;
        text.text = towerName;
        prog = 0;
    }
}