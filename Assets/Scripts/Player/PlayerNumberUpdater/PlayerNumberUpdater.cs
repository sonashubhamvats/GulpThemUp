using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;
public class PlayerNumberUpdater : MonoBehaviour
{
    //also deals with the effects related to the collision of the player with the numbers 
    public TextMeshProUGUI text;
    IEnumerator couroutineForStoppingTheForce;
    public Camera maincamera;
    public float forceOfCollision;
    // Start is called before the first frame update
    
    void Start()
    {
        
        text.text = "0";
       
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        
            if (collision.gameObject.tag == "Friend" || collision.gameObject.tag == "Enemy")
            {
                if (collision.gameObject.tag == "Enemy")
                {
                    if(!ShieldScript.instance.shieldOn)
                    {
                        numberChanger(collision);
                        //accessing the post processing files
                        if(!GameManager.instance.InTutorial)
                        {
                            PostProcessingScript.instance.DecreaseGloomAndIncreaseVignette();

                        }
                    }
                                        }
                else
                {
                    if(collision.gameObject.transform.localScale.x>3f)
                    {
                        //sparks emitted
                        ParticleInitiation.instance.CollisioSparks(collision.contacts[0].point,collision.
                        gameObject.GetComponent<SpriteRenderer>().color);
/*                        if(!TimeManager.instance.InSlowMo)
                        {
                            StartCoroutine(maincamera.GetComponent<CameraScript>().cameraShake(0.1f, 0.3f));
                        }*/
                    }
                    else
                    {
                        
                        numberChanger(collision);
                    }
                }
                //explosion force
                Vector3 collisionContact=new Vector3(collision.contacts[0].point.x,collision.contacts[0].point.y,
                0f);
                Vector3 directionOfForce=collisionContact-gameObject.transform.position;

                if(collision.gameObject.transform.localScale.x>=4f)
                {
                    forceOfCollision = 10f;
                }
                else
                {
                    forceOfCollision = 7f;
                }
                GetComponent<Rigidbody2D>().velocity=(-directionOfForce*forceOfCollision);
                
                //adding camerashake
                if(collision.gameObject.tag=="Enemy")
                {
                    if(!TimeManager.instance.InSlowMo)
                    {
                        StartCoroutine(maincamera.GetComponent<CameraScript>().cameraShake(0.1f, 0.7f));
                    }
                }
                StartStopForceCoroutine();
            }
           


    }
    void StartStopForceCoroutine()
    {
            //stopping the player's control (refer the circle movement script)
            GetComponent<circleMovement>().cantBeControlled = true;

            if (couroutineForStoppingTheForce != null)
            {
                StopCoroutine(couroutineForStoppingTheForce);
            }
            couroutineForStoppingTheForce = stoppingTheForceAndRestarting();
            StartCoroutine(couroutineForStoppingTheForce);

        
    }
    void numberChanger(Collision2D collision)
    {
        //getting the text on the number
        int tempNo;
        int noOnThePlayer;
        noOnThePlayer=(int)double.Parse(text.text);
        tempNo = (int)double.Parse(collision.gameObject.GetComponent<IndividualNumberProvider>().numberOnTheEntity.text);
        noOnThePlayer += tempNo;
        if (noOnThePlayer < -99f && text.fontSize != 1.9f)
        {
            text.fontSize = 1.9f;
        }
        if (noOnThePlayer < -999f && text.fontSize != 1.5f)
        {
            text.fontSize = 1.5f;
        }
        //game end condition
        text.text = noOnThePlayer.ToString();
    }
    IEnumerator stoppingTheForceAndRestarting()
    {
        yield return new WaitForSeconds(0.3f);
        
        GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
        //enabling the player's control ovr the hole
        GetComponent<circleMovement>().cantBeControlled = false;
    }
}
