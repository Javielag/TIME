using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipWithSpeed : MonoBehaviour {

    public bool inverted;
    private Rigidbody2D rb;
    SpriteRenderer sp;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(rb != null && sp != null){
            if(rb.velocity.x > 0) { sp.flipX = inverted; }
            else { sp.flipX = !inverted; }
        }
	}
}
