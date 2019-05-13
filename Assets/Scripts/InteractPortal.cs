using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePortal : MonoBehaviour {

    Transform player;
    float range;
    public float offset = 0.5f;
    Vector2 playerpos, objpos;
    
    // Use this for initialization
	void Start ()
    {
        player = GameManager.instance.GetPlayer().transform;
        objpos = transform.position;
        range = player.GetComponent<PlayerController>().interactRange;
    }
	
	// Update is called once per frame
	void Update()
    {
        playerpos = player.position;
        if ((Vector2.Distance(objpos, playerpos) <= range+offset))
        {
            this.GetComponentInChildren<GameObject>().SetActive(true);
        }
        else this.GetComponentInChildren<GameObject>().SetActive(false);
    }
}