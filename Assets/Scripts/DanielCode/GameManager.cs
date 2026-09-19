using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public List<AbilityBase> abilitiesPool = new List<AbilityBase>();
    public List<AbilityBase> abilitiesPossessed = new List<AbilityBase>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomizeAbilities();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TriggerAbility0(InputAction.CallbackContext context)
    {
        abilitiesPossessed[0].Execute();
    }
    public void TriggerAbility1(InputAction.CallbackContext context)
    {
        abilitiesPossessed[1].Execute();
    }
    public void TriggerAbility2(InputAction.CallbackContext context)
    {
        abilitiesPossessed[2].Execute();
    }
    public void RandomizeAbilities()
    {
        abilitiesPossessed.Clear();
        List<AbilityBase> abilitiesRemaining = new List<AbilityBase>();
        foreach (AbilityBase a in abilitiesPool)
        {
            abilitiesRemaining.Add(a);
        }
        // change i back to 3 later
        for (int i = 0; i < 2; i++)
        {
            int abilityIndex = Random.Range(0, abilitiesPool.Count);
            abilitiesPossessed.Add(abilitiesPool[abilityIndex]);
            abilitiesPool.RemoveAt(abilityIndex);
            abilitiesPossessed[i].timeOfUse = 0f;
        }

    }
}
