using UnityEngine;

[CreateAssetMenu(fileName = "AbilityGlide", menuName = "Scriptable Objects/AbilityGlide")]
public class AbilityGlide : AbilityBase
{
    public float speedModifier = 1.05f;
    public override void TriggerEffect()
    {
        GameManager.isAbility = true;
        Debug.Log("gliding");
        Debug.Log(PlayerController.isGrounded);
        PlayerController.isDrifting = true;
        
        PlayerController.PlayerRb.linearVelocityY = 0;
        
        
        PlayerController.PlayerRb.gravityScale = 0.2f;
        PlayerController.movementLocked = true;
        // PlayerController.movementSpeed = speedModifier;
        PlayerController.PlayerRb.linearVelocityX *= speedModifier;

    }
    public override void AbilityHoldCanceled()
    {

        
        PlayerController.PlayerRb.gravityScale = 1f;
        // PlayerController.movementSpeed = PlayerController.baseSpeed;
            //PlayerController.movementLocked = false;
            //PlayerController.isDrifting = false;

        
        base.AbilityHoldCanceled();
        
    }
    
    
    
    public override void CancelOnGrounded()
    {
        AbilityHoldCanceled();
    }
    


}
