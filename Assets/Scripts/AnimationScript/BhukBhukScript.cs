using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class BhukBhukScript : MonoBehaviour
{

    private void Start() 
    {
        
        SpriteRenderer spriteRenderer=GetComponent<SpriteRenderer>();
        transform.localScale=new Vector3(1f,1f,1f);
        float width=spriteRenderer.sprite.bounds.size.x;
        float height=spriteRenderer.sprite.bounds.size.y;

        float worldHeight=Camera.main.orthographicSize*2f;
        float worldWidth=worldHeight/Screen.height*Screen.width;

        Vector3 tempScale=transform.localScale;
        tempScale.x=worldWidth/width+2f;
        tempScale.y=worldHeight/height+8f;
        
        transform.localScale=tempScale;
    }
    private void Update() {
       
        
    }
    public SpriteRenderer BackGround;
    public void SetActive()
    {
        BackGround.enabled=true;
    }
    public void SetInActive()
    {
        BackGround.enabled=false;
    }
}