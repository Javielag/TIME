using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caballo : MonoBehaviour
{
    // variables públicas (velocidad, daño rango de ataque y tiempo de ataque) y privadas (ángulo de movimiento y booleano que indica cuando ataca y cuando no)
    public int attackTime, damage, jumpDistance;
    GameObject player;
    private Vector2 dir;
    private bool attacking;


    void Start()
    {
        //inicialización de player
        player = GameManager.instance.GetPlayer();
    }

    void Update()
    {
        // mientras no está atacando, va hacia el jugador
        if (!attacking)
        {
            dir = player.transform.position - transform.position;
            Jump();
        }
        else
        {
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                transform.Translate(new Vector2(Mathf.Sign(dir.x) * jumpDistance * 2, Mathf.Sign(dir.y) * jumpDistance)*Time.deltaTime);
            }
            else
            {
                transform.Translate(new Vector2(Mathf.Sign(dir.y) * jumpDistance * 2, Mathf.Sign(dir.x) * jumpDistance)*Time.deltaTime);
            }
        }
    }

    //Cambia la variable de ataque, ataca e invoca el método de reseteo, que devuelve el ataque a false y hace que el enemigo siga de nuevo al jugador
    public void Jump()
    {
        attacking = true;
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        //Falta reproducir animacion de subir



        //falta reproducir animacion de bajar
        Invoke("fall", attackTime);
    }

    // resetea el movimiento
    public void Fall()
    {
        this.gameObject.GetComponent<Collider2D>().enabled = true;
        attacking = false;
        Debug.Log("Siguiendo" + attacking);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.ChangeHealth(-damage, collision.gameObject); //Si el trigger colisiona con el jugador, le daña
        }

    }
}
