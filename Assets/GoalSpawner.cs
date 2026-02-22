using UnityEngine;
public class GoalSpawner : MonoBehaviour
{
    public Transform goal;
    public float range = 40f;
    public void MoveGoalToNewPosition()
    {
        Vector3 newPos = new Vector3(
            Random.Range(-range, range),
            goal.position.y,
            Random.Range(-range, range)
        );
        goal.position = newPos;
    }
}
