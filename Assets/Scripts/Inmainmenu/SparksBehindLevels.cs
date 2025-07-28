using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksBehindLevels : MonoBehaviour
{
    public ParticleSystem[] sparksBehindTheLevels;
    private int index=0;

    private bool canPlay=true;

    private float time=0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    IEnumerator canTrue()
    {
        yield return new WaitForSeconds(time);
        canPlay=true;
    }
    // Update is called once per frame
    void Update()
    {
        if(canPlay==true)
        {
            if((index-1)!=-1)
            {
                sparksBehindTheLevels[index].gameObject.SetActive(false);
            }
            sparksBehindTheLevels[index].gameObject.SetActive(true);
            index+=1;
            if(index==12)
            {
                index=0;
            }
            canPlay=false;
            StartCoroutine(canTrue());
        }
    }
}
