using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StatsUpdater : MonoBehaviour
{
    //here too we will be checking the level change in the player
    private TextMeshProUGUI ExpPoints,CoinCount,Rank;
    void Start()
    {
        ExpPoints=GameObject.Find("Exp").GetComponent<TextMeshProUGUI>();
        ExpPoints.text=""+GameManager.instance.ExpPoints;
        CoinCount=GameObject.Find("Coin").GetComponent<TextMeshProUGUI>();
        CoinCount.text=""+GameManager.instance.CoinCount;
        GetComponent<XpRankManager>().CheckForRankChangeAndInitiateCongrats();
        Rank=GameObject.Find("RankPrompt").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        Rank.text="Rank-"+GameManager.instance.rank;
    }
}
