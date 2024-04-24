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
    [SerializeField] protected LayerMask whatIsGround;
    /*    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
*/

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDoubleJumpState doubleJumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    public PlayerFallFlatState fallFlatState { get; private set; }
    public PlayerHurtState hurtState { get; private set; }
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
        doubleJumpState = new PlayerDoubleJumpState(stateMachine, this, "DoubleJump");
        airState = new PlayerAirState(stateMachine, this, "Jump");
        fallState = new PlayerFallState(stateMachine, this, "Fall");
        fallFlatState = new PlayerFallFlatState(stateMachine, this, "Fall");
        hurtState = new PlayerHurtState(stateMachine, this, "Hurt");
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

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
    }
    #endregion

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    #region Turn
    public void Turn()
    {
        // �÷��̾��� �ӵ��� ���� 0�� �ƴ� ���� ȸ���� ������Ʈ�մϴ�.
        // ���⼭ 0.1f�� �Ӱ谪�̸�, ��Ȳ�� ���� ������ �ʿ��� �� �ֽ��ϴ�.
        if (rb.velocity.sqrMagnitude > 0.1f)
        {
            // rb.velocity ������ �ٶ󺸴� Quaternion�� �����մϴ�.
            Quaternion playerRotation = Quaternion.LookRotation(rb.velocity.normalized);

            // X�� Z���� ȸ���� �����ϰ� Y�� ȸ������ �����մϴ�.
            playerRotation = Quaternion.Euler(0, playerRotation.eulerAngles.y, 0);

            // Quaternion.Slerp�� ������� �ʰ�, playerRotation�� ���� �����մϴ�.
            rb.rotation = playerRotation;
        }
        // �ӵ��� ���� 0�� ���, rb.rotation�� ������Ʈ���� �ʽ��ϴ�.
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
