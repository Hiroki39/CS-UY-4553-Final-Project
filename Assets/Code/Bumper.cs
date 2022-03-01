using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public Material lightMat;
    Material defaultMat;

    Renderer rend;
    public GameObject redLight;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        defaultMat = rend.material;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rend.material = lightMat;
            StartCoroutine(Revert());
            redLight.SetActive(true);
        }
    }

    IEnumerator Revert()
    {
        yield return new WaitForSeconds(.2f);
        rend.material = defaultMat;
        redLight.SetActive(false);
    }
}
