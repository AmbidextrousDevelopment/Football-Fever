using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Class for Fortune Wheel Mechanics
public class FortuneWheel : MonoBehaviour
{
    #region Variables and Objects
    [SerializeField] private GameObject wheel;
    [SerializeField] private float spinDuration = 2.0f; // тривалість обертання у секундах
    private float randomAngle;
    private int result;
    [SerializeField] private TextMeshProUGUI mainWheelText;
    [SerializeField] private Button Spin;
    [SerializeField] private Button Close;
    [SerializeField] private Button BuyMoreSpins;
    [SerializeField] private TextMeshProUGUI spinsText;
    [SerializeField] private GameObject fallingCoins;

    [SerializeField] private GameObject NotificationMenu;
    [SerializeField] private TextMeshProUGUI NotificationText;

    [SerializeField] private CoinManager coins;
    private int spinsCount;

    #endregion

    #region Main Wheel Functions
    private void OnEnable()
    {
        mainWheelText.text = "Spin the Wheel!";
        mainWheelText.color = Color.white;
        LoadSpinsCount();
        UpdateUI();
    }
    private void UpdateUI()
    {
        //spinsText.text = "You have " + spinsCount + " Spin left";       
        spinsText.text = "" + spinsCount;
    }
    public void ActivateFreeSpeenWheel(int spins)
    {
        spinsCount += spins;
        SaveGame();
    }
    public void BuySpin(int spinCount)
    {
        if (spinCount == 1)
        {
            if (coins.GetCoinCount() >= 25)
            {
                coins.UseCoins(25);
                spinsCount += 1;
                UpdateUI();
                SaveGame();
            }
            else
            {
                NotificationMenu.SetActive(true);
                NotificationText.text = "You don't have enough coins";
            }
        }
        else if (spinCount == 2)
        {
            if (coins.GetCoinCount() >= 45)
            {
                coins.UseCoins(45);
                spinsCount += 2;
                UpdateUI();
                SaveGame();
            }
            else
            {
                NotificationMenu.SetActive(true);
                NotificationText.text = "You don't have enough coins";
            }
        }
        else if (spinCount == 3)
        {
            if (coins.GetCoinCount() >= 90)
            {
                coins.UseCoins(90);
                spinsCount += 3;
                UpdateUI();
                SaveGame();
            }
            else
            {
                NotificationMenu.SetActive(true);
                NotificationText.text = "You don't have enough coins";
            }
        }

    }

    public void SpinTheWheel()
    {
        if (spinsCount < 1)
        {
            NotificationMenu.SetActive(true);
            NotificationText.text = "Buy at least 1 spin to use the Wheel";
        }
        else
        {
            spinsCount -= 1;
            SaveGame();
            ObjectsSetActive(false);

            wheel.transform.localEulerAngles = new Vector3(0, 0, -43.8f);
            randomAngle = Random.Range(360f, 720f); // Випадковий кут від 360 до 720 градусів
            StartCoroutine(SpinWheelCoroutine(randomAngle));
        }

    }

    private void ObjectsSetActive(bool setActive)
    {
        Spin.transform.parent.gameObject.SetActive(setActive);
        Close.gameObject.SetActive(setActive);
        //BuyMoreSpins.transform.parent.gameObject.SetActive(setActive);
        spinsText.transform.parent.gameObject.SetActive(setActive);
        fallingCoins.SetActive(setActive);
    }

    private IEnumerator SpinWheelCoroutine(float targetAngle)
    {
        float startRotation = wheel.transform.eulerAngles.z;
        float endRotation = startRotation + targetAngle;
        float elapsedTime = 0f;

        while (elapsedTime < spinDuration)
        {
            elapsedTime += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, endRotation, elapsedTime / spinDuration);
            wheel.transform.eulerAngles = new Vector3(wheel.transform.eulerAngles.x, wheel.transform.eulerAngles.y, zRotation);
            yield return null; // Чекаємо наступного кадру
        }

        // Останнє обертання, щоб забезпечити точність кінцевого кута
        wheel.transform.eulerAngles = new Vector3(wheel.transform.eulerAngles.x, wheel.transform.eulerAngles.y, endRotation);
        RotationResult();
    }

    private void RotationResult()
    {
        float valueAfterRandom;
        valueAfterRandom = (randomAngle - 360) / 45;
        result = ((int)valueAfterRandom + 1) * 5;        
        print((int)result);

        ObjectsSetActive(true);
        coins.AddCoins(result);
        mainWheelText.text = "You won "+result+ " coins!";
        mainWheelText.color = Color.green;
        UpdateUI();
        SaveGame();
    }
    #endregion

    #region Save Load Functions
    private void SaveGame()
    {
        PlayerPrefs.SetInt("SpinsCount", spinsCount);
        PlayerPrefs.Save();
        coins.SaveGame();
    }

    private void LoadSpinsCount()
    {
        spinsCount = PlayerPrefs.GetInt("SpinsCount", 0);
    }
    #endregion
}
