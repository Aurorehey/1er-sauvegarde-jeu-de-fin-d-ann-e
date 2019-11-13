using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoAction: MonoBehaviour
{
    public bool needItem = false;
    [Header("si Item est Vrai")]
    public string itemType;
    public string itemID;// let null insector if not necessary


    public void DoActionNow()
    {
        gameObject.SetActive(false);
    }

}
