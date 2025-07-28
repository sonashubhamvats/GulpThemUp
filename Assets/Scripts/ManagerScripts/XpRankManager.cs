using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public struct rank_info
{
    public int rank;

    public int xp_requirements;

    public string info;
}
public class XpRankManager : MonoBehaviour
{
    public GameObject congrats_in_order;

    public TextMeshProUGUI congrats_sub,rewards;

    public rank_info[] _rank_info;

    public static XpRankManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance==null)
        {
            instance=this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }
    public void CheckForRankChangeAndInitiateCongrats()
    {
        if(GameManager.instance.rank<10)
        {
            if(GameManager.instance.ExpPoints>_rank_info[GameManager.instance.rank-1].xp_requirements)
            {
                if(!GameManager.instance.congrats[GameManager.instance.rank-1])
                {
                    GameManager.instance.congrats[GameManager.instance.rank-1]=true;
                    GameManager.instance.rank++;
                    congrats_sub.text="You have reached Rank-"+GameManager.instance.rank;
                    rewards.text=_rank_info[GameManager.instance.rank-2].info;
                    //initiate the congrats
                    congrats_in_order.SetActive(true);
                    Debug.Log("Congo on the rank up!!");
                }
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
