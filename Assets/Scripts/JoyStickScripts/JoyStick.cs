using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class JoyStick : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    public Image joystick_background, joystick_handler;
    [HideInInspector]
    public Vector3 Input_dir;
    public float offset;
    [HideInInspector]public bool OnJoyStick;
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystick_background.rectTransform,
        eventData.position, eventData.pressEventCamera, out pos))
        {
            float joy_backGroundSizex = joystick_background.rectTransform.sizeDelta.x;
            float joy_backGroundSizey = joystick_background.rectTransform.sizeDelta.y;
          
            pos.x /= joy_backGroundSizex;
            pos.y /= joy_backGroundSizey;
            Input_dir = new Vector3(pos.x, 0f, pos.y);
            Input_dir = Input_dir.magnitude > 1 ? Input_dir.normalized : Input_dir;
        

            joystick_handler.rectTransform.anchoredPosition = new Vector3(Input_dir.x * ((joy_backGroundSizex) / offset), 
            Input_dir.z * ((joy_backGroundSizey) / offset), 0f);
           

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //notifying the touch movement that the touch is on joystick and not to take it for the shoot touch
        OnJoyStick = true;
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnJoyStick = false;
        Input_dir = Vector2.zero;
        joystick_handler.rectTransform.anchoredPosition = Vector2.zero;
    }
    private void Awake()
    {
        offset = 1.8f;
    }
    //used in the touchmovement
    public bool returnBoolOnjoyStick()
    {
        return OnJoyStick;
    }
}
