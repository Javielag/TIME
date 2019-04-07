using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtCursor : MonoBehaviour {

    private void Start()
    {
    }
    private void LateUpdate()
    {
        Vector3 mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);

        Vector2 dir = new Vector2(mouse.x - transform.position.x, mouse.y - transform.position.y);
        PointAt(dir);

        Physics2D.Raycast(transform.position,mouse);
        

    }
    private void PointAt(Vector2 newDir)
    {
        transform.right = newDir;
    }
}
