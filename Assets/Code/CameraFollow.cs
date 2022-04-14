using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;

    Vector3 initOffset;
    Vector3 currOffset;

    Vector3[] rotations;

    bool rotating = false;

    int posStatus = 0;
    float rotateTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initOffset = transform.position - player.transform.position;
        currOffset = initOffset;
        rotations = new Vector3[]{initOffset,
                Quaternion.Euler(0, 45, 0) * initOffset,
                Quaternion.Euler(30, 0, 0) * initOffset,
                Quaternion.Euler(-90, 0, 0) * initOffset,
                Quaternion.Euler(0, -45, 0) * initOffset
        };
    }

    void Update()
    {
        if (!rotating)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (posStatus == 0)
                {
                    StartCoroutine(RotateCamera(0, 1));
                }
                else if (posStatus == 1)
                {
                    StartCoroutine(RotateCamera(1, 0));
                }

            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                if (posStatus == 0)
                {
                    StartCoroutine(RotateCamera(0, 2));
                }
                else if (posStatus == 2)
                {
                    StartCoroutine(RotateCamera(2, 0));
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (posStatus == 0)
                {
                    StartCoroutine(RotateCamera(0, 3));
                }
                else if (posStatus == 3)
                {
                    StartCoroutine(RotateCamera(3, 0));
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (posStatus == 0)
                {
                    StartCoroutine(RotateCamera(0, 4));
                }
                else if (posStatus == 4)
                {
                    StartCoroutine(RotateCamera(4, 0));
                }
            }
        }
    }
    void LateUpdate()
    {
        transform.position = player.transform.position + currOffset;
        transform.LookAt(player.transform);
    }

    IEnumerator RotateCamera(int start, int end)
    {
        Vector3 startOffset = rotations[start];
        Vector3 endOffset = rotations[end];

        float currTime = 0;
        while (currTime < rotateTime)
        {
            currTime += Time.deltaTime / rotateTime;
            currOffset = Vector3.Lerp(startOffset, endOffset, currTime);
            yield return null;
        }
        currOffset = endOffset;
        rotating = false;
        posStatus = end;
        yield return null;
    }

    // float rotateY = player.transform.eulerAngles.y;
    // Vector3 targetOffset = Quaternion.Euler(0, rotateY + 90, 0) * offset;
    // currOffset = Vector3.SmoothDamp(currOffset, targetOffset, ref velocity, 3.0f, 1.5f);

    //     transform.position = player.transform.position + currOffset;
    //     transform.LookAt(player.transform);

}
