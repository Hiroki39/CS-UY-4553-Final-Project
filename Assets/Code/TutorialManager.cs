using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private bool changedPopUp = false;
    private bool canChangePopUp = true;
    public int popUpIndex;
    public float waitTime;
    public GameObject player;
    public PlayerMove playerScript;
    public GameObject tempwall;

    private void Start()
    {
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
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(ChangePopUp());
                changedPopUp = true;
            }
        }
        else if (popUpIndex == 1 && !canChangePopUp && !changedPopUp)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
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
                StartCoroutine(ChangePopUp());
                changedPopUp = true;
            }
        }
        else if (popUpIndex == 3 && !canChangePopUp && !changedPopUp)
        {
            waitTime = 3f;
            StartCoroutine(ChangePopUp());
            changedPopUp = true;
        }
        else if (popUpIndex == 4 && !canChangePopUp && !changedPopUp)
        {
            StartCoroutine(ChangePopUp());
            changedPopUp = true;
        }
        else if (popUpIndex == 5 && !canChangePopUp && !changedPopUp && PublicVars.movedPlatformFirstTime)
        {
            tempwall.SetActive(false);
            StartCoroutine(ChangePopUp());
            changedPopUp = true;
        }
        else if (popUpIndex == 6 && !canChangePopUp && !changedPopUp && PublicVars.pickedYellow)
        {
            StartCoroutine(ChangePopUp());
            changedPopUp = true;
        }
        else if (popUpIndex == 7 && !canChangePopUp && !changedPopUp && PublicVars.pickedGreen)
        {
            StartCoroutine(ChangePopUp());
            changedPopUp = true;
        }
        else if (popUpIndex == 8 && !canChangePopUp && !changedPopUp && PublicVars.pickedGreen)
        {
            StartCoroutine(ChangePopUp());
            changedPopUp = true;
            PublicVars.disableGoal = false;
        }
    }

    public IEnumerator ChangePopUp()
    {
        yield return new WaitForSeconds(waitTime);
        popUps[popUpIndex++].SetActive(false);
        canChangePopUp = true;
    }
}
