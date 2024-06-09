using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    #region Variables and Objects
    [SerializeField] private GameObject NotificationMenu;
    [SerializeField] private TextMeshProUGUI NotificationText;

    [SerializeField] private Image menuBackground;
    [SerializeField] private Image gameBackground;

    [SerializeField] private Sprite[] menuBackgrounds;
    [SerializeField] private Sprite[] gameBackgrounds;

    [SerializeField] private Button[] menuBackgroundpurchaseButtons;
    [SerializeField] private Button[] gameBackgroundpurchaseButtons;
    [SerializeField] private Button[] cardUIBackgroundPurchaseButtons;

    private string saveData;
    #endregion

    #region Main Shop Mechanics Mechanics
    void Start()
    {
        UpdateSelectedMenuBackground();
        UpdateSelectedGameuBackground();
        CheckShopSaveData();
    }

    private void CheckShopSaveData()
    {
        menuBackground.sprite = menuBackgrounds[PlayerPrefs.GetInt("SelectedMenuBackground", 0)];
        gameBackground.sprite = gameBackgrounds[PlayerPrefs.GetInt("SelectedGameBackground", 0)];


        for (int i = 1; i < 6; i++)
        {
            CheckUpdatedMenuBackgrounds(i);
        }

        for (int i = 1; i < 3; i++)
        {
            CheckUpdatedGameBackgrounds(i);
        }

        for (int i = 1; i < 6; i++)
        {
            CheckUpdatedCardUIBackgrounds(i);
        }
    }

    private void CheckUpdatedMenuBackgrounds(int BackgroundNumber)
    {
        saveData = "PurchasedMenuBackground" + BackgroundNumber;
        if (PlayerPrefs.GetInt(saveData, 0) == 1)
        {
            ButtonsChangeText(BackgroundNumber, menuBackgroundpurchaseButtons);
        }
        print(PlayerPrefs.GetInt(saveData, 0));
    }

    private void ButtonsChangeText(int BackgroundNumber, Button[] buttonsForChange)
    {
        buttonsForChange[BackgroundNumber].transform.GetChild(0).gameObject.SetActive(false);
        buttonsForChange[BackgroundNumber].transform.GetChild(1).gameObject.SetActive(true);
        buttonsForChange[BackgroundNumber].transform.GetChild(1).
            GetComponent<TextMeshProUGUI>().text = "Select";
        buttonsForChange[BackgroundNumber].transform.GetChild(2).gameObject.SetActive(false);
        buttonsForChange[BackgroundNumber].transform.GetChild(3).gameObject.SetActive(false);
    }
   
    private void CheckUpdatedGameBackgrounds(int BackgroundNumber)
    {
        saveData = "PurchasedGameBackground" + BackgroundNumber;
        if (PlayerPrefs.GetInt(saveData, 0) == 1)
        {
            ButtonsChangeText(BackgroundNumber, gameBackgroundpurchaseButtons);
        }
    }
  
    private void CheckUpdatedCardUIBackgrounds(int BackgroundNumber)
    {
        saveData = "PurchasedCardUIBackground" + BackgroundNumber;
        if (PlayerPrefs.GetInt(saveData, 0) == 1)
        {
            ButtonsChangeText(BackgroundNumber, cardUIBackgroundPurchaseButtons);
        }
    }   

    public void SelectMenuBackGroundOrBuy(int backGroundNumber)
    {
        saveData = "PurchasedMenuBackground" + backGroundNumber;
        string backgroundType = "menu";
        if (PlayerPrefs.GetInt(saveData, 0) == 1 || backGroundNumber == 0)
        {
            SelectMenuBackGround(backGroundNumber);           
        }
        else
        {
            switch(backGroundNumber)
            {
            case 1:
                    BuyBackGround(100, backGroundNumber, backgroundType);
                break;
                case 2:
                    BuyBackGround(50, backGroundNumber, backgroundType);
                    break;
                case 3:
                    BuyBackGround(50, backGroundNumber, backgroundType);
                    break;
                case 4:
                    BuyBackGround(50, backGroundNumber, backgroundType);
                    break;
                case 5:
                    BuyBackGround(20, backGroundNumber, backgroundType);
                    break;
            }
        } 
        
    }
    public void SelectGameBackGroundOrBuy(int backGroundNumber)
    {
        saveData = "PurchasedGameBackground" + backGroundNumber;
        string backgroundType = "game";
        if (PlayerPrefs.GetInt(saveData, 0) == 1 || backGroundNumber == 0)
        {
            SelectGameBackGround(backGroundNumber);
        }
        else
        {
            switch (backGroundNumber)
            {
                case 1:
                    BuyBackGround(100, backGroundNumber, backgroundType);
                    break;
                case 2:
                    BuyBackGround(50, backGroundNumber, backgroundType);
                    break;
            }
        }
    }
    public void SelectCardUIBackGroundOrBuy(int backGroundNumber)
    {
        saveData = "PurchasedCardUIBackground" + backGroundNumber;
        string backgroundType = "cardUI";
        if (PlayerPrefs.GetInt(saveData, 0) == 1 || backGroundNumber == 0)
        {
            SelectCardUIBackGround(backGroundNumber);
        }
        else
        {
            BuyBackGround(25, backGroundNumber, backgroundType);
        }
    }

    private void SelectMenuBackGround(int backGroundNumber)
    {
        menuBackground.sprite = menuBackgrounds[backGroundNumber];
        PlayerPrefs.SetInt("SelectedMenuBackground", backGroundNumber);
    }
    private void SelectGameBackGround(int backGroundNumber)
    {
        gameBackground.sprite = gameBackgrounds[backGroundNumber];
        PlayerPrefs.SetInt("SelectedGameBackground", backGroundNumber);
    }
    private void SelectCardUIBackGround(int backGroundNumber)
    {
        PlayerPrefs.SetInt("SelectedCardUIBackground", backGroundNumber);
    }


    public void BuyBackGround(int price, int BackgroundNumber, string backgroundType)
    {
        if (CoinManager.Instance.GetCoinCount() >= price)
        {
            CoinManager.Instance.UseCoins(price);

            if (backgroundType == "menu")
            {
                ButtonsChangeText(BackgroundNumber, menuBackgroundpurchaseButtons);
            }
            else if (backgroundType == "game")
            {
                ButtonsChangeText(BackgroundNumber, gameBackgroundpurchaseButtons);
            }
            else
            {
                ButtonsChangeText(BackgroundNumber, cardUIBackgroundPurchaseButtons);
            }
            

            PlayerPrefs.SetInt(saveData, 1);
        }
        else
        {
            NotificationMenu.SetActive(true);
            NotificationText.text = "You don't have enough coins";
        }
    }

    private void UpdateSelectedMenuBackground()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedMenuBackground", 0);

        menuBackground.sprite = menuBackgrounds[selectedIndex];
    }
    private void UpdateSelectedGameuBackground()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedGameBackground", 0);

        gameBackground.sprite = gameBackgrounds[selectedIndex];
    }
    #endregion

}
