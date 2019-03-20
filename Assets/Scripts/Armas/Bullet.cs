using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //se mueve con velocidad  constante hacia su derecha 
        rb.velocity = speed * transform.right;
    }

    void FixedUpdate()
    {

    }
    //al ser llamado, cambia la derecha de la bala para que esta coincida con su sprite
    public void PointAt(Vector2 newDirection,float spread)
    {
        transform.right = newDirection;
        transform.Rotate(Vector3.forward*spread);
        rb.velocity = speed * transform.right;
    }
    public void ChangeSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    public float GetSpeed()
    {
        return speed;
    }




}
