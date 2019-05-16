
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTowardsCursor : MonoBehaviour
{

    public bool inverted;
    public SpriteRenderer sp;
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        Vector2 dir = transform.position - mouse;
        if (dir.x < 0)
        {
            sp.flipX = inverted;
        }
        else
        {
            sp.flipX = !inverted;
        }
    }
}