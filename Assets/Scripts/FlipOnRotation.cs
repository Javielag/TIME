using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipOnRotation : MonoBehaviour {

    bool flipped;
	
	// Update is called once per frame
	void Update () {
		if(!flipped && transform.right.x < 0)
        {
            
            foreach(Transform child in transform)
            {
                SpriteRenderer sprite = child.GetComponent<SpriteRenderer>();
                if (sprite)
                {
                    sprite.flipY = true;
                }
            }
            flipped = true;
        }
        else if(flipped && transform.right.x >= 0)
        {
            foreach (Transform child in transform)
            {
                SpriteRenderer sprite = child.GetComponent<SpriteRenderer>();
                if (sprite)
                {
                    sprite.flipY = false;
                }
            }
            flipped = false ;
        }
	}
}
