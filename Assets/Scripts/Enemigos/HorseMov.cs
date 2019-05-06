using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseMov : MonoBehaviour
{

    public float idle, jump, turn, acc,maxSpeed;
    Transform player;
    Rigidbody2D rb;
    Vector2 dir,jumpDir,pointA,pointB,jumpStart;
    bool secJump = false,jumping = false;
    private float nextJump;
    AreaDamage areaDmg;
    public Animator anim;

    // Use this for initialization
    void Start()
    {
        player = GameManager.instance.GetPlayer().transform;
        nextJump = Time.time;
        areaDmg = GetComponent<AreaDamage>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!jumping)
        {
            CalculateJumps();
        }
        //else if(jumping && !secJump)
        //{
        //    FirstJump();
        //}
        //else if(jumping && secJump)
        //{
        //    SecondJump();
        //}
        Debug.DrawLine(jumpStart, pointA, Color.red);
        Debug.DrawLine(pointA,pointB,Color.yellow);
    }
    private void FixedUpdate()
    {
        if (jumping && !secJump)
        {
            FirstJump();           
        }
        else if (jumping && secJump)
        {            
            SecondJump();           
        }
    }
    private void CalculateJumps()
    {
        if (Time.time > nextJump)
        {
            //guarda la posicion inicial del salto
            jumpStart = transform.position;
            //calculamos la direccion normalizada a "casillas" (2,1)(1,-2)...
            dir = player.position - transform.position;
            dir = normalizeJump(dir);
            if (dir.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if (Random.Range(0, 2) > 1)
            {
                pointA = new Vector2(transform.position.x, transform.position.y + (dir.y * jump));
                jumpDir = new Vector2(0, 1 * Mathf.Sign(dir.y));
            }
            else
            {
                pointA = new Vector2(transform.position.x + (dir.x * jump), transform.position.y);
                jumpDir = new Vector2(1 * Mathf.Sign(dir.x), 0);
            }
            pointB = new Vector2(transform.position.x + (dir.x * jump), transform.position.y + (dir.y * jump));
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            //Debug.Log("Horsen't");
            jumping = true;
            //nextJump = Time.time + idle;
        }
        
    }
    private void FirstJump()
    {
        anim.SetBool("Jumping", true);
        if (Time.time > nextJump)
        {
            

            if (rb.velocity.magnitude < maxSpeed)
                rb.AddForce(jumpDir * acc);
            //al acabar el primer salto 3
            if (Vector2.Distance(jumpStart, pointA) < Vector2.Distance(jumpStart, transform.position))
            {
                if (jumpDir.x == 0)
                    jumpDir = new Vector2(1*Mathf.Sign(dir.x), 0);
                else
                    jumpDir = new Vector2(0,1*Mathf.Sign(dir.y));
                secJump = true;
                rb.velocity = Vector2.zero;
                nextJump = Time.time + turn;
                
            }           
        }
        
    }

    private void SecondJump()
    {
        anim.SetBool("Falling", true);
        if (Time.time > nextJump)
        {
            
            if (rb.velocity.magnitude < maxSpeed)
                rb.AddForce(jumpDir * acc);
            //al acabar el segundo salto
            if (Vector2.Distance(jumpStart, pointB) < Vector2.Distance(jumpStart, transform.position))
            {
                Attack();
                secJump = false;
                jumping = false;
                rb.velocity = Vector2.zero;
                this.gameObject.GetComponent<Collider2D>().enabled = true;
                //Debug.Log("Horse");
                nextJump = Time.time + idle;

                anim.SetBool("Falling", false);
                anim.SetBool("Jumping", false);                
            }
        }
        

    }
    private void Attack()
    {
        if (areaDmg)
        {
            areaDmg.DealDamage();
            areaDmg.PushArea();
        }
        else
        {
            Debug.Log("Te falta el componente de daño, melón");
        }
    }
    //devuelve el vector en formato 2,1 como casillas de ajedrez
    private Vector2 normalizeJump(Vector2 v)
    {
        float signX = Mathf.Sign(v.x);
        float signY = Mathf.Sign(v.y);
        if (v.x < v.y)
            return (new Vector2(2 * signX, 1 * signY));
        else
            return (new Vector2(1 * signX, 2 * signY));
    }


}