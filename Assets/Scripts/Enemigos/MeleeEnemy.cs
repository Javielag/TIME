using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    // variables públicas (velocidad, daño rango de ataque y tiempo de ataque) y privadas (ángulo de movimiento y booleano que indica cuando ataca y cuando no)
    public float speed, attackrange, attackTime, casttime;
    public int damage;
    GameObject player;
    private Vector2 angle;
    private bool attacking;

    void Start()
    {
        //inicialización de player
        player = GameManager.instance.GetPlayer();
    }

    void Update()
    {
        //comparación de la posición del jugador y el enemigo
        angle = player.transform.position - transform.position;
        // Si está cerca del jugador y no está atacando, inicia la secuencia de ataque
        if (Vector2.Distance(player.transform.position, transform.position) <= attackrange && !attacking)
        {
            Attack();
        }

        // mientras no está atacando, va hacia el jugador
        if (!attacking)
        {
            transform.Translate(angle.normalized * speed * Time.deltaTime);
        }
    }

    //Cambia la variable de ataque, y tras un pequeño lapso de tiempo comienza la animación de ataque, y tras ella, otro lapso de tiempo que precede al reseteo del movimiento
    public void Attack()
    {
        attacking = true;
        //aquí va la animación
        Invoke("Damage", casttime);
        Invoke("Resetmove", attackTime);
    }

    // Si tras el "casteo" del ataque el jugador se encuentra en rango, es dañado
    public void Damage()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= attackrange)
        {
            GameManager.instance.ChangeHealth(-damage, player.gameObject);
        }

    }

    //Despúes de atacar, haya dañado o no al jugador, vuelve a moverse
    public void Resetmove()
    {
        attacking = false;
    }


}
