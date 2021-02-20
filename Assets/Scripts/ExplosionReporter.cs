using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionReporter : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
    }



    public void TellDaddyImHurt(Collider2D collider)
    {
        GetComponentInParent<ExplosionObjectEffect>().ApplyForceOnChildren(collider);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if (collision.gameObject.GetComponent<FireBlobController>() &&  !StickyBlobController.InStickyRegion(collision.collider.gameObject, 0))
        //     GetComponentInParent<ExplosionObjectEffect>().ApplyForceOnChildren(collision.collider);
    }
}
