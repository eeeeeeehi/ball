using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerLookRelay : MonoBehaviour
{
    public Transform cameraPivot;   // カメラの親
    public float sensitivity = 100f;
    float yRotation;
    public void OnLook(InputValue value)
    {
        float lookX = value.Get<float>();
        yRotation += lookX * sensitivity * Time.deltaTime;
        cameraPivot.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}