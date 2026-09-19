using UnityEngine;

[CreateAssetMenu(fileName = "AbilityGlide", menuName = "Scriptable Objects/AbilityGlide")]
public class AbilityGlide : AbilityBase
{
    public float modifiedSpeed = 7.0f;
    public override void TriggerEffect()
    {
        
        PlayerController.PlayerRb.linearVelocityY = 0;
        PlayerController.PlayerRb.gravityScale = 0.5f;
        PlayerController.movementSpeed = modifiedSpeed;

    }
    public override void AbilityHoldCanceled()
    {
        if (inHoldUse)
        {
            PlayerController.PlayerRb.gravityScale = 1f;
            PlayerController.movementSpeed = PlayerController.baseSpeed;

        }
        base.AbilityHoldCanceled();
        
    }
    public override void CancelOnGrounded()
    {
        AbilityHoldCanceled();
    }


}
