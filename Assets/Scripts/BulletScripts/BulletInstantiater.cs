using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BulletType
{
    normal,
    reflective,
    missile,
    piercing
}
public enum BulletMultiplier
{
    X1,X2,X3
}
public class BulletInstantiater : MonoBehaviour
{

 
    public static BulletInstantiater instance; 
    public GameObject normalBulletPrefab,reflective_bullet_prefab,piercing_bullet_prefab,missile_bullet_prefab;

    public BulletType curr_bullet_type;

    public BulletMultiplier bulletMultiplier;
    [HideInInspector]public Color StartColorOfTheBullet;
    public Transform crosshair,spawnpoint;

    public Animator animator_cannnon;

    [HideInInspector]public ParticleSystem curr_flame;
    private void Awake()
    {
        curr_bullet_type=BulletType.normal;
        bulletMultiplier=BulletMultiplier.X1;
        MakeInstance();
    }

    
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void InstantiateNormalBullet()
    {
        AudioManager.instance.GunShoot();
        //animation of the cannon to shoot/and also the particle system
        animator_cannnon.SetBool("Fire",true);
        if(bulletMultiplier==BulletMultiplier.X1)
        {
            GameObject tempBullet=Instantiate(normalBulletPrefab, spawnpoint.position, Quaternion.identity);
            tempBullet.GetComponent<SpriteRenderer>().color = StartColorOfTheBullet;
            //pointing at the crosshair by the cannon
            PointAtTheCrosshair(tempBullet);
        }
        else if(bulletMultiplier==BulletMultiplier.X2)
        {
            GameObject tempBullet=Instantiate(normalBulletPrefab, new Vector3(spawnpoint.position.x-0.16f,spawnpoint.position.y+0.1f,
            spawnpoint.position.z), Quaternion.identity);
            tempBullet.GetComponent<SpriteRenderer>().color = StartColorOfTheBullet;
            //pointing at the crosshair by the cannon
            PointAtTheCrosshair(tempBullet);
            
            GameObject tempBullet1=Instantiate(normalBulletPrefab, new Vector3(spawnpoint.position.x+0.16f,spawnpoint.position.y+0.1f,
            spawnpoint.position.z), Quaternion.identity);
            tempBullet1.GetComponent<SpriteRenderer>().color = StartColorOfTheBullet;
            //pointing at the crosshair by the cannon
            PointAtTheCrosshair(tempBullet1);

        }
        else if(bulletMultiplier==BulletMultiplier.X3)
        {
            GameObject tempBullet=Instantiate(normalBulletPrefab, new Vector3(spawnpoint.position.x,spawnpoint.position.y+0.1f,
            spawnpoint.position.z), Quaternion.identity);
            tempBullet.GetComponent<SpriteRenderer>().color = StartColorOfTheBullet;
            //pointing at the crosshair by the cannon
            PointAtTheCrosshair(tempBullet);
            
            GameObject tempBullet1=Instantiate(normalBulletPrefab, new Vector3(tempBullet.transform.position.x-0.18f,spawnpoint.position.y+0.1f,
            spawnpoint.position.z), Quaternion.identity);
            tempBullet1.GetComponent<SpriteRenderer>().color = StartColorOfTheBullet;
            //pointing at the crosshair by the cannon
            PointAtTheCrosshair(tempBullet1);
            
            GameObject tempBullet2=Instantiate(normalBulletPrefab, new Vector3(tempBullet.transform.position.x+0.18f,spawnpoint.position.y+0.1f,
            spawnpoint.position.z), Quaternion.identity);
            tempBullet2.GetComponent<SpriteRenderer>().color = StartColorOfTheBullet;
            //pointing at the crosshair by the cannon
            PointAtTheCrosshair(tempBullet2);

        }

    }
    public void InstantiateReflectiveBullet()
    {
        AudioManager.instance.GunShoot();
        //animation of the cannon to shoot/and also the particle system
        animator_cannnon.SetBool("Fire",true);
    
        Vector3 temp_pos=new Vector3(spawnpoint.position.x,spawnpoint.position.y,spawnpoint.position.z);
        GameObject tempBullet=Instantiate(reflective_bullet_prefab, temp_pos, Quaternion.identity);
        tempBullet.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = StartColorOfTheBullet;
        //pointing at the crosshair by the cannon
        PointAtTheCrosshair(tempBullet);
    }

