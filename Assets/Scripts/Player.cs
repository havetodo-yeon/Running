using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isBusy { get; private set; }

    [Header("Move info")]
    public float moveSpeed = 12f;
    public float jumpForce;

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;
    [SerializeField] private float rotateSpeed;


    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;



    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    //public PlayerHurtState hurtState { get; private set; }
    #endregion

    #region Animator
    public Animator anim { get; private set; }
    public Rigidbody rb { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(stateMachine, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, this, "Move");
        dashState = new PlayerDashState(stateMachine, this, "Dash");
        jumpState = new PlayerJumpState(stateMachine, this, "Jump");
        airState = new PlayerAirState(stateMachine, this, "Jump");
        //hurtState = new PlayerHurtState(stateMachine, this, "Hurt;")
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();

        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }
    
    public IEnumerator BusyFor(float _seconds)  //??
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    #region Collision
    public bool IsGroundDetected() => Physics.Raycast(groundCheck.position, Vector3.down, groundCheckDistance, whatIsGround);
    public bool IsWallDetected() => Physics.Raycast(wallCheck.position, Vector3.forward * facingDir, wallCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.forward * facingDir * wallCheckDistance);
    }
    #endregion


    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    #region Turn
    public void Turn()
    {
        Quaternion playerRotation = Quaternion.LookRotation(rb.velocity);
        rb.rotation = Quaternion.Slerp(rb.rotation, playerRotation, rotateSpeed * Time.deltaTime);
    }

    #endregion

    #region Velocity
    public void ZeroVelocity()
    {
        rb.velocity = new Vector3(0, 0, 0);
    }

    public void SetVelocity(float _xVelocity, float _zvelocity)
    {
        rb.velocity = new Vector3(_xVelocity, rb.velocity.y, _zvelocity);
        Turn();
    }
    #endregion
}
