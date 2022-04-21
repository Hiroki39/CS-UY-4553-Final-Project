using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLeftRight : MonoBehaviour
{
    float speed = .2f;
    float distance = 8;
    float startX;

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.SmoothStep(startX - distance, startX + distance, Mathf.PingPong(Time.time * speed, 1));
        transform.position = newPosition;
    }
}