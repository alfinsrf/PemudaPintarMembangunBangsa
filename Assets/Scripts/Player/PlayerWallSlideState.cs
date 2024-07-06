using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (player.IsWallDetected() == false)
        {
            player.canDoubleJump = true;

            stateMachine.ChangeState(player.airState);
        }

        if (Input.GetButtonDown("Jump"))
        {
            player.canDoubleJump = true;

            stateMachine.ChangeState(player.wallJump);
            return;
        }

        if (yInput < 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y * 0.1f);
        }        

        if (xInput != 0 && player.facingDir != xInput)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (player.IsGroundDetected())
        {
            player.dustFx.Play();
            stateMachine.ChangeState(player.idleState);
        }
    }
}
