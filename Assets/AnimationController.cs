using UnityEngine;
public class AnimationController : MonoBehaviour
{
    public Transform target;
    public Animator animator;
    private Vector3 lastPos;
    [SerializeField] private float runThreshold = 0.03f;
    void Start()
    {
        if (target == null)
        {
            Debug.LogError("[AnimationController] target Ç™ñ¢ê›íË");
            return;
        }
        if (animator == null)
            animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("[AnimationController] animator Ç™ñ¢ê›íË");
            return;
        }
        lastPos = target.position;
    }
    void Update()
    {
        if (target == null || animator == null) return;
        Vector3 move = target.position - lastPos;
        move.y = 0f;
        bool isRunning = move.magnitude > runThreshold;
        animator.SetBool("isRunning", isRunning);
        lastPos = target.position;
    }
}