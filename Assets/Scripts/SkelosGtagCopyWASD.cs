using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using GorillaLocomotion;
using GorillaKit;
using UnityEngine.Serialization;

public class SkelosGtagCopyWasd : MonoBehaviour
{
    [FormerlySerializedAs("GorillaPlayer")]
    [Header("BINDINGS\n\nWASD-Moving\nSpace-Jumping\nQ-ZeroGravity\nESC-QuitPlaymode\nShift-FastMove\nLeftClick - Interact")]
    [Space]
    [Header("Script by skelo_thereal")]
    [Space]
    [Header("Player Settings")]
    public Player gorillaPlayer;
    public Camera playerCamera;
    public Transform cameraTransform;
    public Transform controllerR;
    public Transform controllerL;
    [Header("No-Controller Settings")]
    public Transform spotR;
    public Transform spotL;
    [Header("Speed Values")]
    [Range(1f, 30f)]
    public float normalSpeed = 7f;
    [Range(5f, 50f)]
    public float runSpeed = 30f;
    [Range(0.1f, 1f)]
    public float rotationSpeed = 0.5f;
    [Header("Audio Settings")]
    public AudioSource audioSource;
    [Space]
    [Header("Dont Mess With this!!!")]
    [Space]
    [SerializeField] private bool usingZeroGravity = false;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Vector3 rotationVector = Vector3.zero;
    [SerializeField] private float rayDistance = 10f;
    private Mouse mouse;

    void Awake()
    {
        mouse = Mouse.current;
    }
    void Start()
    {
        playerRigidbody = gorillaPlayer.GetComponent<Rigidbody>();

        controllerR.position = spotR.position;
        controllerL.position = spotL.position;
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
        if (Keyboard.current.leftShiftKey.isPressed) moveSpeed = runSpeed;
        else moveSpeed = normalSpeed;

        if (Keyboard.current.spaceKey.isPressed)
        {
            moveDirection -= cameraTransform.up;
            moveDirection.y = 2;
        }

        if (Keyboard.current.qKey.isPressed)
        {
            Player.Instance.GetComponent<Rigidbody>().useGravity = false;
            if (!usingZeroGravity)
                Debug.Log("Entering Zero Gravity Mode...");
            usingZeroGravity = true;
        }
        else
        {
            Player.Instance.GetComponent<Rigidbody>().useGravity = true;
            if (usingZeroGravity)
                Debug.Log("Exiting Zero Gravity Mode...");
            usingZeroGravity = false;
        }

        if (Keyboard.current.escapeKey.isPressed)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            Debug.Log("Exiting Playmode...");
#endif
        }

        playerRigidbody.MovePosition(playerRigidbody.position + moveDirection * (moveSpeed * Time.deltaTime));

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
                Button button = hit.collider.GetComponent<Button>();
                if (button != null) button.Press(hit.collider); PlaySound();
            }
        }
        
        if (mouse.leftButton.wasReleasedThisFrame)
        {
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(mouse.position.ReadValue());
            
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                Button button = hit.collider.GetComponent<Button>();
                if (button != null) button.Release(hit.collider); PlaySound();
            }
        }
    }
    void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}

   //////////////////////////////////////////////
  ///SCRIPT MADE BY SKELO_THEREAL///////////////
 ///DO NOT REMOVE THIS OR YOU ARE A SKIDDER////
//////////////////////////////////////////////