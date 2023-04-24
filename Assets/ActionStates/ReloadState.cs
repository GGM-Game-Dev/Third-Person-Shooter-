using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : ActionBaseState
{
    public override void EnterState(ActionStateManager actionStateManager) {
        actionStateManager.rightHandAim.weight = 0;
        actionStateManager.leftHandIK.weight = 0;
        actionStateManager.anim.SetTrigger("Reload");
    }
    public override void UpdateState(ActionStateManager actionStateManager) { }
}
