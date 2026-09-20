using UnityEngine;

[CreateAssetMenu(fileName = "AbilityRoll", menuName = "Scriptable Objects/AbilityRoll")]
public class AbilityRoll : AbilityBase
{
    public float rollForce = .7f;
    public override void TriggerEffect()
    {
        //PlayerController.movementLocked = true;
        PlayerController.isDrifting = true;
        PlayerController.isRolling = true;
        if (PlayerController.isGrounded)
        {
            PlayerController.controller.Roll(rollForce);
        }
        else
        {
            PlayerController.controller.SlamThenRoll(rollForce);
        }
        // test

    }
    public override void AbilityHoldCanceled()
    {
        PlayerController.groundedLenience = Time.time;
        PlayerController.isDrifting = true;
        PlayerController.isRolling = false;
        
            // PlayerController.movementLocked = false;
            
        
        base.AbilityHoldCanceled();
    }
    public override void CancelOnGrounded()
    {
        
    }
    
}
