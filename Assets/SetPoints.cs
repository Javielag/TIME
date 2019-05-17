using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPoints : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Transform[] points = new Transform[4];
        points = GetComponentsInChildren<Transform>();
        GameManager.instance.SetWarningPoints(points);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
