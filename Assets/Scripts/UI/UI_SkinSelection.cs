using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SkinSelection : MonoBehaviour
{
    [SerializeField] private int[] priceForSkin;
    [SerializeField] private bool[] skinPurchased;
    private int skin_Id;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI bankText;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private Animator anim;    

    private void SetupSkinInfo()
    {
        skinPurchased[0] = true;

        for(int i = 1; i < skinPurchased.Length; i++)
        {
            bool skinUnlocked = PlayerPrefs.GetInt("SkinPurchased" + i) == 1;

            if(skinUnlocked)
            {
                skinPurchased[i] = true;
            }
        }

        bankText.text = " " + PlayerPrefs.GetInt("TotalBooksCollected").ToString();

        selectButton.SetActive(skinPurchased[skin_Id]);
        buyButton.SetActive(!skinPurchased[skin_Id]);

        if (!skinPurchased[skin_Id])
        {
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Price: " + priceForSkin[skin_Id];
        }

        anim.SetInteger("skinId", skin_Id);
    }

    public bool EnoughBooks()
    {
        int totalBooks = PlayerPrefs.GetInt("TotalBooksCollected");

        if(totalBooks > priceForSkin[skin_Id]) 
        {
            totalBooks = totalBooks - priceForSkin[skin_Id];

            PlayerPrefs.SetInt("TotalBooksCollected", totalBooks);

            AudioManager.instance.PlaySFXUI(3);
            return true;
        }
        
        return false;
    }

    public void NextSkin()
    {        
        skin_Id++;

        if(skin_Id > 4)
        {
            skin_Id = 0;
        }

        SetupSkinInfo();
    }
    
    public void PreviousSkin()
    {        
        skin_Id--;

        if(skin_Id < 0)
        {
            skin_Id = 4;
        }

        SetupSkinInfo();
    }

    public void Buy()
    {
        if(EnoughBooks())
        {
            PlayerPrefs.SetInt("SkinPurchased" + skin_Id, 1);
            SetupSkinInfo();
        }
        else
        {
            
        }
    }

    public void Select()
    {
        PlayerManager.instance.choosenSkinId = skin_Id;
    }

    public void SwitchSelectButton(GameObject newButton)
    {
        selectButton = newButton;
    }

    private void OnEnable()
    {
        SetupSkinInfo();
    }

    private void OnDisable()
    {
        selectButton.SetActive(false);
    }
}
