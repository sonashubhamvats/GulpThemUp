using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleMovement : MonoBehaviour
{
    public Material playerMaterial;
    
    public Transform leftMarginX,rightMarginX,UpMarginY,DownMarginY;
    public float smoothness=0.01f;
    public static circleMovement instance;
    //using this to stop player's control over the ball for 0.3s while the ball is experiencing some force also used in PlayerNumberUpdater
    public bool cantBeControlled;
    private Vector3 _InputDir;
    private float xOffSet, yOffSet;
    private JoyStick joy;

    public SpriteRenderer sprite;

    
    private void Awake()
    {
        //asssining the player material and the player material
        GetComponent<SpriteRenderer>().sprite=GameManager.instance.playerSprite;
        GetComponent<SpriteRenderer>().material=GameManager.instance.playerMaterial;

        //getting the joystick
        joy = FindObjectOfType<JoyStick>();

        MakeInstance();
        //Debug.Log(yOffSet);
    }
 
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //Method to get the calibrated input 

    
    // Update is called once per frame
    void Update()
    {
        if (!cantBeControlled)
        {
            if (GameManager.instance.InputType == 0)
            {
                MoveWithJoyStick();
            }
            else
            {
                Move();
            }
        }

    }
    void MoveWithJoyStick()
    {
        Vector2 tempPosition = transform.position;

        tempPosition.x += joy.Input_dir.x*InputSettingsManager.instance.JoyStickSensitivity;
        tempPosition.y += joy.Input_dir.z* InputSettingsManager.instance.JoyStickSensitivity;

        CheckIfTheHoleIsOutOfTheBoundary(ref tempPosition);
        transform.position = tempPosition;
    }
    void Move()
    {
        Vector2 tempPosition = transform.position;
        tempPosition.x += TouchMovement.instance.touchPosition.x*InputSettingsManager.instance.TouchSensitivity*Time.deltaTime;
        tempPosition.y += TouchMovement.instance.touchPosition.y * InputSettingsManager.instance.TouchSensitivity * Time.deltaTime;
        CheckIfTheHoleIsOutOfTheBoundary(ref tempPosition);
        transform.position = tempPosition;
    }

    void CheckIfTheHoleIsOutOfTheBoundary(ref Vector2 tempPosition)
    {
        if(tempPosition.x<leftMarginX.position.x)
        {
           
            tempPosition.x = leftMarginX.position.x;
        }
        else if(tempPosition.x>rightMarginX.position.x)
        {
            tempPosition.x = rightMarginX.position.x;
        }

        if(tempPosition.y>UpMarginY.position.y)
        {
            tempPosition.y = UpMarginY.position.y;
        }
        else if(tempPosition.y < DownMarginY.position.y)
        {
            tempPosition.y = DownMarginY.position.y;
        }

    }
    //called from the movement of numbers
 
 
}
