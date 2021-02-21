using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class StickyBlobController : MonoBehaviour
{
    public static Action<Vector3> explodedHere;

    public static Action<Vector3> smokehere;
    [System.Serializable]
    public class StickySpot
    {
        public Vector3 pos;
        public int blobID = 0; // 0-NoBlob, 1-FireBlob, 2-WaterBlob
        public GameObject firstBlob;

    }



    private bool exploded;
    public static List<StickySpot> stickySpots = new List<StickySpot>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<BlobHandler>().released && !exploded)
        {
            StickySpot ss = new StickySpot();
            ss.pos = transform.position;
            stickySpots.Add(ss);
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            transform.GetChild(0).gameObject.SetActive(true);

            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).parent = null;
            transform.GetChild(0).parent = null;
            exploded = true;
            explodedHere.Invoke(transform.position);
            Destroy(gameObject, 1);


        }
    }

    public static bool InStickyRegion(GameObject gameObject, int blobID)
    {

        foreach (StickySpot spot in stickySpots)
        {

            if ((gameObject.transform.position - spot.pos).magnitude <= 2)
            {
                if (spot.blobID == 0)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    spot.firstBlob = gameObject;
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    spot.blobID = blobID;
                }
                else
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

                    //When both the blobs are different
                    if (blobID != spot.blobID)
                    {
                        gameObject.GetComponent<Collider2D>().enabled = false;
                        gameObject.transform.DOMove(spot.firstBlob.transform.position, 0.5f, false);
                        spot.firstBlob.transform.GetChild(1).gameObject.SetActive(false);
                        spot.firstBlob.transform.GetChild(2).gameObject.SetActive(true);
                        
                        //Set smoke for both?
                        //gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    }

                    //When a same blob is thrown
                    else
                    {
                        gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    }
                }


                return true;
            }

        }
        return false;
    }
}
