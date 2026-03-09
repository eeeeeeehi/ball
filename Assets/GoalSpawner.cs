using UnityEngine;

public class GoalSpawner : MonoBehaviour

{

    [SerializeField] private GameObject goalPrefab;

    [Header("Spawn Area")]

    [SerializeField] private float minX = -20f;

    [SerializeField] private float maxX = 20f;

    [SerializeField] private float minZ = -20f;

    [SerializeField] private float maxZ = 20f;

    [SerializeField] private float y = 0.5f;

    [Header("Spawn Settings")]

    [SerializeField] private int initialSpawnCount = 5;

    [SerializeField] private int spawnPerWave = 2;

    [SerializeField] private float spawnInterval = 3f;

    [SerializeField] private int maxGoals = 20;

    private void Start()

    {

        // ç≈èâÇ…îzíu

        for (int i = 0; i < initialSpawnCount; i++)

        {

            SpawnOneGoal();

        }

        // àÍíËéûä‘Ç≤Ç∆Ç…í«â¡

        InvokeRepeating(nameof(SpawnWave), spawnInterval, spawnInterval);

    }

    private void SpawnWave()

    {

        int currentGoals = GameObject.FindGameObjectsWithTag("Goal").Length;

        if (currentGoals >= maxGoals) return;

        int canSpawn = Mathf.Min(spawnPerWave, maxGoals - currentGoals);

        for (int i = 0; i < canSpawn; i++)

        {

            SpawnOneGoal();

        }

    }

    public void SpawnOneGoal()

    {

        if (goalPrefab == null)

        {

            Debug.LogError("[GoalSpawner] goalPrefab Ç™ñ¢ê›íË");

            return;

        }

        Vector3 pos = new Vector3(

            Random.Range(minX, maxX),

            y,

            Random.Range(minZ, maxZ)

        );

        GameObject obj = Instantiate(goalPrefab, pos, Quaternion.identity);

        ApplyRandomGoalType(obj);

    }

    private void ApplyRandomGoalType(GameObject obj)

    {

        GoalTrigger trigger = obj.GetComponent<GoalTrigger>();

        if (trigger == null) return;

        // 100 / 1000 / 10000 ÇìØämó¶

        int rand = Random.Range(0, 3);

        switch (rand)

        {

            case 0:

                obj.transform.localScale = new Vector3(1f, 1f, 1f);

                trigger.SetTicketsPerGoal(100);

                break;

            case 1:

                obj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

                trigger.SetTicketsPerGoal(1000);

                break;

            case 2:

                obj.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);

                trigger.SetTicketsPerGoal(10000);

                break;

        }

    }

}
