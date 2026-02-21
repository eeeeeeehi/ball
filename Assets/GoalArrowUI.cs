using UnityEngine;
public class GoalArrowUI : MonoBehaviour
{
    public string playerTag = "Player";
    public string goalTag = "Goal";
    private Transform player;
    private Transform goal;
    void Update()
    {
        if (player == null)
        {
            var p = GameObject.FindGameObjectWithTag(playerTag);
            if (p != null) player = p.transform;
        }
        if (goal == null)
        {
            var g = GameObject.FindGameObjectWithTag(goalTag);
            if (g != null) goal = g.transform;
        }
        // どっちか見つからない間は何もしない（エラーにしない）
        if (player == null || goal == null) return;
        Vector3 dir = goal.position - player.position;
        dir.y = 0f;
        // 角度計算してUI矢印を回転（Z回転）
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, -angle);
    }
}