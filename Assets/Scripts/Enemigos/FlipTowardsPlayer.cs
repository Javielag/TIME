using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTowardsPlayer : MonoBehaviour {

    public bool inverted;//cambiar si el GameObject da la espalda al jugador
    private Transform player;
    private SpriteRenderer sp;
    private Vector2 dir;
	// Use this for initialization
	void Start () {
        player = GameManager.instance.GetPlayer().transform;
        sp = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //comprueba si el jugador esta a la izquierda o a la derecha
        dir = player.position - transform.position;
        if (dir.x < 0)
        {
            sp.flipX = inverted;
        }
        else
        {
            sp.flipX = !inverted;
        }
	}
}
