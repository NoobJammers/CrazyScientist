using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBlobController : MonoBehaviour
{
    private bool exploded;
    public static List<Vector3> StickySpots=new List<Vector3>();
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
        if (GetComponent<BlobHandler>().released && !exploded)
        {
            StickySpots.Add(transform.position);
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            transform.GetChild(0).gameObject.SetActive(true);
     
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).parent = null;
            transform.GetChild(0).parent = null;
            exploded = true;
            Destroy(gameObject, 1);


            /*   Destroy(gameObject);*/
        }
    }

    public static bool InStickyRegion(GameObject gameObject)
    {
        foreach(Vector3 spots in StickySpots)
        {
            if((gameObject.transform.position-spots).magnitude<=3)
            {
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                return true;
            }
        }
        return false;
    }
}
