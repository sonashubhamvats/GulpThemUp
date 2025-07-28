using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    public GameObject LaserLeft_prefab,LaserUp_prefab;

    [HideInInspector]public Transform[] SpawnPointsLeftLaser,SpawnPointsUpLaser;

    public void SpawnTheLasers()
    {
        int randomNoLeft=Random.Range(2,4);
        int randomNoUp=Random.Range(1,3);
        for(int i=0;i<randomNoLeft;i++)
        {
            Instantiate(LaserLeft_prefab,new Vector3(SpawnPointsLeftLaser[i].position.x,SpawnPointsLeftLaser[i].position.y,0f),Quaternion.identity);
        }
        for(int i=0;i<randomNoUp;i++)
        {
            Instantiate(LaserUp_prefab,new Vector3(SpawnPointsUpLaser[i].position.x,SpawnPointsUpLaser[i].position.y,0f),Quaternion.Euler(0f,0f,90f));
        }
        Destroy(gameObject);
    }
}
