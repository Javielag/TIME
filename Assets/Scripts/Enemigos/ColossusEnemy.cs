using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColossusEnemy : MonoBehaviour
{
    // variables públicas (velocidad, knockback) y privadas (ángulo de movimiento)
    Pathfinder pathfinder;
    public float acc, maxspeed, knockCoolDown;
    public int healwhenkilled;
    private bool smashed = false;
    GameObject player;
    SpriteRenderer sp;
    Rigidbody2D rb;
    AreaDamage AD;
    private Vector2 angle;

    void Start()
    {
        //inicialización de player
        player = GameManager.instance.GetPlayer();
        pathfinder = GetComponent<Pathfinder>();
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        AD = GetComponent<AreaDamage>();
        ColossusSmash();
    }

    void Update()
    {
        if (pathfinder)
        {
            //comparación de la posición del jugador y el enemigo       
            angle = pathfinder.Direction() - transform.position;
        }

        else
        {
            Debug.Log(gameObject.name + "No tiene pathfinder");
            angle = player.transform.position - transform.position;
        }
        if (angle.x < 0) { sp.flipX = true; }
        else sp.flipX = false;
    }
    private void FixedUpdate()
    {
        if (rb)
        {
            if (rb.velocity.magnitude <= maxspeed)
            {
                rb.AddForce(angle * acc);
            }
        }
    }

    public void ColossusSmash()
    {
        if (!smashed)
        {
            AD.DealDamage();
            AD.PushArea();
            smashed = true;
            Invoke("ResetSmash", knockCoolDown);
        }
    }

    public void ResetSmash()
    {
        smashed = false;
        ColossusSmash();
    }

    private void OnDestroy()
    {
        GameManager.instance.ChangeHealth(healwhenkilled, player.gameObject);
    }
}
