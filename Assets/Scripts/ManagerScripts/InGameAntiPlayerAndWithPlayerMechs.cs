using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameAntiPlayerAndWithPlayerMechs : MonoBehaviour
{
    public Transform[] turret_spawn_points;

    public Transform rainMLeft,rainMRight,rainY;

    
    public Transform[] LaserSpawnLeft,LaserSpawnUp;
    public Transform[] spawnRainParticleSystem;

    
    private bool allowMine,allowLaser,allowTurret,allowRain,coolOff;
    
    private bool allowLaserTimeIncrement,allowTurretTimeIncrement,allowRainTimeIncrement;
    public float intervalMine,intervalLaser,intervalTurret,intervalRain;
    
    private float coolOffInterval;

    private float timePassedSinceLastMineSpawn=0f,timePassedSinceLastLaserSpawn=0f,timePassedSinceLastTurretSpawn=0f,timePassedSinceLastRainSpawn=0f,coolOffTime=0f;
    private int levelNo;
    public GameObject TurretSpawner,RainSpawner,LaserSpawner,mine_prefab;
    
    public bool usingAnItem;
    
    
    public static InGameAntiPlayerAndWithPlayerMechs instance;
    // Start is called before the first frame update
    private void Awake() {
        //using the gamemanager to instantiate the different mechanisms
        
        levelNo=GameManager.instance.levelno;
        
        if(levelNo>=8&&levelNo<=15)
        {
            Debug.Log("SpawnMine");
            allowMine=true;
        }
        else if(levelNo>=16&&levelNo<=24)
        {
            allowMine=true;
            allowRain=true;
            allowRainTimeIncrement=true;
        }
        else if(levelNo>=25&&levelNo<=32)
        {
            allowLaser=true;
            allowMine=true;
            allowRain=true;
            int rand=Random.Range(0,2);
            if(rand==1)
            {
                allowLaserTimeIncrement=true;
                allowRainTimeIncrement=false;
            }
            else
            {
                allowLaserTimeIncrement=false;
                allowRainTimeIncrement=true;

            }

        }
        else if(levelNo>32)
        {
            allowLaser=allowMine=allowRain=allowTurret=true;
            int rand=Random.Range(0,3);
            if(rand==1)
            {
                allowLaserTimeIncrement=true;
                allowRainTimeIncrement=false;
                allowTurretTimeIncrement=false;
            }
            else if(rand==0)
            {
                allowLaserTimeIncrement=true;
                allowRainTimeIncrement=false;
                allowTurretTimeIncrement=false;

            }
            else
            {
                allowTurretTimeIncrement=true;
                allowRainTimeIncrement=false;
                allowLaserTimeIncrement=false;

            }

        }
        
        MakeInstance();
    }
    void MakeInstance()
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
    void Start()
    {
        if(allowMine)
        {
            SpawnMine();
        }
      //SpawnRain(false);
      //SpawnTurret(false);
      //SpawnLasers();
      //SpawnMine();

    }
 
    // Update is called once per frame
    void Update()
    {
        if(!usingAnItem)
        {
            if(!coolOff)
            {

                if(allowRainTimeIncrement)
                {
                    timePassedSinceLastRainSpawn+=Time.deltaTime;
                }
                if(allowTurretTimeIncrement)
                {
                    timePassedSinceLastTurretSpawn+=Time.deltaTime;
                }
                if(allowLaserTimeIncrement)
                {
                    timePassedSinceLastLaserSpawn+=Time.deltaTime;
                }
                if(timePassedSinceLastRainSpawn>intervalRain)
                {
                    if(allowRain)
                    {
                        SpawnRain(false);
                        coolOff=true;
                        coolOffInterval=18f;
                        Debug.Log(coolOffInterval);
                        if(levelNo>24)
                        {
                            allowRainTimeIncrement=false;
  
                            if(levelNo<32)
                            {
                                allowLaserTimeIncrement=true;
                                
                            }
                            else
                            {
                                int rand1=Random.Range(0,2);
                                if(rand1==0)
                                {
                                    allowLaserTimeIncrement=true;
                                    allowTurretTimeIncrement=false;
                                }
                                else
                                {
                                    allowLaserTimeIncrement=false;
                                    allowTurretTimeIncrement=true;

                                }
                            }
                        }
                    }
                    timePassedSinceLastRainSpawn=0f;
                }
                if(timePassedSinceLastLaserSpawn>intervalLaser)
                {
                    if(allowLaser)
                    {
                        SpawnLasers();
                        coolOff=true;
                        coolOffInterval=23f;
                        if(levelNo<32)
                        {
                            allowLaserTimeIncrement=false;
                            allowRainTimeIncrement=true;
                        }
                        else
                        {
                            allowLaserTimeIncrement=false;
                            int rand1=Random.Range(0,2);
                            if(rand1==0)
                            {
                                allowRainTimeIncrement=true;
                                allowTurretTimeIncrement=false;
                            }
                            else
                            {
                                allowRainTimeIncrement=false;
                                allowTurretTimeIncrement=true;

                            }
                        }
                    }
                    timePassedSinceLastLaserSpawn=0f;
                }
                if(timePassedSinceLastTurretSpawn>intervalTurret)
                {
                    if(allowTurret)
                    {
                        SpawnTurret(false);
                        coolOff=true;
                        coolOffInterval=22f;
                        allowTurretTimeIncrement=false;
                        int rand1=Random.Range(0,2);
                        if(rand1==0)
                        {
                            allowRainTimeIncrement=true;
                            allowLaserTimeIncrement=false;
                        }
                        else
                        {
                            allowRainTimeIncrement=false;
                            allowLaserTimeIncrement=true;

                        }

                    }
                    timePassedSinceLastTurretSpawn=0f;
                }
            }
            else
            {
                coolOffTime+=Time.deltaTime;
                Debug.Log("cool of interval "+coolOffTime);
                if(coolOffTime>coolOffInterval)
                {
                    coolOffTime=0;
                    coolOff=false;
                }
            }
        }
        if(timePassedSinceLastMineSpawn>intervalMine)
        {
            if(allowMine)
            {
                int rand=0;
                if(levelNo>15)
                {
                    rand=Random.Range(0,2);
                }
                if(rand==0)
                {

                    SpawnMine();

                }
            }
            
            timePassedSinceLastMineSpawn=0f;
        }
        timePassedSinceLastMineSpawn+=Time.deltaTime;


    }
    public void SpawnRain(bool callBy)
    {
        var rainSpawner=Instantiate(RainSpawner,Vector3.zero,Quaternion.identity);
        
        
        rainSpawner.GetComponent<RainSpawner>().rainMLeft=rainMLeft;
        rainSpawner.GetComponent<RainSpawner>().rainMRight=rainMRight;
        rainSpawner.GetComponent<RainSpawner>().rainY=rainY;
        rainSpawner.GetComponent<RainSpawner>().callFrom=callBy;
        rainSpawner.GetComponent<RainSpawner>().spawnRainParticleSystem=spawnRainParticleSystem;
        
        rainSpawner.gameObject.GetComponent<RainSpawner>().StartRainSequence(callBy);
    }
    public void SpawnTurret(bool callBy)
    {
        var turretSpawner=Instantiate(TurretSpawner,Vector3.zero,Quaternion.identity);
        

        turretSpawner.GetComponent<TurretSpawner>().spawnBy=callBy;
        turretSpawner.GetComponent<TurretSpawner>().TurretSpawnPoint=turret_spawn_points;

        turretSpawner.gameObject.GetComponent<TurretSpawner>().SpawnTurrets();
    }
    public void SpawnLasers()
    {
        var laserSpawner=Instantiate(LaserSpawner,Vector3.zero,Quaternion.identity);
        laserSpawner.GetComponent<LaserSpawner>().SpawnPointsLeftLaser=LaserSpawnLeft;
        laserSpawner.GetComponent<LaserSpawner>().SpawnPointsUpLaser=LaserSpawnUp;
        laserSpawner.GetComponent<LaserSpawner>().SpawnTheLasers();
    }

    public void SpawnMine()
    {
        
        float x_pos=Random.Range((circleMovement.instance.leftMarginX.position.x+2f),(circleMovement.instance.rightMarginX.position.x-2f));
        float y_pos=Random.Range((circleMovement.instance.UpMarginY.position.y-2f),(circleMovement.instance.DownMarginY.position.y+2f));
        Instantiate(mine_prefab,new Vector3(x_pos,y_pos,0f),Quaternion.identity);
    }


}
