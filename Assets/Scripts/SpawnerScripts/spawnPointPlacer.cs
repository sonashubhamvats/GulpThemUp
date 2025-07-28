using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPointPlacer : MonoBehaviour
{
    public Vector3 relativePos;
    public Transform spawnPoint;
    private void Awake()
    {
        relativePos.z=Camera.main.nearClipPlane;
        Vector3 bottomLeft=Camera.main.ViewportToWorldPoint(relativePos);
        if(gameObject.name=="CannonParent")
        {
            bottomLeft.y+=GameManager.instance.offsetForCannon.y;
            var pos=spawnPoint.position;
            pos.y-=(GameManager.instance.offsetForCannon.y/10f);
            spawnPoint.position=pos;
        }
        if(gameObject.name=="LeftCollisionWall")
        {
            bottomLeft.x-=0.5f;
        }
        if(gameObject.name=="RightCollisionWall")
        {
            bottomLeft.x+=0.5f;
        }
        if(gameObject.name=="UpperWall")
        {
            bottomLeft.y+=0.5f;
        }
        if(gameObject.name=="LowerWall")
        {
            bottomLeft.y-=0.5f;
        }
        transform.position=bottomLeft;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
