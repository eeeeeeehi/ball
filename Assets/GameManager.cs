using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Refs")]
    public GameObject goalPrefab;
    public Transform player;
    [Header("Spawn Area")]
    public float range = 40f;
    public float goalY = 0.1f;
    [Header("Respawn")]
    public float minDistanceFromPlayer = 10f; // プレイヤーの近すぎ回避
    public int maxTries = 50;
    private GameObject currentGoal;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        SpawnOrMoveGoal();
    }
    // ゴール達成時に呼ぶ
    public void OnGoalReached()
    {
        Debug.Log("GOAL! -> Next goal");
        SpawnOrMoveGoal();
    }
    void SpawnOrMoveGoal()
    {
        Vector3 pos = FindRandomGoalPosition();
        if (currentGoal == null)
        {
            currentGoal = Instantiate(goalPrefab, pos, Quaternion.identity);
        }
        else
        {
            currentGoal.transform.position = pos;
        }
    }
    Vector3 FindRandomGoalPosition()
    {
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < maxTries; i++)
        {
            float x = Random.Range(-range, range);
            float z = Random.Range(-range, range);
            pos = new Vector3(x, goalY, z);
            if (player == null) return pos;
            float d = Vector3.Distance(new Vector3(player.position.x, goalY, player.position.z), pos);
            if (d >= minDistanceFromPlayer) return pos;
        }
        // 見つからなかったら妥協
        return pos;
    }
}