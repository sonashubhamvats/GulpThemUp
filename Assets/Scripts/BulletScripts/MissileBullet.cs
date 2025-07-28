using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBullet : MonoBehaviour
{
    //this is script is attached to the bullet and gives the bullet its speed and direction
    private Rigidbody2D thisRigidbody2d;
    private Transform player, crosshair,cannon;
    private float speed;
    private float damage;
    private PlayerAttack playerAttack;
    private Vector3 direction;

    public ParticleSystem effect;

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
        //setting the flame's color
        var main=effect.main;
        main.startColor=GetComponent<SpriteRenderer>().color;
        effect.gameObject.SetActive(true);
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

            
            //initiate the blast effect
            int currentNumber=(int)double.Parse(collision.GetComponent<IndividualNumberProvider>().numberOnTheEntity.text);
            
            //initialize the blast
            InGameEffectsManager.instance.SpawnTheBlastEffect(transform.position,GetComponent<SpriteRenderer>().color);


            if (collision.gameObject.tag=="Friend")
            {
                AudioManager.instance.MissileBlast();
                if (collision.gameObject.transform.localScale.x > 3f)
                {
                    //changing the scale of the number
                    Vector3 scale = collision.gameObject.transform.localScale;
                    if(collision.gameObject.transform.localScale.x>3.1f)
                    {
                        scale.x /= 1.6f;
                        scale.y /= 1.6f;
                    }
                    
                    collision.gameObject.GetComponent<SingleNumberHealthOfNumber>().directChangeOfTheCoverNumber(GameManager.instance.inMissileDamage,ref currentNumber);
                    
                    if(currentNumber<=0)
                    { 
                        //cover no is destroyed
                        //adding the effects
                        collision.gameObject.GetComponent<MovementOfTheNumbers>().ExecuteTheEffectsOfRemovalOfTheCoverNumber(true);
                        collision.gameObject.GetComponent<NumberBehaviour>().orderChangeBackTheColor();
                        //doing this so that after the destruction of the cover number this if statement is never again called
                        var tempLocalScale=new Vector3(3f,3f,3f);
                        collision.gameObject.transform.localScale=tempLocalScale;
                        //adding the slowMo-
                        collision.gameObject.GetComponent<MovementOfTheNumbers>().ExecuteSloMoOfDestructionOfSLoMO=true;

                        collision.gameObject.GetComponent<IndividualNumberProvider>().AssisgnTheNumber("NA");

                    }
                    else
                    {
                        //camera shake is called from here becuause the misslie is going to be destroyed 
                        Camera.main.GetComponent<CameraScript>().startCoroutine(0.12f,0.5f);
                    }
                    Handheld.Vibrate();
                }
            }
            if(collision.gameObject.tag=="Enemy")
            {
                AudioManager.instance.MissileBlast();
                //changing the number on the enemy
                
                if (currentNumber < 0)
                {
                    
                    collision.gameObject.GetComponent<SingleNumberHealthOfNumber>().directChangeOfTheCoverNumber(-GameManager.instance.inMissileDamage,ref currentNumber);

                }
                if(currentNumber>0)
                {
                    currentNumber = 0;
                }
                else
                {
                    //camera shake is called from here becuause the misslie is going to be destroyed 
                    Camera.main.GetComponent<CameraScript>().startCoroutine(0.12f,0.5f);
                }
                Handheld.Vibrate();
               
            }
            ParticleInitiation.instance.DestroyTheBullet(gameObject.transform.position,GetComponent<SpriteRenderer>().color);
            Destroy(gameObject);
        }
    }
    IEnumerator selfDestruct()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
