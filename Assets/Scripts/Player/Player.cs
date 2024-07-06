using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    ////state list
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }    
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerFinishLevelState finishLevelState { get; private set; }
    #endregion    
    
    public bool isBusy { get; private set; }       

    [Header("Particles")]    
    public ParticleSystem dustFx;
    private float dustFxTimer;

    [Header("Movement Info")]
    public float moveSpeed;
    public float jumpForce;
    public float doubleJumpForce;    
    [HideInInspector] public bool canDoubleJump = true;
    
    private float defaultMoveSpeed;
    [HideInInspector] public float defaultJumpForce;
    private float defaultGravityScale;
     
    [Header("Knockback Player Info")]    
    [SerializeField] private float knockbackTime;
    [SerializeField] private float knockbackProtectionTime;    
    private bool canBeKnocked = true;    

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");
        finishLevelState = new PlayerFinishLevelState(this, stateMachine, "Win");
    }

    // Start is called before the first frame update
    protected override void Start()
    {              
        base.Start();        

        SetAnimationLayer();

        stateMachine.Initialize(idleState);
        
        defaultMoveSpeed = moveSpeed;
        defaultJumpForce = jumpForce;
        defaultGravityScale = rb.gravityScale;        
    }

    // Update is called once per frame
    protected override void Update()
    {        
        if (Time.timeScale == 0)
        {
            return;
        }

        base.Update();

        stateMachine.currentState.Update();                               
    }    
    
    private void SetAnimationLayer()
    {
        int skinIndex = PlayerManager.instance.choosenSkinId;

        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }

        anim.SetLayerWeight(skinIndex, 1);
    }        

    #region Knockback Player
    public void Knockback(Transform damageTransform)
    {
        if(!canBeKnocked)
        {
            return;
        }        

        if (GameManager.instance.difficulty > 1)
        {
            PlayerManager.instance.OnTakingDamage();
        }

        PlayerManager.instance.ScreenShake(PlayerManager.instance.normalMagnitudeShake, PlayerManager.instance.normalTimeShake);

        isKnocked = true;
        canBeKnocked = false;

        #region x / horizontal direction for knockback
        int hDirection = 0;
        if(transform.position.x > damageTransform.position.x)
        {
            hDirection = 1;
        }
        else if(transform.position.x <  damageTransform.position.x)
        {
            hDirection = -1;
        }
        #endregion

        rb.velocity = new Vector2(5 * hDirection, 10);

        Invoke("CancelKnockback", knockbackTime);
        Invoke("AllowKnockback", knockbackProtectionTime);
    }

    private void CancelKnockback()
    {
        isKnocked = false;
        rb.velocity = new Vector2(0, rb.velocity.y);

    }

    private void AllowKnockback()
    {
        canBeKnocked = true;
    }
    #endregion        

    #region Flip for Dust FX
    public override void FlipController(float _x)
    {
        base.FlipController(_x);
        dustFxTimer -= Time.deltaTime;
    }

    public override void Flip()
    {
        base.Flip();

        if (dustFxTimer < 0)
        {
            dustFx.Play();
            dustFxTimer = 0.5f;
        }
    }
    #endregion

    public void Push(float pushForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, pushForce);
    }    

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();    
    
    public void LevelFinished()
    {
        PlayerManager.instance.playerFinishTheLevel = true;
        stateMachine.ChangeState(finishLevelState);
    }        

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();        
    }
}
