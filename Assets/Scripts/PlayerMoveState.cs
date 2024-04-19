using System;
using System.Collections.Generic;
using System.Linq;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
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
        player.SetVelocity(xInput * player.moveSpeed, zInput * player.moveSpeed);
        if(xInput == 0 && zInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
