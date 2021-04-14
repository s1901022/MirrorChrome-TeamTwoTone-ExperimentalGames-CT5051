using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerMovement : MonoBehaviour
{
    //Variables for Movement and direction
    [SerializeField]
    private int jumplimit;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float minJumpForce;
    [SerializeField]
    private float MaxjumpForce;
    private float jumpForce;
    private float moveInput;
    private bool facingRight = true;
    public bool grounded = false;
    private int extraJumps;
    //Variables for collision detection
    public Transform groundCheck;
    [SerializeField]
    private float checkRadius;
    [SerializeField]
    private LayerMask ground;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private scrEntity m_entity;
    private scrSwitchGravity m_mirror;

    //Animation
    [SerializeField]
    private Sprite idle;
    [SerializeField]
    private Sprite run;

    private enum AnimationStates 
    {
        Idle,
        Running,
        Jumping,
        Pushing
    };

    private Animator anim;
    private AnimationStates animState;


    // Start is called before the first frame update
    void Start()
    {
        extraJumps = jumplimit;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        m_entity = GetComponent<scrEntity>();
        m_mirror = GetComponent<scrSwitchGravity>();
        anim = GetComponent<Animator>();
        animState = AnimationStates.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_entity.GetDead())
        {

            moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            if (m_entity.GetGrounded() && rb.velocity.y == 0f)
            {
                jumpForce = 0f;
                extraJumps = jumplimit;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && m_entity.GetGrounded() && rb.velocity.y == 0f)
            {
                rb.velocity += Vector2.up * MaxjumpForce;
                animState = AnimationStates.Jumping;
                extraJumps--;
                Debug.Log("JUMPING");
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(rb.velocity.y, 0f, 0f));
            }

            anim.SetInteger("state", (int)animState);
            StateSwitch();
        }
    }

    private void StateSwitch()
    {
        if ((facingRight == false && moveInput > 0) || (facingRight == true && moveInput < 0)) {
            AnimationControl();
        }

        if (animState == AnimationStates.Jumping) 
        {

            if (m_entity.GetGrounded())
            {
                animState = AnimationStates.Idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon) {
            // Going right
            animState = AnimationStates.Running;
            if (m_entity.GetGrounded()) { m_entity.GetAudioManager().PlayAudio(0, "FootStep"); }

        } else {
            animState = AnimationStates.Idle;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Handle Death and respawning
        if (col.gameObject.tag == "Death" && !m_entity.GetDead())
        {
            if (m_mirror.isFlipped % 2 != 0) {
                m_mirror.isFlipped = 0;
                m_mirror.ResetForNextTry();
                //m_mirror.ResetRotation();
                //m_mirror.Reset();
            }
            m_mirror.ResetRotation();
            m_mirror.Reset();
        }
    }

    //This will need to be removed and replaced with unitys animation system when ready
    public void AnimationControl()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    //Getters
    public float GetJumpForce() { return MaxjumpForce; }
    public bool GetDirection() { return facingRight; }
    //Setters
    public void FlipJumpGrav() 
    {
        minJumpForce = minJumpForce * -1;
        MaxjumpForce = MaxjumpForce * -1; 
    }
    public void SetDirection(bool a_isRightFace) { facingRight = a_isRightFace; }
}
