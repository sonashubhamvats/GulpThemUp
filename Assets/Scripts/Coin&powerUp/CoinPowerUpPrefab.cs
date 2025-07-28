using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoinPowerUpPrefab : MonoBehaviour
{
    public float force;
    public float flyAwaySpeed;
    private Rigidbody2D thisRigidbody;

    private Transform toTheHeavens,forbiddenZone;

    public bool taken;

    public int touched;

    public bool onceTouched;

    //ammo count only for ammo drops
    [HideInInspector]public int normalAmmo,piercingAmmo,missileAmmo,reflectiveAmmo;
    

    public float rotateSpeed;
    // Start is called before the first frame update
    private void Awake() {
        touched=0;
        thisRigidbody=GetComponent<Rigidbody2D>();
        forbiddenZone=GameObject.Find("ForbiddenY0").transform;
        toTheHeavens=GameObject.Find("ToTheHeavens").transform;
    }

    void Start()
    {
        thisRigidbody.AddForce(new Vector3(Random.Range(-1f,1f),Random.Range(-1f,-1f))*force);
        StartCoroutine(StopMovement());
    }
    private int flag=0;

    private Vector3 flyAwayDir;
    // Update is called once per frame
    public void DeactivateOnceTouched()
    {
        StartCoroutine(DeactivateOnceTouchedCoroutine());
    }
    IEnumerator DeactivateOnceTouchedCoroutine()
    {
        yield return new WaitForSeconds(5f);
        onceTouched=false;
    }
    void Update()
    {
        if(taken)
        {
            rotateSpeed+=1;
            GetComponent<CircleCollider2D>().enabled=false;
            if(flag==0)
            {
                flyAwayDir=(toTheHeavens.position-transform.position).normalized;
                flag=1;
            }
            ToTheHeavens();
        }
        //rotate the powerup
        transform.Rotate(0f,rotateSpeed,0f);

    }
    public void ToTheHeavens()
    {
        thisRigidbody.velocity=flyAwayDir*flyAwaySpeed;
        if(transform.position.y>(forbiddenZone.position.y+1f))
        {
            //this if statement is to check if the drop is a ammo pack or coin
            ChangeTheStats(gameObject.tag);            
        }
    }
    void ChangeTheStats(string tag)
    {
        if(tag=="Ammo")
        {
           InGameStatManager.instance.IncreaseTheBulletCount(normalAmmo,piercingAmmo,reflectiveAmmo,missileAmmo);
        }
        if(tag=="Coin")
        {
            InGameStatManager.instance.IncreaseTheCoinCount();
        }
        Destroy(gameObject);
    }
    IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(1.5f);
        thisRigidbody.gravityScale=0f;
        thisRigidbody.velocity=new Vector3(0f,0f,0f);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Player")
        {
            taken=true;
            if(gameObject.tag=="Coin")
            {
                AudioManager.instance.powerUpAudioSource.PlayOneShot(AudioManager.instance.coin_selection);
                //calling the star effect
                ParticleInitiation.instance.CoinStarEffect(transform.position);
            }
            else
            {
                AudioManager.instance.powerUpAudioSource.PlayOneShot(AudioManager.instance.ammo_pack);
            }
        }
    }

    public void AssignTheAmmoValuesToThePowerUp(int total)
    {
        //assiging the ammo points to the variables

        //if normal count in gamemanager is =0 then normalammo =10 else it is equal to normal count and same is done for all 
        normalAmmo=(int)((GameManager.instance.normalCount-InGameStatManager.instance.normalCOunt)/total)<=0?10:
        (int)((GameManager.instance.normalCount-InGameStatManager.instance.normalCOunt)/total);

        

        piercingAmmo=(int)((GameManager.instance.PiercingCount-InGameStatManager.instance.PierceCount)/total)<=0?10:
        (int)((GameManager.instance.PiercingCount-InGameStatManager.instance.PierceCount)/total);

        reflectiveAmmo=(int)((GameManager.instance.reflectiveCount-InGameStatManager.instance.ReflectiveCount)/total)<=0?2:
        (int)((GameManager.instance.reflectiveCount-InGameStatManager.instance.ReflectiveCount)/total);

        missileAmmo=(int)((GameManager.instance.MissileCount-InGameStatManager.instance.MissileCount)/total)<=0?2:
        (int)((GameManager.instance.MissileCount-InGameStatManager.instance.MissileCount)/total);
    
    }

}
