using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace GorillaKit
{
    public class Grab : MonoBehaviour
    {
        [Header("Settings")]
        public bool snap = true;
        public bool clip = true;
        
        private XRInteractionManager interactionManager;
        private XRGrabInteractable grabInteractable;
        
        private void Start()
        {
            interactionManager = FindAnyObjectByType<XRInteractionManager>();
            
            if (!gameObject.TryGetComponent(out grabInteractable))
            {
                grabInteractable = gameObject.AddComponent<XRGrabInteractable>();
            }
            
            grabInteractable.interactionManager = interactionManager;
            grabInteractable.useDynamicAttach = !snap;
            grabInteractable.movementType = clip ? XRBaseInteractable.MovementType.VelocityTracking : XRBaseInteractable.MovementType.Instantaneous;
        }
    }
}
