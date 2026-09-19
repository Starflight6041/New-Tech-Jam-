using UnityEngine;

[CreateAssetMenu(fileName = "AbilityDoubleJump", menuName = "Scriptable Objects/AbilityDoubleJump")]
public class AbilityDoubleJump : AbilityBase
{
    public float jumpHeight = 10f;
    public override void TriggerEffect()
    {
        Debug.Log("Jumping");
        //PlayerController.PlayerRb.AddForceY(jumpHeight);
        PlayerController.PlayerRb.linearVelocityY = jumpHeight;
    }
   

}
