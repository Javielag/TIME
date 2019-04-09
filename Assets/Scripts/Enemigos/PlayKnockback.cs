using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayKnockback : MonoBehaviour {

    Rigidbody2D rb;
    private float cd=0.05f, timer;
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        timer = Time.time;
	}
    public void KnockThis(Vector2 direction, float force)
    {
        //cooldown para evitar que se sumen retrocesos
        if (Time.time > timer)
        {
            if(GetComponent<PlayerController>())
                Debug.Log("Pushed Player");
            rb.AddForce(direction * force, ForceMode2D.Impulse);
            timer = Time.time + cd;
        }        
    }
}