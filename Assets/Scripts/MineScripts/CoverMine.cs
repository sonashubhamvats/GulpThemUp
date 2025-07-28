using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverMine : MonoBehaviour
{
    public GameObject mine,particle_system;
    public float speed=30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x>2f)
        {
            var scale=transform.localScale;
            scale.x-=Time.deltaTime*speed;
            scale.y-=Time.deltaTime*speed;
            transform.localScale=scale;
        }
        else
        {
            mine.gameObject.SetActive(true);
            particle_system.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
