using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Class for saving Data
public class SaveLoadManager : MonoBehaviour 
{
    private static SaveLoadManager instance;
    public static SaveLoadManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SaveLoadManager>();
            }
            return instance;
        }
    }

    private const string GameDataKey = "GameData";

    public void SaveGame(int coinCount, List<FootballerCard> playerCards)
    {
        GameData gameData = new GameData(coinCount, playerCards);
        string jsonData = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString(GameDataKey, jsonData);
        PlayerPrefs.Save();
    }

    public GameData LoadGame()
    {
        if (PlayerPrefs.HasKey(GameDataKey))
        {
            string jsonData = PlayerPrefs.GetString(GameDataKey);
            GameData gameData = JsonUtility.FromJson<GameData>(jsonData);
            return gameData;
        }
        return null;
    }

    public void DeleteGame()
    {
        if (PlayerPrefs.HasKey(GameDataKey))
        {
            PlayerPrefs.DeleteKey(GameDataKey);
            PlayerPrefs.Save();
            SceneManager.LoadScene(0);
        }
    }
    public void DeleteAllSave()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}

[System.Serializable]
public class GameData
{
    public int CoinCount;
    public List<CardData> PlayerCards;

    public GameData(int coinCount, List<FootballerCard> playerCards)
    {
        CoinCount = coinCount;
        PlayerCards = new List<CardData>();
        foreach (var card in playerCards)
        {
            PlayerCards.Add(new CardData(card));
        }
    }
}

[System.Serializable]
public class CardData
{
    public string Name;
    public int Strength;
    public int Speed;
    public int Stamina;
    public int Accuracy;
    public int Teamwork;

    public int HairIndex;
    public int HairColorIndex;
    public int SkinColorIndex;
    public int EyeColorIndex;
    public int ShirtColorIndex;
    public CardData(FootballerCard card)
    {
        Name = card.Name;
        Strength = card.Strength;
        Speed = card.Speed;
        Stamina = card.Stamina;
        Accuracy = card.Accuracy;
        Teamwork = card.Teamwork;

        HairIndex = card.HairIndex;
        HairColorIndex = card.HairColorIndex;
        SkinColorIndex = card.SkinColorIndex;
        EyeColorIndex = card.EyeColorIndex;
        ShirtColorIndex = card.ShirtColorIndex;
    }
}
