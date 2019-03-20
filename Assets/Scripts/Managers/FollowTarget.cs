using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public Transform target;
    public float xLimit, yLimit;

    private void Start()
    {
    }
    private void LateUpdate()
    {
        Vector3 follow = Vector3.zero;
        //calcula el offset en x
        float offX = target.position.x - transform.position.x;
        if(offX > xLimit)
        {
            follow.x = offX - xLimit;
        }
        else if(offX < -xLimit)
        {
            follow.x = offX + xLimit;   

        }
        //calcula el offset en y
        float offY = target.position.y - transform.position.y;
        if (offY > yLimit)
        {
            follow.y = offY - yLimit;
        }
        else if (offY < -yLimit)
        {
            follow.y = offY + yLimit;
        }
        transform.Translate(follow);


    }
}
