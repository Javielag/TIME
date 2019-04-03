using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormHole : MonoBehaviour {
    public int damage;
    private bool damaged;

    // Use this for initialization
    void Start()
    {
        damaged = false;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    //Al entrar el jugador en el area de daño del gusano, esta le daña una sola vez
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!damaged && other.gameObject.tag=="Player")
        {
            damaged = true;
            GameManager.instance.ChangeHealth(-damage, other.gameObject);
        }

    }
}
