using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Rigidbody2D rb;
    
    protected float xInput;
    protected float yInput;
    
    protected PlayerStateMachine stateMachine;
    protected Player player;

    private string animBoolName;

    protected bool triggerCalled;
    
    protected float stateTimer;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        if(PlayerManager.instance.playerIsDead == false)
        {           
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");
         
            player.anim.SetFloat("yVelocity", rb.velocity.y);
        }        
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
