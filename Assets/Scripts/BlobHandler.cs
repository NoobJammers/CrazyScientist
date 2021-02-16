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
    public bool isFollowing { get; private set; }   //Is following the user?





    float timeToMergePos = 1f; //Time for blob to go to mergePos



    void Start()
    {
        isFollowing = false;
    }



    //Called when the user collects the blob
    public void StartFollowing()
    {
        isFollowing = true;
    }



    //Called when the user clicks on the blob
    private void OnMouseDown()
    {

        myCollider.enabled = false;
        transform.DOMove(mergePos.position, timeToMergePos, false).SetEase(moveToMergePos).OnComplete(() =>
        {

            //animator.SetTrigger("spin");
        });
        StartCoroutine(KickStartBlobRotate());

    }
    IEnumerator KickStartBlobRotate()
    {
        playerShooter.SetRigidBody(rigidBody);
        yield return new WaitForSeconds(timeToMergePos - 0.2f);
        animator.SetTrigger("spin");

    }

}
