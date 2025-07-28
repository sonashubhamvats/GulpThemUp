using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPoweupSpawner : MonoBehaviour
{
    public GameObject coin_prefab,ammo_prefab;
    public static CoinPoweupSpawner instance;
    
    //this will be used in the ammo assertion in the inGameStatsManager
    [HideInInspector]public int totalEntitiesInEnemyScene;
    private void Awake() {
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
    public void SpawnCoinPrefabs(int noOfSpawns,Vector3 positionOfSpawn)
    {
        for(int i=0;i<noOfSpawns;i++)
        {
            Instantiate(coin_prefab,positionOfSpawn,Quaternion.identity);
        }
    }
    public void SpawnAmmoPrefabs(Vector3 positionOfSPawn)
    {
        var powerupAmmo=Instantiate(ammo_prefab,positionOfSPawn,Quaternion.identity);
        powerupAmmo.GetComponent<CoinPowerUpPrefab>().AssignTheAmmoValuesToThePowerUp(totalEntitiesInEnemyScene);
    }
    public void AssignTheTotalEntities(int temptotal)
    {
        totalEntitiesInEnemyScene=temptotal;
    }
}
