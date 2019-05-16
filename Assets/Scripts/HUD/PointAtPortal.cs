using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtPortal : MonoBehaviour {

	// Use this for initialization
    private void Start()
    {
    }
    public Transform Target;
    private void Update()
    {
        Vector2 target = Target.position;
        Vector2 dir = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        PointAt(dir);       
    }
    private void PointAt(Vector2 newDir)
    {
        transform.up = -newDir;
    }
}
