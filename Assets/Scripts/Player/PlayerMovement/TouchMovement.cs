using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMovement : MonoBehaviour
{
    public Vector2 touchPosition;
    public GameObject crossHair;
    [HideInInspector]
    public int screenTouch,joyStickTouch;

    public Transform forbiddenMarkerY,forbiddenMarkerX;

    public Transform fy0,fy1,fy2,fy3;
    private GameObject Player;

    public bool clutch;
    public static TouchMovement instance;
    public Camera _camera;
    //linked with the joystick's onJoyStick
    public bool OnJoyStick;
    JoyStick joy;
    // Update is called once per frame
    private void Awake()
    {
        Player=gameObject;
        joy = FindObjectOfType<JoyStick>();
        if (joy != null)
        {
            OnJoyStick = joy.OnJoyStick;
        }
        else
        {
            OnJoyStick = false;
        }
        screenTouch = -1;
        joyStickTouch = -1;

        //assiging the forbidden marker as per the item bought
        switch(GameManager.instance.listOfSelectedItems.Count)
        {
            case 0:
                forbiddenMarkerY=fy0;
                break;
            case 1:
                forbiddenMarkerY=fy1;
                break;
            case 2:
                forbiddenMarkerY=fy2;
                break;
            case 3:
                forbiddenMarkerY=fy3;
                break;
        }
        MakeInstance();
    }
    void MakeInstance()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        
        if(!clutch)
        {
            TouchPosition();

        }
        if(!GameManager.instance.tutDone&&TutorialManager.instance.inSpawnMessage)
        {
            for(int i=0;i<Input.touchCount;i++)
            {
                Touch touch=Input.GetTouch(i);
                switch(touch.phase)
                {
                    case TouchPhase.Began:
                        
                        TutorialManager.instance.CompleteTheWholeMessage();
                    
                        break;
                }

            }
        }
       
    }
    void TouchPosition()
    {
        for(int i=0;i<Input.touchCount;i++)
        {
            
            Touch touch = Input.GetTouch(i);
         
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnJoyStick = joy.returnBoolOnjoyStick();
                    if(screenTouch==-1)
                    {
                        
                        if (OnJoyStick == false||(OnJoyStick&&joyStickTouch!=-1))
                        {
                         
                            
                            //checking if the touch is not in the forbidden zone
                            if(_camera.ScreenToWorldPoint(touch.position).x>forbiddenMarkerX.position.x&&
                            _camera.ScreenToWorldPoint(touch.position).y>forbiddenMarkerY.position.y)
                            {                            
                            }
                            else
                            {
                                screenTouch = touch.fingerId;
                                //checking for the input type
                                if(GameManager.instance.InputType==0)
                                {
                                    
                                    if(TeleportScript.instance.inUse&&TeleportScript.instance.counter!=2)
                                    {

                                    }
                                    else
                                    {
                                        PlayerAttack.instance.fire = true;
                                        crossHair.transform.position = _camera.ScreenToWorldPoint(touch.position);

                                    }
                                }
                                else
                                {
                                    if(!circleMovement.instance.cantBeControlled)
                                    {
                                        Player.transform.position=_camera.ScreenToWorldPoint(touch.position);
                                    }
                                }

                            }

                        }

                    }
                    if(joyStickTouch==-1&&OnJoyStick==true)
                    {
                        //it is the case where the user has directly touched the joystick
                        joyStickTouch = touch.fingerId;
                        if(GameManager.instance.InputType==1)
                        {
                            PlayerAttack.instance.fire=true;
                        }
                    }
                    break;
                case TouchPhase.Moved:
                    if(touch.fingerId==screenTouch)
                    {
                        //the change in the touch position is faciliated here
                        touchPosition = touch.deltaPosition;
                    }
                    break;
                case TouchPhase.Stationary:
                    if(touch.fingerId==screenTouch)
                    {
                      
                        touchPosition = Vector2.zero;
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if(touch.fingerId==screenTouch)
                    {
                        if(GameManager.instance.InputType==0)
                        {
                            //stopping the shoot
                            PlayerAttack.instance.fire = false;
                        }
                        touchPosition = Vector2.zero;
                        screenTouch = -1;
                    }
                    if(touch.fingerId==joyStickTouch)
                    {
                        if(GameManager.instance.InputType==1)
                        {
                            //stopping the attack
                            PlayerAttack.instance.fire=false;
                        }
                        joyStickTouch = -1;
                    }
                    break;
            }

        }
    }
}
