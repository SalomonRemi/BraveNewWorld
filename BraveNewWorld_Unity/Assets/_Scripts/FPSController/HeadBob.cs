using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour {

    public GameObject camera;
    public GameObject cameraHolder;

    [Header("Movement Bob Settings")]
    public float verticalFloatStrength;
    public float verticalBobSpeed;
    public float sideMovementAngle;
    [Range(1f,2f)] public float sprintBobMultiplier;
    [Range(60f, 80f)] public float sprintFOV;

    [Header("IDLE Bob Settings")]
    public float IDLEFloatStrength;
    public float IDLEBobSpeed;

    [Header("Falling Bob Settings")]
    public AnimationCurve hitGroundCurve;
    [Range(1.5f,3.5f)] public float bobDuration;


    private float affectedCamLocalY;
    private float originalCamLocalY;
    private float camPauseCounter;
    private float originalFov;

    private float affectedLocalY;
    private float originalLocalY;
    private float pauseCounter;

    private float timeSinceInput;
    private float hitBobValue;
    private bool hitGroundBoolean;
    private bool wasInAir;

    private Vector3 sideAngle;

    CharacterController cc;
    GameObject player;
    FPSController fpsController;
    Camera cam;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        fpsController = player.GetComponent<FPSController>();
        cc = player.GetComponent<CharacterController>();

        cam = camera.GetComponent<Camera>();

        originalCamLocalY = camera.transform.localPosition.y;
        originalLocalY = transform.localPosition.y;
        originalFov = cam.fieldOfView;
    }


    void Update()
    {
        HeadBobIDLE();
        HeadBobVertical();
        HeadBobSideMovement();
        //HeadBobHitGround();
    }


    private void HeadBobVertical()
    {
        affectedCamLocalY = camera.transform.localPosition.y;

        if (fpsController.GetIsMovingForward() == true && cc.isGrounded || fpsController.GetIsMovingBackward() == true && cc.isGrounded
            || fpsController.GetIsMovingLeft() == true && cc.isGrounded || fpsController.GetIsMovingRight() == true && cc.isGrounded)
        {
            //MOVING FORWARD
            camPauseCounter += Time.deltaTime;
            if (camPauseCounter > 0.17) // time before start
            {
                camera.transform.localPosition = new Vector3(camera.transform.localPosition.x,
                affectedCamLocalY + ((float)Mathf.Sin(Time.time * verticalBobSpeed) * (verticalFloatStrength)),
                camera.transform.localPosition.z);
            }
        }
        else if(camera.transform.localPosition.y != originalCamLocalY)
        {
            camPauseCounter = 0;
            LeanTween.moveLocalY(cam.gameObject, originalCamLocalY, .5f);
        }
    }


    private void HeadBobIDLE()
    {
        affectedLocalY = transform.localPosition.y;

        if (fpsController.GetIsSprinting() == false && fpsController.GetIsMovingForward() == false && fpsController.GetIsMovingBackward() == false
            && fpsController.GetIsMovingLeft() == false && fpsController.GetIsMovingRight() == false
            && fpsController.GetIsFalling() == false && fpsController.GetIsJumping() == false)
        {
            //IDLE
            pauseCounter += Time.deltaTime;
            if (pauseCounter > 0.17) // time before start
            {
                transform.localPosition = new Vector3(transform.localPosition.x,
                affectedLocalY + ((float)Mathf.Sin(Time.time * IDLEBobSpeed) * IDLEFloatStrength),
                transform.localPosition.z);
            }
        }
        else if (transform.localPosition.y != originalLocalY)
        {
            pauseCounter = 0;
            if (transform.localPosition.y < originalLocalY && transform.localPosition.y > originalLocalY - 0.005f 
                || transform.localPosition.y > originalLocalY && transform.localPosition.y < originalLocalY + 0.005f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, originalLocalY, transform.localPosition.z);
            }
            if (transform.localPosition.y != originalLocalY)
            {
                if (transform.localPosition.y < originalLocalY)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.006f, transform.localPosition.z);
                }
                else if (transform.localPosition.y > originalLocalY)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.006f, transform.localPosition.z);
                }
            }
        }
    }


    private void HeadBobSideMovement()
    {
        if (fpsController.GetIsMovingLeft())
        {
            StopCoroutine(SideAngleBackTween());
            StartCoroutine(SideAngleTween(sideMovementAngle));
            transform.localEulerAngles = sideAngle;
        }
        else if (fpsController.GetIsMovingRight())
        {
            StopCoroutine(SideAngleBackTween());
            StartCoroutine(SideAngleTween(-sideMovementAngle));
            transform.localEulerAngles = sideAngle;
        }
        else
        {
            StopCoroutine(SideAngleTween(sideMovementAngle));
            StartCoroutine(SideAngleBackTween());
            transform.localEulerAngles = sideAngle;
        }
    }

    IEnumerator SideAngleTween(float angle)
    {
        LeanTween.value(sideAngle.z, angle, .3f)
        .setOnUpdate((float val) =>
        {
            sideAngle.z = val;
        });
        yield return new WaitForSeconds(.33f);
    }

    IEnumerator SideAngleBackTween()
    {
        LeanTween.value(sideAngle.z, 0, .2f)
        .setOnUpdate((float val) =>
        {
            sideAngle.z = val;
        });
        yield return new WaitForSeconds(.25f);
    }


    private void HeadBobHitGround()
    {
        if(cc.isGrounded == false)
        {
            wasInAir = true;
        }
        else if (cc.isGrounded && wasInAir)
        {
            hitGroundBoolean = true;
            wasInAir = false;
        }

        if(hitGroundBoolean)
        {
            timeSinceInput += Time.deltaTime * bobDuration;

            hitBobValue = hitGroundCurve.Evaluate(timeSinceInput);
            if (hitBobValue >= hitGroundCurve.Evaluate(1))
            {
                hitGroundBoolean = false;
                timeSinceInput = 0f;
                hitBobValue = 0f;
            }
        }

        cameraHolder.transform.localPosition = new Vector3(0,hitBobValue,0);
    }
}
