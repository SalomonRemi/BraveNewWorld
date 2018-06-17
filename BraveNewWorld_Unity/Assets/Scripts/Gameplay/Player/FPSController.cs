using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour {

    [Header("Movement Settings")]
    public float forwardSpeed;
    public float backwardSpeed;
    public float strafeSpeed;
    public bool canSprint;
    [Range(1f, 3f)] public float sprintMultiplier;

    [Header("Mouse Settings")]
    [Range(1f, 8f)] public float mouseSensitivity;
    public float clampRotation;

    [Header("Jump and Falling Settings")]
    public bool canJump;
    public bool canAirControl;
    public float jumpForce;
    public float gravityMultiplier;


    private float forwardVelocity;
    private float sideVelocity;
    private float verticalVelocity;
    private float originalForwardSpeed;
    private float _xRot;
    private float _yRot;
    private float stickToGroundForce;
    private float hitGroundSpeedModifier = 1f;

    private bool sprint;
    private bool isJumping;
    private bool isSprinting;
    private bool isFalling;

    //HeadBob boolean
    private bool isMovingForward;
    private bool isMovingBackward;
    private bool isMovingSideLeft;
    private bool isMovingSideRight;

    private Vector3 velocity;

    private Quaternion rotation;

    CharacterController cc;


    private void Start()
    {
        cc = GetComponent<CharacterController>();

        stickToGroundForce = 4f;
        originalForwardSpeed = forwardSpeed;

        if (canSprint) sprint = true;
    }


    private void Update ()
    {
        GravityManagement();

        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            Jump();
        }

        RotateCamera();

        if(isFalling == true && canAirControl == false)
        {
        }
        else Movement();

        StateCheck();
    }

    private void LateUpdate()
    {
        ApplyMovement();
    }


    private void Movement()
    {
        //GET MOVEMENT VELOCITY
        forwardVelocity = Input.GetAxis("Vertical");
        sideVelocity = Input.GetAxis("Horizontal");

        forwardVelocity = forwardVelocity * forwardSpeed;
        sideVelocity = sideVelocity * strafeSpeed;
        
        //APPLY SPRINT
        if (sprint && Input.GetKey(KeyCode.LeftShift) && forwardVelocity > 0)
        {
            LeanTween.value(forwardSpeed, originalForwardSpeed * sprintMultiplier, .1f)
                .setOnUpdate((float val) =>
                {
                    forwardSpeed = val;
                });
            isSprinting = true;
        }
        else if (isSprinting == true)
        {
            LeanTween.value(forwardSpeed, originalForwardSpeed, .2f)
                .setOnUpdate((float val) =>
                {
                    forwardSpeed = val;
                });
            isSprinting = false;
        }
        else if (sprint) forwardSpeed = originalForwardSpeed; 

        // REDUCE SPEED WHEN STRAFE + FORWARD
        if (Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Horizontal") != 0) 
        {
            forwardVelocity = forwardVelocity / 1.3f;
        }

        rotation = transform.rotation;
    }


    private void ApplyMovement()
    {
        //APPLY MOVEMENT VELOCITY
        velocity = new Vector3(sideVelocity * hitGroundSpeedModifier, verticalVelocity, forwardVelocity * hitGroundSpeedModifier);
        velocity = rotation * velocity;
        cc.Move(velocity * Time.deltaTime);
    }


    private void Jump()
    {
        if (cc.isGrounded && isJumping == false)
        {
            isJumping = true;
            verticalVelocity = jumpForce * 1.5f;
        }
    }


    private void RotateCamera()
    {
        _yRot = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, _yRot, 0);

        _xRot -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        _xRot = Mathf.Clamp(_xRot, -clampRotation, clampRotation);
        Camera.main.transform.localRotation = Quaternion.Euler(_xRot, 0, 0);
    }


    private void GravityManagement()
    {
        if (cc.isGrounded)
        {
            verticalVelocity = -stickToGroundForce;
            isJumping = false;
        }
        else
        {
            isFalling = true;
            verticalVelocity += Physics.gravity.y * Time.deltaTime * gravityMultiplier;
        }

        if(isFalling == true && cc.isGrounded == true)
        {
            StartCoroutine(HitGroundDecceleration());
            if (canSprint) sprint = true;
            isFalling = false;
        }
    }


    private void StateCheck()
    {
        if(forwardVelocity > 0)
        {
            isMovingBackward = false;
            isMovingForward = true;
        }
        else if (forwardVelocity < 0)
        {
            isMovingBackward = true;
            isMovingForward = false;
        }
        else
        {
            isMovingBackward = false;
            isMovingForward = false;
        }

        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            isMovingSideLeft = false;
            isMovingSideRight = true;
        }
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            isMovingSideLeft = true;
            isMovingSideRight = false;
        }
        else
        {
            isMovingSideLeft = false;
            isMovingSideRight = false;
        }
    }


    IEnumerator HitGroundDecceleration()
    {
        if (forwardVelocity != 0)
        {
            LeanTween.value(.7f, .5f, .05f)
                .setOnUpdate((float val) =>
                {
                    hitGroundSpeedModifier = val;
                });
        }

        yield return new WaitForSeconds(.07f);

        LeanTween.value(hitGroundSpeedModifier, 1f, .1f)
        .setOnUpdate((float val) =>
        {
            hitGroundSpeedModifier = val;
        });

        yield return new WaitForSeconds(.12f);

        hitGroundSpeedModifier = 1f;
    }


    //ACCESSOR FOR HEADBOB SCRIPT
    public bool GetIsSprinting()
    {
        return isSprinting;
    }

    public bool GetIsJumping()
    {
        return isJumping;
    }

    public bool GetIsFalling()
    {
        return isFalling;
    }

    public bool GetIsMovingForward()
    {
        return isMovingForward;
    }

    public bool GetIsMovingBackward()
    {
        return isMovingBackward;
    }

    public bool GetIsMovingLeft()
    {
        return isMovingSideLeft;
    }

    public bool GetIsMovingRight()
    {
        return isMovingSideRight;
    }
}