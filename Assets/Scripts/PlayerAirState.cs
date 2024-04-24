using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerAirState : PlayerGroundedState
{
    public PlayerAirState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
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

        if (!player.IsGroundDetected() && Input.GetKeyDown(KeyCode.LeftControl))
        {
            stateMachine.ChangeState(player.doubleJumpState);
        }
        if (player.IsGroundDetected() && rb.velocity.y <= 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
        if(rb.velocity.y <= -8)
        {
            stateMachine.ChangeState(player.fallState);
        }



    }
}
