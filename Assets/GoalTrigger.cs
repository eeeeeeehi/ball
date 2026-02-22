using UnityEngine;
public class GoalTrigger : MonoBehaviour
{
    public GoalSpawner goalSpawner;
    public LotteryManager lotteryManager;
    public LotteryUI lotteryUI;
    public int ticketsPerGoal = 10000;
    private bool alreadyTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (alreadyTriggered) return;
        if (!other.CompareTag("Player")) return;
        alreadyTriggered = true;
        // 抽選
        LotterySummary summary = lotteryManager.RunLottery(ticketsPerGoal);
        // UI更新
        lotteryUI.SetResult(summary);
        // ゴール移動
        goalSpawner.MoveGoalToNewPosition();
        Invoke(nameof(ResetTrigger), 0.2f);
    }
    private void ResetTrigger()
    {
        alreadyTriggered = false;
    }
}