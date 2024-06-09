using System;


[Serializable]
//Opponent Card Class
public class OpponentCard
{
    #region Variables
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

    public int TotalStats => Strength + Speed + Stamina + Accuracy + Teamwork;
    public float AverageStats => TotalStats / 5f;
    #endregion

    #region Main Functions
    public float GetAverageStats()
    {
        return AverageStats;
    }

    public void GenerateRandomAttributes(int playerTotalStats)
    {
        int randomFactor = UnityEngine.Random.Range(1, 4); // Випадковий фактор для різноманітності
        int totalPoints = playerTotalStats + randomFactor;

        Strength = UnityEngine.Random.Range(1, totalPoints / 5 + randomFactor);
        Speed = UnityEngine.Random.Range(1, totalPoints / 5 + randomFactor);
        Stamina = UnityEngine.Random.Range(1, totalPoints / 5 + randomFactor);
        Accuracy = UnityEngine.Random.Range(1, totalPoints / 5 + randomFactor);
        Teamwork = UnityEngine.Random.Range(1, totalPoints / 5 + randomFactor);

        int finalRandomFactor = UnityEngine.Random.Range(0, 5);
        switch (finalRandomFactor)
        {
            case 0:
                Strength = totalPoints - (Strength + Speed + Stamina + Accuracy);
                break;
            case 1:
                Speed = totalPoints - (Strength + Speed + Stamina + Accuracy);
                break;
            case 2:
                Stamina = totalPoints - (Strength + Speed + Stamina + Accuracy);
                break;
            case 3:
                Accuracy = totalPoints - (Strength + Speed + Stamina + Accuracy);
                break;
            case 4:
                Teamwork = totalPoints - (Strength + Speed + Stamina + Accuracy);
                break;
            default:
                Teamwork = totalPoints - (Strength + Speed + Stamina + Accuracy);
                break;
        }

        CheckIfNormalStat();

        Name = FootballerCard.GenerateRandomName();
    }

    private void CheckIfNormalStat()
    {
        if (Strength < 1) Strength = 1;
        if (Speed < 1) Speed = 1;
        if (Stamina < 1) Stamina = 1;
        if (Accuracy < 1) Accuracy = 1;
        if (Teamwork < 1) Teamwork = 1;
    }
    #endregion
}
