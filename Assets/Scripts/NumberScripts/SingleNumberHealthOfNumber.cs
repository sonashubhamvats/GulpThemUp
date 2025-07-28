using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SingleNumberHealthOfNumber : MonoBehaviour
{
    public Image health;



    public Text valueOnTheNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       if(transform.localScale.x<=3f)
       {
           if(gameObject.tag=="Friend")
           {
               //disabling the health image for smaller numbers
               health.gameObject.SetActive(false);
           }
       }
    }
    public void decreaseFillAmount(float damage)
    {

        float fillAmount=health.fillAmount;

            
        fillAmount-=damage;
        if(fillAmount<0)
        {
            DecreaseValueByOne();
            health.fillAmount=1f-(-1f*fillAmount);
        }
        else
        {
            if(fillAmount==0)
            {
                DecreaseValueByOne();
                health.fillAmount=1f;
            }
            else
            {
                health.fillAmount=fillAmount;
            }
        }
        
    }
    void DecreaseValueByOne()
    {
        int value=(int)double.Parse(valueOnTheNumber.text);
        if(gameObject.tag=="Friend")
        {
            value-=1;
        }
        else
        {
            if(value<0)
            {
                value+=1;
            }
        }
           
        valueOnTheNumber.text=value.ToString();
    }    
    public void directChangeOfTheCoverNumber(int damage,ref int collisionNumber)
    {
        Debug.Log("In");
        int value=(int)double.Parse(valueOnTheNumber.text);
        value-=damage;
       
        if(value<0&&gameObject.tag=="Friend")
        {
            value=0;
        }
        if(value>0&&gameObject.tag=="Enemy")
        {
            value=0;
        }
        valueOnTheNumber.text=value.ToString();
        collisionNumber=value;

        health.fillAmount=1f;
    }
    public void DamageByTheBlast()
    {
        int value=(int)double.Parse(valueOnTheNumber.text);
        int sub=(int)(value*0.08f);
        if(sub==0)
        {
            if(gameObject.tag=="Friend")
            {
                sub=2;
            }
            else
            {
                sub=-2;
            }
        }
        value-=sub;
        //the case when the  blast radius makes the value of the number=0
        if(gameObject.tag=="Enemy")
        {
            if(value>0)
            {
                value=0;
            }
            valueOnTheNumber.text=value.ToString();

        }
        else
        {
            if(gameObject.transform.localScale.x>3f)
            {
                if(value<=0)
                {
                    //effects when the cover no is destroyed
                    gameObject.GetComponent<MovementOfTheNumbers>().ExecuteTheEffectsOfRemovalOfTheCoverNumber();
                    //changign the color of number
                    gameObject.GetComponent<NumberBehaviour>().orderChangeBackTheColor();
                    //changing the local scale of the number  so that this if statemrnt is not called again
                    gameObject.transform.localScale=new Vector3(3f,3f,3f);
                    //enebling the execute slo mo bool to execute the slow mo
                    gameObject.GetComponent<MovementOfTheNumbers>().ExecuteSloMoOfDestructionOfSLoMO=true;
                    //assiging the new no which the actual no for that 
                    gameObject.GetComponent<IndividualNumberProvider>().AssisgnTheNumber("NA");
                }
                else
                {
                
                    valueOnTheNumber.text=value.ToString();

                }
    
            }

        }
    } 
}
