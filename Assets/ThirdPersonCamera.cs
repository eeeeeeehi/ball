using UnityEngine;
public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 8f;
    public float height = 3f;
    public float rotationSpeed = 120f;
    float currentAngle = 0f;
    Vector2 lookInput;
    public void SetLook(Vector2 v)
    {
        lookInput = v;
    }
    void LateUpdate()
    {
        if (target == null) return;
        currentAngle += lookInput.x * rotationSpeed * Time.deltaTime;
        Quaternion rot = Quaternion.Euler(0f, currentAngle, 0f);
        Vector3 offset = rot * new Vector3(0f, height, -distance);
        transform.position = target.position + offset;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}