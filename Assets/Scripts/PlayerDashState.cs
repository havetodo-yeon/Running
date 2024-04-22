﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerDashState : PlayerGroundedState
{
    public PlayerDashState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
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
        
        player.SetVelocity(xInput * player.moveSpeed * 1.5f , zInput * player.moveSpeed * 1.5f);

        if (xInput == 0 && zInput == 0 || Input.GetKeyUp(KeyCode.Z))
        {
            stateMachine.ChangeState(player.idleState);
        }

    }
}
