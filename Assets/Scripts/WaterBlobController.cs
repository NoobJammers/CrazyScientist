using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WaterBlobController : MonoBehaviour
{
    public bool exploded = false;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<BlobHandler>().released && !exploded)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).parent = null;
            exploded = true;
            Destroy(gameObject, 1f);

        }
    }
}
