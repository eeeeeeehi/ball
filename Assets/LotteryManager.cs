using UnityEngine;

public class LotteryManager : MonoBehaviour

{

    [Header("Grades")]

    [SerializeField] private int gradeCount = 7; // 1等〜7等（ハズレは別）

    [Header("Prize Settings (per ticket)")]

    [Tooltip("1等〜n等の当選金（1枚あたり）")]

    [SerializeField] private int[] payoutPerGrade;

    [Header("Probability Weights (bigger = more likely)")]

    [Tooltip("1等〜n等の抽選重み（大きいほど当たりやすい）。ハズレは別計算。")]

    [SerializeField] private int[] weightPerGrade;

    [Header("Lose Settings")]

    [Tooltip("ハズレの重み（大きいほどハズレが増える）")]

    [SerializeField] private int loseWeight = 200;

    // ===== Totals (累計) =====

    [Header("Totals (ReadOnly)")]

    [SerializeField] private int totalTicketsBought;

    [SerializeField] private int totalPayout;

    [SerializeField] private int totalLoseTickets;

    [SerializeField] private int[] totalWinTicketsPerGrade;

    // ===== Last Run (今回) =====

    [Header("Last Result (ReadOnly)")]

    [SerializeField] private int payoutThisRun;

    [SerializeField] private int loseThisRun;

    [SerializeField] private int[] winThisRunPerGrade;

    // 最後の結果（参照用）

    public LotteryResult LastResult { get; private set; }

    // ===== 外部参照用プロパティ（UIが使う）=====

    public int GradeCount => gradeCount;

    public int[] PayoutPerGrade => payoutPerGrade;

    public int[] WinCountsPerGrade => winThisRunPerGrade;          // 今回

    public int[] TotalWinCountsPerGrade => totalWinTicketsPerGrade; // 累計

    public int LoseThisRun => loseThisRun;

    public int TotalLose => totalLoseTickets;

    public int TotalTicketsBought => totalTicketsBought;

    public int PayoutThisRun => payoutThisRun;

    public int TotalPayout => totalPayout;

    public int WinThisRun

    {

        get

        {

            int sum = 0;

            for (int i = 0; i < winThisRunPerGrade.Length; i++) sum += winThisRunPerGrade[i];

            return sum;

        }

    }

    public int TotalWins

    {

        get

        {

            int sum = 0;

            for (int i = 0; i < totalWinTicketsPerGrade.Length; i++) sum += totalWinTicketsPerGrade[i];

            return sum;

        }

    }

    private void Awake()

    {

        EnsureArrays();

        ClearThisRun();

    }

#if UNITY_EDITOR

    private void OnValidate()

    {

        if (gradeCount < 1) gradeCount = 1;

        EnsureArrays();

    }

#endif

    private void EnsureArrays()

    {

        if (payoutPerGrade == null || payoutPerGrade.Length != gradeCount)

            payoutPerGrade = new int[gradeCount];

        if (weightPerGrade == null || weightPerGrade.Length != gradeCount)

            weightPerGrade = new int[gradeCount];

        if (totalWinTicketsPerGrade == null || totalWinTicketsPerGrade.Length != gradeCount)

            totalWinTicketsPerGrade = new int[gradeCount];

        if (winThisRunPerGrade == null || winThisRunPerGrade.Length != gradeCount)

            winThisRunPerGrade = new int[gradeCount];

    }

    // =========================

    // ここが「抽選の本体」

    // =========================

    public LotteryResult RunLottery(int tickets)

    {

        EnsureArrays();

        ClearThisRun();

        // 0以下が来たら安全に無視

        if (tickets <= 0)

        {

            LastResult = new LotteryResult(gradeCount, 0);

            return LastResult;

        }

        totalTicketsBought += tickets;

        // 抽選

        int sumWinWeights = 0;

        for (int i = 0; i < gradeCount; i++)

            sumWinWeights += Mathf.Max(0, weightPerGrade[i]);

        int totalWeight = sumWinWeights + Mathf.Max(0, loseWeight);

        if (totalWeight <= 0)

        {

            // 全部0なら全部ハズレ扱い

            loseThisRun = tickets;

            totalLoseTickets += tickets;

            LastResult = new LotteryResult(gradeCount, tickets);

            LastResult.loseTickets = tickets;

            LastResult.payoutThisRun = 0;

            return LastResult;

        }

        for (int t = 0; t < tickets; t++)

        {

            int r = Random.Range(0, totalWeight); // 0..totalWeight-1

            // まずハズレ判定（loseWeight分）

            if (r < loseWeight)

            {

                loseThisRun++;

                totalLoseTickets++;

                continue;

            }

            // 当たり判定（1等〜n等）

            r -= loseWeight;

            int pickedGrade = PickByWeight(r, weightPerGrade);

            if (pickedGrade < 0)

            {

                // 念のため安全策：当たり側が全部0ならハズレ

                loseThisRun++;

                totalLoseTickets++;

                continue;

            }

            winThisRunPerGrade[pickedGrade]++;

            totalWinTicketsPerGrade[pickedGrade]++;

            int payout = (payoutPerGrade != null && pickedGrade < payoutPerGrade.Length)

                ? payoutPerGrade[pickedGrade]

                : 0;

            payoutThisRun += payout;

            totalPayout += payout;

        }

        // 結果をまとめて返す（UIが参照できる）

        LastResult = new LotteryResult(gradeCount, tickets);

        for (int i = 0; i < gradeCount; i++)

            LastResult.winCountsPerGrade[i] = winThisRunPerGrade[i];

        LastResult.loseTickets = loseThisRun;

        LastResult.payoutThisRun = payoutThisRun;

        return LastResult;

    }

    // r は「当たり側の重み合計」の範囲で来る想定

    private int PickByWeight(int r, int[] weights)

    {

        int acc = 0;

        for (int i = 0; i < weights.Length; i++)

        {

            int w = Mathf.Max(0, weights[i]);

            acc += w;

            if (r < acc) return i;

        }

        return -1;

    }

    // 今回分だけクリア（累計は残る）

    public void ClearThisRun()

    {

        EnsureArrays();

        payoutThisRun = 0;

        loseThisRun = 0;

        for (int i = 0; i < winThisRunPerGrade.Length; i++)

            winThisRunPerGrade[i] = 0;

    }

    // 全部リセット（累計も消す）

    public void ClearAll()

    {

        EnsureArrays();

        totalTicketsBought = 0;

        totalPayout = 0;

        totalLoseTickets = 0;

        for (int i = 0; i < totalWinTicketsPerGrade.Length; i++)

            totalWinTicketsPerGrade[i] = 0;

        ClearThisRun();

        LastResult = null;

    }

    // 確率チェック用（デバッグ）

    public float GetProbabilityOfGrade(int gradeIndex)

    {

        int sumWinWeights = 0;

        for (int i = 0; i < gradeCount; i++)

            sumWinWeights += Mathf.Max(0, weightPerGrade[i]);

        int totalWeight = sumWinWeights + Mathf.Max(0, loseWeight);

        if (totalWeight <= 0) return 0f;

        if (gradeIndex < 0 || gradeIndex >= gradeCount) return 0f;

        return (float)Mathf.Max(0, weightPerGrade[gradeIndex]) / totalWeight;

    }

}
