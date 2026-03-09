using UnityEngine;
public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private LotteryManager lotteryManager;
    [SerializeField] private LotteryUI lotteryUI;
    [SerializeField] private int ticketsPerGoal = 100;
    private bool alreadyTriggered = false;
    private void Awake()
    {
        if (lotteryManager == null) lotteryManager = FindFirstObjectByType<LotteryManager>();
        if (lotteryUI == null) lotteryUI = FindFirstObjectByType<LotteryUI>();
    }
    public void SetTicketsPerGoal(int value)
    {
        ticketsPerGoal = value;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (alreadyTriggered) return;
        if (!other.CompareTag("Player")) return;
        alreadyTriggered = true;
        if (lotteryManager != null)
        {
            lotteryManager.RunLottery(ticketsPerGoal);
        }
        if (lotteryUI != null)
        {
            lotteryUI.Refresh();
        }
        Destroy(gameObject);
    }
}