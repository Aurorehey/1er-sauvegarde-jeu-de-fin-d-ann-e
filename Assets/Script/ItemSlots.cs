using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlots : MonoBehaviour
{
    public Text textItem;
    public GameObject textDisplay;


    [Header("Item's Datas")]
    public string itemType;
    public string itemID;
    public Sprite itemSprite;
    public string itemDescription;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = itemSprite; //modification de l'image dans image(script) dans unity
        textItem.text = itemDescription;
    }
    private void OnEnable()
    {
        DisableText(); //appeller a chaque fois que l'oject en question est activer.inventaire activer la fonction Enable appeller.
    }
    public void ActiveText()//activer la description 
    {
        textDisplay.SetActive(true);
    }
    public void DisableText()//désactiver la description
    {
        textDisplay.SetActive(false);
    }
    public void TakeItem()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<FPCSupport>().YouAreoldingItem(this.gameObject, itemType,itemID);
    }

}
