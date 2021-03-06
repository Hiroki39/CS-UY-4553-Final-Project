using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBody : MonoBehaviour
{

    bool isRewinding = false;

    public float recordTime = 5f;
    public Slider rewindBar;

    List<PointInTime> pointsInTime;
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
        rewindBar.value = PublicVars.rewind;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<PlayerMove>().isDying)
        {
            if (Input.GetKeyDown(KeyCode.R) && rewindBar.value > 0)
                StartRewind();
            if (Input.GetKeyUp(KeyCode.R))
                StopRewind();
        }
    }

    void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0 && rewindBar.value > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            PublicVars.rewind -= pointInTime.timeScale;
            rewindBar.value = PublicVars.rewind;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, Time.timeScale));
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
    }
}
