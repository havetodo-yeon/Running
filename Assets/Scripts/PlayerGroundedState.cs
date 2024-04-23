using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
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
        if (Input.GetKeyDown(KeyCode.LeftControl) && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        if (Input.GetKeyDown(KeyCode.Z) && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.dashState);
        }
        if (rb.velocity.y <= 0)
        {
            stateMachine.ChangeState(player.fallState);
        }

    }
}
