using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUpDown: MonoBehaviour
{
    float speed = .2f;
    public float distance = 8;
    float startY;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = Mathf.SmoothStep(startY + distance, startY, Mathf.PingPong(Time.time * speed, 1));
        transform.position = newPosition;
    }
}