using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public bool fire;
    public float firerate,normal_firerate=0.1f,missile_firerate=1f,reflective_firerate=0.3f,piercing_firerate=0.1f;
    
    //damage variables
    public float normalDamageF,normalDamageE;
    public int reflectiveDamage,MissileDamage;
    //damage variables closed

    public float speedOfTheBullet = 20f; 
    private float currentTime=0f;
    public static PlayerAttack instance;

    public bool firstBullet;

    public Animator animatorOfTheCannon;
    private void Awake()
    {
        firstBullet=true;
        MakeInstance();
        //assigning the damage values
        normalDamageE=GameManager.instance.normalDamageEnemy;
        normalDamageF=GameManager.instance.normalDamagefFriend;
        reflectiveDamage=GameManager.instance.inReflectiveDamage;
        MissileDamage=GameManager.instance.inMissileDamage;
    }
    void MakeInstance()
    {

        if (instance == null)
        {
            
            instance = this;
        }
        else
        {
            Debug.Log("done");
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Attack()
    {
        //checking if the total no of bullets is greater than zero
        if(InGameStatManager.instance.returnBulletCount()>0)
        {
            if(BulletInstantiater.instance.curr_bullet_type==BulletType.normal)
            {
                //temporary check
                if(BulletInstantiater.instance.bulletMultiplier==BulletMultiplier.X2)
                {
                    if(InGameStatManager.instance.returnBulletCount()>=2)
                    {
                        InGameStatManager.instance.decreaseTheBulletNo(BulletInstantiater.instance.curr_bullet_type,1);
                        BulletInstantiater.instance.InstantiateNormalBullet();
                    }
                }
                else if(BulletInstantiater.instance.bulletMultiplier==BulletMultiplier.X3)
                {
                    if(InGameStatManager.instance.returnBulletCount()>=3)
                    {
                        BulletInstantiater.instance.InstantiateNormalBullet();
                        InGameStatManager.instance.decreaseTheBulletNo(BulletInstantiater.instance.curr_bullet_type,2);
                    }
                }
                else
                {
                    BulletInstantiater.instance.InstantiateNormalBullet();
                    InGameStatManager.instance.decreaseTheBulletNo(BulletInstantiater.instance.curr_bullet_type);
                }

                

            }
            if(BulletInstantiater.instance.curr_bullet_type==BulletType.piercing)
            {
                //temporary check
                if(BulletInstantiater.instance.bulletMultiplier==BulletMultiplier.X2)
                {
                    if(InGameStatManager.instance.returnBulletCount()>=2)
                    {
                        InGameStatManager.instance.decreaseTheBulletNo(BulletInstantiater.instance.curr_bullet_type,1);
                        BulletInstantiater.instance.InstantiateThePiercingBullet();
                    }
                }
                else if(BulletInstantiater.instance.bulletMultiplier==BulletMultiplier.X3)
                {
                    if(InGameStatManager.instance.returnBulletCount()>=3)
                    {
                        InGameStatManager.instance.decreaseTheBulletNo(BulletInstantiater.instance.curr_bullet_type,2);
                        BulletInstantiater.instance.InstantiateThePiercingBullet();
                    }
                }
                else
                {
                    BulletInstantiater.instance.InstantiateThePiercingBullet();
                    InGameStatManager.instance.decreaseTheBulletNo(BulletInstantiater.instance.curr_bullet_type);
                }

                

            }
            
            if(BulletInstantiater.instance.curr_bullet_type==BulletType.missile)
            {
                BulletInstantiater.instance.InstantiateMissileBulletPrefab();
                InGameStatManager.instance.decreaseTheBulletNo(BulletInstantiater.instance.curr_bullet_type);
            }
            if(BulletInstantiater.instance.curr_bullet_type==BulletType.piercing)
            {
                BulletInstantiater.instance.InstantiateThePiercingBullet();
                InGameStatManager.instance.decreaseTheBulletNo(BulletInstantiater.instance.curr_bullet_type);
            }
            if(BulletInstantiater.instance.curr_bullet_type==BulletType.reflective)
            {
                BulletInstantiater.instance.InstantiateReflectiveBullet();
                InGameStatManager.instance.decreaseTheBulletNo(BulletInstantiater.instance.curr_bullet_type);
            }
            
        }
        else
        {
            AudioManager.instance.EmptyMagSound();
        }
 
    }
    // Update is called once per frame
    void Update()
    {
 
        if(firstBullet&&fire)
        {
            Attack();
            firstBullet=false;
            return;
        }
        if (Time.time > (currentTime + firerate))
        {

            currentTime = Time.time;
            if (fire && !circleMovement.instance.cantBeControlled)
            {
                Attack();
               
                
            }

        }
        
    }
    //called from the bullet change script to facilitate the chage in the cannon firerate
    public void UpdateTheFirerate()
    {
        if(BulletInstantiater.instance.curr_bullet_type==BulletType.normal)
        {
            firerate=normal_firerate;
        }
        if(BulletInstantiater.instance.curr_bullet_type==BulletType.missile)
        {
            firerate=missile_firerate;
        }
        if(BulletInstantiater.instance.curr_bullet_type==BulletType.piercing)
        {
            firerate=piercing_firerate;
        }
        if(BulletInstantiater.instance.curr_bullet_type==BulletType.reflective)
        {
            firerate=reflective_firerate;
        }    
    }
}
