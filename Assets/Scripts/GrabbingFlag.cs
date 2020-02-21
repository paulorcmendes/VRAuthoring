using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingFlag : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isGrabed;

    public Vector3 initialPos;
    public Quaternion initialRot;

    void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;

    }
    void Awake()
    {
        isGrabed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
