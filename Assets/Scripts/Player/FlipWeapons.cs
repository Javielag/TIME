using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipWeapons : MonoBehaviour {

    private bool flipped = false;

	void Update () {
		if(!flipped && transform.right.x < 0)
        {
            SpriteRenderer sprite;
            foreach(Transform child in transform)
            {
                sprite = child.GetComponent<SpriteRenderer>();
                if (sprite)
                {
                    sprite.flipY = true;
                }
            }
            flipped = true;

        }
        else if (flipped && transform.right.x >= 0)
        {
            SpriteRenderer sprite;
            foreach (Transform child in transform)
            {
                sprite = child.GetComponent<SpriteRenderer>();
                if (sprite)
                {
                    sprite.flipY = false;
                }
            }
            flipped = false;

        }
    }
}
