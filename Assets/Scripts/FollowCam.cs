using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] Transform bodyTarget;

    private static Transform currentTarget;

    public float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if(currentTarget != null)
        {
            Vector3 targetPosition = new Vector3(currentTarget.position.x, currentTarget.position.y, transform.position.z);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    public static void ChangeTarget(GameObject newTarget)
    {
        currentTarget = newTarget.transform;
    }
}
