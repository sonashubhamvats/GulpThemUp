using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class RainCharacter : MonoBehaviour
{
    public bool callBy; 

    public Text textOnNumber;
 
    public void SettingCharactersForNumber()
    {
        
        if(callBy)
        {
            GetComponent<SpriteRenderer>().color=Color.green;
            textOnNumber.text=1.ToString();
        }    
        else
        {
            GetComponent<SpriteRenderer>().color=Color.red;
            textOnNumber.text=(-1).ToString();
        }
        

    }

    public void StartDestructionCroutine()
    {
        StartCoroutine(DestroyRain());
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.name=="Player"&&!callBy)
        {
            Debug.Log("rain detected");
            //only when the shield is not up
            if(!ShieldScript.instance.shieldOn)
            {
                int tempNo;
                tempNo = (int)double.Parse(collision.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text);
                tempNo--;
                collision.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text=tempNo.ToString();
            }
            ParticleInitiation.instance.destructionOfaNumber(gameObject.transform,GetComponent<SpriteRenderer>().color,2f);
            Destroy(gameObject);        
            
        }
        if(collision.name=="Player"&&callBy)
        {
            int tempNo;
            tempNo = (int)double.Parse(collision.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text);
            tempNo++;
            collision.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text=tempNo.ToString();
            ParticleInitiation.instance.destructionOfaNumber(gameObject.transform,GetComponent<SpriteRenderer>().color,2f);
            Destroy(gameObject);        

        }
 
    }    
    public IEnumerator DestroyRain()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
