using UnityEngine;
public class GoalSpawner : MonoBehaviour
{
    public float range = 40f;  // °‚Ì”¼•ª‚­‚ç‚¢
    void Start()
    {
        float randomX = Random.Range(-range, range);
        float randomZ = Random.Range(-range, range);
        transform.position = new Vector3(randomX, 0.1f, randomZ);
    }
}
