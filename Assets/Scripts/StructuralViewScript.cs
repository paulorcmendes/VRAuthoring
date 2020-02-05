using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructuralViewScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Dictionary<GameObject, GameObject> mediaObjects;
    private Dictionary<Link, GameObject> links;
    public float dist;
    private int size;
    public GameObject linkGraphPrefab;
    public GameObject initialStructPrefab;

    private int FindSizeOfSquare(int qtd)
    {
        float sqrt = Mathf.Sqrt((float)qtd);

        if(sqrt == Mathf.Floor(sqrt))
        {
            return (int)sqrt;
        }
        else
        {
            return (int)sqrt + 1;
        }
    }

    public void AddLinkToGraph(Link link)
    {
        GameObject from = mediaObjects[link.MediaCondition];
        GameObject to = mediaObjects[link.MediaAction];
        Vector3 position = (from.transform.localPosition + to.transform.localPosition)/2;
        Vector3 rotation = (to.transform.localPosition + from.transform.localPosition);

        GameObject newLink = Instantiate(linkGraphPrefab, transform, false);
        newLink.transform.localPosition = position;
        Vector3 distMedia = to.transform.position - from.transform.position;
        newLink.transform.up = distMedia;
        newLink.transform.localScale = new Vector3(newLink.transform.localScale.x, newLink.transform.localScale.y *1.1f, newLink.transform.localScale.z);
        MeshRenderer[] meshes = newLink.GetComponentsInChildren<MeshRenderer>();
        Material condMat = null, actMat= null;
        foreach(Material mat in Resources.FindObjectsOfTypeAll<Material>())
        {
            if (mat.name.Equals(link.Description[0]))
            {
                condMat = mat;
            }
            if (mat.name.Equals(link.Description[2]))
            {
                actMat = mat;
            }
        }
        
        meshes[1].material = condMat;
        meshes[2].material = actMat;

        links.Add(link, newLink);
    }
    public void RemoveLinkFromGraph(Link link)
    {
        GameObject linkToBeRemoved = links[link];
        links.Remove(link);
        Destroy(linkToBeRemoved);
    }

    public void AddEntry(GameObject media)
    {
        GameObject myMedia = mediaObjects[media];
        Instantiate(initialStructPrefab, myMedia.transform, false);
    }
    public void RemoveEntry(GameObject media)
    {
        GameObject myMedia = mediaObjects[media];
        foreach (Transform child in myMedia.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void Start()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("OriginalMedia");
        this.mediaObjects = new Dictionary<GameObject, GameObject>();
        this.links = new Dictionary<Link, GameObject>();
        this.size = objects.Length;
        float i, j;
        float angStep = 2*Mathf.PI/ size;
        float curAng = 0;
        foreach(GameObject media in objects)
        {
            i = Mathf.Cos(curAng);
            j = Mathf.Sin(curAng);
            GameObject myMedia = new GameObject();
            myMedia.transform.parent = transform;
            myMedia.transform.localPosition = new Vector3(i*dist, j*dist);
            myMedia.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            myMedia.transform.Rotate(new Vector3(0,180,0)); 

            MeshFilter meshFilter = myMedia.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = myMedia.AddComponent<MeshRenderer>();

            meshFilter.mesh = media.GetComponent<MeshFilter>().mesh;
            meshRenderer.materials = media.GetComponent<MeshRenderer>().materials;

            this.mediaObjects.Add(media, myMedia);
            
            curAng += angStep;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
