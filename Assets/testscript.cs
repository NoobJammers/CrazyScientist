using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{
    public GameObject finalposition;
    Rigidbody2D RIGIDBODY;
    private float yposition;
    private float xposition;
    float TIME = 0;
    bool jump;
    // Start is called before the first frame update
    void Start()
    {
        RIGIDBODY = GetComponent<Rigidbody2D>();
        yposition = Mathf.Abs(transform.position.y-finalposition.transform.position.y-1.2f);
        Debug.Log("final pos " + yposition);
        xposition = Mathf.Abs(finalposition.transform.position.x-transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void FixedUpdate()
    {

        // RIGIDBODY.AddForce(((new Vector3(finalposition.transform.position.x-0.5f, finalposition.transform.position.y, finalposition.transform.position.z) - transform.position)/((new Vector3(finalposition.transform.position.x - 0.5f, finalposition.transform.position.y, finalposition.transform.position.z) - transform.position).magnitude))*10, ForceMode2D.Impulse);
        if (!jump)
        {
            Debug.Log(-2 * Physics2D.gravity.y * yposition);
            RIGIDBODY.AddForce(RIGIDBODY.mass*Mathf.Sqrt((-2*Physics2D.gravity.y*yposition)) * transform.up, ForceMode2D.Impulse);
            RIGIDBODY.AddForce(xposition * transform.right, ForceMode2D.Impulse);
            jump = true;
      
            
          /*  TIME += Time.fixedDeltaTime;*/
        }
       
          
        
        Debug.Log("how close " + Mathf.Abs(transform.position.y - finalposition.transform.position.y));

        /*  Debug.Log(transform.position.y);*/
        /*  jump = true;*/

    }
}
