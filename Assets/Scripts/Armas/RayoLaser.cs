﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayoLaser : MonoBehaviour {

    public int dmg;
    public float ancho, knock;
    Collider2D hitBox;

    private void Start()
    {
        hitBox = GetComponent<Collider2D>();
        findEnd();
        Shoot(findEnd());
    }
    private Vector2 findEnd()
    {
        Vector2 end = new Vector2 (100,100);
        LayerMask mask = LayerMask.GetMask("Paredes");
        RaycastHit2D wall = Physics2D.Raycast(transform.position, transform.right, 100, mask);
        if (wall)
        {
            end = wall.point;
            Debug.Log("Dibujado");
            Debug.DrawLine(transform.position, end, Color.green, 2);
            //Debug.DrawRay(transform.position, transform.right, Color.red, 2);
            if (wall.collider.CompareTag("Pared"))
                Debug.Log("Pared");
        }
        return end;
    }
    private Vector2 Centre(Vector2 p1,Vector2 p2)
    {
        return (new Vector2(p1.x + (p2.x - p1.x) / 2, p1.y + (p2.y - p1.y) / 2));
    }
    private void Shoot(Vector2 end)
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(Centre(transform.position,end),new Vector2(Vector2.Distance(transform.position,end),ancho)
            ,Vector2.Angle(transform.right,new Vector2(1,0)));
        int i = 0;
        foreach (Collider2D hit in hits)
        {
            i++;
            Debug.Log("Lazered " + hit.name + " " + i);
        }
    }
}
