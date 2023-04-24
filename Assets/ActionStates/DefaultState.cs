using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : ActionBaseState
{
    public override void EnterState(ActionStateManager actionStateManager) {
        actionStateManager.rightHandAim.weight = 1;
        actionStateManager.leftHandIK.weight = 1;

    }
    public override void UpdateState(ActionStateManager actionStateManager) 
    {
        actionStateManager.rightHandAim.weight = Mathf.Lerp(actionStateManager.rightHandAim.weight, 1, 10 * Time.deltaTime);
        actionStateManager.leftHandIK.weight = Mathf.Lerp(actionStateManager.leftHandIK.weight, 1, 10 * Time.deltaTime);

        if (Input.GetKey(KeyCode.R) && CanReload(actionStateManager)) 
        {
            actionStateManager.SwitchState(actionStateManager.reloadState);
        }
    }

    bool CanReload(ActionStateManager actionStateManager) {
        //clip is full
        if (actionStateManager.ammo.currentAmmo == actionStateManager.ammo.clipSize)
        {
            return false;
        }
        //no ammo
        else if (actionStateManager.ammo.extraAmmo == 0) 
        {
            return false;
        }
        return true;
    }
}
