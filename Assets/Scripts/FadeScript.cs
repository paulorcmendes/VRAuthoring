using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 startpos;
    private Vector3 lastpos;
    private bool isFading;
    public float duration;
    private Vector3 fadeDirection;
    private float startTime;
    void Start()
    {
        startpos = this.transform.position;
        isFading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFading)
        {
            transform.position = Vector3.Lerp(lastpos, fadeDirection, (Time.time-startTime) / duration);
            if((transform.position-fadeDirection).magnitude < 0.01f)
            {
                isFading = false;
            }
        }
    }

    public void StartFade(Vector3 fadeDirection)
    {
        startTime = Time.time;
        this.lastpos = transform.position;
        this.fadeDirection = fadeDirection;
        isFading = true;
    }
    public void StartFade()
    {
        startTime = Time.time;
        this.lastpos = transform.position;
        this.fadeDirection = startpos;
        isFading = true;
    }
}
