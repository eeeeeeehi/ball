using UnityEngine;
public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 3, -6);
    public float smooth = 10f;
    void LateUpdate()
    {
        if (target == null) return;
        // 回転は無視して、単純に後ろ＋上に配置
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smooth * Time.deltaTime
        );
        // 常にボールを見る
        transform.LookAt(target.position + Vector3.up * 1f);
    }
}
