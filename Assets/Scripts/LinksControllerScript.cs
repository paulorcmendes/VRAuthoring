using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinksControllerScript : MonoBehaviour
{
    public GameObject linkPrefab;
    private Dictionary<GameObject, Link> links;
    private float lastRemoval;
    private float delayRemoval;
    private NCLParser nclParser;
    private StructuralViewScript structural;
    // Start is called before the first frame update
    void Start()
    {
        delayRemoval = 5f;
        links = new Dictionary<GameObject, Link>();
        lastRemoval = -delayRemoval;
        nclParser = GameObject.FindGameObjectWithTag("GameController").GetComponent<NCLParser>();
        structural = GameObject.FindGameObjectWithTag("StructuralView").GetComponent<StructuralViewScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLink(Link link)
    {
        GameObject linkObject = Instantiate(linkPrefab, transform, false);
        LinkCollision linkCollision = linkObject.GetComponent<LinkCollision>();
        linkCollision.Description = link.ToString();
        linkCollision.linksController = this;
        linkObject.transform.Translate(new Vector3(0,1.5f*links.Count));
        links.Add(linkObject, link);
        structural.AddLinkToGraph(link);
    }

    public void RemoveLink(GameObject linkObject)
    {
        float currentTime = Time.time;
        if (currentTime > lastRemoval + delayRemoval)
        {
            lastRemoval = currentTime;
            structural.RemoveLinkFromGraph(links[linkObject]);
            this.links.Remove(linkObject);
            Destroy(linkObject);
            UpdatePosLinks();
        }
    }

    public void ApplyLinks()
    {
        foreach (KeyValuePair<GameObject, Link> pair in links)
        {
            Link link = pair.Value;
            nclParser.AddLink(link.Description[0], link.Description[1], link.Description[2], link.Description[3]);

            switch (link.LinkKind)
            {
                case(LinkKind.onBeginStart):
                    ConnectorBase.OnBeginStart(link.MediaCondition, link.MediaAction);
                    break;
                case (LinkKind.onBeginStop):
                    ConnectorBase.OnBeginStop(link.MediaCondition, link.MediaAction);
                    break;
                case (LinkKind.onEndStart):
                    ConnectorBase.OnEndStart(link.MediaCondition, link.MediaAction);
                    break;
                case (LinkKind.onEndStop):
                    ConnectorBase.OnEndStop(link.MediaCondition, link.MediaAction);
                    break;
            }
        }
    }

    public void RemoveLinks()
    {
        foreach (KeyValuePair<GameObject, Link> pair in links)
        {
            Link link = pair.Value;
            switch (link.LinkKind)
            {
                case (LinkKind.onBeginStart):
                    ConnectorBase.RemoveOnBeginStart(link.MediaCondition, link.MediaAction);
                    break;
                case (LinkKind.onBeginStop):
                    ConnectorBase.RemoveOnBeginStop(link.MediaCondition, link.MediaAction);
                    break;
                case (LinkKind.onEndStart):
                    ConnectorBase.RemoveOnEndStart(link.MediaCondition, link.MediaAction);
                    break;
                case (LinkKind.onEndStop):
                    ConnectorBase.RemoveOnEndStop(link.MediaCondition, link.MediaAction);
                    break;
            }
        }
    }
    private void UpdatePosLinks() {
        int i = 0;
        foreach (KeyValuePair<GameObject,Link> pair in links)
        {
            pair.Key.transform.localPosition = new Vector3(0, 1.5f * i);
            i++;
        }
    } 
}
