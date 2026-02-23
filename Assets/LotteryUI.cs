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
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("【当選内訳】");
        for (int i = 0; i < manager.GradeCount; i++)
        {
            sb.AppendLine(
                $"{i + 1}等 : {manager.WinCountsPerGrade[i]}枚（累計{manager.TotalWinCountsPerGrade[i]}枚）"
            );
        }
        sb.AppendLine(
            $"ハズレ : {manager.LoseThisRun}枚（累計{manager.TotalLose}枚）"
        );
        if (lotteryText != null)
            lotteryText.text = sb.ToString();
        StringBuilder s = new StringBuilder();
        s.AppendLine($"購入枚数 : {manager.TotalTicketsBought}枚");
        s.AppendLine($"今回の当たり枚数 : {manager.WinThisRun}枚");
        s.AppendLine($"累計の当たり枚数 : {manager.TotalWins}枚");
        s.AppendLine($"今回当選金額 : {manager.PayoutThisRun}円");
        s.AppendLine($"合計当選金額 : {manager.TotalPayout}円");
        if (summaryText != null)
            summaryText.text = s.ToString();
    }
}