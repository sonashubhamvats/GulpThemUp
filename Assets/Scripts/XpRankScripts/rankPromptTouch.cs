using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class rankPromptTouch : MonoBehaviour,IPointerDownHandler
{
    public GameObject rank_touch_panel;
    public TextMeshProUGUI head,curr_rank,next_rank,percentage;
    public Image progress_image;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(GameManager.instance.rank<10)
        {
            rank_touch_panel.SetActive(true);
            head.text="Rank-"+GameManager.instance.rank;
            curr_rank.text="Rank-"+GameManager.instance.rank;
            next_rank.text="Rank-"+(GameManager.instance.rank+1);
            percentage.text=GameManager.instance.ExpPoints+"/"+XpRankManager.instance._rank_info[GameManager.instance.rank-1].xp_requirements;
            float fillAmount=((float)GameManager.instance.ExpPoints
            /(float)XpRankManager.instance._rank_info[GameManager.instance.rank-1].xp_requirements);
            progress_image.fillAmount=fillAmount;
            Debug.Log(fillAmount);
        }
        else
        {
            Handheld.Vibrate();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
