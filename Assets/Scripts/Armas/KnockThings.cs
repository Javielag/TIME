using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockThings : MonoBehaviour {

    public float knockbackForce=5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayKnockback knock = collision.GetComponent<PlayKnockback>();
        if (knock)
        { 
            knock.KnockThis(transform.right,knockbackForce);
        }
        
    }
    public void ChangeForce(float newForce)
    {
        knockbackForce = newForce;        
    }
}
