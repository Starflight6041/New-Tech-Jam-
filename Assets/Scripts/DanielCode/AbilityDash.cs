using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityDash", menuName = "Scriptable Objects/AbilityDash")]
public class AbilityDash : AbilityBase
{
    public float dashDuration = 0.5f;
    public float dashLength = 3.0f;
    public override void TriggerEffect()
    {
        Debug.Log("Dashing");
        PlayerController.controller.Dash(dashDuration, dashLength);
    }

   
}
