using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    private SpriteRenderer this_spriteRenderer;
    [HideInInspector]public bool shieldOn;
    public static ShieldScript instance;
    private void Awake() {
        
        this_spriteRenderer=GetComponent<SpriteRenderer>();
        MakeInstance();
    }
    void MakeInstance()
    {
        Debug.Log("Here in shiled script");
        if(instance==null)
        {
            instance=this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwitchOnTheShield()
    {
        shieldOn=true;
        this_spriteRenderer.enabled=true;
    }
    public void SwitchOffTheShield()
    {
        shieldOn=false;
        this_spriteRenderer.enabled=false;

    }
}
