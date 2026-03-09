using UnityEngine;

public class GoalTrigger : MonoBehaviour

{

    [Header("References")]

    [SerializeField] private LotteryManager lotteryManager;

    [SerializeField] private LotteryUI lotteryUI;

    [SerializeField] private GoalSpawner goalSpawner;

    [Header("Current Goal Setting")]

    [SerializeField] private int ticketsPerGoal = 100;

    private bool alreadyTriggered = false;

    private void Awake()

    {

        if (lotteryManager == null) lotteryManager = FindFirstObjectByType<LotteryManager>();

        if (lotteryUI == null) lotteryUI = FindFirstObjectByType<LotteryUI>();

        if (goalSpawner == null) goalSpawner = FindFirstObjectByType<GoalSpawner>();

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

        if (lotteryManager == null || lotteryUI == null)

        {

            Debug.LogError("[GoalTrigger] éQè∆Ç™ë´ÇËÇ»Ç¢");

            Invoke(nameof(ResetTrigger), 0.3f);

            return;

        }

        LotteryResult result = lotteryManager.RunLottery(ticketsPerGoal);

        lotteryUI.Refresh();

        if (goalSpawner != null)

        {

            goalSpawner.MoveGoalToNewPosition();

        }

        Invoke(nameof(ResetTrigger), 0.3f);

    }

    private void ResetTrigger()

    {

        alreadyTriggered = false;

    }

}
