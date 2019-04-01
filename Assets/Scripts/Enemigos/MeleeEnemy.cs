using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    // variables públicas (velocidad, daño rango de ataque y tiempo de ataque) y privadas (ángulo de movimiento y booleano que indica cuando ataca y cuando no)
    public int speed,damagerange,attackrange, attackTime,damage;
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
        angle = this.GetComponent<Pathfinder>().Direction() - transform.position;
        if (Vector2.Distance(player.transform.position,transform.position) <=attackrange && !attacking)
        {
            
            attack();
            GameManager.instance.ChangeHealth(-damage, player.gameObject);
        }
        // mientras no está atacando, va hacia el jugador
        if(!attacking)
        {
            transform.Translate(angle.normalized * speed * Time.deltaTime);
        }  
    }

    //Cambia la variable de ataque, ataca e invoca el método de reseteo, que devuelve el ataque a false y hace que el enemigo siga de nuevo al jugador
    public void attack()
    {        
        attacking = true;
        //aquí va la animación
        Invoke("resetmove", attackTime);
    }

    // resetea el movimiento
    public void resetmove()
    {        
        attacking = false;
    }


}
