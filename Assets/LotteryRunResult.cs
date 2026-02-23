using System;
[Serializable]
public class LotteryRunResult
{
    public int ticketsThisRun;      // 今回買った枚数
    public int loseThisRun;         // 今回ハズレ枚数
    public long payoutThisRun;      // 今回当選金（円）
    public int[] winCountsThisRun;  // 今回の等級別当選枚数（index 0=1等 ... 5=6等）
    public int ticketsTotal;        // 累計購入枚数
    public int loseTotal;           // 累計ハズレ枚数
    public long payoutTotal;        // 累計当選金（円）
    public int[] winCountsTotal;    // 累計の等級別当選枚数
}