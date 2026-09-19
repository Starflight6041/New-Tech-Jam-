using NUnit.Framework.Internal.Filters;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityBase", menuName = "Scriptable Objects/AbilityBase")]
public class AbilityBase : ScriptableObject
{
    public Sprite abilitySprite;
    public float cooldown = 1f;
    public float timeOfUse;
    public bool isHold;
    public bool inHoldUse = false;
    public void Awake()
    {
        timeOfUse = 0f;
    }
    public virtual void Execute()
    {
        if (Time.time - timeOfUse >= cooldown && !isHold)
        {
            timeOfUse = Time.time;
            TriggerEffect();

        }
    }
    public virtual void ExecuteHold()
    {
        if (Time.time - timeOfUse >= cooldown && isHold)
        {
            inHoldUse = true;
            TriggerEffect();

        }
    }
    public virtual void TriggerEffect()
    {

    }
    public virtual void AbilityHoldCanceled()
    {
        if (isHold && inHoldUse)
        {
            inHoldUse = false;
            timeOfUse = Time.time;

        }
        
    }
    public virtual void CancelOnGrounded()
    {

    }

}
