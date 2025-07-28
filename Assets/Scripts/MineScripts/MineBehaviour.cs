using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviour : MonoBehaviour
{
    public GameObject blast;

    
    public float speed=10f;

    private void Awake() {
        StartCoroutine(destroy());
    }
    private void Update() {
        transform.Rotate(0f,0f,Time.deltaTime*speed);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player")
        {
            var temp_blast=Instantiate(blast,transform.position,Quaternion.identity);
            temp_blast.GetComponent<SpriteRenderer>().color=Color.yellow;
            //onlyt when the shield is not up
            if(!ShieldScript.instance.shieldOn)
            {
                int no=(int)double.Parse(other.gameObject.GetComponent<PlayerNumberUpdater>().text.text);
                no-=(int)(NumberProvider.instance.sceneTotal*0.7f);
                other.gameObject.GetComponent<AudioSource>().PlayOneShot(AudioManager.instance.mine_blast,AudioManager.instance.volume);
                other.gameObject.GetComponent<PlayerNumberUpdater>().text.text=no.ToString();
                other.GetComponent<Animator>().Play("PlayerHurt",0);
            }
            if(!TimeManager.instance.InSlowMo)
            {
                Camera.main.GetComponent<CameraScript>().startCoroutine(0.1f,0.3f);
            }

            //check input setting
            Handheld.Vibrate();

            Destroy(transform.parent.gameObject);
        }
    }
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(10f);
        var temp_blast=Instantiate(blast,transform.position,Quaternion.identity);
        temp_blast.GetComponent<SpriteRenderer>().color=Color.yellow;
        if(!TimeManager.instance.InSlowMo)
        {
            Camera.main.GetComponent<CameraScript>().startCoroutine(0.1f,0.3f);
        }

        //check input setting
        Handheld.Vibrate();

        Destroy(transform.parent.gameObject);

    }
}
