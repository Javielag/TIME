using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : MonoBehaviour
{

    public int damage, speed, attackrange, attacktime, casttime, kamikazespeed, explodingtime;
    GameObject player;
    Transform bulletPool;
    public Explosion explosionPrefab;
    private Vector2 angle;
    private bool attacking, kamikazing;

    // Use this for initialization
    void Start()
    {
        //inicialización de player
        player = GameManager.instance.GetPlayer();
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").transform;
    }

    // Update is called once per frame
    void Update()
    {       
        //Comportamiento como enemigo a melé
        if (!kamikazing)
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

        //Comportamiento como kamikaze
        else if (kamikazing)
        {
            angle = player.transform.position - transform.position;
            transform.Translate(angle.normalized * kamikazespeed * Time.deltaTime);
        }


    }

    //MÉTODOS DE COMPORTAMIENTO KAMIKAZE

    //Cambia al estado kamikaze e invoca la explosión por tiempo
    public void Kamikaze()
    {
        kamikazing = true;
        Invoke("Explode", explodingtime);
    }

    //Si está en kamikaze y colisiona con el jugador, explota
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && kamikazing)
        {
            kamikazing = false;
            Explode();
        }
    }

    //Explota, activa el trigger de daño y se destruye
    public void Explode()
    {
        Explosion newExplosion = Instantiate<Explosion>(explosionPrefab, transform.position, Quaternion.identity, bulletPool);
        Destroy(this.gameObject);
    }

    //Devuelve el estado kamikaze (se utiliza en Health.cs)
    public bool Kamikazing()
    {
        return kamikazing;
    }




    //MÉTODOS DE COMPORTAMIENTO A MELÉ

    //Cambia la variable de ataque, y tras un pequeño lapso de tiempo comienza la animación de ataque, y tras ella, otro lapso de tiempo que precede al reseteo del movimiento
    public void Attack()
    {
        attacking = true;
        //aquí va la animación
        Invoke("Damage", casttime);
        Invoke("Resetmove", attacktime);
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
