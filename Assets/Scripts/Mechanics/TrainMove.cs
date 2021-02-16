using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMove : MonoBehaviour
{
    public static FollowedBy Head;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ExtendTrain(FollowedBy leader)
    {
        if (Head == null)
        { Head= leader;
           
        }

        else
        { var temp = Head.followedby;
            /*while (temp.followedby!=null)
            {
                temp = temp.followedby;
            }*/
            leader.followedby = temp;
            Head.followedby = leader;
        }
    }

    public static void flip()
    {
     /*   var temphead = Head;*/
        // FollowedBy first = null;
        // List<FollowedBy> list = new List<FollowedBy>();
        // var temp = Head.followedby;
        // while(temp.followedby!=null)
        // {
        //     list.Add(temp);
        //     temp = temp.followedby;
        // }
        // first = temp;

        // for(int i=list.Count-1;i>=0;i--)
        // {
        //     temp.followedby = list[i];
        //     temp = temp.followedby;
        // }
        // temp.followedby = null;
        // Head.followedby = first;

    }
}
