using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : MovementBaseState
{
    
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Crouching", true);

    }
    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(movement, movement.Running);
        }
        else if (Input.GetKeyUp(KeyCode.C)) 
        {
            if (movement.direction.magnitude < 0.1f)
            {
                ExitState((MovementStateManager)movement, movement.Idle);
            }
            else 
            {
                ExitState((MovementStateManager)movement, movement.Walk);
            }
        }

        if (movement.vertInput < 0)
        {
            movement.currentMoveSpeed = movement.crouchBackSpeed;
        }
        else
        {
            movement.currentMoveSpeed = movement.crouchSpeed;
        }
    }
    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("Crouching", false);
        movement.SwitchState(state);
    }
}
