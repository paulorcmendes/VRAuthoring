using UnityEngine;
using UnityEngine.Video;

public enum VIDEO_STATE {PLAYING, STOPED}

public class MediaSettings : MonoBehaviour {
    private VideoPlayer videoPlayer;
    private AudioSource audioSource;
    public GameObject MediaNamePrefab;
    //private Camera camera;
    private MediaControllerScript controller;
    private NCLParser nclParser;
    private GameObject anchorDisplay;
    private GameObject displayVideo;
    //public bool isPlaying;
    //public int frameCount;
    //public int frame;

	private string MediaId;
    public string url;
    [Range(0f, 100f)]
    public float volume;
    public Rect rect;
    public int zIndex;
    public delegate void MyHandler();

    public event MyHandler OnBegin;
    public event MyHandler OnEnd;

    // Use this for initialization
    void Awake () {
        InitialConfiguration();
        SetMediaSize();
        SetMediaVolume();
        videoPlayer.loopPointReached += Ended;
        MediaId = GetComponent<MediaKind>().MediaId;
    }
    void Start() {
        GameObject myName = Instantiate(MediaNamePrefab, transform, false);
        myName.GetComponent<TextMesh>().text = this.url;
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<MediaControllerScript>();
        nclParser = GameObject.FindGameObjectWithTag("GameController").GetComponent<NCLParser>();        
        //SaveToNCL
        nclParser.AddMedia(MediaId, url, volume, rect.x, rect.y, rect.width, rect.height, zIndex);
    }
    // Update is called once per frame
    void Update()
    {
        if (controller.myMode == CurrentMode.EDITING)
        {
            videoPlayer.Stop();
            //this.camera.enabled = false;
        }
    }
    

    private void InitialConfiguration()
    {
        anchorDisplay = GameObject.FindGameObjectWithTag("AnchorDisplay");
        this.displayVideo = GameObject.CreatePrimitive(PrimitiveType.Quad);
        this.displayVideo.tag = "DisplayVideo";

        this.audioSource = this.gameObject.AddComponent<AudioSource>();
        this.videoPlayer = this.gameObject.AddComponent<VideoPlayer>();
        this.videoPlayer.playOnAwake = false;
       //this.camera = this.gameObject.AddComponent<Camera>();
        //this.camera.enabled = false;
        //this.camera.orthographic = true;
        //this.camera.cullingMask = LayerMask.GetMask("Nothing");
        //this.camera.depth = this.zIndex;
        this.videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        this.videoPlayer.source = VideoSource.Url;
        this.videoPlayer.SetTargetAudioSource(0, this.audioSource);
        this.videoPlayer.targetMaterialRenderer = displayVideo.GetComponent<MeshRenderer>();
        this.videoPlayer.renderMode = VideoRenderMode.MaterialOverride;
        this.videoPlayer.aspectRatio = VideoAspectRatio.Stretch;
        //this.videoPlayer.targetCamera = this.camera;

        this.videoPlayer.isLooping = false;
        this.videoPlayer.url = this.url;
        //this.GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", videoPlayer.texture);
        /*GameObject text = new GameObject();
        TextMesh t = text.AddComponent<TextMesh>();
        t.text = "new text set";
        t.fontSize = 30;
        t.transform.localEulerAngles += new Vector3(90, 0, 0);
        t.transform.localPosition += new Vector3(56f, 3f, 40f);*/
    }

    private void SetMediaSize()
    {
        //this.camera.GetComponent<Camera>().rect = rect;

        this.displayVideo.SetActive(false);
        this.displayVideo.transform.parent = this.anchorDisplay.transform;
        this.displayVideo.transform.localPosition = new Vector3(rect.x+ rect.width/2, rect.y+ rect.height/2, this.anchorDisplay.transform.localPosition.z-(1+this.zIndex)*0.001f);
        this.displayVideo.transform.localScale = new Vector3(rect.width, rect.height, 1);
    }
    private void SetMediaVolume()
    {
        this.audioSource.volume = volume / 100;
    }
    
    public void Play()
    {        
        if(videoPlayer.isPlaying) videoPlayer.Stop();        
        videoPlayer.Play();

        this.displayVideo.SetActive(true);
        //this.camera.enabled = true;
        Debug.Log(url + " begun");       

        if (OnBegin != null) OnBegin();
    }

    public void Stop() {
        videoPlayer.Stop();
        //this.camera.enabled = false;
        this.displayVideo.SetActive(false);
        Debug.Log(url + " stopou");
        if (OnEnd != null) OnEnd();
    }

    private void Ended(VideoPlayer source)
    {
        if (OnEnd != null) OnEnd();
        this.displayVideo.SetActive(false);
        Debug.Log(url + " ended");
        //this.camera.enabled = false;
    }


}
