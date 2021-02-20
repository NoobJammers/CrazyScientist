using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlobHandler : MonoBehaviour
{


   private Rigidbody2D rigidBody;
   private Collider2D myCollider;
   private Animator animator;
    [SerializeField] Transform mergePos; //Position above player's head
   private PlayerShooter playerShooter;
    [SerializeField] Ease moveToMergePos;
    public bool held = false;
    public bool isFollowing { get; private set; }   //Is following the user?

    public bool released = false;



    float timeToMergePos = 1f; //Time for blob to go to mergePos



    void Start()
    {
        myCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        playerShooter = mergePos.GetComponent<PlayerShooter>();
        rigidBody = GetComponent<Rigidbody2D>();
    }



    //Called when the user collects the blob



    //Called when the user clicks on the blob
    private void OnMouseDown()
    {

        if (!held)
        {
            TrainMove.RemoveCarriage(GetComponent<FollowedBy>());
            Debug.Log("CLICKED");
            Rigidbody2D rigid = GetComponent<Rigidbody2D>();
            rigid.drag = 10;
            rigid.angularDrag = 3;



            playerShooter.SetRigidBody(rigidBody);
        }
        /*  rigid.isKinematic = true;*/
        /*rigid.GetComponent<SpringJoint2D>().enabled = true;*/

     
        /*rigid.GetComponent<SpringJoint2D>().enabled = true;*/
        /*  transform.DOMove(mergePos.position, timeToMergePos, false).SetEase(moveToMergePos).OnComplete(() =>
          {

              //animator.SetTrigger("spin");
              KickStartBlobRotate();
      });*/
        /*  StartCoroutine();*/






    }
   void KickStartBlobRotate()
    {
       
      
        animator.SetTrigger("spin");

    }

}
