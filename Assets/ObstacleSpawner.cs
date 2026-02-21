using UnityEngine;
public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public int count = 30;
    public float range = 40f;
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            float x = Random.Range(-range, range);
            float z = Random.Range(-range, range);
            Vector3 pos = new Vector3(x, 1f, z);
            Instantiate(obstaclePrefab, pos, Quaternion.identity);
        }
    }
}
