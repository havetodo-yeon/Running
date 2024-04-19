﻿using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerJumpState : PlayerGroundedState
{
    public PlayerJumpState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector3(rb.velocity.x, player.jumpForce, rb.velocity.z);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(rb.velocity.y < 0 && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}