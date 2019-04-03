using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gusano : MonoBehaviour
{
    // variables públicas (velocidad, daño rango de ataque y tiempo de ataque) y privadas (ángulo de movimiento y booleano que indica cuando ataca y cuando no)
    public int speed,jumpspeed,jumprange,idletime, jumptime, digtime;
    GameObject player;
    public GameObject wormholeprefab,wormholeadvice;
    Transform bulletPool;
    BoxCollider2D Collider;
    private Vector2 angle, wormhole1, wormhole2,dir;
    private bool jumping, advicing,digging;

    void Start()
    {
        //inicialización de player
        player = GameManager.instance.GetPlayer();
        Collider = GetComponent<BoxCollider2D>();
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").transform;
    }

    void Update()
    {
        if (!jumping && !advicing && !digging)
        {
            //comparación de la posición del jugador y el enemigo
            angle = player.transform.position - transform.position;
            transform.Translate(angle.normalized * speed * Time.deltaTime);
            if (Vector2.Distance(player.transform.position, transform.position) <= jumprange)
            {
                Advice();
            }
        }
        if (jumping)
        {
            transform.Translate(angle * jumpspeed * Time.deltaTime);
        }



    }
    public void Advice()
    {
        advicing = true;
        wormhole1 = transform.position;
        wormhole2 = player.transform.position;
        GameObject WHA = Instantiate<GameObject>(wormholeadvice, wormhole1, Quaternion.identity, bulletPool);
        Invoke("Jump", idletime);
    }

    public void Jump()
    {
        Collider.enabled = true;
        GameObject thisWormHole = Instantiate<GameObject>(wormholeprefab, wormhole1, Quaternion.identity, bulletPool);
        jumping = true;
        advicing = false;
        angle = (wormhole2 - wormhole1).normalized;
        Debug.Log(angle);
        Invoke("Dig", jumptime);

    }
    public void Dig()
    {
        wormhole1 = transform.position;
        GameObject thisWormHole = Instantiate<GameObject>(wormholeprefab, wormhole1, Quaternion.identity, bulletPool);
        digging = true;
        jumping = false;
        Invoke("Resetmove", digtime);
    }
    public void Resetmove()
    {
        Collider.enabled = false;
        digging = false;
    }

}
