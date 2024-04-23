using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallFlatState : PlayerState
{
    public PlayerFallFlatState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Z) && player.IsGroundDetected())
        {
            Debug.Log("착지대시!!!!!");
        }

        if (player.IsGroundDetected())
        {
            Debug.Log("착지!!!!!"); 
            stateMachine.ChangeState(player.idleState);
        }

    }
}
