using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOfTheNumbers : MonoBehaviour
{
    public Vector3 center;
    private Rigidbody2D thisRigidBody;
    [HideInInspector]
    public Vector3 movementOfTheNumber;
    [HideInInspector]
    public float speed;
    [HideInInspector]public bool dead;
    private float deathTimer=0f;
    public float flyawaySpeed=100f;
    private  float scale;
    public bool immediateDeath;//for referencing in the gameChanger Script
    
    public bool ExecuteSloMoOfDestructionOfSLoMO;
    public int randSelectionOfSlo;

    private AudioSource _this_audio_source;

    private bool coverNoDestructionRecover;
   
    private void Awake()
    {
        _this_audio_source=GetComponent<AudioSource>();
        thisRigidBody = GetComponent<Rigidbody2D>();
    }
    
 
    // Start is called before the first frame update
    void Start()
    {
        
     
        //randomizing the initial direction of motion
        float rand = Random.Range(-2f, 2f);
        center.x += rand;

        movementOfTheNumber = -transform.position +center;
        movementOfTheNumber = movementOfTheNumber.normalized*speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!immediateDeath)
        {
            if (dead == false)
            {
                MoveTheNumber();
            }
            else
            {
                if(randSelectionOfSlo==1)
                {
                    //this is a system that disallows any consecutive slo mos
                    if(!TimeManager.instance.notAllowSlomo)
                    {
                        //post processing ultra glow
                        PostProcessingScript.instance.UltraGlowGloom(false);
                        //instant slo mo in event of a colliision
                        TimeManager.instance.StartSloInstant();

                    }   
                }
                deadAnimation();
            }
        }
        else
        {
            ImmediateDeath();
        }
        if(ExecuteSloMoOfDestructionOfSLoMO)
        {
            ExecuteSloMoOfDestructionOfSLoMO=false;
            //this is a system that disallows any consecutive slo mos
            if(!TimeManager.instance.notAllowSlomo)
            {
                //post processing ultra glow
                PostProcessingScript.instance.UltraGlowGloom(false,false);

                TimeManager.instance.startSlowmoCoroutine();
            }
        }
 
    }
    public void ExecuteTheEffectsOfRemovalOfTheCoverNumber(bool miss=false)
    {
        ParticleInitiation.instance.CoverParticleDestruction(transform.position,GetComponent<SpriteRenderer>().color);
         //camera shake involuntarily but when coming from the missile bullet no camera shake as the camera shake had already been initiated
        if(!miss&&!TimeManager.instance.InSlowMo)
        {
            StartCoroutine(Camera.main.GetComponent<CameraScript>().cameraShake(0.1f,0.05f));
        }
        if(!miss)
        {
            _this_audio_source.PlayOneShot(AudioManager.instance.cover_no_blast,AudioManager.instance.volume);
        }
        movementOfTheNumber=-thisRigidBody.velocity*2f;
        coverNoDestructionRecover=true;
    }
    void MoveTheNumber()
    {
        
        thisRigidBody.velocity = movementOfTheNumber;
    }

    //collision of the numbers
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //i have integrated this link for the fact that the 
        if(!dead)
        {
            if(collision.gameObject.CompareTag("Walls"))
            {
               
                Vector3 tempDirection = Vector3.Reflect(movementOfTheNumber, collision.contacts[0].normal);
                
                if (speed <8f)
                {
                    speed += 0.3f;
                }
                if(coverNoDestructionRecover)
                {
                    coverNoDestructionRecover=false;
                    speed=10f;
                }

                

                movementOfTheNumber = tempDirection.normalized * speed;
            }
            else if(collision.gameObject.CompareTag("Player"))
            {
                
                if (gameObject.tag == "Friend")
                {
                    //if collision with a player
                    if (transform.localScale.x > 3f)
                    {
                        _this_audio_source.PlayOneShot(AudioManager.instance.thud_cover_no,AudioManager.instance.volume);
                        Vector3 collisionContact=new Vector3(collision.contacts[0].point.x,collision.contacts[0].point.y,
                        0f);
                        Vector3 directionOfForce=collisionContact-gameObject.transform.position;
                        movementOfTheNumber = -directionOfForce.normalized*speed;
                    }
                    else
                    {
                        //scale should be dependant on the time of the death of the number thats why here
                        collision.gameObject.GetComponent<AudioSource>().PlayOneShot(AudioManager.instance.green_no,AudioManager.instance.volume);
                        scale=GetComponent<Transform>().localScale.x;
                        randSelectionOfSlo=Random.Range(1,3);
                        if(randSelectionOfSlo!=1)
                        {
                            //camera shake only when no slowmo
                            //camera shake
                            if(!TimeManager.instance.InSlowMo)
                            {
                                StartCoroutine(Camera.main.GetComponent<CameraScript>().cameraShake(0.1f,0.5f));
                            }
                            
                            //post processing ultra glow
                            PostProcessingScript.instance.UltraGlowGloom(true);

                        }
                        //flyaway
                        Vector3 collisionContact=new Vector3(collision.contacts[0].point.x,collision.contacts[0].point.y,
                        0f);
                        Vector3 directionOfForce=collisionContact-gameObject.transform.position;
                        
                        //temporary Exp increment
                        GameManager.instance.ExpPoints+=(GameManager.instance.xp_type*10);
                        ParticleInitiation.instance.XpPointsEmergemce(transform.position);

                        thisRigidBody.velocity=-directionOfForce.normalized*flyawaySpeed;
                        //to prevent collisions from walls
                        GetComponent<CircleCollider2D>().enabled=false;
                        dead = true;
                    }
                }
                else if(gameObject.tag=="Enemy")
                {
                    
                    if(!ShieldScript.instance.shieldOn)
                    {
                        collision.gameObject.GetComponent<AudioSource>().PlayOneShot(AudioManager.instance.negative_clash,AudioManager.instance.volume);
                        //check for settings
                        Handheld.Vibrate();
                        scale=GetComponent<Transform>().localScale.x;
                        //flyaway
                        Vector3 collisionContact=new Vector3(collision.contacts[0].point.x,collision.contacts[0].point.y,
                        0f);
                        Vector3 directionOfForce=collisionContact-gameObject.transform.position;
                        thisRigidBody.velocity=-directionOfForce.normalized*flyawaySpeed;
                        //to prevent collisions from walls
                        GetComponent<CircleCollider2D>().enabled=false;
                        dead = true;
                    }
                    else
                    {
                        //sparks emitted
                        _this_audio_source.PlayOneShot(AudioManager.instance.thud_cover_no,AudioManager.instance.volume);
                        
                        ParticleInitiation.instance.CollisioSparks(collision.contacts[0].point,
                        gameObject.GetComponent<SpriteRenderer>().color);
                        Vector3 collisionContact=new Vector3(collision.contacts[0].point.x,collision.contacts[0].point.y,
                        0f);
                        Vector3 directionOfForce=collisionContact-gameObject.transform.position;
                        movementOfTheNumber = -directionOfForce.normalized*speed;

                    }
                }

            }
        }
    }
    void deadAnimation()
    {
        if(deathTimer==0f)
        {
            //initiaze the death parameter
            GameObject.Find("ParticleManager").GetComponent<ParticleInitiation>().destructionOfaNumber(gameObject.transform,
            GetComponent<SpriteRenderer>().color,scale);

        }
        if (deathTimer<0.5f)
        {
            deathTimer+=Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    //for the gameChanger

    void ImmediateDeath()
    {
         //initiaze the death parameter
        GameObject.Find("ParticleManager").GetComponent<ParticleInitiation>().destructionOfaNumber(gameObject.transform,
        GetComponent<SpriteRenderer>().color,scale);
        gameObject.SetActive(false);
    }

    //the function called from the blast execution to change the direction according to the blast
    public void ChangeTheDirectionWithBlast(Vector3 centerOfExplosion)
    {
        Vector3 new_direction=(transform.position-centerOfExplosion);
        var tempDirection=new_direction;
        float yValue=transform.position.y-centerOfExplosion.y;
        float xValue=transform.position.x-centerOfExplosion.x;
        tempDirection.x=Mathf.Cos(Mathf.Atan2(yValue,xValue));
        tempDirection.y=Mathf.Sin(Mathf.Atan2(yValue,xValue));
        new_direction=tempDirection;

        movementOfTheNumber=new_direction*speed;
         
    }

    

    
}
