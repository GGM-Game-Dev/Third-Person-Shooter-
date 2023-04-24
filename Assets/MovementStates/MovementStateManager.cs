using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public float currentMoveSpeed =3f;
    public float walkSpeed =3f, walkBackSpeed =2f;
    public float runSpeed =5f, runBackSpeed =4f;
    public float crouchSpeed =2f, crouchBackSpeed =1.5f;

    [HideInInspector] public float horInput, vertInput;
    [HideInInspector] public Vector3 direction;
    CharacterController controller;

    //Gravity Variables
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePosition;
    //Graviry on earth
    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;


    MovementBaseState currentState;
    public IdleState Idle= new IdleState();
    public WalkState Walk = new WalkState();
    public RunningState Running = new RunningState();
    public CrouchState Crouch = new CrouchState();

    [HideInInspector] public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();
        currentState.UpdateState(this);
        anim.SetFloat("hzInput",horInput);
        anim.SetFloat("vInput", vertInput);
    }

    void GetDirectionAndMove() 
    {
        horInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");

        //Always looking forward
        direction = transform.forward * vertInput + transform.right * horInput;

        controller.Move(direction.normalized * currentMoveSpeed * Time.deltaTime);
    }

    bool IsGrounded() 
    {
        spherePosition = new Vector3(transform.position.x, transform.position.y + groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePosition, controller.radius - 0.05f, groundMask)) 
            return true;
        return false;
    }

    void Gravity() 
    {
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        //When we hit the ground, we allways have velocity
        else if (velocity.y < 0) 
        {
            velocity.y = -2;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(spherePosition, controller.radius - 0.05f); 
    }

    public void SwitchState(MovementBaseState state) 
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
