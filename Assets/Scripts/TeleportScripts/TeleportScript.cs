using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class TeleportScript : MonoBehaviour,IPointerDownHandler
{

    public TextMeshProUGUI timer;

    public Image Cover;
    public bool canUse=true,inUse,recharging;

    public int counter=0;

    public GameObject[] teleportObjects;

    public static TeleportScript instance;
    public float recharge_time,use_time;
    public Camera _main_camera;
    int JoytouchId=-1;
    float time;
    private void Awake() {
        if(instance==null)
        {
            instance=this;
        }    
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(canUse&&!inUse)
        {
            inUse=true;
            Color color=Cover.color;
            color.a=1f;
            Cover.color=color;
            Cover.fillAmount=0f;
            time=0f;
            timer.text="0s";

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canUse)
        {
            
            for(var i=0;i<Input.touchCount;i++)
            {
                Touch touch=Input.GetTouch(i);
                switch(touch.phase)
                {
                    case TouchPhase.Began:
                        if(TouchMovement.instance.OnJoyStick)
                        {
                            if(touch.fingerId!=JoytouchId&&JoytouchId==-1)
                            {
                                Debug.Log("JoyStick touched");
                                JoytouchId=touch.fingerId;
                            }  
                        }
                        if(counter<2&&inUse)
                        {
 
                            if(_main_camera.ScreenToWorldPoint(touch.position).x>TouchMovement.instance.forbiddenMarkerX.position.x&&
                            _main_camera.ScreenToWorldPoint(touch.position).y>TouchMovement.instance.forbiddenMarkerY.position.y)
                            {
                               
                            }
                            else
                            {
                                if(!TouchMovement.instance.OnJoyStick)
                                {
                                    CreatePortal(touch);
                                }
                                else
                                {
                                    if(JoytouchId!=-1)
                                    {
                                        if(touch.fingerId!=JoytouchId)
                                        {
                                            CreatePortal(touch);

                                        }
                                    }
                                }

                            }
                        }
                        

                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        if(touch.fingerId==JoytouchId)
                        {
                            JoytouchId=-1;
                        }
                        break;
                }
            }
        }

        //the fuictionms controlling the clock of the teleport
        if(inUse)
        {
            if(time<(use_time))
            {
                time+=(Time.deltaTime);
                timer.text=time.ToString("F0")+"s";
                Cover.fillAmount+=(Time.deltaTime/use_time);

            }
            else
            {
                canUse=false;
                inUse=false;
                teleportObjects[0].GetComponent<SpriteRenderer>().enabled=true;
                teleportObjects[0].gameObject.SetActive(false);
                teleportObjects[1].GetComponent<SpriteRenderer>().enabled=true;
                teleportObjects[1].gameObject.SetActive(false);
                recharging=true;
                
                time=recharge_time;
            }

        }
        if(recharging)
        {
            if(time>1)
            {
                time-=(Time.deltaTime);
                timer.text=time.ToString("F0")+"s";
                Cover.fillAmount-=(Time.deltaTime/use_time);

            }
            else
            {
                recharging=false;
                canUse=true;
                counter=0;
                timer.text="";
                Cover.fillAmount=0f;
            }
        }
    }
    void CreatePortal(Touch touch)
    {
        teleportObjects[counter].transform.position=new Vector3(_main_camera.ScreenToWorldPoint(touch.position).x,_main_camera.ScreenToWorldPoint(touch.position).y,0f);

        teleportObjects[counter].gameObject.SetActive(true);

        //play the portal sound
        AudioManager.instance.PortalAudio();
        counter++;

    }
}
