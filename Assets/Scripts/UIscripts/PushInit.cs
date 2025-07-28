using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PushInit : MonoBehaviour,IPointerDownHandler
{
    private Image cover;

    public Transform player;
    
    private TextMeshProUGUI timer;
    private bool recharge;

    public float rechargeTime,currentTime;
    public void OnPointerDown(PointerEventData eventData)
    {

        if(!recharge)
        {
            //push power audio
            AudioManager.instance.pushAudioItem();
            currentTime=rechargeTime;
            recharge=true;
            //putting on the cover 
            timer.text=rechargeTime+"s";
            var col =cover.color;
            col.a=0.8f;
            cover.color=col;


            //call the push function 
            PushPower();
            

        }
    }
    private void Awake() 
    {
        //gettintg the components
        cover=transform.GetChild(0).gameObject.GetComponent<Image>();
        timer=transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(recharge)
        {
            if(currentTime>0f)
            {
                currentTime-=Time.deltaTime;
                timer.text=((int)currentTime)+"s";
                cover.fillAmount-=(Time.deltaTime/rechargeTime);
            }
            else
            {
                recharge=false;
                cover.fillAmount=1f;
                timer.text="";
                var col =cover.color;
                col.a=0f;
                cover.color=col;

            }

        }
    }

    //the pushing power
    public void PushPower()
    {
        GameObject[] all_numbers;
        InGameEffectsManager.instance.SpawnTheBlastEffect(player.position,Color.cyan);
        if(EnemySpawner.instance.friend)
        {
            all_numbers=GameObject.FindGameObjectsWithTag("Friend");
        }
        else
        {
            all_numbers=GameObject.FindGameObjectsWithTag("Enemy");
        }

        for(var i=0;i<all_numbers.Length;i++)
        {
            var pos_vector=(all_numbers[i].transform.position-player.transform.position);
            pos_vector=pos_vector.normalized;
            all_numbers[i].GetComponent<MovementOfTheNumbers>().movementOfTheNumber=pos_vector*all_numbers[i].GetComponent<MovementOfTheNumbers>().speed;
        }
    }
}
