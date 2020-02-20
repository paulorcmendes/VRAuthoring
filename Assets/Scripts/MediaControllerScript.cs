using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Valve.VR;

public enum CurrentMode {EDITING, PLAYING}

public class MediaControllerScript : MonoBehaviour {

    public delegate void MyHandler();
    public CurrentMode myMode;
    //public GameObject ARCamera;
    public event MyHandler Port;
    private NCLParser nclParser;
    private LinksControllerScript linksController;
    private List<GameObject> portsList;
    public GameObject canvas;
    private StructuralViewScript structural;
    private GameObject desk;
    private GameObject mytv;
    // Use this for initialization

    public SteamVR_Action_Boolean PlayPause;
    // a reference to the hand
    public SteamVR_Input_Sources handType;

    void Start () {
        //ARCamera = GameObject.FindGameObjectWithTag("ARCamera");
        portsList = new List<GameObject>();
        myMode = CurrentMode.EDITING;
        nclParser = GameObject.FindGameObjectWithTag("GameController").GetComponent<NCLParser>();
        linksController = GameObject.FindGameObjectWithTag("LinksController").GetComponent<LinksControllerScript>();
        structural = GameObject.FindGameObjectWithTag("StructuralView").GetComponent<StructuralViewScript>();
        desk = GameObject.FindGameObjectWithTag("Desk");
        mytv = GameObject.FindGameObjectWithTag("MyTV");
        PlayPause.AddOnStateDownListener(EmpunhaduraDown, handType);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)) SwitchMyMode();
	}

    public void EmpunhaduraDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        SwitchMyMode();
    }
          
    public void SwitchMyMode()
    {
        if (myMode == CurrentMode.EDITING)
        {
            canvas.SetActive(false);
            //ARCamera.SetActive(false);
            myMode = CurrentMode.PLAYING;
            ApplyPorts();
            linksController.ApplyLinks();
            Play();
            nclParser.Save();
            desk.GetComponent<FadeScript>().StartFade(new Vector3(0f,-1f, 0.411f));
            mytv.GetComponent<FadeScript>().StartFade(new Vector3(0f, 1f, 2f));
        }
        else
        {
            RemovePorts();
            linksController.RemoveLinks();
            nclParser.StartNewDocument();
            //ARCamera.SetActive(true);
            canvas.SetActive(true);
            myMode = CurrentMode.EDITING;

            var displaysVideo = GameObject.FindGameObjectsWithTag("DisplayVideo");
            
            foreach (GameObject displayVideo in displaysVideo)
            {
                displayVideo.SetActive(false);
            }
            //desk.SetActive(true);
            desk.GetComponent<FadeScript>().StartFade();
            mytv.GetComponent<FadeScript>().StartFade();
        }
    }

    private void Play() {
        if(Port != null)  this.Port();
    }
    private void ApplyPorts()
    {
        foreach (GameObject media in portsList)
        {
            this.Port += media.GetComponent<MediaSettings>().Play;
            nclParser.AddPort(media.GetComponent<MediaKind>().MediaId);
        }
    }
    private void RemovePorts()
    {
        foreach (GameObject media in portsList)
        {
            this.Port -= media.GetComponent<MediaSettings>().Play;
        }
    }
    public void OnEntry(GameObject media)
    {
        Debug.Log("Port " + media.GetComponent<MediaKind>().MediaId);
        portsList.Add(media);
        structural.AddEntry(media);
    }
    public void RemoveEntry(GameObject media)
    {
        Debug.Log("Port Removed " + media.GetComponent<MediaKind>().MediaId);
        portsList.Remove(media);
        structural.RemoveEntry(media);
    }
    public void CreateLink(GameObject mediaCondition, GameObject mediaAction) {
        string condition = Enum.GetName(typeof(ConditionActionK), mediaCondition.GetComponent<MediaKind>().MyKind);
        string media1 = mediaCondition.GetComponent<MediaKind>().MediaId;
        string action = Enum.GetName(typeof(ConditionActionK), mediaAction.GetComponent<MediaKind>().MyKind);
        string media2 = mediaAction.GetComponent<MediaKind>().MediaId;
        string[] description = { condition, media1, action, media2 };

        Debug.Log(condition + " " + media1 + " " + action + " " + media2);
        //nclParser.AddLink(condition, media1, action, media2);

        switch (mediaCondition.GetComponent<MediaKind>().MyKind) {
            case ConditionActionK.onBegin:
                switch (mediaAction.GetComponent<MediaKind>().MyKind) {
                    case ConditionActionK.Start:
                        //ConnectorBase.OnBeginStart(mediaCondition, mediaAction);
                        linksController.AddLink(new Link(LinkKind.onBeginStart, mediaCondition, mediaAction, description));
                        break;
                    case ConditionActionK.Stop:
                        //ConnectorBase.OnBeginStop(mediaCondition, mediaAction);
                        linksController.AddLink(new Link(LinkKind.onBeginStop, mediaCondition, mediaAction, description));
                        break;
                }
                break;
            case ConditionActionK.onEnd:
                switch (mediaAction.GetComponent<MediaKind>().MyKind)
                {
                    case ConditionActionK.Start:
                        //ConnectorBase.OnEndStart(mediaCondition, mediaAction);
                        linksController.AddLink(new Link(LinkKind.onEndStart, mediaCondition, mediaAction, description));
                        break;
                    case ConditionActionK.Stop:
                        linksController.AddLink(new Link(LinkKind.onEndStop, mediaCondition, mediaAction, description));
                        //ConnectorBase.OnEndStop(mediaCondition, mediaAction);
                        break;
                }
                break;
        }
     }
}
