using System;
using UnityEngine;
[Serializable]
public class LotteryResult
{
    // 0..gradesCount-1 が 1等..n等
    public int[] winCountsPerGrade;
    // この回のハズレ枚数
    public int loseTickets;
    // この回の当選金合計
    public int payoutThisRun;
    // この回の購入枚数
    public int ticketsBought;
    public LotteryResult(int gradesCount, int ticketsBought)
    {
        winCountsPerGrade = new int[gradesCount];
        this.ticketsBought = ticketsBought;
        loseTickets = 0;
        payoutThisRun = 0;
    }
    public int TotalWinsThisRun()
    {
        int sum = 0;
        for (int i = 0; i < winCountsPerGrade.Length; i++) sum += winCountsPerGrade[i];
        return sum;
    }
}