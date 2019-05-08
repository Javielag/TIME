using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{

    public int acc, speedMax, rangeMax, rangeMin;
    GameObject player;
    [SerializeField]
    bool debugging = false, isMoving = false, canMove = true;
    public bool timer = true;
    float timeCount = 0;
    public Vector2 path;
    SpriteRenderer sp;
    private Pathfinder find;
    Rigidbody2D rb;

    private void Start()
    {
        player = GameManager.instance.GetPlayer();
        sp = GetComponent<SpriteRenderer>();
        find = GetComponent<Pathfinder>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Vector2 pos = player.transform.position - transform.position;
        if (pos.x < 0) { sp.flipX = false; }
        else sp.flipX = true;
        path = find.Direction() - transform.position; //Pilla la dirección del pathfinder
        pos = pos.normalized;
        if (canMove)
        {
            if (Vector2.Distance(player.transform.position, transform.position) >= rangeMax) //Si está fuera de rango
            {
                if (timer) //Empieza a correr un contador
                {
                    timeCount = Time.time;
                    timer = false;
                }
                if (Time.time - timeCount >= 2)//Si ese contador dura 2 segundos
                {
                    acc = Mathf.Abs(acc);
                    isMoving = true;
                }
            }
            else if (Vector2.Distance(player.transform.position, transform.position) <= rangeMin) //Igual que arriba
            {
                if (timer)
                {
                    timeCount = Time.time;
                    timer = false;
                }
                if (Time.time - timeCount >= 0.5)
                {
                    acc = -Mathf.Abs(acc);
                    isMoving = true;
                }
            }
            else //Si está dentro del rango resetea el timer y se para
            {
                rb.velocity = Vector2.zero;
                timer = true;
                isMoving = false;
            }
        }       
        if (debugging)
        {
            Debug.Log(isMoving);
        }

    }
    private void FixedUpdate()
    {
        if (isMoving && rb.velocity.magnitude < speedMax)
        {
            rb.AddForce(acc*path);
        }
    }
    public bool Moving()
    {
        return isMoving;
    }
    public void SetCanMove(bool state)
    {
        canMove = state;
    }
}
