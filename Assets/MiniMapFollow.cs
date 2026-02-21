using UnityEngine;

public class MiniMapFollow : MonoBehaviour

{

    public Transform target;

    public float height = 60f;

    void LateUpdate()

    {

        if (target == null) return;

        transform.position = new Vector3(target.position.x, height, target.position.z);

    }

}

