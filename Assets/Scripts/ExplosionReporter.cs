using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionReporter : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Blob")
            GetComponentInParent<ExplosionObjectEffect>().ApplyForceOnChildren(collision.collider);
    }
}
