using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCallFromGameplay : MonoBehaviour
{
    public ItemName _name;

    public GameObject blast;
    public Transform multiplierBlastTransform;
    //calling the realPoweruphere
    public void CallThePowerUp()
    {
        switch(_name)
        {
            case ItemName._2xMultiplier:
                var blastt=Instantiate(blast,multiplierBlastTransform.transform.position,Quaternion.identity);
                blastt.GetComponent<SpriteRenderer>().color=GameColorUnification.instance.selectedColor;
                BulletInstantiater.instance.bulletMultiplier=BulletMultiplier.X2;
                AudioManager.instance.itemAudioSource.PlayOneShot(AudioManager.instance.cover_no_blast);
                break;
            case ItemName._3xMultiplier:
                blastt=Instantiate(blast,multiplierBlastTransform.transform.position,Quaternion.identity);
                blastt.GetComponent<SpriteRenderer>().color=GameColorUnification.instance.selectedColor;
                BulletInstantiater.instance.bulletMultiplier=BulletMultiplier.X3;
                AudioManager.instance.itemAudioSource.PlayOneShot(AudioManager.instance.cover_no_blast);
                break;
            case ItemName.GunsOfRetribution:
                InGameAntiPlayerAndWithPlayerMechs.instance.SpawnTurret(true);
                break;
            case ItemName.RainOfLife:
                InGameAntiPlayerAndWithPlayerMechs.instance.SpawnRain(true);
                break;
        }
        gameObject.SetActive(false);
    }
}
