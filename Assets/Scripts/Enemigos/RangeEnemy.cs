using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{

    public int speed, rangeMax, rangeMin;
    GameObject player;
    [SerializeField]
    bool debugging = false, isMoving = false, canMove = true;
    public bool timer = true;
    float timeCount = 0;
    public Vector2 path;
    SpriteRenderer sp;
    private void Start()
    {
        player = GameManager.instance.GetPlayer();
        sp = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Vector2 pos = player.transform.position - transform.position;
        if (pos.x < 0) { sp.flipX = false; }
        else sp.flipX = true;
        path = this.GetComponent<Pathfinder>().Direction() - transform.position; //Pilla la dirección del pathfinder
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
                    transform.Translate(path.normalized * speed * Time.deltaTime); //Se mueve hacia el jugador  ESTO ES LO QUE HAY QUE CAMBIAR PARA EL REWORK
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
                    transform.Translate(-pos * speed * Time.deltaTime); //ESTO ES LO QUE HAY QUE CAMBIAR PARA EL REWORK
                    isMoving = true;
                }
            }
            else //Si está dentro del rango resetea el timer y se para
            {
                timer = true;
                isMoving = false;
            }
        }       
        if (debugging)
        {
            Debug.Log(isMoving);
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
