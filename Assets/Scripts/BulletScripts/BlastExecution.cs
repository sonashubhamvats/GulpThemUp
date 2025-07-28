using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastExecution : MonoBehaviour
{
    private SpriteRenderer this_sprite_renderer;
    private float this_localScale=1f;
    private List<string> nameOfEffectedGameObjects=new List<string>();
    public float speed_of_disappearance;
    private void Awake() {
        this_sprite_renderer=GetComponent<SpriteRenderer>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale=new Vector3(1f,1f,1f)*this_localScale;
    }

    // Update is called once per frame
    void Update()
    {
        this_localScale+=Time.deltaTime*speed_of_disappearance;
        transform.localScale=new Vector3(1f,1f,1f)*this_localScale;
        Color color_in_alpha=this_sprite_renderer.color;
        color_in_alpha.a-=Time.deltaTime;
        if(color_in_alpha.a<=0)
        {
            //instant death
            Destroy(gameObject);
        }
        this_sprite_renderer.color=color_in_alpha;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.tag=="Friend"||other.gameObject.tag=="Enemy")
        {
            int flag=0;
            for(int i=0;i<nameOfEffectedGameObjects.Count;i++)
            {
                if(nameOfEffectedGameObjects[i]==other.gameObject.name)
                {
                    flag=1;
                    break;
                }
            }
            if(flag==0)
            {
                //initialize the change in velocity
                Debug.Log(other.name);
                //other.gameObject.GetComponent<MovementOfTheNumbers>().ChangeTheDirectionWithBlast(transform.position);
                //reducing the health a bit 
                other.gameObject.GetComponent<SingleNumberHealthOfNumber>().DamageByTheBlast();
                nameOfEffectedGameObjects.Add(other.gameObject.name);
            }
        }
    }
}
