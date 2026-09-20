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
    public static float gamblerPatience = .1f;
    public static float patienceDrain = .05f;
    public static float baseDrain = 0.05f;
    public static float wealth = 20;
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
        wealth -= currentBet;
        wealthAtStake = 0;
        earningsModifier = 1;
        currentBet = 0;
        patienceDrain = baseDrain;
        gamblerPatience = 1;
        result = GambleResult.NoPatience;

    }

    
    public static void ResetSession()
    {
        wealth = 20;
        currentBet = 10;
        wealthAtStake = 0;
        baseBet = 10;
        
        earningsModifier = 1;
        gamblerPatience = .1f;
        patienceDrain = .05f;
        result = GambleResult.NoPatience;
    }
}
