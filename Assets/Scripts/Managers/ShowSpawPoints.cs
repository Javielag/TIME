using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSpawPoints : MonoBehaviour {

    public float rad;
    public Color color;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        foreach (Transform child in transform)
        {
            Gizmos.DrawWireSphere(child.position, rad);
        }
    }
    

}
