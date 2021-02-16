using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
[RequireComponent(typeof(Rigidbody2D))]
public class FollowedBy : MonoBehaviour
{
    Rigidbody2D body;
    public float speed;
    // public SpriteRenderer spriteRenderer;
    public FollowedBy followedby;

    // Start is called before the first frame update
    void Start()
    {

      
    }


    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {


        if (followedby)
        {
            
                body = followedby.GetComponent<Rigidbody2D>();
            body.AddForce((-followedby.transform.position + transform.position) * speed); }
     
            



    }
    public void Add()
    {
        TrainMove.ExtendTrain(this);
    }

}
