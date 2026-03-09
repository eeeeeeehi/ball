using UnityEngine;

public class GoalSpawner : MonoBehaviour

{

    [SerializeField] private Transform goal;

    [SerializeField] private GoalTrigger goalTrigger;

    [Header("Spawn Area")]

    [SerializeField] private float minX = -20f;

    [SerializeField] private float maxX = 20f;

    [SerializeField] private float minZ = -20f;

    [SerializeField] private float maxZ = 20f;

    [SerializeField] private float y = 0.5f;

    [Header("Goal Size")]

    [SerializeField] private Vector3 smallGoalScale = new Vector3(1f, 1f, 1f);

    [SerializeField] private Vector3 mediumGoalScale = new Vector3(1.5f, 1.5f, 1.5f);

    [SerializeField] private Vector3 largeGoalScale = new Vector3(2.2f, 2.2f, 2.2f);

    private void Awake()

    {

        if (goalTrigger == null && goal != null)

            goalTrigger = goal.GetComponent<GoalTrigger>();

    }

    public void MoveGoalToNewPosition()

    {

        if (goal == null) return;

        Vector3 newPos = new Vector3(

            Random.Range(minX, maxX),

            y,

            Random.Range(minZ, maxZ)

        );

        goal.position = newPos;

        ApplyRandomGoalType();

    }

    private void ApplyRandomGoalType()

    {

        if (goal == null || goalTrigger == null) return;

        int rand = Random.Range(0, 3);

        switch (rand)

        {

            case 0:

                // 小ゴール：100枚

                goal.localScale = smallGoalScale;

                goalTrigger.SetTicketsPerGoal(100);

                break;

            case 1:

                // 中ゴール：1000枚

                goal.localScale = mediumGoalScale;

                goalTrigger.SetTicketsPerGoal(1000);

                break;

            case 2:

                // 大ゴール：10000枚

                goal.localScale = largeGoalScale;

                goalTrigger.SetTicketsPerGoal(10000);

                break;

        }

    }

}
