using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;
    Vector3 initOffset;
    Vector3 currOffset;
    Vector3 shakeOffset = new Vector3(0f, 0f, 0f);
    Vector3[] rotations;
    bool rotating = false;
    [HideInInspector] public bool shaking = false;
    float rotateTime = 1f;
    float shakeTime = 0.15f;
    float shakeAmplitude = 0.35f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initOffset = transform.position - player.transform.position;
        rotations = new Vector3[]{initOffset,
                Quaternion.Euler(0, 45, 0) * initOffset,
                Quaternion.Euler(30, 0, 0) * initOffset,
                Quaternion.Euler(-90, 0, 0) * initOffset,
                Quaternion.Euler(0, -45, 0) * initOffset
        };
        currOffset = rotations[PublicVars.camPos];
    }

    void Update()
    {
        if (!rotating)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (PublicVars.camPos == 0)
                {
                    StartCoroutine(RotateCamera(0, 1));
                }
                else if (PublicVars.camPos == 1)
                {
                    StartCoroutine(RotateCamera(1, 0));
                }

            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                if (PublicVars.camPos == 0)
                {
                    StartCoroutine(RotateCamera(0, 2));
                }
                else if (PublicVars.camPos == 2)
                {
                    StartCoroutine(RotateCamera(2, 0));
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (PublicVars.camPos == 0)
                {
                    StartCoroutine(RotateCamera(0, 3));
                }
                else if (PublicVars.camPos == 3)
                {
                    StartCoroutine(RotateCamera(3, 0));
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (PublicVars.camPos == 0)
                {
                    StartCoroutine(RotateCamera(0, 4));
                }
                else if (PublicVars.camPos == 4)
                {
                    StartCoroutine(RotateCamera(4, 0));
                }
            }
        }
    }
    void LateUpdate()
    {
        transform.position = player.transform.position + currOffset + shakeOffset;
        if (shaking)
        {
            transform.LookAt(player.transform.position + shakeOffset);
        }
        else
        {
            transform.LookAt(player.transform.position);
        }
    }

    IEnumerator RotateCamera(int start, int end)
    {
        rotating = true;
        Vector3 startOffset = rotations[start];
        Vector3 endOffset = rotations[end];

        float currTime = 0;
        while (currTime < rotateTime)
        {
            currTime += Time.deltaTime;
            currOffset = Vector3.Lerp(startOffset, endOffset, currTime);
            yield return null;
        }
        currOffset = endOffset;
        rotating = false;
        PublicVars.camPos = end;
        yield return null;
    }

    // called by PlayerMove.cs
    public IEnumerator ShakeCamera()
    {
        shaking = true;
        float currTime = 0;
        while (currTime < shakeTime)
        {
            currTime += Time.deltaTime;
            shakeOffset = Random.insideUnitSphere * shakeAmplitude;
            yield return null;
        }
        shakeOffset = new Vector3(0f, 0f, 0f);
        shaking = false;
        yield return false;
    }
}
