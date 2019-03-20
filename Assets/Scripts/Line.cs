using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {


    void OnDrawGizmosSelected()
    {
        Vector3 mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, mouse);
    }
}
