using UnityEngine;

[CreateAssetMenu(fileName = "Gambler", menuName = "Scriptable Objects/Gambler")]
public class Gambler : ScriptableObject
{
    public float patience;
    public float patienceDrain;
    public float currentBet;
    public float successiveWinCount;
    public float amountWon;
}
