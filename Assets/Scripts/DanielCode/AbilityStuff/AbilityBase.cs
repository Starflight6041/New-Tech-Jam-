using UnityEngine;

[CreateAssetMenu(fileName = "AbilityBase", menuName = "Scriptable Objects/AbilityBase")]
public class AbilityBase : ScriptableObject
{
    public Sprite abilitySprite;
    public float cooldown = 1f;
    public float timeOfUse;
    public void Awake()
    {
        timeOfUse = 0f;
    }
    public virtual void Execute()
    {
        if (Time.time - timeOfUse >= cooldown)
        {
            timeOfUse = Time.time;
            TriggerEffect();

        }
    }
    public virtual void TriggerEffect()
    {

    }

}
