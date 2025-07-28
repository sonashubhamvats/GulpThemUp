using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShieldIcon : MonoBehaviour,IPointerDownHandler
{
    public TextMeshProUGUI count_down;

    public AudioSource playerAudioSource;

    public Image fiil_up;

    private bool canUse;

    public float life_alve=6f;

    public float current_time;
    public float recharge_time=10f;

    private bool canRecharge;
    private bool startUse;
    private void Awake() {
        canUse=true;
        life_alve=GameManager.instance.shieldTime;
        recharge_time=GameManager.instance.cooldown;
        current_time=life_alve;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(canUse)
        {
            playerAudioSource.PlayOneShot(AudioManager.instance.shieldOn,AudioManager.instance.volume);
            count_down.text=current_time+"s";
            ShieldScript.instance.SwitchOnTheShield();
            startUse=true;
            canUse=false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(startUse)
        {
            Debug.Log("Hell");
            current_time-=Time.deltaTime;
            fiil_up.fillAmount+=(Time.deltaTime/life_alve);
            if(current_time>=0f)
            {
                count_down.text=current_time.ToString("0")+"s";
            }
            else
            {
                startUse=false;
                playerAudioSource.PlayOneShot(AudioManager.instance.shieldOn,AudioManager.instance.volume);
                canRecharge=true;
                fiil_up.fillAmount=1f;
                ShieldScript.instance.SwitchOffTheShield();
                current_time=recharge_time;
            }

        }
        if(canRecharge)
        {
            count_down.text=current_time.ToString("0")+"s";
            current_time-=Time.deltaTime;
            fiil_up.fillAmount-=(Time.deltaTime/recharge_time);
            if(current_time<=0f)
            {
                current_time=life_alve;
                count_down.text="";
                fiil_up.fillAmount=0f;
                canRecharge=false;
                canUse=true;
            }
        }
    }

}
