using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);

        transform.position = mouse;

    }
}
