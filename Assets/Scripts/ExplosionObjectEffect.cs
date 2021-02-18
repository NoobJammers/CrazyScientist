using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ExplosionObjectEffect : MonoBehaviour
{
    public float ForceMultiplier = 10000;
    public float dissapearaftertime = 1.5f;
    public float dissappearingtime = 1f;
    public bool vanishingdoor;
    public void ApplyForceOnChildren(Collider2D collider)
    {
        if (collider.GetComponent<BlobHandler>().held)
            return;
        foreach (Rigidbody2D rigid in GetComponentsInChildren<Rigidbody2D>())

        {
            rigid.isKinematic = false;
            rigid.AddForce((-collider.attachedRigidbody.position + rigid.position) * ForceMultiplier, ForceMode2D.Impulse);
           
            if (vanishingdoor )
            StartCoroutine( Delay(dissapearaftertime, () =>
            {
                rigid.GetComponent<Collider2D>().enabled = false;
                if (rigid)
                { StartCoroutine(


                    FadeAndDestroy(

                    rigid.GetComponent<SpriteRenderer>(), dissappearingtime)

                    );
            } }));
        }
    }

  
 
    public IEnumerator Delay(float delayby, Action a)
    {
        yield return new WaitForSecondsRealtime(delayby);
        a.Invoke();
    }

    public IEnumerator FadeAndDestroy(SpriteRenderer sprite, float time)
    {
       
        float timestart = 0;
        while (timestart < time)
        {
            float howmuch = 255 - (int)((255 / time) * timestart);

           sprite.color = new Color32(255, 255, 255, (byte)howmuch);
            timestart += Time.deltaTime;
            yield return null;
        }
       
        Destroy(sprite.gameObject);
     


    }

}
