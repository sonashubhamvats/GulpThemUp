using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainBlast : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroy());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale=transform.localScale;
        scale.x+=Time.deltaTime*speed;
        scale.y+=Time.deltaTime*speed;
        transform.localScale=scale;
    }
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
