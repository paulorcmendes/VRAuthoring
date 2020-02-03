using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LinkKind
{
    onBeginStart, onBeginStop, onEndStart, onEndStop
}

public class Link 
{
    private LinkKind linkKind;
    private GameObject mediaCondition;
    private GameObject mediaAction;
    private string[] description;

    public Link (LinkKind linkKind, GameObject mediaCondition, GameObject mediaAction, string[] description)
    {
        this.linkKind = linkKind;
        this.mediaCondition = mediaCondition;
        this.mediaAction = mediaAction;
        this.description = description;
    }
    public string[] Description
    {
        get
        {
            return this.description;
        }
    }
    public override string ToString()
    {
        return description[0] +" "+ description[1] + " " + description[2] + " " + description[3];
    }
    public LinkKind LinkKind
    {
        get
        {
            return this.linkKind;
        }
    }

    public GameObject MediaCondition
    {
        get
        {
            return this.mediaCondition;
        }
    }

    public GameObject MediaAction
    {
        get
        {
            return this.mediaAction;
        }
    }
}
