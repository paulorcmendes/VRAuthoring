using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorBase : MonoBehaviour {

    public static void OnEndStart(GameObject mediaCondition, GameObject mediaAction)
    {
        mediaCondition.GetComponent<MediaSettings>().OnEnd += mediaAction.GetComponent<MediaSettings>().Play;
    }
    public static void OnEndStop(GameObject mediaCondition, GameObject mediaAction)
    {
        mediaCondition.GetComponent<MediaSettings>().OnEnd += mediaAction.GetComponent<MediaSettings>().Stop;
    }
    public static void OnBeginStart(GameObject mediaCondition, GameObject mediaAction)
    {
        mediaCondition.GetComponent<MediaSettings>().OnBegin += mediaAction.GetComponent<MediaSettings>().Play;
    }
    public static void OnBeginStop(GameObject mediaCondition, GameObject mediaAction)
    {
        mediaCondition.GetComponent<MediaSettings>().OnBegin += mediaAction.GetComponent<MediaSettings>().Stop;
    }

    //REMOÇÕES
    public static void RemoveOnEndStart(GameObject mediaCondition, GameObject mediaAction)
    {
        mediaCondition.GetComponent<MediaSettings>().OnEnd -= mediaAction.GetComponent<MediaSettings>().Play;
    }
    public static void RemoveOnEndStop(GameObject mediaCondition, GameObject mediaAction)
    {
        mediaCondition.GetComponent<MediaSettings>().OnEnd -= mediaAction.GetComponent<MediaSettings>().Stop;
    }
    public static void RemoveOnBeginStart(GameObject mediaCondition, GameObject mediaAction)
    {
        mediaCondition.GetComponent<MediaSettings>().OnBegin -= mediaAction.GetComponent<MediaSettings>().Play;
    }
    public static void RemoveOnBeginStop(GameObject mediaCondition, GameObject mediaAction)
    {
        mediaCondition.GetComponent<MediaSettings>().OnBegin -= mediaAction.GetComponent<MediaSettings>().Stop;
    }
}
