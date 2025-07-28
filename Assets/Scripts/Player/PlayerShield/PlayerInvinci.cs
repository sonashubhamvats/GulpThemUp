using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvinci : MonoBehaviour
{
    private bool invinciAnimate;
    private SpriteRenderer _this_renderer;

    private CircleCollider2D _this_collider;
    private float interval;
    // Start is called before the first frame update
    void Start()
    {
        _this_renderer=GetComponent<SpriteRenderer>();
        _this_collider=GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invinciAnimate)
        {
            if(interval>=0.2f)
            {
                var colo=_this_renderer.color;
                if(colo.a>0f)
                {
                    colo.a=0f;
                }
                else
                {
                    colo.a=1f;
                }
                _this_renderer.color=colo;
                interval=0f;
            }  
            interval+=Time.deltaTime;
        }    
    }
    public void MakePlayerInvincible()
    {
        invinciAnimate=true;
        _this_collider.enabled=false;
        StartCoroutine(invincible());
    }
    IEnumerator invincible()
    {
        yield return new WaitForSeconds(4f);
        _this_collider.enabled=true;
        var colo=_this_renderer.color;
        colo.a=1f;
        _this_renderer.color=colo;
        invinciAnimate=false;
    }

}
