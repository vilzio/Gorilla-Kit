using UnityEngine;

[ExecuteAlways]
public class FollowObject : MonoBehaviour
{
    public Transform target;
    public bool position;
    public bool rotation;
    public bool previewInEditMode;

    private void Update()
    {
        if (!Application.isPlaying && !previewInEditMode)
            return;
        
        if (target)
        {
            if (position)
            {
                transform.position = target.position;
            }
            
            if (rotation)
            {
                transform.rotation = target.rotation;
            }
        }
    }
}
