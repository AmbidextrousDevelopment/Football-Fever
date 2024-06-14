using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Main Game class
public class CardManager : MonoBehaviour
{
    #region Variables And Objects References
    [Header("Main Objects")]
    public GameObject cardPrefab;
    public Transform cardContainer;

    public GameObject selectedCardPrefab;
    public GameObject opponentCardPrefab;

    private GameObject selectedCardObject;
    private GameObject opponentCardObject;

    public List<FootballerCard> playerCards = new List<FootballerCard>();

    [Header("Single Battle Mode Objects")]
    public Transform selectedCardContainer;
    public Transform opponentCardContainer;
    public FootballerCard selectedCard;
    public OpponentCard opponentCard;

    [Header("Team Battle Mode Objects")]
    [SerializeField] private Transform selectedCardsContainer;
    [SerializeField] private Transform opponentCardsContainer;
    public List<FootballerCard> selectedCards = new List<FootballerCard>();
    public List<OpponentCard> opponentCards = new List<OpponentCard>();
    private int howMuchCardsToPlay;
    [Header("UI Menu")]
    [SerializeField] private GameObject gameModeMenu;
    [SerializeField] private GameObject teamModeMenu;
    [SerializeField] private GameObject gamePlayMenu;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject NotificationMenu;
    [SerializeField] private GameObject FortuneWheelMenu;

    [Header("UI Objects")]
    [SerializeField] private TextMeshProUGUI MainWindowText;
    [SerializeField] private TextMeshProUGUI NotificationText;
    [SerializeField] private GameObject playerCardsWindow;
    [SerializeField] private GameObject battleCardsWindow;
    [SerializeField] private GameObject singleBattleWindow;
    [SerializeField] private GameObject teamBattleWindow;

    [SerializeField] private GameObject crestAnimation;

    [SerializeField] private GameObject congratulationMenu;
    [SerializeField] private TextMeshProUGUI addCoinsText;
    [SerializeField] private AppearanceManager manager;

    [SerializeField] private GameObject menuBackground;


    private int wonCoins;
   
    [Header("Buttons")]
    [SerializeField] private Button startBattleButton;
    [SerializeField] private Button startTeamBattleButton;
    [SerializeField] private Button toMainMenuButton;
    [SerializeField] private Button yourCardsButton;
    [SerializeField] private Button yourCardsTeamButton;

    private string battleMode; //= "Single";

    [Header("Save Game?")]
    [SerializeField] private bool saveGame;
    #endregion

    #region UI Menu Functions
    public void ToMainMenu()
    {
        selectedCards.Clear();
        DisplayPlayerCards();
        DisplaySelectedCards();

        battleCardsWindow.SetActive(false);
        gamePlayMenu.SetActive(false);
        MainMenu.SetActive(true);
        MainWindowText.text = "Main Menu";

        menuBackground.SetActive(true);
    }

    public void OpenFortuneWheelWithFreeSpin(int addSpins)
    {
        FortuneWheelMenu.GetComponent<FortuneWheel>().ActivateFreeSpeenWheel(addSpins);
        FortuneWheelMenu.SetActive(true);
    }
    #endregion

