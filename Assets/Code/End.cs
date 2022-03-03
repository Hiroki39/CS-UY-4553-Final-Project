using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public GameObject end;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            showEnd();
        }
    }

    void showEnd()
    {
        end.SetActive(true);
    }
}
