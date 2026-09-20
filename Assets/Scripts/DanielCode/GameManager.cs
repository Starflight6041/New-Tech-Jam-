using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEditor.Rendering;

public class GameManager : MonoBehaviour
{
    public List<AbilityBase> abilitiesPool = new List<AbilityBase>();
    public List<AbilityBase> abilitiesPossessed = new List<AbilityBase>();
    public List<Slider> abilityBars = new List<Slider>();
    public List<Image> abilityImages = new List<Image>();
    [SerializeField] private InputActionAsset playerMap;

    private InputAction ability0;
    private InputAction ability1;
    private InputAction ability2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomizeAbilities();
        
    }
    /*
    void OnEnable()
    {
        InputSystem.actions.FindAction("Ability0").canceled += CancelAbility0;
        InputSystem.actions.FindAction("Ability1").canceled += CancelAbility1;
        InputSystem.actions.FindAction("Ability2").canceled += CancelAbility2;
    }
    void OnDisable()
    {
        InputSystem.actions.FindAction("Ability0").canceled -= CancelAbility0;
        InputSystem.actions.FindAction("Ability1").canceled -= CancelAbility1;
        InputSystem.actions.FindAction("Ability2").canceled -= CancelAbility2;
    }
    */
    

    private void Awake()
    {
        // Find the specific actions inside the assigned asset
        ability0 = playerMap.FindAction("Ability0");
        ability1 = playerMap.FindAction("Ability1");
        ability2 = playerMap.FindAction("Ability2");
    }

    private void OnEnable()
    {
        ability0.canceled += CancelAbility0;
        ability1.canceled += CancelAbility1;
        ability2.canceled += CancelAbility2;

        playerMap.Enable(); // Enables action maps within this asset
    }

    private void OnDisable()
    {
        ability0.canceled -= CancelAbility0;
        ability1.canceled -= CancelAbility1;
        ability2.canceled -= CancelAbility2;

        playerMap.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < abilitiesPossessed.Count; i++)
        {
            abilityBars[i].value = (Time.time - abilitiesPossessed[i].timeOfUse) / abilitiesPossessed[i].cooldown;
        }
    }
    public void TriggerAbility0(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        /*
        if (context.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
        {
            abilitiesPossessed[0].Execute();
        }
        else if (context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction)
        {
            abilitiesPossessed[0].ExecuteHold();
            Debug.Log("Holding");
        }
        */
        abilitiesPossessed[0].Execute();
        
        
    }
    public void CancelAbility0(InputAction.CallbackContext context)
    {
        
        
        abilitiesPossessed[0].AbilityHoldCanceled();
        
        
    }
    public void TriggerAbility1(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        /*
        if (context.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
        {
            abilitiesPossessed[1].Execute();
        }
        else if (context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction)
        {
            abilitiesPossessed[1].ExecuteHold();
            Debug.Log("Holding");
        */
        abilitiesPossessed[1].Execute();


    }
    public void CancelAbility1(InputAction.CallbackContext context)
    {
        
        abilitiesPossessed[1].AbilityHoldCanceled();
        
    }
    public void TriggerAbility2(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        /*
        if (context.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
        {
            abilitiesPossessed[2].Execute();
        }
        else if (context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction)
        {
            abilitiesPossessed[2].ExecuteHold();
            Debug.Log("Holding");
        }
        */
        abilitiesPossessed[2].Execute();

    }
    public void CancelAbility2(InputAction.CallbackContext context)
    {
        
        abilitiesPossessed[2].AbilityHoldCanceled();
        
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
        for (int i = 0; i < 3; i++)
        {
            int abilityIndex = Random.Range(0, abilitiesPool.Count);
            abilitiesPossessed.Add(abilitiesPool[abilityIndex]);
            abilitiesPool.RemoveAt(abilityIndex);
            abilitiesPossessed[i].timeOfUse = 0f;
        }

    }
}
