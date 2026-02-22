using UnityEngine;
using TMPro;
public class LotteryUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public void SetResult(LotterySummary s)
    {
        if (text == null) return;
        text.text =
            $"çwì¸ñáêî: {s.tickets:N0}ñá\n" +
            $"çáåvìñëIã‡äz: {s.totalPrize:N0}â~\n\n" +
            $"1ìô: {s.c1:N0}ñá / {s.p1:N0}â~\n" +
            $"2ìô: {s.c2:N0}ñá / {s.p2:N0}â~\n" +
            $"3ìô: {s.c3:N0}ñá / {s.p3:N0}â~\n" +
            $"4ìô: {s.c4:N0}ñá / {s.p4:N0}â~";
    }
}
