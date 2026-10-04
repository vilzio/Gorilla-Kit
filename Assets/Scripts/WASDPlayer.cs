using UnityEngine;
using UnityEngine.InputSystem;
using GorillaLocomotion;
using GorillaKit;

public class WasdPlayer : MonoBehaviour
{
    [Header("Settings")]
    public Player gorillaPlayer;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public float turnSens = 1f;
    public LayerMask desktopMask;
    
    private Rigidbody rb;
    private Camera cam;
    private Vector3 moveDirection;
    private float moveSpeed;
    private Vector3 turnDirection;
    private Trigger lastTrigger;
    private LayerMask vrMask;

    private void Start()
    {
        if (!gorillaPlayer)
            gorillaPlayer = GetComponentInChildren<Player>();

        rb = gorillaPlayer.GetComponent<Rigidbody>();
        cam = Camera.main;
        moveSpeed = walkSpeed;
        vrMask = gorillaPlayer.locomotionEnabledLayers;
    }
    
    private void Update()
    {
        Movement();
        Jump();
        Turn();
        Ray();
    }

    private void Movement()
    {
        moveDirection.x = Keyboard.current.dKey.isPressed ? 1f : Keyboard.current.aKey.isPressed ? -1f : 0f;
        moveDirection.z = Keyboard.current.wKey.isPressed ? 1f : Keyboard.current.sKey.isPressed ? -1f : 0f;
        moveDirection.Normalize();
        moveDirection = cam.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;
        
        moveSpeed = Keyboard.current.leftShiftKey.isPressed ? runSpeed : walkSpeed;
        
        rb.MovePosition(rb.position + moveDirection * (moveSpeed * Time.deltaTime));
    }
    
    private void Jump()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Turn()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            turnDirection.x -= Mouse.current.delta.y.ReadValue();
            turnDirection.y += Mouse.current.delta.x.ReadValue();
            turnDirection.x = Mathf.Clamp(turnDirection.x, -180, 180);
            cam.transform.eulerAngles = new Vector3(turnDirection.x * turnSens,
                cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
            gorillaPlayer.transform.eulerAngles = new Vector3(gorillaPlayer.transform.eulerAngles.x,
                turnDirection.y * turnSens, gorillaPlayer.transform.eulerAngles.z);
            
            gorillaPlayer.locomotionEnabledLayers = desktopMask;
        }
        else
        {
            gorillaPlayer.locomotionEnabledLayers = vrMask;
        }
    }

    private void Ray()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out hit))
        {
            if (Mouse.current.leftButton.wasPressedThisFrame) 
            {
                Trigger trigger = hit.collider.GetComponent<Trigger>();
                if (trigger)
                {
                    trigger.Enter(hit.collider);
                    lastTrigger = trigger;
                }
            }

            if (Mouse.current.leftButton.wasReleasedThisFrame) 
            {
                Trigger trigger = hit.collider.GetComponent<Trigger>();
                if (trigger)
                {
                    trigger.Exit(hit.collider);
                    lastTrigger = null;
                }
                else if (lastTrigger)
                {
                    lastTrigger.Exit(hit.collider);
                    lastTrigger = null;
                }
            }
        }
    }
}