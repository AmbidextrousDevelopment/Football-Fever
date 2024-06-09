using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI teamworkText;
    [SerializeField] private TextMeshProUGUI accuracyText;

    [SerializeField] private TextMeshProUGUI nameText;
    private FootballerCard card;

    [SerializeField] private TextMeshProUGUI starText;

    public Button SelectButton;
    public Button StrengthButton;
    public Button SpeedButton;
    public Button EnduranceButton;
    public Button AccuracyButton;
    public Button TeamingButton;

    // Елементи персонажа
    public GameObject defaultCharacter;
    public GameObject upgradedCharacter;
    public Image hairImage;
    public Image skinImage;
    public Image additiveskinImage;
    public Image secondadditiveImage;
    public Image eyeImage;
    public Image secondEyeImage;
    public Image shirtImage;

    // Варіації
    public Sprite[] hairVariations;
    public Color[] hairColors;
    public Color[] skinColors;
    public Color[] eyeColors;
    public Color[] shirtColors;

    private OpponentCard opponentCard;
    public bool isPlayerCard = true;

    [Header("Front And Back Parts")]
    [SerializeField] private GameObject frontPart;
    [SerializeField] private GameObject backPart;
    private bool turned = true;

    [Header("BackGroundColor")]
    [SerializeField] private Image cardColor;
    [SerializeField] private Color[] cardColors;

    #endregion

    #region Main Functions

    public bool isTurned()
    {
        return turned;
    }
    public void TurnCard()
    {
        turned = true;
        frontPart.SetActive(true);
        backPart.SetActive(false);
    }
    public void ChangeCardPart()
    {
        
        if (!turned)
        {
            turned = true;
            frontPart.SetActive(true);
            backPart.SetActive(false);
        }
        else
        {
            turned = false;
            frontPart.SetActive(false);
            backPart.SetActive(true);
        }

    }

    public void Initialize(FootballerCard footballerCard)
    {
        card = footballerCard;
        if (card.turned)
        {
            turned = false;
            frontPart.SetActive(false);
            backPart.SetActive(true);
        }
        UpdateUI();    
    }

    public void Initialize(OpponentCard card)
    {
        opponentCard = card;
        isPlayerCard = false;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (isPlayerCard /*&& playerCard != null*/)
        {
            nameText.text = card.Name;
            strengthText.text = "" + card.Strength;
            speedText.text = "" + card.Speed;
            staminaText.text = "" + card.Stamina;
            teamworkText.text = "" + card.Teamwork;
            accuracyText.text = "" + card.Accuracy;
            starText.text = "" + card.AverageStats.ToString("F1");

            cardColor.color = cardColors[PlayerPrefs.GetInt("SelectedCardUIBackground", 0)];

            UpdateCharacterAppearance(card);

            
        }
        else if (!isPlayerCard /*&& opponentCard != null*/)
        {
            nameText.text = opponentCard.Name;
            strengthText.text = "" + opponentCard.Strength;
            speedText.text = "" + opponentCard.Speed;
            staminaText.text = "" + opponentCard.Stamina;
            accuracyText.text = "" + opponentCard.Accuracy;
            teamworkText.text = "" + opponentCard.Teamwork;
            starText.text = "" + opponentCard.AverageStats.ToString("F1");

            UpdateCharacterAppearance(opponentCard);
        }
    }
    private void UpdateCharacterAppearance(FootballerCard card)
    {
        float averageStats = card.GetAverageStats();
        if (averageStats >= 1)
        {
            defaultCharacter.SetActive(false);
            upgradedCharacter.SetActive(true);

            hairImage.sprite = hairVariations[card.HairIndex];
            hairImage.color = hairColors[card.HairColorIndex];
            skinImage.color = skinColors[card.SkinColorIndex];
            eyeImage.color = eyeColors[card.EyeColorIndex];
            shirtImage.color = shirtColors[card.ShirtColorIndex];

            additiveskinImage.color = skinImage.color;
            secondadditiveImage.color = skinImage.color;
            secondEyeImage.color = eyeImage.color;
        }
        else
        {
            defaultCharacter.SetActive(true);
            upgradedCharacter.SetActive(false);
        }
    }

    private void UpdateCharacterAppearance(OpponentCard card)
    {
        float averageStats = card.GetAverageStats();
        if (averageStats >= 1)
        {
            defaultCharacter.SetActive(false);
            upgradedCharacter.SetActive(true);

            hairImage.sprite = hairVariations[card.HairIndex];
            hairImage.color = hairColors[card.HairColorIndex];
            skinImage.color = skinColors[card.SkinColorIndex];
            eyeImage.color = eyeColors[card.EyeColorIndex];
            shirtImage.color = shirtColors[card.ShirtColorIndex];


            additiveskinImage.color = skinImage.color;
            secondadditiveImage.color = skinImage.color;
            secondEyeImage.color = eyeImage.color;
        }
        else
        {
            defaultCharacter.SetActive(true);
            upgradedCharacter.SetActive(false);
        }
    }
    #endregion
}
