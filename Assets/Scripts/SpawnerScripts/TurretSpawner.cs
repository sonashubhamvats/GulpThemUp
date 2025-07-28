using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{

    [HideInInspector]public Transform[] TurretSpawnPoint;

    public GameObject Turret_prefab;
    private int randomNoOfTurrets; 

    [HideInInspector]public bool spawnBy;



    public void SpawnTurrets()
    {
        var randomNumber=Random.Range(2,5);
        if(spawnBy)
        {
            if(randomNumber<4)
            {
                randomNumber=4;
            }
        }
        Debug.Log(randomNumber);
        for(int i=0;i<randomNumber;i++)
        {
            var temp_turret=Instantiate(Turret_prefab,new Vector3(TurretSpawnPoint[i].position.x,TurretSpawnPoint[i].position.y,0f),Quaternion.identity);
            temp_turret.SetActive(false);
            temp_turret.gameObject.transform.GetChild(0).GetComponent<TurretShoot>().spawmBy=spawnBy;
            if(spawnBy)
            {
                temp_turret.gameObject.transform.GetChild(0).GetComponent<TurretShoot>().time_shooting=11f;
            }
            else
            {
                temp_turret.gameObject.transform.GetChild(0).GetComponent<TurretShoot>().time_shooting=7f;
            }
            temp_turret.SetActive(true);
        }
        Destroy(gameObject);
    }
}