    #region Competition Type Functions
    public void SingleCompetition()
    {
        battleMode = "Single";
        gamePlayMenu.SetActive(true);
        gameModeMenu.SetActive(false);
        menuBackground.SetActive(false);
        playerCardsWindow.SetActive(true);
        OpenCardsMenu();
    }
    public void TeamCompetitionMenu()
    {
        if (playerCards.Count >= 6)
        {
            gameModeMenu.SetActive(false);
            teamModeMenu.SetActive(true);
            playerCardsWindow.SetActive(true);
        }
        else
        {
            NotificationMenu.SetActive(true);
            NotificationText.text = "To open Team Competition you need to have at least 6 Cards";
        }
        
    }
    public void TeamCompetitionModes(int howMuchCards)
    {
        int cardsToEnter = howMuchCards + 4;
        if (howMuchCards == 6) cardsToEnter = 12;

        if (playerCards.Count >= cardsToEnter)
        {
            yourCardsTeamButton.gameObject.SetActive(true);
            startTeamBattleButton.gameObject.SetActive(true);

            battleMode = "Team";
            gamePlayMenu.SetActive(true);
            teamModeMenu.SetActive(false);
            playerCardsWindow.SetActive(true);
            menuBackground.SetActive(false);
            howMuchCardsToPlay = howMuchCards;

            if (howMuchCardsToPlay <= 5)
            {
                selectedCardsContainer.GetComponent<GridLayoutGroup>().constraintCount = 2;
                opponentCardsContainer.GetComponent<GridLayoutGroup>().constraintCount = 2;
            }
            else
            {
                selectedCardsContainer.GetComponent<GridLayoutGroup>().constraintCount = 3;
                opponentCardsContainer.GetComponent<GridLayoutGroup>().constraintCount = 3;
            }
            DisplayPlayerCards();
        }
        else
        {
            NotificationMenu.SetActive(true);
            NotificationText.text = "To open Team Competition you need to have at least " + cardsToEnter + " Cards";
        }
    }
    private bool menuCards = true;
    public void OpenCloseCardsMenu()
    {
        if (menuCards) menuCards = false;
        else menuCards = true;

        playerCardsWindow.SetActive(menuCards);
    }
    #endregion

    #region Main Game Mechanics
    private void Start()
    {
        LoadGame();

        
        OpenCardsMenu();
        startTeamBattleButton.onClick.AddListener(StartTeamBattle);

        MainWindowText.text = "Main Menu";
    }
    public void CreateNewPlayerCard()
    {
        int hairIndex, hairColorIndex, skinColorIndex, eyeColorIndex, shirtColorIndex;
        manager.GenerateRandomAppearance
            (out hairIndex, out hairColorIndex, out skinColorIndex, out eyeColorIndex, out shirtColorIndex);

        FootballerCard newCard = new FootballerCard
        {
            Name = FootballerCard.GenerateRandomName(),

            HairIndex = hairIndex,
            HairColorIndex = hairColorIndex,
            SkinColorIndex = skinColorIndex,
            EyeColorIndex = eyeColorIndex,
            ShirtColorIndex = shirtColorIndex
        };
        playerCards.Add(newCard);
    }

