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
    bool pickedYellow = false;
    bool pickedGreen = false;
    bool pickedRed = false;
    int popUpIndex = 0;
    private void Start()
    {
        goal.SetActive(false);
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
                    StartCoroutine(ChangePopUp(2f));
                }
            }
            else if (popUpIndex == 1)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    StartCoroutine(ChangePopUp(2f));
                }
            }
            else if (popUpIndex == 2)
            {
                playerScript.jumpForce = 5;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(ChangePopUp(5f));
                    tempWalls[0].SetActive(false);
                }
            }
            else if (popUpIndex == 3)
            {
                StartCoroutine(ChangePopUp(3f));
                tempWalls[1].SetActive(false);
            }
            else if (popUpIndex == 4 && PublicVars.movedToLastPlatform)
            {
                StartCoroutine(ChangePopUp(1f));
            }
            else if (popUpIndex == 5 && pickedYellow)
            {
                StartCoroutine(ChangePopUp(3f));
            }
            else if (popUpIndex == 6 && pickedGreen)
            {
                StartCoroutine(ChangePopUp(3f));
            }
            else if (popUpIndex == 7 && pickedRed)
            {
                StartCoroutine(ChangePopUp(3f));
            }
            else if (popUpIndex == 8)
            {
                StartCoroutine(ChangePopUp(4f));
            }
            else if (popUpIndex == 9)
            {
                goal.SetActive(true);
                StartCoroutine(ChangePopUp(4f));
                tempWalls[2].SetActive(false);
            }
        }
    }

    public IEnumerator ChangePopUp(float waitTime)
    {
        popUpChanging = true;
        yield return new WaitForSeconds(waitTime);
        popUps[popUpIndex++].SetActive(false);
        if (popUpIndex < popUps.Length)
        {
            popUps[popUpIndex].SetActive(true);
        }
        popUpChanging = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gem2"))
        {
            pickedRed = true;
        }

        if (other.gameObject.CompareTag("Gem3"))
        {
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            pickedGreen = true;
        }

        if (other.gameObject.CompareTag("Gem4"))
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            pickedYellow = true;
        }
    }
}
