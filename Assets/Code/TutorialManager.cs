using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private bool changedPopUp = false;
    private bool canChangePopUp = true;
    private bool showTempWall = true;
    public int popUpIndex;
    public float waitTime;
    public GameObject player;
    private PlayerMove playerScript;
    public GameObject tempwall;
    public GameObject tempWall2;
    public GameObject tempWall3;
    public GameObject goal;

    private void Start()
    {
        for (int popUpIndex = 0; popUpIndex < popUps.Length; ++popUpIndex)
        {
            popUps[popUpIndex].SetActive(false);
        }
        playerScript = player.GetComponent<PlayerMove>();
        playerScript.jumpForce = 0;
        waitTime = 2f;
        PublicVars.disableGoal = true;
    }

    private void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                if (canChangePopUp)
                {
                    canChangePopUp = false;
                    changedPopUp = false;
                    popUps[popUpIndex].SetActive(true);
                }
            }
        }
        if (popUpIndex == 0 && !canChangePopUp && !changedPopUp)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                StartCoroutine(ChangePopUp());
                changedPopUp = true;
            }
        }
        else if (popUpIndex == 1 && !canChangePopUp && !changedPopUp)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartCoroutine(ChangePopUp());
                changedPopUp = true;
            }
        }
        else if (popUpIndex == 2 && !canChangePopUp && !changedPopUp)
        {
            playerScript.jumpForce = 5;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                waitTime = 5f;
                StartCoroutine(ChangePopUp());
                changedPopUp = true;
                tempWall2.SetActive(false);
            }
        }
        else if (popUpIndex == 3 && !canChangePopUp && !changedPopUp && playerScript.jumped)
        {
            waitTime = 3f;
            StartCoroutine(ChangePopUp());
            changedPopUp = true;
            tempWall3.SetActive(false);
        }
        else if (popUpIndex == 4 && !canChangePopUp && !changedPopUp && PublicVars.movedPlatformFirstTime)
        {
            showTempWall = false;
            waitTime = 1f;
            StartCoroutine(ChangePopUp());
            changedPopUp = true;
        }
        else if (popUpIndex == 5 && !canChangePopUp && !changedPopUp && PublicVars.pickedYellow)
        {
            waitTime = 3f;
            StartCoroutine(ChangePopUp());
            changedPopUp = true;
        }
        else if (popUpIndex == 6 && !canChangePopUp && !changedPopUp && PublicVars.pickedGreen)
        {
            StartCoroutine(ChangePopUp());
            changedPopUp = true;
        }
        else if (popUpIndex == 7 && !canChangePopUp && !changedPopUp && PublicVars.pickedRed)
        {
            StartCoroutine(ChangePopUp());
            changedPopUp = true;
        }
        else if (popUpIndex == 8 && !canChangePopUp && !changedPopUp && PublicVars.pickedRed)
        {
            waitTime = 4f;
            StartCoroutine(ChangePopUp());
            changedPopUp = true;
        }
        else if (popUpIndex == 9 && !canChangePopUp && !changedPopUp)
        {
            goal.SetActive(true);
            waitTime = 4f;
            StartCoroutine(ChangePopUp());
            changedPopUp = true;
            PublicVars.disableGoal = false;
        }
    }

    public IEnumerator ChangePopUp()
    {
        yield return new WaitForSeconds(waitTime);
        tempwall.SetActive(showTempWall);
        popUps[popUpIndex++].SetActive(false);
        canChangePopUp = true;
    }
}
