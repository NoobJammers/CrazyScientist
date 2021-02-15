using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class TrainMovement : MonoBehaviour
{
    static List<GameObject> Train = new List<GameObject>();
    public static float separateDistance =0.5f;
    public static GameObject Player;

    // Start is called before the first frame update
    public GameObject[] compartments;
    public static int flip
    {
        get { return PlayerController.flipBool ? 1 : -1; }
        

    }
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("player");
        Train.Add(Player);

        StartCoroutine(JumpToPosition());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addCarriage(GameObject GAMEOBJECT)
    {
        //Animate to the distance away but for now:
        /*  if(checkForSpace())*/
        Debug.Log(Player.GetComponent<SpriteRenderer>().flipX);
        /* GAMEOBJECT.transform.position = Train[(Train.Count - 1)].transform.position +new Vector3 (flip * separateDistance,0, 0);*/

       /* Train.Add(GAMEOBJECT);
        */
      
        

    }

    public IEnumerator JumpToPosition()
    {
        
        foreach (GameObject g in compartments)
        {
            Rigidbody2D RIGIDBODY = g.GetComponent<Rigidbody2D>();
            RIGIDBODY.bodyType = RigidbodyType2D.Dynamic;
            RIGIDBODY.AddForce(RIGIDBODY.mass * Mathf.Sqrt((2 * Physics2D.gravity.magnitude * Mathf.Abs(Train[(Train.Count - 1)].transform.position.y + 3f))) * g.transform.up, ForceMode2D.Impulse);
            RIGIDBODY.AddForce(Mathf.Abs(Train[(Train.Count - 1)].transform.position.x - g.transform.position.x - separateDistance) * flip * Vector3.right, ForceMode2D.Impulse);
            addCarriage(g);
            yield return new WaitForSecondsRealtime(0.5f);
        }



    }

 /*   bool checkForSpace()
    {

    }*/

}
