using UnityEngine;

[CreateAssetMenu(fileName = "AbilityGlide", menuName = "Scriptable Objects/AbilityGlide")]
public class AbilityGlide : AbilityBase
{
    public float speedModifier = 1.5f;
    public override void TriggerEffect()
    {
        Debug.Log("gliding");
        
        PlayerController.PlayerRb.linearVelocityY = 0;
        PlayerController.PlayerRb.gravityScale = 0.2f;
        PlayerController.movementLocked = true;
        // PlayerController.movementSpeed = speedModifier;
        PlayerController.PlayerRb.linearVelocityX *= speedModifier;

    }
    public override void AbilityHoldCanceled()
    {

        if (inHoldUse)
        {
            PlayerController.PlayerRb.gravityScale = 1f;
            PlayerController.movementSpeed = PlayerController.baseSpeed;
            PlayerController.movementLocked = false;

        }
        base.AbilityHoldCanceled();
        
    }
    /*
    public override void CancelOnGrounded()
    {
        AbilityHoldCanceled();
    }
    */


}