    public void InstantiateMissileBulletPrefab()
    {
        AudioManager.instance.MissileLaunch();
        //animation of the cannon to shoot/and also the particle system
        animator_cannnon.SetBool("Fire",true);

 
        GameObject tempBullet=Instantiate(missile_bullet_prefab, spawnpoint.position, Quaternion.identity);
        tempBullet.GetComponent<SpriteRenderer>().color = StartColorOfTheBullet;
        //pointing at the crosshair by the cannon
        PointAtTheCrosshair(tempBullet);
    }
    public void InstantiateThePiercingBullet()
    {
        AudioManager.instance.GunShoot();
        //animation of the cannon to shoot/and also the particle system
        animator_cannnon.SetBool("Fire",true);

        if(BulletInstantiater.instance.bulletMultiplier==BulletMultiplier.X1)
        {
            GameObject tempBullet=Instantiate(piercing_bullet_prefab, spawnpoint.position, Quaternion.identity);
            tempBullet.GetComponent<SpriteRenderer>().color = StartColorOfTheBullet;
            //pointing at the crosshair by the cannon
            PointAtTheCrosshair(tempBullet);
        }
        else if(BulletInstantiater.instance.bulletMultiplier==BulletMultiplier.X2)
        {
            GameObject tempBullet=Instantiate(piercing_bullet_prefab, new Vector3(spawnpoint.position.x-0.04f
            ,spawnpoint.position.y+0.1f,spawnpoint.position.z), Quaternion.identity);
            tempBullet.GetComponent<SpriteRenderer>().color = StartColorOfTheBullet;
            //pointing at the crosshair by the cannon
            PointAtTheCrosshair(tempBullet);
            GameObject tempBullet1=Instantiate(piercing_bullet_prefab, new Vector3(spawnpoint.position.x+0.04f
            ,spawnpoint.position.y+0.1f,spawnpoint.position.z), Quaternion.identity);
            tempBullet1.GetComponent<SpriteRenderer>().color = StartColorOfTheBullet;
            //pointing at the crosshair by the cannon
            PointAtTheCrosshair(tempBullet1);
        
        }   
        else if(bulletMultiplier==BulletMultiplier.X3)
        {
            GameObject tempBullet=Instantiate(piercing_bullet_prefab, new Vector3(spawnpoint.position.x,spawnpoint.position.y+0.1f,
            spawnpoint.position.z), Quaternion.identity);
            tempBullet.GetComponent<SpriteRenderer>().color = StartColorOfTheBullet;
            //pointing at the crosshair by the cannon
            PointAtTheCrosshair(tempBullet);
            
            GameObject tempBullet1=Instantiate(piercing_bullet_prefab, new Vector3(tempBullet.transform.position.x-0.08f,spawnpoint.position.y+0.1f,
            spawnpoint.position.z), Quaternion.identity);
            tempBullet1.GetComponent<SpriteRenderer>().color = StartColorOfTheBullet;
            //pointing at the crosshair by the cannon
            PointAtTheCrosshair(tempBullet1);
            
            GameObject tempBullet2=Instantiate(piercing_bullet_prefab, new Vector3(tempBullet.transform.position.x+0.08f,spawnpoint.position.y+0.1f,
            spawnpoint.position.z), Quaternion.identity);
            tempBullet2.GetComponent<SpriteRenderer>().color = StartColorOfTheBullet;
            //pointing at the crosshair by the cannon
            PointAtTheCrosshair(tempBullet2);

        }

    } 
    private void PointAtTheCrosshair(GameObject tempBullet)
    {
        //cannon sparks
        Vector3 temp_pos=new Vector3(spawnpoint.position.x,spawnpoint.position.y,spawnpoint.position.z);
        
        //ParticleInitiation.instance.CannonSparks(temp_pos,GetComponent<SpriteRenderer>().color);

        Vector3 diff = crosshair.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //rotating the bullet to the specified point
        tempBullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

    }
    public void SwitchOffTheFireBool()
    {
        //animation of the cannon to shoot/and also the particle system
        animator_cannnon.SetBool("Fire",false);

    }



    //calling these functions in the player attack script which further calls the function in the ParticleInitiation
    /*public void SwitchOnTheFlame()
    {
        //cannon sparks
        Vector3 temp_pos=new Vector3(spawnpoint.position.x,spawnpoint.position.y+1.5f,spawnpoint.position.z);
        ParticleInitiation.instance.CannonSparks(temp_pos,GetComponent<SpriteRenderer>().color);
        curr_flame=ParticleInitiation.instance.SwitchOnTheFlames(gameObject.transform,GetComponent<SpriteRenderer>().color);

    }
    public void SwitchOffTheFlames()
    {
        ParticleInitiation.instance.SwitchOffTheFlames(curr_flame);
    }*/
    

}
