using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

    public Transform target;

    private void Update()
    {
        if (target)
        {
            Vector2 dir = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
            PointAt(dir);
        }
        
    }
    private void PointAt(Vector2 newDir)
    {
        transform.right = newDir;
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}

