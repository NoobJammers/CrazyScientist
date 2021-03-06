using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FireBlobController : MonoBehaviour
{
    public static Action<Vector3> explodedHere;
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

        if (GetComponent<BlobHandler>().released && !exploded && !StickyBlobController.InStickyRegion(gameObject, 1))
        {
            if (collision.gameObject.GetComponent<ExplosionReporter>())
            {
                collision.gameObject.GetComponent<ExplosionReporter>().TellDaddyImHurt(GetComponent<Collider2D>());

            }
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).parent = null;
            exploded = true;
            explodedHere.Invoke(transform.position);
            Destroy(gameObject, 1);

            /*   Destroy(gameObject);*/

        }
    }

}
