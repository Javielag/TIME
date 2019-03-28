using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{

    public int speed, rangeMax, rangeMin;
    GameObject player;
    [SerializeField]bool debugging = false;
    bool isMoving = false, canMove = true;
    public bool timer = true;
    float a = 0;
    private void Start()
    {
        player = GameManager.instance.GetPlayer();
    }
    void Update()
    {
        Vector2 pos = player.transform.position - transform.position;

        pos = pos.normalized;
        if (canMove)
        {
            if (Vector2.Distance(player.transform.position, transform.position) >= rangeMax)
            {
                if (timer)
                {
                    a = Time.time;
                    timer = false;
                }
                if (Time.time - a >= 2)
                {
                    transform.Translate(pos * speed * Time.deltaTime);
                    isMoving = true;
                }
            }
            else if (Vector2.Distance(player.transform.position, transform.position) <= rangeMin)
            {
                if (timer)
                {
                    a = Time.time;
                    timer = false;
                }
                if (Time.time - a >= 0.5)
                {
                    transform.Translate(-pos * speed * Time.deltaTime);
                    isMoving = true;
                }
            }
            else
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
