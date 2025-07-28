using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    public GameObject turret_bullet_prefab;
    public Transform spawnPoint;

    private Vector3 endPoint;

    public float firerate;
    
    private float currentTime=0;

    public bool spawmBy,atPos;

    public Color red,green;
    
    private float rot_z;

    private bool dead_sequence;

    private Quaternion start_rotation;

    public float time_shooting;
    public GameObject blast;
    private void Awake() {
        
    }
    private void Start() {
        int rand=Random.Range(-2,3);
        int y=0+rand;
        endPoint=new Vector3(0f,y,0f);
    }
    // Start is called before the first frame update
    void OnEnable() 
    {
        //used for going backk to the original rotation in the death sequence
        start_rotation = transform.parent.rotation;
        
        if(spawmBy)
        {
            GetComponent<SpriteRenderer>().color=green;
        }
        else
        {
            GetComponent<SpriteRenderer>().color=red;          
        }
        
        StartCoroutine(StartShooting());
    }
   void RotateIf()
    {
        
        Vector3 diff = endPoint - transform.parent.transform.position;
        diff.Normalize();
        rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        Quaternion currentRotation = transform.parent.rotation;
        Quaternion wantedRotation = Quaternion.Euler(0,0,rot_z-90);
        transform.parent.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime *70f);
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateIf();
        if(atPos&&!dead_sequence)
        {
            
            if(currentTime>=(firerate))
            {
                currentTime=0;
                InstantiateNormalBulletFromTurret(spawnPoint.position);
            }    
            currentTime+=Time.deltaTime;
        }       
        if(dead_sequence)
        {
            RotateBack();
        }
 
    }
    void RotateBack()
    {
        Quaternion currentRotation=transform.parent.rotation;
        Quaternion wantedRotation = start_rotation;
        transform.parent.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime *100f);
    }
    IEnumerator StartShooting()
    {
        yield return new WaitForSeconds(6f);
        StartCoroutine(StartTheDeadSequence());
        if(spawmBy)
        {
            AudioManager.instance.itemAudioSource.PlayOneShot(AudioManager.instance.turret_shoot,AudioManager.instance.volume);
        }
        else
        {
            AudioManager.instance.mech_Audio_source.PlayOneShot(AudioManager.instance.turret_shoot,AudioManager.instance.volume);
        }

        atPos=true;
    }
    IEnumerator StartTheDeadSequence()
    {
        yield return new WaitForSeconds(time_shooting);
        dead_sequence=true;

        StartCoroutine(DestroyG());
    }
    IEnumerator DestroyG()
    {
        yield return new WaitForSeconds(3f);
        AudioManager.instance.itemAudioSource.PlayOneShot(AudioManager.instance.cover_no_blast,AudioManager.instance.volume);
        var blastt=Instantiate(blast,transform.position,Quaternion.identity);
        blastt.GetComponent<SpriteRenderer>().color=GetComponent<SpriteRenderer>().color;
        ParticleInitiation.instance.CollisioSparks(transform.position,GetComponent<SpriteRenderer>().color);
        Destroy(gameObject);
    }
    //turret shoot
    public void InstantiateNormalBulletFromTurret(Vector3 position)
    {
        GameObject tempBullet=Instantiate(turret_bullet_prefab, position, Quaternion.identity);

        tempBullet.GetComponent<TurretBulletCharacter>().direction=(endPoint-position).normalized*tempBullet.
        GetComponent<TurretBulletCharacter>().speed;

        tempBullet.GetComponent<TurretBulletCharacter>().callFrom=spawmBy;
        if(spawmBy)
        {
            tempBullet.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            tempBullet.GetComponent<SpriteRenderer>().color=Color.red;
        }
        
    }
}
