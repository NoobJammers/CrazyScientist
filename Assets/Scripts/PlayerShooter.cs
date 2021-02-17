using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{

    private  Rigidbody2D rigidBody;
    [SerializeField] Rigidbody2D hook;
    [SerializeField] GameObject trajectoryBlob;
    [SerializeField] float impulseForce;
    public float releaseTime = 0.15f;
    public float maxDragDistance = 2f;


    bool isPressed = false, hasHoveringBlob = false;
    float timeInterval = 1f;

    void Start()
    {

    }

    void Update()
    {


        if (rigidBody != null && Input.GetMouseButtonUp(0) && (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).magnitude > 1 && (transform.position-rigidBody.transform.position).magnitude<0.5f)
        {
            rigidBody.drag = 19.71f;
            rigidBody.angularDrag = 4.25f;
            isPressed = false;
            rigidBody.GetComponent<SpringJoint2D>().enabled = false;
            rigidBody.isKinematic = false;
            TrainMove.ExtendTrain(rigidBody.GetComponent<FollowedBy>());
            hasHoveringBlob = false;
            rigidBody = null;
        }
        if (isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
                rigidBody.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
            else
                rigidBody.position = mousePos;

            StartCoroutine(SetMeUp());

            //Debug.Log(rigidBody.GetComponent<SpringJoint2D>().breakForce);

        }

    }


    IEnumerator SetMeUp()
    {
        yield return new WaitForSeconds(timeInterval);
      /*  SetTrajectory();*/
    }
    void SetTrajectory()
    {

        GameObject go = Instantiate(trajectoryBlob, rigidBody.transform.position, Quaternion.identity);
        // go.GetComponent<SpringJoint2D>().connectedBody = hook;
        // go.GetComponent<SpringJoint2D>().enabled = true;
        // go.GetComponent<Rigidbody2D>().isKinematic = true;
        // go.GetComponent<Rigidbody2D>().position = rigidBody.position;

        go.GetComponent<Rigidbody2D>().AddForce(((hook.transform.position - go.transform.position) * impulseForce), ForceMode2D.Impulse);
        //StartCoroutine(SmalDelay(go));

    }
    IEnumerator SmalDelay(GameObject go)
    {
        yield return new WaitForSeconds(0.3f);
        go.GetComponent<Rigidbody2D>().isKinematic = false;
        go.GetComponent<SpringJoint2D>().enabled = false;
        // this.enabled = false;
    }


    public void SetRigidBody(Rigidbody2D rb)
    {
        rigidBody = rb;
     /*   rigidBody.transform.SetParent(gameObject.transform,true);*/
        hasHoveringBlob = true;

    }

    void OnMouseDown()
    {
        if (hasHoveringBlob)
        {
            rigidBody.GetComponent<Collider2D>().enabled = false;
            isPressed = true;
            rigidBody.isKinematic = true;
            rigidBody.GetComponent<SpringJoint2D>().enabled = true;
            GetComponent<FollowedBy>().followedby = null;
        }
    }

    void OnMouseUp()
    {
        if (hasHoveringBlob)
        {
            isPressed = false;
          
            rigidBody.drag = 0;
            rigidBody.angularDrag = 0;
            rigidBody.isKinematic = false;
            
            hasHoveringBlob = false;
           
            StartCoroutine(Release());
        }
    }
    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);
        rigidBody.GetComponent<SpringJoint2D>().enabled = false;
        yield return new WaitForSeconds(0.12f);
        rigidBody.GetComponent<Collider2D>().enabled = true;
        rigidBody = null;

     
        // this.enabled = false;
        /*
                yield return new WaitForSeconds(2f);*/



    }

}
