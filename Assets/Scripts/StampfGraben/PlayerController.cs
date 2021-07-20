using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{
    private Rigidbody player;
    private AudioSource audio;
    public bool IsPlayer1;

    [HideInInspector]public bool AllowedToMove;
    
    public float Speed;
    public float JumpForce = 5f;
    public float FallMultiplier = 2.5f;
    public float LowJumpMultiplier = 2f;
    public float StopInAirTime;
    [HideInInspector] public float TempAirTime;
    public float StompSpeed;
    public float StompPower = 0f;
    private float fallSpeed;
    public Vector2 moveInput;
    
    private ParticleController particles;
    
    public LayerMask WhatIsGround;
    public bool IsGrounded;
    public bool IsFalling;
    public bool IsStomping;
    public bool FacingRight = true;
    public bool FacingFront = true;
    public bool WalkedBack;
    private bool jumpPressed = false;
    private bool isJumping = false;
    private bool isFalling = false;

    public Animation FrontAnim;
    public Animation FrontWalk;
    public Animation FrontJump;
    public Animation FrontFall;
    public Animation FrontStomp;
    public Animation BackAnim;
    public Animation BackWalk;
    public Animation BackJump;
    public Animation BackFall;
    public Animation BackStomp;
    public SpriteAnimation Animator;
    
    private Flip flip;
    
    
    public Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>(){};
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
        if (IsPlayer1)
        {
            controls = ControlDicts.PlayerControls_1;
        }
        else
        {
            controls = ControlDicts.PlayerControls_2;
        }
        TempAirTime = StopInAirTime;
        flip = GetComponent<Flip>();
        particles = GetComponent<ParticleController>();
        audio = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        jumpPressed |= Input.GetKeyDown(controls["jump"]);
        flip.FlipSprite(FacingRight, FacingFront);
    }

    private void FixedUpdate()
    {
        DetermineInput();
        DetermineAnimations();
        moveInput.Normalize();
        player.velocity = new Vector3(moveInput.x, 0f, moveInput.y) * Speed;

        //Check For Ground
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, .5f, WhatIsGround))
        {
            IsGrounded = true;
            IsFalling = false;
            IsStomping = false;
        }
        else
        {
            IsGrounded = false;
        }

        //All things Jumping
        if (jumpPressed && IsGrounded)
        {
            fallSpeed = JumpForce;
            StompPower = 0;
        }

        //Press longer to jump higher adapted code from Board To Bits Games https://www.youtube.com/watch?v=7KiK0Aqtmzc
        if (!IsGrounded || fallSpeed > 0)
        {
            float gravityMultiplier = 1f;
            if (fallSpeed < 0)
            {
                gravityMultiplier = FallMultiplier;
            }
            else if (!Input.GetKey(controls["jump"]))
            {
                gravityMultiplier = LowJumpMultiplier;
            }

            if (!IsGrounded && !IsFalling && Input.GetKey(controls["stomp"]) && !IsStomping)
            {
                IsStomping = true;
                fallSpeed = 0;
                player.velocity = new Vector3(0f, fallSpeed, 0f);
                particles.StartEffect();
                if (StompPower <= 0.3f)
                {
                    Physics.Raycast(transform.position, Vector3.down, out hit, 60f, WhatIsGround);
                    StompPower = hit.distance;
                }

                StartCoroutine(nameof(GroundPound));
            }
            else
            {
                fallSpeed += Physics.gravity.y * gravityMultiplier * Time.fixedDeltaTime;
                player.velocity += new Vector3(0f, fallSpeed, 0f);
            }

        }
        else
        {
            fallSpeed = 0f;
        }

        player.velocity += new Vector3(0f, fallSpeed, 0f);

        jumpPressed = false;
    }

    //adapted Code from freaky Feet https://www.youtube.com/watch?v=BCPyxUk64Bg
    private IEnumerator GroundPound()
    {
        player.isKinematic = true;
        DetermineAnimations();
        audio.Play();
        yield return new WaitForSeconds(TempAirTime);
        player.isKinematic = false;   
        fallSpeed += StompSpeed;
        player.velocity = new Vector3(0, fallSpeed, 0);

    }

    public void DetermineInput()
    {
        if (!Input.GetKey(controls["left"]) && !Input.GetKey(controls["right"]))
        {
            moveInput.x = 0;
        }
        else if (Input.GetKey(controls["left"]))
        {
            moveInput.x = -Speed;
        }
        else if (Input.GetKey(controls["right"]))
        {
            moveInput.x = Speed;
        }

        /*if (!Input.GetKey(controls["forward"]) && !Input.GetKey(controls["back"]))
        {
            moveInput.y = 0;
        }
        else if (Input.GetKey(controls["forward"]))
        {
            moveInput.y = Speed;
        }
        else if (Input.GetKey(controls["back"]))
        {
            moveInput.y = -Speed;
        }*/
        
    }
    
    public void DetermineAnimations()
    {
        DetermineDirection();
        if (Input.GetKey(controls["stomp"]) || IsStomping)
        {
            if(WalkedBack)
            {
                Animator.AnimationReset();
                Animator.Animation = BackStomp;
            }else
            {
                Animator.AnimationReset();
                Animator.Animation = FrontStomp;
            }
            return;
        }
        //If NOT MOVING AT ALL
        if(player.velocity == Vector3.zero)
        {
                if(WalkedBack)
                {
                    Animator.AnimationReset();
                    Animator.Animation = BackAnim;
                }
                else
                {
                    Animator.AnimationReset();
                    Animator.Animation = FrontAnim;
                }
        }
        else //when theres ANY KIND of movement happening
        {
            if(FacingFront)
            {
                if(isJumping)
                {
                    Animator.AnimationReset();
                    Animator.Animation = FrontJump;
                }
                else if(IsFalling)
                {
                    Animator.AnimationReset();
                    Animator.Animation = FrontFall;
                }
                else
                {
                    if(Animator.Animation != FrontWalk)
                    {
                        Animator.AnimationReset();
                    }
                    Animator.Animation = FrontWalk;
                }
            }
            else
            {
                if(isJumping)
                {
                    Animator.AnimationReset();
                    Animator.Animation = BackJump;
                }
                else if(isFalling)
                {
                    Animator.AnimationReset();
                    Animator.Animation = BackFall;
                }
                else
                {
                    if(Animator.Animation != BackWalk)
                    {
                        Animator.AnimationReset();
                    }
                    Animator.Animation = BackWalk;
                }
            }
        }
    }
    
    public void DetermineDirection()
    {
        if(player.velocity.x < 0 && FacingRight)
        {
            FacingRight = false;
        }
        if(player.velocity.x > 0 && !FacingRight)
        {
            FacingRight = true;
        }
        if(player.velocity.z > 0 && FacingFront)
        {
            FacingFront = false;
            WalkedBack = true;
        }
        if(player.velocity.z < 0 && !FacingFront)
        {
            FacingFront = true;
            WalkedBack = false;
        }
        if(IsGrounded)
        {
            IsFalling = false;
            isJumping = false;
        }
        if(player.velocity.y < 0 && !IsGrounded)
        {
            IsFalling = true;
            isJumping = false;
        }
        if(player.velocity.y > 0 && IsGrounded)
        {
            isJumping = true;
        }

    }
    
}
