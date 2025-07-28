using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInitiation : MonoBehaviour
{
    public ParticleSystem particleSystemForDeathOfANumber,_10xp,_20xp,_30xp,_40xp,_50xp;
    public ParticleSystem bulletSparks;

    public ParticleSystem collisionSparks_prefab;

    public ParticleSystem spaltEffect_prefab,starts_prefab;

    public ParticleSystem flames_prefab_cannon,laser_hurt;
    public static ParticleInitiation instance;

    void Awake()
    {
        MakeInstance();
    }
    private void MakeInstance()
    {
        if(instance==null)
        {
            instance=this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void XpPointsEmergemce(Vector3 position)
    {
        ParticleSystem par=null;
        switch(GameManager.instance.xp_type)
        {
            case 1:
                par=Instantiate(_10xp,position,Quaternion.identity);
                break;
            case 2:
                par=Instantiate(_20xp,position,Quaternion.identity);
                break;
            case 3:
                par=Instantiate(_30xp,position,Quaternion.identity);
                break;
            case 4:
                par=Instantiate(_40xp,position,Quaternion.identity);
                break;
            case 5:
                par=Instantiate(_50xp,position,Quaternion.identity);
                break;
        }
        destroyTheParticleEffect(par);
    }
    public void destructionOfaNumber(Transform destroyedObject,Color color,float scale)
    {
        ParticleSystem particle=Instantiate(particleSystemForDeathOfANumber, destroyedObject.position, Quaternion.identity);
       
        particle.gameObject.SetActive(false);
        var main = particle.main;
        main.startColor = color;

        if (scale > 4f)
        {
            main.startSize = 0.3f;
            main.startSpeed = 5f;
        }
        particle.gameObject.SetActive(true);
        StartCoroutine(destroyTheParticleEffect(particle));
        // particleSystemForDeathOfANumber.Play();
    }
    public void DestroyTheBullet(Vector3 posOfTheBullet,Color color)
    {
        //bullet collision effect
        var temporaryParticleEffect=Instantiate(bulletSparks,posOfTheBullet,Quaternion.identity);
        temporaryParticleEffect.gameObject.SetActive(false);
        var main=temporaryParticleEffect.main;
        main.startColor=color;
        temporaryParticleEffect.gameObject.SetActive(true);
        StartCoroutine(destroyTheParticleEffect(temporaryParticleEffect));
    }
    IEnumerator destroyTheParticleEffect(ParticleSystem particle)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(particle.gameObject);
    }
    // Update is called once per frame
    public void CollisioSparks(Vector2 pos,Color color)
    {
        //collision Sparks
        var collisionSpark=Instantiate(collisionSparks_prefab,pos,Quaternion.identity);
        collisionSpark.gameObject.SetActive(false);
        var main=collisionSpark.main;
        main.startColor=color;
        collisionSpark.gameObject.SetActive(true);
        StartCoroutine(destroyTheParticleEffect(collisionSpark));
    }
    public void LaserHurtSparks(Vector2 pos)
    {
        //collision Sparks
        var laserSpark=Instantiate(laser_hurt,pos,Quaternion.identity);
        StartCoroutine(destroyTheParticleEffect(laserSpark));
    }
    public void CannonSparks(Vector2 pos,Color color)
    {
        //collision Sparks
        var cannonSparks=Instantiate(bulletSparks,pos,Quaternion.identity);
        cannonSparks.gameObject.SetActive(false);
        var main=cannonSparks.main;
        main.startColor=color;
        cannonSparks.gameObject.SetActive(true);
        StartCoroutine(destroyTheParticleEffect(cannonSparks));
    }
    public void CoverParticleDestruction(Vector2 pos,Color color)
    {
        //collision Sparks
        var spalteffect=Instantiate(spaltEffect_prefab,pos,Quaternion.identity);
        ParticleSystem particle=Instantiate(particleSystemForDeathOfANumber, pos, Quaternion.identity);
        spalteffect.gameObject.SetActive(false);
        particle.gameObject.SetActive(false);
        var main=spalteffect.main;
        var main2=particle.main;
        main.startColor=color;
        main2.startColor=color;
        main2.startSize=0.25f;
        spalteffect.gameObject.SetActive(true);
        particle.gameObject.SetActive(true);
        StartCoroutine(destroyTheParticleEffect(spalteffect));
    }
    public void CoinStarEffect(Vector3 pos)
    {
        Debug.Log("Initiated coin star");
        var starEffect=Instantiate(starts_prefab,pos,Quaternion.identity);
        StartCoroutine(destroyTheParticleEffect(starEffect));
    }

   /* public ParticleSystem SwitchOnTheFlames(Transform parent,Color color)
    {
        //if statement to ensure that only one flame switched on
        if(BulletInstantiater.instance.curr_flame==null)
        {
            //the flame_prefab is the already placed prefab in the cannon which is in inactive state to use it in setting the poition of the new flame
            var flames=Instantiate(flames_prefab_cannon,flames_prefab_cannon.transform.position,flames_prefab_cannon.transform.rotation);
            
            flames.gameObject.SetActive(false);
            flames.gameObject.transform.SetParent(parent);
            flames.transform.localScale=flames_prefab_cannon.transform.localScale;
            var main=flames.main;
            main.startColor=color;
            flames.gameObject.SetActive(true);
            return flames;
        }
        else
        {
            return BulletInstantiater.instance.curr_flame;
        }
    }
    public void SwitchOffTheFlames(ParticleSystem flame)
    {
        if(flame!=null)
        {
            flame.gameObject.transform.parent=null;
            Destroy(flame.gameObject);
        }
    }*/
    
}
