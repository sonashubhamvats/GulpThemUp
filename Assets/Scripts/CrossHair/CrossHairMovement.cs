using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairMovement : MonoBehaviour
{
    
    public Transform leftMarginX;
    public Transform UpMarginY;

    private JoyStick joy;
    public Transform rightMarginX, DownMarginY;
    
    public bool cantBeControlled;
    private SpriteRenderer thisSpriteRenderer;
    public SpriteRenderer playerRenderer;

    public static CrossHairMovement instance;
    private void Awake()
    {
        MakeInstance();
        joy=FindObjectOfType<JoyStick>();
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
  
    }
    void MakeInstance()
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
    // Update is called once per frame
    void Start()
    {
        thisSpriteRenderer.color = playerRenderer.color;
    }
    void Update()
    {
        if(!cantBeControlled)
        {
            if(GameManager.instance.InputType==0)
            {
                Move();
            }
            else
            {
                MoveWithJoyStick();
            }
        }
    }
    void Move()
    {
        Vector2 tempPosition = transform.position;
        tempPosition.x += TouchMovement.instance.touchPosition.x*InputSettingsManager.instance.TouchSensitivity*Time.deltaTime;
        tempPosition.y += TouchMovement.instance.touchPosition.y * InputSettingsManager.instance.TouchSensitivity * Time.deltaTime;
        CheckIfTheHoleIsOutOfTheBoundary(ref tempPosition);
        transform.position = tempPosition;
    }
    void MoveWithJoyStick()
    {
        Vector2 tempPosition = transform.position;

        tempPosition.x += joy.Input_dir.x*InputSettingsManager.instance.JoyStickSensitivity;
        tempPosition.y += joy.Input_dir.z* InputSettingsManager.instance.JoyStickSensitivity;

        CheckIfTheHoleIsOutOfTheBoundary(ref tempPosition);
        transform.position = tempPosition;
    }

    void CheckIfTheHoleIsOutOfTheBoundary(ref Vector2 tempPosition)
    {
        if (tempPosition.x < leftMarginX.position.x)
        {

            tempPosition.x = leftMarginX.position.x;
        }
        else if (tempPosition.x > rightMarginX.position.x)
        {
            tempPosition.x = rightMarginX.position.x;
        }

        if (tempPosition.y > UpMarginY.position.y)
        {
            tempPosition.y = UpMarginY.position.y;
        }
        else if (tempPosition.y < DownMarginY.position.y)
        {
            tempPosition.y = DownMarginY.position.y;
        }

    }
}
