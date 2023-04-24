using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleHipState : AimBaseState
{
    public override void EnterState(AimStateManager aimStateManager) 
    {
        aimStateManager.anim.SetBool("Aiming", false);
        aimStateManager.currentFOV = aimStateManager.hipFOV;


    }
    public override void UpdateState(AimStateManager aimStateManager) 
    {
        if (Input.GetKey(KeyCode.Mouse1)) 
        {
            aimStateManager.SwitchState(aimStateManager.RifleAimingState);
        }
    }
}
