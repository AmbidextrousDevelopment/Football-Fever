using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Class for Game Coins
public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    [SerializeField] private TextMeshProUGUI coinText;

    private int coins;

    #region Main Coin Functions
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //coins = 7; // Початкова кількість монет
        LoadGame();

    }


    public int GetCoinCount()
    {
        return coins;
    }

    public void SetCoinCount(int count)
    {
        coins = count;
        UpdateUI();
    }

    public bool UseCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            UpdateUI();
            SaveGame();
            return true;
        }
        return false;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateUI();
        SaveGame();
    }

    private void UpdateUI()
    {
        coinText.text = "" + coins;
    }
    #endregion

    #region Save Load Functions
    public void SaveGame()
    {
        SaveLoadManager.Instance.SaveGame(coins, FindObjectOfType<CardManager>().playerCards);
    }
    public void LoadGame()
    {
        GameData gameData = SaveLoadManager.Instance.LoadGame();
        if (gameData != null)
        {
            coins = gameData.CoinCount;
            UpdateUI();
        }
        else
        {
            coins = 7;
            UpdateUI();
        }
    }
    #endregion
}
