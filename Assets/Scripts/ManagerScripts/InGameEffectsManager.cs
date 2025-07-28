using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameEffectsManager : MonoBehaviour
{
    public GameObject blast_Effect_prefab;
    public static InGameEffectsManager instance;
    // Start is called before the first frame update
    private void Awake() {
        MakeInstance();
    }
    void MakeInstance()
    {
        if(instance==null)
        {
            instance=this;
        }
        else
        {
            Destroy(gameObject);
        }
    }//make instance closes
    

    //spawn the blast effect
    public void SpawnTheBlastEffect(Vector3 spawnPointOftheEffect,Color color_of_the_bullet)
    {
        GameObject BlastEffect=Instantiate(blast_Effect_prefab,spawnPointOftheEffect,Quaternion.identity);
        //assigning the color
        BlastEffect.GetComponent<SpriteRenderer>().color=color_of_the_bullet;
        StartCoroutine(destroyTheEffect(BlastEffect));
    
    }//close the spawn effect blast

    //coroutine for destroying the effect
    IEnumerator destroyTheEffect(GameObject Effect_blast)
    {
        yield return new WaitForSeconds(1.3f);
        //destroy
        if(Effect_blast!=null)
        {
            Destroy(Effect_blast);
        }
    }//the coroutine ends

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
