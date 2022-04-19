using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTrap : MonoBehaviour
{
    public GameObject blueRegular;
    public GameObject blueTrap;
    public GameObject violetRegular;
    public GameObject violetTrap;

    public Transform blueLoc;
    public Transform violetLoc;

    void Start()
    {
        bool isBlueTrap = (Random.value >= 0.5f);
        if (isBlueTrap)
        {
            Instantiate(blueTrap, blueLoc.position, Quaternion.identity);
            Instantiate(violetRegular, violetLoc.position, Quaternion.identity);
        }
        else
        {
            Instantiate(blueRegular, blueLoc.position, Quaternion.identity);
            Instantiate(violetTrap, violetLoc.position, Quaternion.identity);
        }
    }
}
