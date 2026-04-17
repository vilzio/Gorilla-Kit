using UnityEngine;
using UnityEngine.Events;

namespace GorillaKit
{
    public class Button : MonoBehaviour
    {
        public UnityEvent<Collider> onPress;
        public UnityEvent<Collider> onRelease;
        
        [Header("Interaction Settings")]
        public bool head;
        public bool body;
        public bool hand = true;
        public bool debug;
        
        [Header("Visual Settings")]
        public MeshRenderer meshRenderer;
        public Color pressColor = Color.red;
        public Color releaseColor = Color.white;
        public TextMesh textMesh;
        [TextArea(2, 5)]
        public string pressText = "ACTIVE";
        [TextArea(2, 5)]
        public string releaseText = "CLICK\nME";

        [Header("Audio Settings")]
        public AudioSource audioSource;
        public AudioClip pressClip;
        public AudioClip releaseClip;

        void Start()
        {
            if (meshRenderer != null)
                meshRenderer.material.color = releaseColor;
            if (textMesh != null)
                textMesh.text = releaseText;
        }
    
        void OnTriggerEnter(Collider other)
        {
            if (CanInteract(other))
            {
                Press(other);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (CanInteract(other))
            {
                Release(other);
            }
        }

        bool CanInteract(Collider other)
        {
            return (head && other.CompareTag("MainCamera"))
                || (body && other.CompareTag("Player"))
                || (hand && other.CompareTag("Hand"));
        }
    
        public void Press(Collider other)
        {
            onPress.Invoke(other);
            
            if (meshRenderer != null)
                meshRenderer.material.color = pressColor;
            if (textMesh != null)
                textMesh.text = pressText;
            if (audioSource != null && pressClip != null)
                audioSource.PlayOneShot(pressClip);
            if (debug)
                Debug.Log($"{gameObject.name} pressed by {other.name}");
        }
    
        public void Release(Collider other)
        {
            onRelease.Invoke(other);
        
            if (meshRenderer != null)
                meshRenderer.material.color = releaseColor;
            if (textMesh != null)
                textMesh.text = releaseText;
            if (audioSource != null && releaseClip != null)
                audioSource.PlayOneShot(releaseClip);
            if (debug)
                Debug.Log($"{gameObject.name} released by {other.name}");
        }
    }
}
