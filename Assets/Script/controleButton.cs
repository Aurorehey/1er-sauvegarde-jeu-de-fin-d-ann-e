using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleButton : MonoBehaviour
{
    public void StartButton()
    {
        GameObject mainUI = GameObject.Find("MainUI");
        mainUI.SetActive(false);
        GameObject fpsController = GameObject.Find("FPSController");
        (fpsController.GetComponent("FirstPersonController") as MonoBehaviour).enabled = true;
       

    }
}
