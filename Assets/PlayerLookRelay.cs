using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerLookRelay : MonoBehaviour
{
    [SerializeField] Transform cameraRig; // Main Camera か、カメラの親
    [SerializeField] float yawSpeed = 180f;
    [SerializeField] float deadzone = 0.15f;
    public void OnTurn(InputValue value)
    {
        float v = value.Get<float>();
        Debug.Log("TURN raw: " + v);
        // 0〜1 のAxis（中心0.5）っぽい時は補正
        float x = (v >= 0f && v <= 1f) ? (v - 0.5f) * 2f : v;
        if (Mathf.Abs(x) < deadzone) return;
        if (cameraRig != null)
            cameraRig.Rotate(0f, x * yawSpeed * Time.deltaTime, 0f, Space.World);
    }
}