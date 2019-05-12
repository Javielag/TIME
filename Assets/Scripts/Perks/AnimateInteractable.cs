using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateInteractable : MonoBehaviour {

    GameObject player;
    float range;
    Vector2 playerpos, objpos;
    public Animator anim;
    
    // Use this for initialization
	void Start ()
    {
        player = GameManager.instance.GetPlayer();
        objpos = transform.position;
        range = player.GetComponent<PlayerController>().interactRange;
    }
	
	// Update is called once per frame
	void Update()
    {
        playerpos = player.transform.position;
        if ((Vector2.Distance(objpos, playerpos) <= range))
        {
            anim.SetBool("pickup", true);
        }
        else anim.SetBool("pickup", false);
    }
}
