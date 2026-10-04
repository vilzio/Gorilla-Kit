using UnityEngine;

namespace GorillaKit
{
    public class Button : MonoBehaviour
    {
        [Header("References")]
        public MeshRenderer meshRenderer;
        public AudioSource audioSource;
        
        [Header("Settings")]
        public Material pressMaterial;
        public Material releaseMaterial;
        public AudioClip pressClip;
        public AudioClip releaseClip;

        public void Press()
        {
            if (meshRenderer && pressMaterial)
                meshRenderer.material = pressMaterial;
            if (audioSource && pressClip)
                audioSource.PlayOneShot(pressClip);
        }

        public void Release()
        {
            if (meshRenderer && releaseMaterial)
                meshRenderer.material = releaseMaterial;
            if (audioSource && releaseClip)
                audioSource.PlayOneShot(releaseClip);
        }
    }
}
