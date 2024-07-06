using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (player.IsWallDetected())
        {
            stateMachine.ChangeState(player.wallSlide);
        }

        if (player.IsGroundDetected())
        {
            player.dustFx.Play();
            stateMachine.ChangeState(player.idleState);
        }
        
        if (Input.GetButtonDown("Jump") && player.canDoubleJump)
        {            
            player.canDoubleJump = false;
            player.jumpForce = player.doubleJumpForce;
            stateMachine.ChangeState(player.jumpState);
        }        

        if (xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y);
        }
    }
}
