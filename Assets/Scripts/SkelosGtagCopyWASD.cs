using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using GorillaLocomotion;
using GorillaKit;

public class SkelosGtagCopyWASD : MonoBehaviour
{
    [Header("BINDINGS\n\nWASD-Moving\nSpace-Jumping\nQ-ZeroGravity\nESC-QuitPlaymode\nShift-FastMove\nLeftClick - Interact")]
    [Space]
    [Header("Script by skelo_thereal")]
    [Space]
    [Header("Player Settings")]
    public Player GorillaPlayer;
    public Camera playerCamera;
    public Transform cameraTransform;
    public Transform ControllerR;
    public Transform ControllerL;
    [Header("No-Controller Settings")]
    public Transform SpotR;
    public Transform SpotL;
    [Header("Speed Values")]
    [Range(1f, 30f)]
    public float NormalSpeed = 7f;
    [Range(5f, 50f)]
    public float RunSpeed = 30f;
    [Range(0.1f, 1f)]
    public float rotationSpeed = 0.5f;
    [Header("Audio Settings")]
    public AudioSource AudioSource;
    [Space]
    [Header("Dont Mess With this!!!")]
    [Space]
    [SerializeField] private bool UsingZeroGravity = false;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Vector3 rotationVector = Vector3.zero;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private Mouse mouse;

    void Awake()
    {
        mouse = Mouse.current;
    }
    void Start()
    {
        playerRigidbody = GorillaPlayer.GetComponent<Rigidbody>();

        ControllerR.position = SpotR.position;
        ControllerL.position = SpotL.position;
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        moveDirection.y = 0;
        moveDirection.Normalize();

        if (Keyboard.current.wKey.isPressed) moveDirection += cameraTransform.forward;
        if (Keyboard.current.sKey.isPressed) moveDirection -= cameraTransform.forward;
        if (Keyboard.current.aKey.isPressed) moveDirection -= cameraTransform.right;
        if (Keyboard.current.dKey.isPressed) moveDirection += cameraTransform.right;
        if (Keyboard.current.leftShiftKey.isPressed) moveSpeed = RunSpeed;
        else moveSpeed = NormalSpeed;

        if (Keyboard.current.spaceKey.isPressed)
        {
            moveDirection -= cameraTransform.up;
            moveDirection.y = 2;
        }

        if (Keyboard.current.qKey.isPressed)
        {
            if (!UsingZeroGravity)
            {
                Player.Instance.GetComponent<Rigidbody>().useGravity = false;
                UsingZeroGravity = true;
                Debug.Log("Entering Zero Gravity Mode...");
            }
        }
        else
        {
            if (UsingZeroGravity)
            {
                Player.Instance.GetComponent<Rigidbody>().useGravity = true;
                UsingZeroGravity = false;
                Debug.Log("Exiting Zero Gravity Mode...");
            }
        }

        if (Keyboard.current.escapeKey.isPressed)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            Debug.Log("Exiting Playmode...");
#endif
        }

        playerRigidbody.MovePosition(playerRigidbody.position + moveDirection * moveSpeed * Time.deltaTime);

        if (Mouse.current.rightButton.isPressed)
        {
            float mouseX = Mouse.current.delta.x.ReadValue() * rotationSpeed;
            float mouseY = Mouse.current.delta.y.ReadValue() * rotationSpeed;

            rotationVector.x -= mouseY;
            rotationVector.y += mouseX;
            rotationVector.x = Mathf.Clamp(rotationVector.x, -90f, 90f);

            cameraTransform.localRotation = Quaternion.Euler(rotationVector);
        }

        if (mouse.leftButton.wasPressedThisFrame)
        {
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(mouse.position.ReadValue());

            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                Trigger trigger = hit.collider.GetComponent<Trigger>();
                if (trigger != null) trigger.Enter(hit.collider); PlaySound();
            }
        }

        if (mouse.leftButton.wasReleasedThisFrame)
        {
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(mouse.position.ReadValue());
            
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                Trigger trigger = hit.collider.GetComponent<Trigger>();
                if (trigger != null) trigger.Exit(hit.collider);
            }
        }
    }
    void PlaySound()
    {
        if (AudioSource != null)
        {
            AudioSource.Play();
        }
    }
}

   //////////////////////////////////////////////
  ///SCRIPT MADE BY SKELO_THEREAL///////////////
 ///DO NOT REMOVE THIS OR YOU ARE A SKIDDER////
//////////////////////////////////////////////