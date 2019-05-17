using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtEnemy : MonoBehaviour
{
    float height, width;
    // Use this for initialization
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        height = cam.orthographicSize;
        width = height * cam.aspect;
    }

    public Transform Target;
    Camera cam;
    public Vector2 newpos;

    private void Update()
    {
        Vector2 target = Target.position;
        Vector2 dir = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        PointAt(dir);
        if (target.x > -width + cam.transform.position.x && target.x < width + cam.transform.position.x && target.y < height + cam.transform.position.y && target.y > -height + cam.transform.position.y)
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
        else
        {
            if (GetComponentInChildren<SpriteRenderer>().enabled == false) GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
        newpos = new Vector2(Mathf.Clamp(target.x,-width + cam.transform.position.x, width + cam.transform.position.x), Mathf.Clamp(target.y,-height + cam.transform.position.y, height + cam.transform.position.y));
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