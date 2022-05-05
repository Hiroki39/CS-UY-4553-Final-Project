using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyManager : MonoBehaviour
{
    public void Start()
    {
        AdjustTransparency(0.35f);
    }
    public void AdjustTransparency(float alpha)
    {
        GameObject[] grounds1 = GameObject.FindGameObjectsWithTag("Ground1");
        GameObject[] grounds2 = GameObject.FindGameObjectsWithTag("Ground2");
        foreach (GameObject ground in grounds1)
        {
            Color tmpColor = ground.GetComponent<Renderer>().material.color;
            tmpColor.a = alpha;
            ground.GetComponent<Renderer>().material.color = tmpColor;
        }
        foreach (GameObject ground in grounds2)
        {
            Color tmpColor = ground.GetComponent<Renderer>().material.color;
            tmpColor.a = alpha;
            ground.GetComponent<Renderer>().material.color = tmpColor;
        }
    }

};