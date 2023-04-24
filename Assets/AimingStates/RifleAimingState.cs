using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleAimingState : AimBaseState
{
    // Start is called before the first frame update
    public override void EnterState(AimStateManager aimStateManager)
    {
        aimStateManager.anim.SetBool("Aiming", true);
        aimStateManager.currentFOV = aimStateManager.adsFOV;

    }
    public override void UpdateState(AimStateManager aimStateManager)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            aimStateManager.SwitchState(aimStateManager.RifleHipState);
        }
    }
}
