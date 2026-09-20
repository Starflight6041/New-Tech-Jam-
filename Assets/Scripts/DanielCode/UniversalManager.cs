using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UniversalManager
{
    public static float baseBet;
    public static float currentBet;
    public static float wealthAtStake;
    public static float earningsModifier;
    public static float gamblerPatience;
    public static float patienceDrain;
    public static float baseDrain = 0.01f;
    public static float wealth;
    public enum GambleResult { Win, Lose, NoPatience }
    public static GambleResult result;
    


    public static void WinBet()
    {
        wealth -= currentBet;
        wealthAtStake += currentBet;
        earningsModifier *= 2;
        currentBet = baseBet * earningsModifier + wealthAtStake;
        patienceDrain += baseDrain;
        gamblerPatience += .3f;
        result = GambleResult.Win;
        
    }
    public static void LoseBet()
    {
        wealth += currentBet;
        wealthAtStake = 0;
        earningsModifier = 1;
        currentBet = 0;
        patienceDrain = baseDrain;
        gamblerPatience = 1;
        result = GambleResult.Lose;

    }
    public static void PatienceExpires()
    {
        wealthAtStake = 0;
        earningsModifier = 1;
        currentBet = 0;
        patienceDrain = baseDrain;
        gamblerPatience = 1;
        result = GambleResult.NoPatience;

    }

    
    public static void ResetSession()
    {
        wealth = 0;
        currentBet = 0;
        wealthAtStake = 0;
        baseBet = 0;
        earningsModifier = 1;
        gamblerPatience = 1;
        patienceDrain = 0.01f;
        result = GambleResult.NoPatience;
    }
}
