using UnityEngine;

[CreateAssetMenu(fileName = "AbilityGlide", menuName = "Scriptable Objects/AbilityGlide")]
public class AbilityGlide : AbilityBase
{
    public float speedModifier = 1.05f;
    public override void TriggerEffect()
    {
        if(PlayerController.PlayerRb.linearVelocityX < 0)
        {
            SimplePlayer.animator.SetBool("GlideLeft", true);
        }
        else if (PlayerController.PlayerRb.linearVelocityX > 0)
        {
            SimplePlayer.animator.SetBool("GlideRight", true);
        }
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
            SimplePlayer.animator.SetBool("GlideLeft", false);
            SimplePlayer.animator.SetBool("GlideRight", false);
        
        base.AbilityHoldCanceled();
        
    }
    
    
    
    public override void CancelOnGrounded()
    {
        SimplePlayer.animator.SetBool("GlideLeft", false);
        SimplePlayer.animator.SetBool("GlideRight", false);
        AbilityHoldCanceled();
    }
    


}
