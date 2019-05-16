using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : MonoBehaviour
{

    public int damage, acc,maxSpeed, attackrange, attacktime, casttime, kAcc,kMaxSpeed, dyingtime, explodingtime;
    Rigidbody2D rb;
    GameObject player;
    Transform bulletPool;
    public GameObject explosionPrefab;
    private Vector2 angle;
    private bool attacking, kamikazing, dying;
    public Animator anim;
    SpriteRenderer sp;
    Pathfinder pathfinder;

    // Use this for initialization
    private void Start()
    {
        //inicialización de player
        player = GameManager.instance.GetPlayer();
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").transform;
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        pathfinder = GetComponent<Pathfinder>();
        if (rb)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (pathfinder)
            //comparación de la posición del jugador y el enemigo       
            angle = pathfinder.Direction() - transform.position;
        else Debug.Log(gameObject.name + "No tiene pathfinder");
        if (angle.x > 0) { sp.flipX = true; }
        else sp.flipX = false;
        //Comportamiento como enemigo a melé
        if (!kamikazing)
        {
            
            // Si está cerca del jugador y no está atacando, inicia la secuencia de ataque
            if (Vector2.Distance(player.transform.position, transform.position) <= attackrange && !attacking)
            {
                Attack();
            }

        }

    }
    private void FixedUpdate()
    {
        if (rb)
        {
            // mientras no está atacando, va hacia el jugador
            if (!kamikazing && !attacking && rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(acc*angle);
                anim.SetBool("isMoving",true);

            }
            //Comportamiento como kamikaze
            else if (dying && rb.velocity.magnitude < kMaxSpeed)
            {
                rb.AddForce(angle*kAcc);
            }
        }
        else
        {
            Debug.Log("Falta el Rigidbody de " + gameObject.name);
        }
    }

    //MÉTODOS DE COMPORTAMIENTO KAMIKAZE

    //Cambia al estado kamikaze e invoca la explosión por tiempo
    public void Kamikaze()
    {
        kamikazing = true;
        anim.SetBool("isKamikazing", true);
        Invoke("Die", dyingtime);
    }

    //Si está en kamikaze y colisiona con el jugador, explota
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (kamikazing && other.gameObject.tag == "Player")
        {
            kamikazing = false;
            Explode();
        }
    }

    //Explota, activa el trigger de daño y se destruye
    public void Explode()
    {
        GameObject newExplosion = Instantiate<GameObject>(explosionPrefab, transform.position, Quaternion.identity, bulletPool);
        Enemy en = GetComponent<Enemy>();
        if (en)
        {
            en.OnDead();
        }
        else { Debug.Log("El kamikaze no tiene componente enemigo"); }
        Destroy(this.gameObject);
    }

    //Devuelve el estado kamikaze (se utiliza en Health.cs)
    public bool Kamikazing()
    {
        return kamikazing;
    }

    public void Die()
    {
        dying = true;
        anim.SetBool("isDying", true);
        Invoke("Explode", explodingtime);
    }

    public bool CheckDie()
    {
        return dying;
    }




    //MÉTODOS DE COMPORTAMIENTO A MELÉ

    //Cambia la variable de ataque, y tras un pequeño lapso de tiempo comienza la animación de ataque, y tras ella, otro lapso de tiempo que precede al reseteo del movimiento
    public void Attack()
    {
        attacking = true;
        //aquí va la animación
        anim.SetBool("isAttacking",true);
        anim.SetBool("isMoving", false);
        Invoke("Damage", casttime);
        Invoke("Resetmove", attacktime);
    }

    // Si tras el "casteo" del ataque el jugador se encuentra en rango, es dañado
    public void Damage()
    {
        anim.SetBool("isAttacking", false);

        if (Vector2.Distance(player.transform.position, transform.position) <= attackrange)
        {
            GameManager.instance.ChangeHealth(-damage, player.gameObject);
        }
    }

    //Despúes de atacar, haya dañado o no al jugador, vuelve a moverse
    public void Resetmove()
    {
        attacking = false;
        //pasa a la animación de Idle
    }


}
