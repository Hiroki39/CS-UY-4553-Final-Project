using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoController : MonoBehaviour
{
    public TMP_Text promptText;
    public GameObject Instruction;
    // Start is called before the first frame update
    void Start()
    {
        if (PublicVars.infoShow)
        {
            PublicVars.infoShow = true;
            Instruction.SetActive(true);
            promptText.text = "Hide";
        }
        else
        {
            PublicVars.infoShow = false;
            Instruction.SetActive(false);
            promptText.text = "Info";
        }
    }
    public void ShowHide()
    {
        if (PublicVars.infoShow)
        {
            PublicVars.infoShow = false;
            Instruction.SetActive(false);
            promptText.text = "Info";
        }
        else
        {
            PublicVars.infoShow = true;
            Instruction.SetActive(true);
            promptText.text = "Hide";
        }
    }
}
