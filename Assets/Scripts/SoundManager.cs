using UnityEngine;
using UnityEngine.UI;

//Class for Sound
public class SoundManager : MonoBehaviour
{
    #region Variables
    public Toggle soundToggle;
    public Slider volumeSlider;
    public AudioSource audioSource; // The audio source playing the background music
    #endregion

    #region Main Functions
    private void Start()
    {
        // Load saved settings
        soundToggle.isOn = PlayerPrefs.GetInt("SoundOn", 0) == 1;
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.5f);

        // Apply saved settings
        ApplySettings();

        // Add listeners for UI elements
        soundToggle.onValueChanged.AddListener(delegate { ToggleSound(); });
        volumeSlider.onValueChanged.AddListener(delegate { AdjustVolume(); });
    }

    private void ApplySettings()
    {
        audioSource.mute = !soundToggle.isOn;
        audioSource.volume = volumeSlider.value;
    }

    public void ToggleSound()
    {
        bool isSoundOn = soundToggle.isOn;
        audioSource.mute = !isSoundOn;
        PlayerPrefs.SetInt("SoundOn", isSoundOn ? 1 : 0);
    }

    public void AdjustVolume()
    {
        float volume = volumeSlider.value;
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }
    #endregion
}
