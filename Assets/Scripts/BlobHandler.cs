using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlobHandler : MonoBehaviour
{


    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] Collider2D myCollider;
    [SerializeField] Animator animator;
    [SerializeField] Transform mergePos; //Position above player's head
    [SerializeField] PlayerShooter playerShooter;
    [SerializeField] Ease moveToMergePos;
    private bool clicked = false;
    public bool isFollowing { get; private set; }   //Is following the user?





    float timeToMergePos = 1f; //Time for blob to go to mergePos



    void Start()
    {
        
    }



    //Called when the user collects the blob
    public void StartFollowing()
    {
    
    }



    //Called when the user clicks on the blob
    private void OnMouseDown()
    {
        
            TrainMove.RemoveCarriage(GetComponent<FollowedBy>());
            Debug.Log("CLICKED");
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.isKinematic = true;
        rigid.GetComponent<SpringJoint2D>().enabled = true;
        myCollider.enabled = false;
            transform.DOMove(mergePos.position, timeToMergePos, false).SetEase(moveToMergePos).OnComplete(() =>
            {

                //animator.SetTrigger("spin");
                KickStartBlobRotate();
        });
          /*  StartCoroutine();*/
        
       
         
          
    

    }
   void KickStartBlobRotate()
    {
        playerShooter.SetRigidBody(rigidBody);
      
        animator.SetTrigger("spin");

    }

}
