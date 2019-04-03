using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gusano : MonoBehaviour
{
    // variables públicas (velocidad bajo tierra, en el aire, distancia con el jugador a partir de la cual salta, tiempo que tarda en saltar, tiempo en el aire, tiempo excavando)
    public int speed,jumpspeed,jumprange,idletime, jumptime, digtime;
    GameObject player;
    // prefabs de aviso de salto y objeto encargado de hacer daño
    public GameObject wormholeprefab,wormholeadvice;
    Transform bulletPool;
    BoxCollider2D Collider;
    // Vectores de ángulo y posición a partir de la cual salta y a la cual se dirige
    private Vector2 angle,wormhole1, wormhole2;
    private bool jumping, advicing,digging;

    void Start()
    {
        //inicialización de player y el collider, que se activa o desactiva
        player = GameManager.instance.GetPlayer();
        Collider = GetComponent<BoxCollider2D>();
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").transform;
    }

    void Update()
    {
        //Si está bajo tierra, se mueve lentamente hasta estar cerca del jugador
        if (!jumping && !advicing && !digging)
        {
            //comparación de la posición del jugador y el enemigo
            angle = player.transform.position - transform.position;
            transform.Translate(angle.normalized * speed * Time.deltaTime);
            //Si entra en rango de salto activa el aviso de salto
            if (Vector2.Distance(player.transform.position, transform.position) <= jumprange)
            {
                Advice();
            }
        }
        //si está saltando, se mueve rápidamente
        if (jumping)
        {
            transform.Translate(angle * jumpspeed * Time.deltaTime);
        }



    }

    //Avisa al jugador de que va a saltar,decide la dirección del salto, instancia un "aviso de salto" y comienza la secuencia de salto
    public void Advice()
    {
        advicing = true;
        wormhole1 = transform.position;
        wormhole2 = player.transform.position;
        GameObject WHA = Instantiate<GameObject>(wormholeadvice, wormhole1, Quaternion.identity, bulletPool);
        Invoke("Jump", idletime);
    }

    //salta, activa el collider para que sea posible dañarlo, y crea una zona de daño en su posición inicial
    public void Jump()
    {
        Collider.enabled = true;
        GameObject thisWormHole = Instantiate<GameObject>(wormholeprefab, wormhole1, Quaternion.identity, bulletPool);
        jumping = true;
        advicing = false;
        angle = (wormhole2 - wormhole1).normalized;
        Invoke("Dig", jumptime);

    }

    //Al terminar el tiempo de salto, vuelve a introducirse bajo tierra y crea otra zona de daño
    public void Dig()
    {
        wormhole1 = transform.position;
        GameObject thisWormHole = Instantiate<GameObject>(wormholeprefab, wormhole1, Quaternion.identity, bulletPool);
        digging = true;
        jumping = false;
        Invoke("Resetmove", digtime);
    }

    //al estar bajo tierra reseteal el movimiento normal y desactiva su collider para ser invulnerable de nuevo
    public void Resetmove()
    {
        Collider.enabled = false;
        digging = false;
    }

}
