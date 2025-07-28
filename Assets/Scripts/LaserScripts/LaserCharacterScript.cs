using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCharacterScript : MonoBehaviour
{
    public float speed;

    private GameObject this_parent;

    private bool cangrow,receed,wait;

    private Color colorOftheLaser;

    public GameObject blast;
    private string parent_name;

    public ParticleSystem LaserInitiation;
    private float decreament_time=0.1f,current_time=0f;
    private void Awake() {
        LaserInitiation.gameObject.SetActive(true);
        StartCoroutine(WaitForSomeTimeBeforeInitiation());
        wait=true;
        colorOftheLaser=GetComponent<SpriteRenderer>().color;
        this_parent=transform.parent.gameObject;
        parent_name=this_parent.name;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(receede());
    }

    // Update is called once per frame
    void Update()
    {
        if(cangrow)
        {
            var tempscale=this_parent.transform.localScale;
            tempscale.x+=Time.deltaTime*speed;
            this_parent.transform.localScale=tempscale;
        }
        if(receed&&this_parent.transform.localScale.x>0)
        {
            var tempscale=this_parent.transform.localScale;
            tempscale.x-=Time.deltaTime*speed;
            this_parent.transform.localScale=tempscale;

        }
        if(wait&&this_parent.transform.localScale.x<1f)
        {
            var tempscale=this_parent.transform.localScale;
            tempscale.x+=Time.deltaTime*0.4f;
            this_parent.transform.localScale=tempscale;

        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.tag=="Walls")
        {
            if(parent_name=="LaserParentUp")
            {
                if(other.name=="LowerWall")
                {
                    cangrow=false;
                }
            }
            else
            {
                if(other.name=="RightCollisionWall")
                {
                    cangrow=false;
                }
            }
            
        }
        
    }
    
    private void OnTriggerStay2D(Collider2D other) {
        if(!ShieldScript.instance.shieldOn)
        {
            if(other.gameObject.tag=="Player"&&transform.parent.localScale.x>1f)
            {
                if(current_time>decreament_time)
                {
                    current_time=0;
                    int no=(int)double.Parse(other.gameObject.GetComponent<PlayerNumberUpdater>().text.text);
                    no-=2;
                    Handheld.Vibrate();
                    other.gameObject.GetComponent<PlayerNumberUpdater>().text.text=no.ToString();

                }
                current_time+=Time.deltaTime;
                other.GetComponent<Animator>().Play("PlayerHurt",0);
            }
        }
    }
    IEnumerator receede()
    {
        yield return new WaitForSeconds(10f);
        receed=true;
        AudioManager.instance.mech_Audio_source.Stop();
        StartCoroutine(DestroyLaser());
    }
    IEnumerator DestroyLaser()
    {
        yield return new WaitForSeconds(3f);
        var blastt=Instantiate(blast,transform.position,Quaternion.identity);
        blastt.GetComponent<SpriteRenderer>().color=Color.red;
        Destroy(transform.parent.parent.gameObject);
    }
    IEnumerator WaitForSomeTimeBeforeInitiation()
    {
        yield return new WaitForSeconds(3f);
        cangrow=true;
        AudioManager.instance.mech_Audio_source.clip=AudioManager.instance.laserWhirring;
        AudioManager.instance.mech_Audio_source.Play();

        LaserInitiation.gameObject.SetActive(false);
        wait=false;
    }
}
