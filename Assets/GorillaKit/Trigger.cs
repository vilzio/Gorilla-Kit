using UnityEngine;
using UnityEngine.Events;

namespace GorillaKit
{
    public class Trigger : MonoBehaviour
    {
        public bool debug;
        [Header("Interaction")]
        public bool toggle;
        private bool isActive;
        [Space]
        public bool head;
        public bool body;
        public bool hand = true;
        public UnityEvent<Collider> onEnter;
        public UnityEvent<Collider> onExit;
        [Space]
        public bool autoFillMissing = true;
        [Header("Visuals")]
        public MeshRenderer meshRenderer;
        public Material enterMaterial;
        public Material exitMaterial;
        [Header("Text")]
        public TextMesh textMesh;
        [Multiline]
        public string enterText;
        [Multiline]
        public string exitText;
        [Header("Audio")]
        public AudioSource audioSource;
        public AudioClip enterClip;
        public AudioClip exitClip;

        private void Awake()
        {
            if (autoFillMissing)
            {
                if (meshRenderer == null)
                    meshRenderer = GetComponent<MeshRenderer>();
                if (textMesh == null)
                    textMesh = GetComponent<TextMesh>();
                if (audioSource == null)
                    audioSource = GetComponent<AudioSource>();
                
                if (meshRenderer && !exitMaterial)
                    exitMaterial = meshRenderer.sharedMaterial;
                if (textMesh && string.IsNullOrEmpty(exitText))
                    exitText = textMesh.text;
                if (audioSource && !enterClip)
                    enterClip = audioSource.clip;
                
                if (meshRenderer && exitMaterial)
                    meshRenderer.sharedMaterial = exitMaterial;
                if (textMesh && !string.IsNullOrEmpty(exitText))
                    textMesh.text = exitText;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (BodyCheck(other))
                Enter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (BodyCheck(other))
                Exit(other);
        }

        public void Enter(Collider other)
        {
            if (toggle)
            {
                isActive = !isActive;
                Interact(
                    other,
                    isActive ? onEnter : onExit,
                    isActive ? "activated" : "deactivated",
                    isActive ? enterMaterial : exitMaterial,
                    isActive ? enterText : exitText,
                    isActive ? enterClip : exitClip
                    );
            }
            else if (!isActive)
            {
                isActive = true;
                Interact(other, onEnter, "entered", enterMaterial, enterText, enterClip);
            }
        }
        
        public void Exit(Collider other)
        {
            if (!toggle && isActive)
            {
                isActive = false;
                Interact(other, onExit, "exited", exitMaterial, exitText, exitClip);
            }
        }
        
        private bool BodyCheck(Collider other)
        {
            return (head && other.CompareTag("MainCamera")) ||
                   (body && other.CompareTag("Player")) ||
                   (hand && other.CompareTag("Hand"));
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void Interact(Collider other, UnityEvent<Collider> evt, string status, Material material, string text, AudioClip clip)
        {
            evt.Invoke(other);
            if (debug)
                Debug.Log($"{other.name} {status} {gameObject.name}");
            if (meshRenderer && material)
                meshRenderer.sharedMaterial = material;
            if (textMesh && !string.IsNullOrEmpty(text))
                textMesh.text = text;
            if (audioSource && clip)
                audioSource.PlayOneShot(clip);
        }
    }
}
