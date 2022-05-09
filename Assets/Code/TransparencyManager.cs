using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransparencyManager : MonoBehaviour
{
    public Slider opacitySlider;

    public void Start()
    {
        AdjustTransparency(PublicVars.opacity);
        opacitySlider.value = PublicVars.opacity;
    }

    public void AdjustTransparency(float alpha)
    {
        PublicVars.opacity = alpha;
        GameObject[] grounds1 = GameObject.FindGameObjectsWithTag("Ground1");
        GameObject[] grounds2 = GameObject.FindGameObjectsWithTag("Ground2");
        foreach (GameObject ground in grounds1)
        {
            if (ground.GetComponent<Renderer>() == null)
            {
                Debug.Log(ground.name);
            }
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