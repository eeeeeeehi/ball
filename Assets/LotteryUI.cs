using UnityEngine;

using TMPro;

using System.Text;

public class LotteryUI : MonoBehaviour

{

    [SerializeField] private LotteryManager manager;

    [SerializeField] private TextMeshProUGUI lotteryText;

    [SerializeField] private TextMeshProUGUI summaryText;

    public void Refresh()

    {

        if (manager == null) return;

        // 内訳

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("【当選内訳】");

        for (int i = 0; i < manager.GradeCount; i++)

        {

            sb.AppendLine(

                $"{i + 1}等：{manager.WinCountsPerGrade[i]}枚（{manager.TotalWinCountsPerGrade[i]}枚）"

            );

        }

        sb.AppendLine(

            $"ハズレ：{manager.LoseThisRun}枚（{manager.TotalLose}枚）"

        );

        if (lotteryText != null)

            lotteryText.text = sb.ToString();

        // 今回の購入枚数は「今回の当たり枚数 + 今回のハズレ枚数」で出す

        int ticketsThisRun = manager.WinThisRun + manager.LoseThisRun;

        int purchaseCostThisRun = ticketsThisRun * manager.TicketPrice;

        int totalPurchaseCost = manager.TotalTicketsBought * manager.TicketPrice;

        int profitThisRun = manager.PayoutThisRun - purchaseCostThisRun;

        int totalProfit = manager.TotalPayout - totalPurchaseCost;

        // サマリー

        StringBuilder s = new StringBuilder();

        s.AppendLine($"購入枚数：{ticketsThisRun}枚（{manager.TotalTicketsBought}枚）");

        s.AppendLine($"今回の当たり枚数：{manager.WinThisRun}枚 ({manager.TotalWins}枚)");

        s.AppendLine($"今回購入金額：{purchaseCostThisRun:N0}円 ({totalPurchaseCost:N0}円)");

        s.AppendLine($"今回当選金額：{manager.PayoutThisRun:N0}円 ({manager.TotalPayout:N0}円)");

        s.AppendLine($"今回収支：{profitThisRun:N0}円 ({totalProfit:N0}円)");


        if (summaryText != null)

            summaryText.text = s.ToString();

    }

}
