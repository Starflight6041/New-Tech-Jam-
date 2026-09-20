using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GambleManagement : MonoBehaviour
{
    public TMP_Text betText;
    public TMP_Text commitmentText;
    public TMP_Text wealthText;
    public TMP_Text requirementText;
    public Slider patienceSlider;
    public static float wealthRequirement;
    public static float baseBetMidpoint = 50f;
    public static int weekProgress = 0;
    public static int weekDeadline = 7;
    public static float finalGoal = 1000;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (UniversalManager.result)
        {
            case (UniversalManager.GambleResult.Win):

                break;
            case (UniversalManager.GambleResult.Lose):
                NewGambler();
                break;
            case (UniversalManager.GambleResult.NoPatience):
                NewGambler();
                break;

        }
        betText.text = UniversalManager.currentBet.ToString();
        commitmentText.text = UniversalManager.earningsModifier.ToString();
        wealthText.text = UniversalManager.wealth.ToString();
        requirementText.text = wealthRequirement.ToString();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewGambler()
    {
        weekProgress += 1;
        if (weekProgress >= weekDeadline)
        {
            
            float rand = Random.Range(2f, 4f);
            baseBetMidpoint *= (UniversalManager.wealth * rand) / 14;
            wealthRequirement = UniversalManager.wealth * rand;
            if (UniversalManager.wealth < wealthRequirement)
            {
                Lose();
            }
            else if (UniversalManager.wealth > finalGoal)
            {
                Win();
            }
            else
            {

            }
            UniversalManager.baseBet = baseBetMidpoint * Random.Range(0.7f, 1.3f);
            UniversalManager.currentBet = UniversalManager.baseBet;
            

        }
    }
    public void Lose()
    {

    }
    public void Win()
    {

    }
    public IEnumerator GoToPlatformer()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(1);
    }

}