    public void DisplayPlayerCards()
    {
        foreach (Transform child in cardContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (FootballerCard card in playerCards)
        {
            GameObject cardObject = Instantiate(cardPrefab, cardContainer);
            CardUI cardUI = cardObject.GetComponent<CardUI>();
            cardUI.Initialize(card);
            cardUI.SelectButton.onClick.AddListener(() => SelectCard(card));
            cardUI.StrengthButton.onClick.AddListener(() => UpgradeCardAttribute(card, "Strength"));
            cardUI.SpeedButton.onClick.AddListener(() => UpgradeCardAttribute(card, "Speed"));
            cardUI.EnduranceButton.onClick.AddListener(() => UpgradeCardAttribute(card, "Stamina"));
            cardUI.AccuracyButton.onClick.AddListener(() => UpgradeCardAttribute(card, "Accuracy"));
            cardUI.TeamingButton.onClick.AddListener(() => UpgradeCardAttribute(card, "Teamwork"));
            
        }
    }
    public void OpenCardsMenu()
    {
        playerCardsWindow.SetActive(true);
        battleCardsWindow.SetActive(false);

        MainWindowText.gameObject.SetActive(true);
        MainWindowText.text = "Choose your card";
        toMainMenuButton.gameObject.SetActive(true);
        yourCardsButton.gameObject.SetActive(false);

        startBattleButton.gameObject.SetActive(true);
        DisplayPlayerCards();
    }

    private void SelectCard(FootballerCard card)
    {
        selectedCard = card;
        Debug.Log("Selected Card: " + card.Name);
        if (battleMode == "Single")
        {
            singleBattleWindow.gameObject.SetActive(true);
            teamBattleWindow.gameObject.SetActive(false);

            CreateOpponentCard();
            DisplayOpponentCard();
            DisplaySelectedCard();
        }
        else
        {
            singleBattleWindow.gameObject.SetActive(false);
            teamBattleWindow.gameObject.SetActive(true);

            if (!selectedCards.Contains(card))
            {
                if (selectedCards.Count <= howMuchCardsToPlay)
                {
                   
                    Debug.Log("Selected Card: " + card.Name);

                    if (selectedCards.Count < howMuchCardsToPlay)
                    {
                        selectedCards.Add(card);
                        startTeamBattleButton.gameObject.SetActive(false);
                        MainWindowText.text = "Choose " + howMuchCardsToPlay + " cards";
                       
                    }
                    if (selectedCards.Count == howMuchCardsToPlay)
                    {
                        startTeamBattleButton.gameObject.SetActive(true);
                        MainWindowText.text = "Challenge Your Opponents!";
                    }
                    DisplaySelectedCards();
                }
                else if (selectedCards.Count > howMuchCardsToPlay)
                {
                    MainWindowText.text = "You can't choose more cards!";
                }
            }
            else
            {
                selectedCards.Remove(card);
                Debug.Log("Deselected Card: " + card.Name);

                if (selectedCards.Count == howMuchCardsToPlay)
                {
                    startTeamBattleButton.gameObject.SetActive(true);
                    MainWindowText.text = "Challenge Your Opponents!";
                }
                else
                {
                    startTeamBattleButton.gameObject.SetActive(false);
                    MainWindowText.text = "Choose " + howMuchCardsToPlay + " cards";
                }
                DisplaySelectedCards();
            }

            print(selectedCards.Count);
            CreateOpponentCards(selectedCards.Count);
            DisplayOpponentCards();
        }
        OpenBattleWindow();
    }

    private void OpenBattleWindow()
    {
        if (battleMode == "Single")
        {
            playerCardsWindow.SetActive(false);
            yourCardsButton.gameObject.SetActive(true);
            MainWindowText.text = "Challenge Your Opponent!";
        }

        battleCardsWindow.SetActive(true);
        toMainMenuButton.gameObject.SetActive(true);
        
    }

    private void DisplaySelectedCard()
    {
        foreach (Transform child in selectedCardContainer)
        {
            Destroy(child.gameObject);
        }

        selectedCardObject = Instantiate(selectedCardPrefab, selectedCardContainer);
        CardUI cardUI = selectedCardObject.GetComponent<CardUI>();
        cardUI.Initialize(selectedCard);
    }

    private void UpgradeCardAttribute(FootballerCard card, string attribute)
    {
        if (CoinManager.Instance.UseCoins(1))
        {
            card.Upgrade(attribute);
            card.turned = true;
            DisplayPlayerCards();
            card.turned = false;
            SaveGame();
        }
    }

    private void CreateOpponentCard()
    {
        int hairIndex, hairColorIndex, skinColorIndex, eyeColorIndex, shirtColorIndex;
        manager.GenerateRandomAppearance
            (out hairIndex, out hairColorIndex, out skinColorIndex, out eyeColorIndex, out shirtColorIndex);

        opponentCard = new OpponentCard()
        {
            HairIndex = hairIndex,
            HairColorIndex = hairColorIndex,
            SkinColorIndex = skinColorIndex,
            EyeColorIndex = eyeColorIndex,
            ShirtColorIndex = shirtColorIndex
        };

        opponentCard.GenerateRandomAttributes(selectedCard.TotalStats);
    }

    private void CreateOpponentCards(int count)
    {
        int hairIndex, hairColorIndex, skinColorIndex, eyeColorIndex, shirtColorIndex;
        
        opponentCards.Clear();
        for (int i = 0; i < count; i++)
        {
            manager.GenerateRandomAppearance
            (out hairIndex, out hairColorIndex, out skinColorIndex, out eyeColorIndex, out shirtColorIndex);

            OpponentCard opponentCard = new OpponentCard()
            {
                HairIndex = hairIndex,
                HairColorIndex = hairColorIndex,
                SkinColorIndex = skinColorIndex,
                EyeColorIndex = eyeColorIndex,
                ShirtColorIndex = shirtColorIndex
            };
            opponentCard.GenerateRandomAttributes(selectedCards[i].TotalStats);
            opponentCards.Add(opponentCard);
        }
    }
    private void СhangeOpponentCard()
    {
        if (CoinManager.Instance.UseCoins(1))
        {
            SelectCard(selectedCard);
        }
    }

    private void DisplayOpponentCard()
    {
        foreach (Transform child in opponentCardContainer)
        {
            Destroy(child.gameObject);
        }

        opponentCardObject = Instantiate(opponentCardPrefab, opponentCardContainer);
        CardUI cardUI = opponentCardObject.GetComponent<CardUI>();
        cardUI.Initialize(opponentCard);

        cardUI.SelectButton.onClick.AddListener(() => СhangeOpponentCard());
        
    }

    void DisplayOpponentCards()
    {
        foreach (Transform child in opponentCardsContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (OpponentCard opponentCard in opponentCards)
        {
            GameObject opponentCardObject = Instantiate(opponentCardPrefab, opponentCardsContainer);
            opponentCardObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            CardUI cardUI = opponentCardObject.GetComponent<CardUI>();
            cardUI.Initialize(opponentCard);
            cardUI.SelectButton.transform.parent.gameObject.SetActive(false);
        }
    }

    void DisplaySelectedCards()
    {
        foreach (Transform child in selectedCardsContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (FootballerCard selectedCard in selectedCards)
        {
            GameObject selectedCardObject = Instantiate(selectedCardPrefab, selectedCardsContainer);
            selectedCardObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            CardUI cardUI = selectedCardObject.GetComponent<CardUI>();
            cardUI.Initialize(selectedCard);
        }
    }
    private void AddOpponentCardToPlayer(OpponentCard opponentCard)
    {
        FootballerCard newCard = new FootballerCard
        {
            Name = opponentCard.Name,
            Strength = opponentCard.Strength,
            Speed = opponentCard.Speed,
            Stamina = opponentCard.Stamina,
            Accuracy = opponentCard.Accuracy,
            Teamwork = opponentCard.Teamwork,
            HairIndex = opponentCard.HairIndex,
            HairColorIndex = opponentCard.HairColorIndex,
            SkinColorIndex = opponentCard.SkinColorIndex,
            EyeColorIndex = opponentCard.EyeColorIndex,
            ShirtColorIndex = opponentCard.ShirtColorIndex
        };
        playerCards.Add(newCard);
        DisplayPlayerCards();
        SaveGame();
    }

    public void StartBattle()
    {
        if (selectedCard != null && opponentCard != null)
        {
            int playerScore = CalculateScore(selectedCard);
            int opponentScore = CalculateScore(opponentCard);

            opponentCardObject.GetComponent<CardUI>().SelectButton.transform.parent.gameObject.SetActive(false);
            if (playerScore > opponentScore)
            {
                // Гравець переміг
                
                opponentCardObject.transform.GetChild(0).GetComponent<Image>().color = Color.gray;
                Instantiate(crestAnimation, opponentCardObject.transform);
                //MainWindowText.text = "Player Wins!";

                wonCoins = selectedCard.TotalStats + (opponentCard.TotalStats);

                Invoke("CongratulationStart", 1.5f);
                Debug.Log("Player Wins!");
            }
            else
            {
                // Гравець програв

                startBattleButton.gameObject.SetActive(false);
                selectedCardObject.transform.GetChild(0).GetComponent<Image>().color = Color.gray;
                Instantiate(crestAnimation, selectedCardObject.transform);
                playerCards.Remove(selectedCard);
                CreateNewPlayerCard(); // Додаємо нову картку для гравця з випадковими характеристиками

                MainWindowText.gameObject.SetActive(true);
                MainWindowText.text = "You Lost Card";

                toMainMenuButton.gameObject.SetActive(true);
                yourCardsButton.gameObject.SetActive(true);

                Invoke("LoseSoloCompetition", 2f);
              
                Debug.Log("Player Loses!");
            }
            toMainMenuButton.gameObject.SetActive(false);
            DisplayPlayerCards();
        }
    }

    public void StartTeamBattle()
    {
        if (selectedCards.Count > 1)
        {
            int playerTeamScore = CalculateTeamScore(selectedCards);
            int opponentTeamScore = CalculateTeamScore(opponentCards);

            if (playerTeamScore > opponentTeamScore)
            {
                wonCoins = 0;
                foreach (var card in opponentCards)
                {
                    wonCoins += card.TotalStats;
                }
                MainWindowText.text = "Player Team Wins!";
                Debug.Log("Player Team Wins!");

                foreach (Transform cardObject in opponentCardsContainer)
                {
                    cardObject.transform.GetChild(0).GetComponent<Image>().color = Color.gray;
                    Instantiate(crestAnimation, cardObject.transform);
                }

                Invoke("CongratulationStart", 1.5f); 
                
            }
            else
            {
                MainWindowText.text = "Player Team Loses!";
                foreach (Transform cardObject in selectedCardsContainer)
                {
                    cardObject.transform.GetChild(0).GetComponent<Image>().color = Color.gray;
                    Instantiate(crestAnimation, cardObject.transform);
                }
                yourCardsTeamButton.gameObject.SetActive(false);
                startTeamBattleButton.gameObject.SetActive(false);
                Invoke("LoseTeamCompetition", 1.5f);
            }
            toMainMenuButton.gameObject.SetActive(false);
        }
    }

    private void LoseSoloCompetition()
    {
        toMainMenuButton.gameObject.SetActive(true);
        if (CoinManager.Instance.GetCoinCount() <= 2)
        {
            OpenFortuneWheelWithFreeSpin(1);
        }
    }

    private void LoseTeamCompetition()
    {
        foreach (var card in selectedCards)
        {
            playerCards.Remove(card);
        }
        for (int i = 0; i < selectedCards.Count; i++)
        {
            CreateNewPlayerCard();
        }
        selectedCards.Clear();
        Debug.Log("Player Team Loses!");
        //ToMainMenu();

        if (CoinManager.Instance.GetCoinCount() <= 2)
        {
            OpenFortuneWheelWithFreeSpin(2);
        }
        toMainMenuButton.gameObject.SetActive(true);
        SaveGame();
    }

    public void CongratulationStart()
    {
        if (battleMode == "Single")
        {
            AddOpponentCardToPlayer(opponentCard);
        }
        else
        {
            foreach (var opponentCard in opponentCards)
            {
                AddOpponentCardToPlayer(opponentCard);
            }
            selectedCards.Clear();
        }
        playerCardsWindow.SetActive(false);

        toMainMenuButton.gameObject.SetActive(false);
        yourCardsButton.gameObject.SetActive(false);

        congratulationMenu.SetActive(true);
        addCoinsText.text = "+" + wonCoins;

        
        CoinManager.Instance.AddCoins(wonCoins);
        SaveGame();
    }
    #endregion

    #region Calculate Scores
    int CalculateScore(FootballerCard card)
    {
        int highestStat = Mathf.Max(card.Strength, card.Speed, card.Stamina, card.Accuracy, card.Teamwork);
        if (highestStat == card.Strength)
        {
            return (int)(0.7 * card.Strength + card.Accuracy / card.Speed + 0.2 * card.Stamina);
        }
        else if (highestStat == card.Speed)
        {
            return (int)(0.7 * card.Speed + 0.5 * card.Accuracy + 0.5 * card.Strength + 0.7 * card.Stamina);
        }
        else if (highestStat == card.Stamina)
        {
            return (int)(0.7 * card.Strength + 0.7 * card.Accuracy + 0.7 * card.Speed + 0.6 * card.Stamina);
        }
        else if (highestStat == card.Accuracy)
        {
            return (int)(card.Accuracy / card.Strength + card.Stamina + 0.1 * card.Speed);
        }
        return 0;
    }

    int CalculateScore(OpponentCard card)
    {
        int highestStat = Mathf.Max(card.Strength, card.Speed, card.Stamina, card.Accuracy, card.Teamwork);
        if (highestStat == card.Strength)
        {
            return (int)(0.7 * card.Strength + card.Accuracy / card.Speed + 0.2 * card.Stamina);
        }
        else if (highestStat == card.Speed)
        {
            return (int)(0.7 * card.Speed + 0.5 * card.Accuracy + 0.5 * card.Strength + 0.7 * card.Stamina);
        }
        else if (highestStat == card.Stamina)
        {
            return (int)(0.7 * card.Strength + 0.7 * card.Accuracy + 0.7 * card.Speed + 0.6 * card.Stamina);
        }
        else if (highestStat == card.Accuracy)
        {
            return (int)(card.Accuracy / card.Strength + card.Stamina + 0.1 * card.Speed);
        }
        return 0;
    }

    int CalculateTeamScore(List<FootballerCard> team)
    {
        int highestStat = 0;
        int sumStat = 0;
        int teaming = 0;

        foreach (var card in team)
        {
            highestStat += Mathf.Max(card.Strength, card.Speed, card.Stamina, card.Accuracy);
            sumStat += (card.Strength + card.Speed + card.Stamina + card.Accuracy);
            teaming += card.Teamwork;
        }

        return highestStat * teaming + sumStat;
    }

    int CalculateTeamScore(List<OpponentCard> team)
    {
        int highestStat = 0;
        int sumStat = 0;
        int teaming = 0;

        foreach (var card in team)
        {
            highestStat += Mathf.Max(card.Strength, card.Speed, card.Stamina, card.Accuracy);
            sumStat += (card.Strength + card.Speed + card.Stamina + card.Accuracy);
            teaming += card.Teamwork;
        }

        return highestStat * teaming + sumStat;
    }
    #endregion

    #region Save/Load Functions
    public void SaveGame()
    {
        SaveLoadManager.Instance.SaveGame(CoinManager.Instance.GetCoinCount(), playerCards);
    }

    public void LoadGame()
    {
        GameData gameData = SaveLoadManager.Instance.LoadGame();
        if (gameData != null & saveGame)
        {
            CoinManager.Instance.SetCoinCount(gameData.CoinCount);

            playerCards.Clear();
            foreach (var cardData in gameData.PlayerCards)
            {
                FootballerCard card = new FootballerCard
                {
                    Name = cardData.Name,
                    Strength = cardData.Strength,
                    Speed = cardData.Speed,
                    Stamina = cardData.Stamina,
                    Accuracy = cardData.Accuracy,
                    Teamwork = cardData.Teamwork,

                    HairIndex = cardData.HairIndex,
                    HairColorIndex = cardData.HairColorIndex,
                    SkinColorIndex = cardData.SkinColorIndex,
                    EyeColorIndex = cardData.EyeColorIndex,
                    ShirtColorIndex = cardData.ShirtColorIndex
                };
                playerCards.Add(card);
            }
        }
        else
        {
            // If no saved data, create new cards
            for (int i = 0; i < 5; i++)
            {
                CreateNewPlayerCard();
            }
        }
    }
    #endregion
}


