using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FireBlobController : MonoBehaviour
{
    public bool exploded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(GetComponent<BlobHandler>().released && !exploded)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).parent = null;
            exploded = true;
            StartCoroutine(Delay(1f, () => { Destroy(gameObject); }));

         /*   Destroy(gameObject);*/
        }
    }
    public IEnumerator Delay(float delayby, Action a)
    {
        yield return new WaitForSecondsRealtime(delayby);
        a.Invoke();
    }
}
