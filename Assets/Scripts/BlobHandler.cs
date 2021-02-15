using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlobHandler : MonoBehaviour
{


    [SerializeField] Rigidbody2D rigidBody2D;
    [SerializeField] Transform mergePos;
    [SerializeField] Ease EaseType;
    public bool isFollowing { get; private set; }   //Is following the user?


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
        //rigidBody2D.velocity = (mergePos.position - transform.position).normalized;
        transform.DOMove(mergePos.position, 1f, false).SetEase(EaseType);
    
    }


}
