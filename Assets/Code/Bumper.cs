using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    // public Material lightMat;

    Renderer rend;
    Color initColor;

    float colorchangetime = 1f;

    // public GameObject redLight;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        initColor = rend.material.color;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rend.material.color = other.gameObject.GetComponent<Renderer>().material.color;
            StartCoroutine(Revert());
        }
    }

    IEnumerator Revert()
    {
        Color currColor = rend.material.color;
        float currTime = 0;
        while (currTime < colorchangetime)
        {
            currTime += Time.deltaTime / colorchangetime;
            rend.material.color = Color.Lerp(currColor, initColor, currTime);
            yield return null;
        }
        rend.material.color = initColor;
        yield return null;
    }
}
