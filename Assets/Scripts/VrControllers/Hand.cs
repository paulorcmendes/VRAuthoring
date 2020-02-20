using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
    // a reference to the action
    public SteamVR_Action_Boolean SphereOnOff;
    // a reference to the hand
    public SteamVR_Input_Sources handType;
    // Start is called before the first frame update

    private GameObject grabing_object;

    public GameObject desk;

    private Vector3 last_pos;
    private Vector3 last_scale;
    private Quaternion last_rot;
    private GameObject multimedia;
    void Start()
    {
        SphereOnOff.AddOnStateDownListener(TriggerDown, handType);
        SphereOnOff.AddOnStateUpListener(TriggerUp, handType);
        multimedia = GameObject.FindGameObjectWithTag("Multimedia");
        grabing_object = null;
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is up");
        if(grabing_object != null)
        {
            Debug.Log("Released");
            grabing_object.transform.parent = multimedia.transform;
            grabing_object.transform.position = last_pos;
            grabing_object.transform.rotation = last_rot;
            grabing_object.transform.localScale = last_scale;
            //Debug.Log(last_pos.position);
            //grabing_object.GetComponent<BoxCollider>().enabled = true;
            // this.GetComponent<BoxCollider>().enabled = true;

            grabing_object.GetComponent<GrabbingFlag>().isGrabed = false;
            grabing_object = null;
        }
    }
    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down");
        if (grabing_object != null )
        {
            Debug.Log("Set Parent");
            last_pos = grabing_object.transform.position;
            last_rot = grabing_object.transform.rotation;
            last_scale = grabing_object.transform.localScale;
            //Debug.Log("Pegou pos "+last_pos.position);
            grabing_object.transform.parent = gameObject.transform;
            grabing_object.GetComponent<GrabbingFlag>().isGrabed = true;
            //grabing_object.GetComponent<BoxCollider>().enabled = false;
            //this.GetComponent<BoxCollider>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        bool someTag = collision.gameObject.CompareTag("Initial") || collision.gameObject.CompareTag("OriginalMedia") || collision.gameObject.CompareTag("Action") || collision.gameObject.CompareTag("Condition") || collision.gameObject.CompareTag("ConditionMedia") || collision.gameObject.CompareTag("ActionMedia") || collision.gameObject.CompareTag("Delete");
        if (grabing_object == null && someTag && collision.gameObject.GetComponent<GrabbingFlag>().isGrabed == false)
        {            
            grabing_object = collision.gameObject;
            //grabing_object.GetComponent<GrabbingFlag>().isGrabed = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        bool someTag = collision.gameObject.CompareTag("Initial") || collision.gameObject.CompareTag("OriginalMedia") || collision.gameObject.CompareTag("Action") || collision.gameObject.CompareTag("Condition") || collision.gameObject.CompareTag("ConditionMedia") || collision.gameObject.CompareTag("ActionMedia") || collision.gameObject.CompareTag("Delete");
        if (grabing_object == null && someTag && collision.gameObject.GetComponent<GrabbingFlag>().isGrabed == false)
        {
            grabing_object = collision.gameObject;
           //grabing_object.GetComponent<GrabbingFlag>().isGrabed = true;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.Equals(grabing_object))
        {
            //grabing_object.GetComponent<GrabbingFlag>().isGrabed = false;
            grabing_object = null;
        }
    }


}
