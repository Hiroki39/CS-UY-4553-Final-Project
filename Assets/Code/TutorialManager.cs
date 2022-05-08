using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private PlayerMove playerScript;
    public GameObject[] tempWalls;
    public GameObject goal;

    bool popUpChanging = false;
    bool pickedBig = false;
    bool pickedSmall = false;
    bool pickedCheckpoint = false;
    bool movedToLastPlatform = false;
    bool jumped = false;
    int popUpIndex = 0;
    private void Start()
    {
        goal.SetActive(false);
        popUps[0].SetActive(true);
        for (int i = 1; i < popUps.Length; ++i)
        {
            popUps[i].SetActive(false);
        }
        playerScript = GetComponent<PlayerMove>();
        playerScript.jumpForce = 0;
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
            else if (popUpIndex == 8 && pickedBig)
            {
                StartCoroutine(ChangePopUp(3f, -1));
            }
            else if (popUpIndex == 9 && pickedSmall)
            {
                StartCoroutine(ChangePopUp(3f, -1));
            }
            else if (popUpIndex == 10 && pickedCheckpoint)
            {
                StartCoroutine(ChangePopUp(3f, -1));
            }
            else if (popUpIndex == 11)
            {
                StartCoroutine(ChangePopUp(4f, -1));
            }
            else if (popUpIndex == 12)
            {
                StartCoroutine(ChangePopUp(4f, -1));
            }
            else if (popUpIndex == 13)
            {
                StartCoroutine(ChangePopUp(2f, -1));
            }
            else if (popUpIndex == 14)
            {
                StartCoroutine(ChangePopUp(5f, 2));
            }
        }
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
        if (other.gameObject.CompareTag("Gem3"))
        {
            pickedSmall = true;
        }
        if (other.gameObject.CompareTag("Gem4"))
        {
            pickedBig = true;
        }
        if (other.gameObject.CompareTag("JumpPlane"))
        {
            jumped = true;
        }
    }
}
