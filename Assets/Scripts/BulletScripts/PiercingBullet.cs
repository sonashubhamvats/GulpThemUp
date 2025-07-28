using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingBullet : MonoBehaviour
{
    //this is script is attached to the bullet and gives the bullet its speed and direction
    private Rigidbody2D thisRigidbody2d;
    private Transform player, crosshair,cannon;
    private float speed;
    private float damage;
    private PlayerAttack playerAttack;
    private Vector3 direction;

    // Start is called before the first frame update
    private void Awake()
    {
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        speed = playerAttack.speedOfTheBullet;
        
        //all these stats are there on the player so that they are easily customizable
        player = playerAttack.gameObject.transform;
        crosshair = GameObject.Find("CrossHair").transform;
        //take cannon
        cannon = GameObject.Find("Cannon").transform;
    }
    void Start()
    {
        thisRigidbody2d = GetComponent<Rigidbody2D>();
        direction =( -cannon.position + crosshair.position).normalized;
        //attempting the same speed thing 
        UnificationOfTheValuesOfTheSpeed(ref direction);
        StartCoroutine(selfDestruct());
    }
    void UnificationOfTheValuesOfTheSpeed(ref Vector3 direction)
    {
        //making all the vectors in the scale 1
        var tempDirection=direction;
        float yValue=crosshair.position.y-cannon.position.y;
        float xValue=crosshair.position.x-cannon.position.x;
        tempDirection.x=Mathf.Cos(Mathf.Atan2(yValue,xValue));
        tempDirection.y=Mathf.Sin(Mathf.Atan2(yValue,xValue));
        direction=tempDirection;
    }
    // Update is called once per frame
    void Update()
    {
        move();
    }
 
    void move()
    {
        thisRigidbody2d.velocity = direction * speed;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Enemy"||collision.gameObject.tag=="Friend")
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
                    collision.gameObject.GetComponent<SingleNumberHealthOfNumber>().decreaseFillAmount(playerAttack.normalDamageF+0.3f);
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
                    collision.gameObject.GetComponent<SingleNumberHealthOfNumber>().decreaseFillAmount(playerAttack.normalDamageE+0.3f);
                }
                if(currentNumber>0)
                {
                    currentNumber = 0;
                }
               
            }
            //ParticleInitiation.instance.DestroyTheBullet(gameObject.transform.position,GetComponent<SpriteRenderer>().color);
            
        }
    }
    //this function called to make an effect that the bullet went through the number
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="Enemy"||other.gameObject.tag=="Friend")
        {
            ParticleInitiation.instance.DestroyTheBullet(gameObject.transform.position,GetComponent<SpriteRenderer>().color);
        }
        
    }
    IEnumerator selfDestruct()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
