using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtEnemy : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }
    public Transform Target;
    public Camera camera;
    private void Update()
    {
        Vector2 target = Target.position;
        Vector2 dir = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        PointAt(dir);
        
        transform.position = new Vector2(Mathf.Clamp(target.x, target.x - camera.pixelWidth/2, target.x + camera.pixelWidth / 2), Mathf.Clamp(target.y, target.y - camera.pixelWidth / 2, target.y + camera.pixelWidth / 2));
    }
    private void PointAt(Vector2 newDir)
    {
        transform.up = -newDir;
    }
}
