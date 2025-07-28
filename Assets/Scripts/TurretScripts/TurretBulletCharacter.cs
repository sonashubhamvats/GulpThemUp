using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TurretBulletCharacter : MonoBehaviour
{
    public bool callFrom;

    private Rigidbody2D thisRigidbody;
    
    public Vector3 direction;

    public int speed=10;
    private void Awake() {
        thisRigidbody=GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBullet());
        
    }

    // Update is called once per frame
    void Update()
    {
        thisRigidbody.velocity=direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.tag=="Enemy"||collision.gameObject.tag=="Friend")&&callFrom)
        {
            int currentNumber=(int)double.Parse(collision.GetComponent<IndividualNumberProvider>().numberOnTheEntity.text);
            if (collision.gameObject.tag=="Friend")
            {
                if (collision.gameObject.transform.localScale.x > 3f)
                {
                    //changing the scale of the number
                    Vector3 scale = collision.gameObject.transform.localScale;
                    if(collision.gameObject.transform.localScale.x>3.1f)
                    {
                        scale.x -= 0.1f;
                        scale.y -= 0.1f;
                    }
                    collision.gameObject.GetComponent<SingleNumberHealthOfNumber>().decreaseFillAmount(1f);
                    if(currentNumber<=0)
                    { 
                        //cover no is destroyed
                        //adding the effects
                        collision.gameObject.GetComponent<MovementOfTheNumbers>().ExecuteTheEffectsOfRemovalOfTheCoverNumber();
                        collision.gameObject.GetComponent<NumberBehaviour>().orderChangeBackTheColor();
                        //doing this so that after the destruction of the cover number this if statement is never again called
                        var tempLocalScale=new Vector3(3f,3f,3f);
                        collision.gameObject.transform.localScale=tempLocalScale;
                        //adding the slowMo-
                        collision.gameObject.GetComponent<MovementOfTheNumbers>().ExecuteSloMoOfDestructionOfSLoMO=true;

                        collision.gameObject.GetComponent<IndividualNumberProvider>().AssisgnTheNumber("NA");

                    }
                }
            }
            if(collision.gameObject.tag=="Enemy")
            {
                //changing the number on the enemy
                
                if (currentNumber < 0)
                {
                    collision.gameObject.GetComponent<SingleNumberHealthOfNumber>().decreaseFillAmount(1f);
                }
                if(currentNumber>0)
                {
                    currentNumber = 0;
                }
               
            }
            ParticleInitiation.instance.DestroyTheBullet(gameObject.transform.position,GetComponent<SpriteRenderer>().color);
            Destroy(gameObject);
        }
        if(collision.gameObject.tag=="Player"&&!callFrom)
        {
            //only when the shield is not up
            if(!ShieldScript.instance.shieldOn)
                numberChanger(collision);
            ParticleInitiation.instance.DestroyTheBullet(gameObject.transform.position,GetComponent<SpriteRenderer>().color);
            Destroy(gameObject);
        }
    }
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
    void numberChanger(Collider2D collision)
    {
        //getting the text on the number
        int tempNo;
        tempNo = (int)double.Parse(collision.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text);
        tempNo--;
        collision.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text=tempNo.ToString();

    }
    
}
