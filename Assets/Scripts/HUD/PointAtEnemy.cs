using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtEnemy : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    public Transform Target;
    Camera cam;
    public Vector2 newpos;

    private void Update()
    {
        Vector2 target = Target.position;
        Vector2 dir = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        PointAt(dir);
        if (target.x > -18 + cam.transform.position.x && target.x < 18 + cam.transform.position.x && target.y < 10 + cam.transform.position.y && target.y > -10 + cam.transform.position.y)
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
        else
        {
            if (GetComponentInChildren<SpriteRenderer>().enabled == false) GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
        newpos = new Vector2(Mathf.Clamp(target.x,-18 + cam.transform.position.x, 18 + cam.transform.position.x), Mathf.Clamp(target.y,-10 + cam.transform.position.y, 10 + cam.transform.position.y));
        transform.position = newpos;
    }
    private void PointAt(Vector2 newDir)
    {
        transform.up = -newDir;
    }
    public void SetTarget(Transform target)
    {
        Target = target;
    }
}