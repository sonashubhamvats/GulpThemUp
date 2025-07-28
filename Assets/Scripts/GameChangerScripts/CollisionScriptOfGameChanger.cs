using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScriptOfGameChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //translation
        transform.Translate(Vector3.down*Time.deltaTime*8f);
        if(Time.timeScale==0f)
        {
            GetComponents<AudioSource>()[0].Pause();
        }
        else
        {
            if(!GetComponents<AudioSource>()[0].isPlaying)
            {
                GetComponents<AudioSource>()[0].Play();
                if(Time.timeScale<1f)
                {
                    GetComponents<AudioSource>()[0].pitch=0.4f;
                }
                else
                {
                    GetComponents<AudioSource>()[0].pitch=1f;
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Friend"||collision.gameObject.tag=="Enemy")
        {
            GetComponents<AudioSource>()[1].PlayOneShot(AudioManager.instance.cover_no_blast,AudioManager.instance.volume);
            if(collision.gameObject.tag=="Friend")
            {
                int noOnTheEntity=(int)double.Parse(collision.gameObject.GetComponent<IndividualNumberProvider>().numberOnTheEntity.text);
                int noOfCoins=noOnTheEntity/10;
                if(noOfCoins==0)
                {
                    noOfCoins=5;
                }
                //if the no's cover had not been blown give out minimal coins
                if(collision.gameObject.transform.localScale.x>3f)
                {
                    //if a big no then 1 in 3 chanes that the coins will spawm
                    noOfCoins=3;
                }
                //spawning the coins
                CoinPoweupSpawner.instance.SpawnCoinPrefabs(noOfCoins,collision.gameObject.transform.position);
               
            }
            else
            {
                int noOnTheEntity=(int)double.Parse(collision.gameObject.GetComponent<IndividualNumberProvider>().numberOnTheEntity.text);
                if(noOnTheEntity!=0)
                {
                    //spawning the ammo
                    CoinPoweupSpawner.instance.SpawnAmmoPrefabs(collision.gameObject.transform.position);

                }
            }
            //killing the numbers on impact
            collision.gameObject.GetComponent<MovementOfTheNumbers>().immediateDeath=true;
        }
        else
        {
            if(collision.gameObject.tag=="Coin")
            {
                Debug.Log("CoinPowerup");
                if(!collision.gameObject.GetComponent<CoinPowerUpPrefab>().onceTouched)
                {
                    collision.gameObject.GetComponent<CoinPowerUpPrefab>().touched++;
                    if(collision.gameObject.GetComponent<CoinPowerUpPrefab>().touched==2)
                    {
                        collision.gameObject.GetComponent<CoinPowerUpPrefab>().taken=true;
                        ParticleInitiation.instance.CoinStarEffect(collision.gameObject.transform.position);

                    }

                    collision.gameObject.GetComponent<CoinPowerUpPrefab>().onceTouched=true;
                    collision.gameObject.GetComponent<CoinPowerUpPrefab>().DeactivateOnceTouched();
                }
            }
            else
            {
                if(collision.gameObject.tag!="RainNumer"&&collision.gameObject.tag!="Turret"&&
                collision.gameObject.tag!="Mine"&&collision.gameObject.tag!="Laser"&&collision.gameObject.tag!="Shield"&&collision.gameObject.tag.CompareTo("teleport")!=0)
                {
                    //changing the color when the gameChangerTouches the elements
                    GameColorUnification.instance.ChangeTheColorOfTheObject(collision.gameObject);
                }

            }
        }
    }
}
