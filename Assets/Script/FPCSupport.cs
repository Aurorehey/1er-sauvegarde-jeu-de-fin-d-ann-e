using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCSupport: MonoBehaviour
{
    public GameObject playerCam;
    private UnityStandardAssets.ImageEffects.Blur blur;//pour rendre la page flou
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsComp;//pour controler les mouvements du joueur.

    public float pickupRange = 3.0f;
    private GameObject objectInteract; //stockage des imforamtions de item

    [Header("Button List")] //titre pour les boutons.
    public string InventoryButton;
    public string InteractButton;
    [Header("Tag List")]
    public string ItemTag = "item";

    [Header("inventory's Data")]
    public GameObject inventoryCanvas;//acceder à l'inventaire en lui meme.regroupe les elements de l'inventaire
    [HideInInspector] public bool inventoryOn = false;//variable caché dans l'inspecteur.pas utile priver. il peut etre remplace par private. cette variable permet de savoir si l'inventaire est activé ou non grace à une fonction.
    public Transform itemPrefab;
    public Transform inventorySlots;
    public int slotCount = 16;//si on verifie pas le nombre de casse cela continura à l'infini. 
    // Start is called before the first frame update
    void Start()
    {
        if (playerCam == null)
        {
            playerCam = GameObject.FindWithTag("MainCamera");
        }//securiter pour la camera

        blur = playerCam.GetComponent<UnityStandardAssets.ImageEffects.Blur>();//acceder au script blur
        blur.enabled = false;//blur bien désactivé au démarage.Juste un composant on utilise enabled.

        fpsComp = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();//acceder aux composants de fps.

        if (inventoryCanvas == null)
        {
            inventoryCanvas = GameObject.Find("Inventory Panel");
        }//inventory canvas bien renseigné.
        inventoryCanvas.SetActive(false);//quand c'est un game object on utilise setActive.
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(InventoryButton))
        {
            ShowOnHideInventory(); //fonction appeller à chaque fois qu'on appuis sur le bouton et qui va simplifier le travaille de recherche si l'inventaire est activé.
        }
        //controler si on appuye sur inventoryButton.
        if (Input.GetButtonDown(InteractButton))
        {
            TryToInteract();//lance la fonction TryToInteract.
            
        }
    }

    void TryToInteract()
    {
        Ray ray = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));//lancer un rayon devant le joueur de la distance pickupRnage si il y a pas un item devant lui.
        RaycastHit hit;//hit point d'impact du rayon avec un colider

        if(Physics.Raycast(ray,out hit, pickupRange))
        {
            objectInteract = hit.collider.gameObject;
            if(objectInteract.tag == ItemTag)
            {
                //pick up ramacer
                //verifier si l'inventaire est complet
                if (inventorySlots.childCount == slotCount)
                {
                    Debug.Log("L'inventaire est complet!");
                }

                //hoever
                else
                {
                    //faire disparaitre  l'objet 
                    objectInteract.SetActive(false);
                    //integrer notre nouvelle item dans l'inventaire.
                    Transform newItem;
                    newItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity) as Transform;//si on laisse comme ça le new item va apparaitre à une possition aléatoire.
                    newItem.SetParent(inventorySlots,false);//si on lui donne un parent est ce que l'objet va garder ça possition oui = true le parent va réévaluaer la position pour etre ajuster au parent.

                    // transferrer les données  de l'item de l'inventaire
                    ItemSlots itemInventory = newItem.GetComponent<ItemSlots>();
                    ItemVariables itemScene = objectInteract.GetComponent<ItemVariables>();
                    itemInventory.itemType = itemScene.itemType;
                    itemInventory.itemID = itemScene.itemID;
                    itemInventory.itemSprite = itemScene.itemSprite;
                    itemInventory.itemDescription = itemScene.itemDescription;
                }

               
            }
        }
    }
    void ShowOnHideInventory()
    {
        inventoryCanvas.SetActive(!inventoryOn);
        blur.enabled = !inventoryOn;
        fpsComp.enabled = inventoryOn; //fonctionne de façon désinchroniser car il est true au debut et il deveindra false après.

        Cursor.visible = !inventoryOn;
        if (inventoryOn)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else { Cursor.lockState = CursorLockMode.None; }




        inventoryOn =! inventoryOn; //avec le ! cela veut dire que si inventoryOn est = a false alors il deviendra true et inversement.
    }
}
