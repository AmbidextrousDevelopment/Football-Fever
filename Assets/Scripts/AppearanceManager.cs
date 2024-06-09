using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for New Character Clothes
public class AppearanceManager : MonoBehaviour
{
    public static AppearanceManager Instance;
    public Sprite[] hairVariations;
    public Color[] hairColors;
    public Color[] skinColors;
    public Color[] eyeColors;
    public Color[] shirtColors;

    public void GenerateRandomAppearance(out int hairIndex, out int hairColorIndex, out int skinColorIndex, out int eyeColorIndex, out int shirtColorIndex)
    {
        hairIndex = Random.Range(0, hairVariations.Length);
        hairColorIndex = Random.Range(0, hairColors.Length);
        skinColorIndex = Random.Range(0, skinColors.Length);
        eyeColorIndex = Random.Range(0, eyeColors.Length);
        shirtColorIndex = Random.Range(0, shirtColors.Length);
    }
}
