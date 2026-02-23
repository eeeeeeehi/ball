using UnityEngine;

public class GoalTrigger : MonoBehaviour

{

    [Header("References (auto-find if empty)")]

    [SerializeField] private LotteryManager lotteryManager;

    [SerializeField] private LotteryUI lotteryUI;

    [SerializeField] private GoalSpawner goalSpawner;

    [Header("Settings")]

    [SerializeField] private int ticketsPerGoal = 100;

    [SerializeField] private float cooldown = 0.3f;

    private bool alreadyTriggered = false;

    // 追加：自動探索

    private void AutoFindRefs(bool log = false)

    {

        if (lotteryManager == null)

            lotteryManager = FindFirstObjectByType<LotteryManager>();

        if (lotteryUI == null)

            lotteryUI = FindFirstObjectByType<LotteryUI>();

        if (goalSpawner == null)

            goalSpawner = FindFirstObjectByType<GoalSpawner>();

        if (log)

        {

            Debug.Log($"[GoalTrigger] AutoFindRefs: " +

                      $"LotteryManager={(lotteryManager ? "OK" : "NULL")}, " +

                      $"LotteryUI={(lotteryUI ? "OK" : "NULL")}, " +

                      $"GoalSpawner={(goalSpawner ? "OK" : "NULL")}");

        }

    }

    // Editorで「Add Component」した瞬間に埋める

    private void Reset()

    {

        AutoFindRefs(log: true);

    }

    // Inspectorで値が変わった時に埋める（Editorのみ）

    private void OnValidate()

    {

        if (!Application.isPlaying)

            AutoFindRefs(log: false);

    }

    // 実行開始時にも念のため埋める

    private void Awake()

    {

        AutoFindRefs(log: true);

    }

    private void OnTriggerEnter(Collider other)

    {

        if (alreadyTriggered) return;

        if (!other.CompareTag("Player")) return;

        alreadyTriggered = true;

        if (lotteryManager == null)

        {

            Debug.LogError("[GoalTrigger] LotteryManager が見つからない。Sceneに LotteryManager があるか確認。");

            Invoke(nameof(ResetTrigger), cooldown);

            return;

        }

        // 抽選

        LotteryResult result = lotteryManager.RunLottery(ticketsPerGoal);

        // UI更新（LotteryUI側で Refresh() を用意してる想定）

        if (lotteryUI != null) lotteryUI.Refresh();

        // ゴール移動（任意）

        if (goalSpawner != null) goalSpawner.MoveGoalToNewPosition();

        Invoke(nameof(ResetTrigger), cooldown);

    }

    private void ResetTrigger()

    {

        alreadyTriggered = false;

    }

}
