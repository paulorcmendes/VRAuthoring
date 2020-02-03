using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkCollision : MonoBehaviour
{
    public string Description;
    public GameObject linkNamePrefab;
    public LinksControllerScript linksController;

    private GameObject myName;
    // Start is called before the first frame update
    void Start()
    {
        myName = Instantiate(linkNamePrefab, transform, false);
        myName.GetComponent<TextMesh>().text = this.Description;
        linksController = GameObject.FindGameObjectWithTag("LinksController").GetComponent<LinksControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Delete"))
        {
            linksController.RemoveLink(gameObject);
        }
    }
}
