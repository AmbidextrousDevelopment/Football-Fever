using System;

[Serializable]
//Player card Class
public class FootballerCard
{
    #region Variables
    public string Name;
    public int Strength = 1;
    public int Speed = 1;
    public int Stamina = 1;
    public int Teamwork = 1;
    public int Accuracy = 1;

    // Нові властивості для зовнішнього вигляду персонажа
    public int HairIndex;
    public int HairColorIndex;
    public int SkinColorIndex;
    public int EyeColorIndex;
    public int ShirtColorIndex;

    public bool turned;

    public int TotalStats => Strength + Speed + Stamina + Accuracy + Teamwork;
    public float AverageStats => TotalStats / 5f;

    #endregion

    #region Main Functions
    public float GetAverageStats()
    {
        return AverageStats;
    }
    public void Upgrade(string attribute)
    {
        switch (attribute)
        {
            case "Strength":
                Strength++;
                break;
            case "Speed":
                Speed++;
                break;
            case "Stamina":
                Stamina++;
                break;
            case "Accuracy":
                Accuracy++;
                break;
            case "Teamwork":
                Teamwork++;
                break;
        }
    }
    public static int GenerateRandomIndex(int maxIndex)
    {
        return UnityEngine.Random.Range(0, maxIndex +1);
    }

    public static string GenerateRandomName()
    {
        string[] firstParts = { "Mes", "Ron", "Dav", "Art", "Rob", "And", "Iva", "Jo", "Alb", "Aju", "Zavi" };

        string[] secondParts = { "si", "aldo", "vid", "ur", "in", "res", "an", "sh", "erto", "shel", "ido", "odi" };

        string firstPart = firstParts[UnityEngine.Random.Range(0, firstParts.Length)];
        string secondPart = secondParts[UnityEngine.Random.Range(0, secondParts.Length)];

        return firstPart + secondPart;
    }
    #endregion
}
