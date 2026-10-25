using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] bool cursorLock = true;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float Speed = 6.0f;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] float gravity = -30f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] float flightSpeed = 6f;

    public float jumpHeight = 6f;
    float velocityY;
    bool isGrounded;
    float cameraCap;
    bool cameraLocked;
    bool movementLocked;
    bool isFlying;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;
    CharacterController controller;
    Vector2 currentDir;
    Vector2 currentDirVelocity;
    Vector3 velocity;



    void Start()
    {

        controller = GetComponent<CharacterController>();

        cameraCap = 0f;
        playerCamera.localRotation = Quaternion.Euler(cameraCap, 0f, 0f);

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        UpdateMouse();
        UpdateMove();
    }
    void UpdateMouse()
    {
        if (cameraLocked)
        {
            return; //Prevents camera movement
        }
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);
        cameraCap -= currentMouseDelta.y * mouseSensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -180.0f, 0.0f);
        playerCamera.localRotation = Quaternion.Euler(cameraCap, 0f, 0f);
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }
    void UpdateMove()
    {
        if (movementLocked == true)
        {
            return; //Prevents player from jittering when trying to sit at workstation
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        velocityY += gravity * 2f * Time.deltaTime;


        //For flight powerup
        float verticalInput = 0f;
        if (isFlying == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                verticalInput = 1f;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                verticalInput = -1f;
            }
            velocityY = verticalInput * flightSpeed; //Flight speed gives us easily tweakable access to flight speed for when we revamp/tweak the values of this script for the final game.
        }
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * Speed + Vector3.up * velocityY;




        controller.Move(velocity * Time.deltaTime);

        if (!isFlying && isGrounded && Input.GetButtonDown("Jump"))
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (!isFlying && !isGrounded && controller.velocity.y < -1f)
        {
            velocityY = -8f;
        }
    }

    //Camera locked method
    public void SetCameraLocked(bool locked)
    {
        cameraLocked = locked;
    }

    public void SetMovementLocked(bool locked)
    {
        movementLocked = locked;
    }

    //Wingardium Leviosa powerup - Flight
    //Will add a "quick cancel" to this later.
    //Found a tutorial
    public void StartFlight(float duration)
    {
        if (isFlying)
        {
            //Probably should start a cooldown timer from here.
            return;
        }
        StartCoroutine(FlightRoutine(duration));
    }

    IEnumerator FlightRoutine(float duration)
    {
        isFlying = true;
        float originalGravity = gravity; //Storing it here, so that it can be restored once flight powerup ends.

        gravity = 0f; //Reassigning so that we can fly
        velocityY = 0f;

        yield return new WaitForSeconds(duration); //Waits for the duration of the ability.

        gravity = originalGravity;

        isFlying = false;
    }
}
