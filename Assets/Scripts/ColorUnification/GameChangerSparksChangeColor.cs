using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameChangerSparksChangeColor : MonoBehaviour
{
    public ParticleSystem[] sparks;
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    void Start()
    {
        
    }
    public void changeTheColorOfTheSparks(Color color)
    {
        GetComponent<SpriteRenderer>().color=color;
        for(int i=0;i<3;i++)
        {
            var main=sparks[i].main;
            main.startColor=GetComponent<SpriteRenderer>().color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
