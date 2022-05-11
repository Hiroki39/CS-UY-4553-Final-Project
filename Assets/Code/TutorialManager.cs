using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject popUpsParent;
    private PlayerMove playerScript;
    public GameObject[] tempWalls;
    public GameObject goal;

    GameObject[] popUps;
    bool popUpChanging = false;
    bool pickedBig = false;
    bool pickedSmall = false;
    bool pickedCheckpoint = false;
    bool movedToLastPlatform = false;
    bool jumped = false;
    int popUpIndex = 0;

    private void Start()
    {
        popUps = new GameObject[popUpsParent.transform.childCount];
        for (int i = 0; i < popUpsParent.transform.childCount; i++)
        {
            popUps[i] = popUpsParent.transform.GetChild(i).gameObject;
        }

        goal.SetActive(false);
        popUps[0].SetActive(true);
        for (int i = 1; i < popUps.Length; ++i)
        {
            popUps[i].SetActive(false);
        }
        playerScript = GetComponent<PlayerMove>();
        playerScript.jumpForce = 0f;
    }

    private void Update()
    {
        if (!popUpChanging)
        {
            if (popUpIndex == 0)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    StartCoroutine(ChangePopUp(2f, -1));
                }
            }
            else if (popUpIndex == 1)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    StartCoroutine(ChangePopUp(2f, -1));
                }
            }
            else if (popUpIndex == 2)
            {
                playerScript.jumpForce = 5;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(ChangePopUp(2f, 0));
                }
            }
            else if (popUpIndex == 3 && jumped)
            {
                StartCoroutine(ChangePopUp(1f, -1));
            }
            else if (popUpIndex == 4)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A)
                || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                {
                    StartCoroutine(ChangePopUp(6f, -1));
                }
            }
            else if (popUpIndex == 5)
            {
                StartCoroutine(ChangePopUp(4f, -1));
            }
            else if (popUpIndex == 6)
            {
                StartCoroutine(ChangePopUp(3f, 1));
            }
            else if (popUpIndex == 7 && movedToLastPlatform)
            {
                StartCoroutine(ChangePopUp(1f, -1));
            }
            else if (popUpIndex == 8)
            {
                StartCoroutine(ChangePopUp(4f, -1));
            }
            else if (popUpIndex == 9)
            {
                StartCoroutine(ChangePopUp(4f, -1));
            }
            else if (popUpIndex == 10 && pickedBig)
            {
                StartCoroutine(ChangePopUp(4f, -1));
            }
            else if (popUpIndex == 11 && pickedSmall)
            {
                StartCoroutine(ChangePopUp(3f, -1));
            }
            else if (popUpIndex == 12 && pickedCheckpoint)
            {
                GameObject.FindGameObjectsWithTag("GroundFallTutorial")[0].GetComponent<Rigidbody>().isKinematic = false;
                StartCoroutine(ChangePopUp(0.5f, -1));
                StartCoroutine(DoSlowmo(1f, 5f));  
            }
            else if (popUpIndex == 13)
            {
                StartCoroutine(ChangePopUp(1.5f, -1));
            }
            else if (popUpIndex == 14)
            {
                StartCoroutine(ChangePopUp(4f, 2));
            }
        }
    }

    float slowdownFactor = 0.1f;

    IEnumerator DoSlowmo(float delayStartSlowmo, float delayStopSlowmo)
    {
        yield return new WaitForSeconds(delayStartSlowmo);
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        yield return new WaitForSeconds(delayStopSlowmo * slowdownFactor);
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public IEnumerator ChangePopUp(float waitTime, int wall)
    {
        popUpChanging = true;
        yield return new WaitForSeconds(waitTime);
        popUps[popUpIndex++].SetActive(false);
        if (popUpIndex < popUps.Length)
        {
            popUps[popUpIndex].SetActive(true);
        }
        if (wall == 2)
        {
            goal.SetActive(true);
        }
        if (wall != -1)
        {
            tempWalls[wall].SetActive(false);
        }
        popUpChanging = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground2") && !movedToLastPlatform)
        {
            movedToLastPlatform = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gem2"))
        {
            pickedCheckpoint = true;
        }
        else if (other.gameObject.CompareTag("Gem3"))
        {
            pickedSmall = true;
        }
        else if (other.gameObject.CompareTag("Gem4"))
        {
            pickedBig = true;
        }
        else if (other.gameObject.CompareTag("JumpPlane"))
        {
            jumped = true;
        }
    }

    
}
