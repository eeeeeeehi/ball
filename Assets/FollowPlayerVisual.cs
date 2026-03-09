using UnityEngine;
public class FollowPlayerVisual : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, 0f);
    [SerializeField] private bool faceMoveDirection = true;
    private Vector3 lastPos;
    void Start()
    {
        if (target != null)
            lastPos = target.position;
    }
    void LateUpdate()
    {
        if (target == null) return;
        // ˆÊ’u‚¾‚¯’Ç]
        transform.position = target.position + offset;
        // ˆÚ“®•ûŒü‚ðŒü‚­
        if (faceMoveDirection)
        {
            Vector3 move = target.position - lastPos;
            move.y = 0f;
            if (move.sqrMagnitude > 0.0001f)
            {
                transform.rotation = Quaternion.LookRotation(move.normalized);
            }
        }
        lastPos = target.position;
    }
}