using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MediaCollision : MonoBehaviour {
	private Text text;
    private MediaControllerScript controller;

	void Start () 
	{
		text = GameObject.FindGameObjectWithTag ("OutText").GetComponent<Text>();
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<MediaControllerScript>();
    }
   
	void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Condition"))
        {
            ConditionActionK colK = collision.gameObject.GetComponent<ConditionAction>().MyKind;
            gameObject.GetComponent<MediaKind>().MyKind = colK;
            gameObject.tag = "ConditionMedia";
        }
        else if (collision.gameObject.CompareTag("Action"))
        {
            ConditionActionK colK = collision.gameObject.GetComponent<ConditionAction>().MyKind;
            gameObject.GetComponent<MediaKind>().MyKind = colK;
            gameObject.tag = "ActionMedia";
        }
        /*else if (collision.gameObject.CompareTag("Initial"))
        {
            MediaKind Inst = gameObject.GetComponent<MediaKind>();
            Inst.IsInitialMedia = !Inst.IsInitialMedia;            
            if (Inst.IsInitialMedia) {
                controller.OnEntry(gameObject);
                text.text = "Port " + Inst.MediaId;
            }
            else {
                controller.RemoveEntry(gameObject);
                text.text = "Port Removed "+ Inst.MediaId;
            }
        }*/
        else if (collision.gameObject.CompareTag("Initial"))
        {
            MediaKind Inst = gameObject.GetComponent<MediaKind>();
            if (!Inst.IsInitialMedia)
            {                
                Inst.IsInitialMedia = true;
                controller.OnEntry(gameObject);
                text.text = "Port " + Inst.MediaId;
            }
            else
            {
                Inst.IsInitialMedia = false;
                controller.RemoveEntry(gameObject);
            }
        }
        else if (gameObject.CompareTag("ConditionMedia"))
        {
            MediaKind Inst = gameObject.GetComponent<MediaKind>();
            MediaKind colInst = collision.gameObject.GetComponent<MediaKind>();
            if (collision.gameObject.CompareTag("ActionMedia"))
            {
                 /*Debug.Log(
                    Enum.GetName(typeof(ConditionActionK), Inst.MyKind) + " " + Inst.MediaId + " " +
                    Enum.GetName(typeof(ConditionActionK), colInst.MyKind) + " " + colInst.MediaId + " "
                );*/
                text.text = Enum.GetName(typeof(ConditionActionK), Inst.MyKind) + " " + Inst.MediaId + " " +
                            Enum.GetName(typeof(ConditionActionK), colInst.MyKind) + " " + colInst.MediaId + " ";
                controller.CreateLink(gameObject, collision.gameObject);
            }
        }
        
	}

}
