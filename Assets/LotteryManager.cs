using UnityEngine;

[System.Serializable]

public class LotterySummary

{

    public int tickets;

    public long totalPrize;

    public int c1; public long p1;

    public int c2; public long p2;

    public int c3; public long p3;

    public int c4; public long p4;

    public LotterySummary(int tickets)

    {

        this.tickets = tickets;

    }

}

public class LotteryManager : MonoBehaviour

{

    // 例：ジャンボ宝くじっぽい金額（後で好きに変えてOK）

    public long prize1 = 100000000; // 1等 1億

    public long prize2 = 1000000;   // 2等 100万

    public long prize3 = 10000;     // 3等 1万

    public long prize4 = 300;       // 4等 300円

    // 確率は適当（ゲーム用）。リアルにしたいなら後で調整でOK

    // 合計が1を超えないようにしてる

    public float prob1 = 0.0000001f;

    public float prob2 = 0.00001f;

    public float prob3 = 0.001f;

    public float prob4 = 0.10f;

    public LotterySummary RunLottery(int ticketCount)

    {

        var s = new LotterySummary(ticketCount);

        for (int i = 0; i < ticketCount; i++)

        {

            float r = Random.value;

            if (r < prob1)

            {

                s.c1++; s.p1 += prize1; s.totalPrize += prize1;

            }

            else if (r < prob1 + prob2)

            {

                s.c2++; s.p2 += prize2; s.totalPrize += prize2;

            }

            else if (r < prob1 + prob2 + prob3)

            {

                s.c3++; s.p3 += prize3; s.totalPrize += prize3;

            }

            else if (r < prob1 + prob2 + prob3 + prob4)

            {

                s.c4++; s.p4 += prize4; s.totalPrize += prize4;

            }

        }

        return s;

    }

}

