using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject popUpsParent;
    private PlayerMove playerScript;
    public GameObject[] tempWalls;
    public GameObject goal;
    public GameObject trapGround;

    GameObject[] popUps;
    bool popUpChanging = false;
    bool pickedBig = false;
    bool pickedSmall = false;
    bool pickedCheckpoint = false;
    bool movedToLastPlatform = false;
    bool jumped = false;
    // int popUpIndex = 0;

    private void Start()
    {
        popUps = new GameObject[popUpsParent.transform.childCount];
        for (int i = 0; i < popUpsParent.transform.childCount; i++)
        {
            popUps[i] = popUpsParent.transform.GetChild(i).gameObject;
        }

        goal.SetActive(false);

        if (new List<int> { 3, 7, 13, 15 }.Contains(PublicVars.popUpIndex))
        {
            PublicVars.popUpIndex--;
        }

        popUps[PublicVars.popUpIndex].SetActive(true);
        for (int i = 0; i < popUps.Length; ++i)
        {
            if (i != PublicVars.popUpIndex)
            {
                popUps[i].SetActive(false);
            }
        }

        trapGround.GetComponent<FallingPlatform>().landed = true;

        playerScript = GetComponent<PlayerMove>();
        if (PublicVars.popUpIndex < 3)
        {
            playerScript.jumpForce = 0f;
        }
    }

    private void Update()
    {
        if (!popUpChanging)
        {
            if (PublicVars.popUpIndex == 0)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    StartCoroutine(ChangePopUp(2f, -1));
                }
            }
            else if (PublicVars.popUpIndex == 1)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    StartCoroutine(ChangePopUp(2f, -1));
                }
            }
            else if (PublicVars.popUpIndex == 2)
            {
                playerScript.jumpForce = 5;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(ChangePopUp(2f, 0));
                }
            }
            else if (PublicVars.popUpIndex == 3 && jumped)
            {
                StartCoroutine(ChangePopUp(1f, -1));
            }
            else if (PublicVars.popUpIndex == 4)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A)
                || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                {
                    StartCoroutine(ChangePopUp(6f, -1));
                }
            }
            else if (PublicVars.popUpIndex == 5 && Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(ChangePopUp(4f, -1));
            }
            else if (PublicVars.popUpIndex == 6)
            {
                StartCoroutine(ChangePopUp(4f, 1));
            }
            else if (PublicVars.popUpIndex == 7 && movedToLastPlatform)
            {
                StartCoroutine(ChangePopUp(1f, -1));
            }
            else if (PublicVars.popUpIndex == 8)
            {
                StartCoroutine(ChangePopUp(4f, -1));
            }
            else if (PublicVars.popUpIndex == 9)
            {
                StartCoroutine(ChangePopUp(4f, -1));
            }
            else if (PublicVars.popUpIndex == 10 && pickedBig)
            {
                StartCoroutine(ChangePopUp(4f, -1));
            }
            else if (PublicVars.popUpIndex == 11 && pickedSmall)
            {
                StartCoroutine(ChangePopUp(3f, -1));
            }
            else if (PublicVars.popUpIndex == 12 && pickedCheckpoint)
            {
                trapGround.GetComponent<FallingPlatform>().landed = false;
                StartCoroutine(ChangePopUp(1.5f, -1));
                playerScript.slowdownFactor = 0.1f;
                StartCoroutine(playerScript.DoSlowmo(1.5f, 5f));
            }
            else if (PublicVars.popUpIndex == 13 && gameObject.transform.position.y > 0)
            {
                playerScript.slowdownFactor = 0.2f;
                StartCoroutine(ChangePopUp(3f, -1));
            }
            else if (PublicVars.popUpIndex == 14)
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
        popUps[PublicVars.popUpIndex++].SetActive(false);
        if (PublicVars.popUpIndex < popUps.Length)
        {
            popUps[PublicVars.popUpIndex].SetActive(true);
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
        if (other.gameObject.CompareTag("Gem2Ex"))
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
        else if (other.gameObject.CompareTag("CheckPointPlane"))
        {
            CheckPointAttribute checkPointAttr = other.gameObject.GetComponent<CheckPointAttribute>();
            PublicVars.checkPointIsBlue = checkPointAttr.isBlue;
            PublicVars.checkPointScale = checkPointAttr.scale;
            PublicVars.checkPointPosition = other.gameObject.transform.position;
            Destroy(other.gameObject);
        }
    }


}
