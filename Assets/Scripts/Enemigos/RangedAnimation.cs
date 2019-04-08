using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAnimation : MonoBehaviour {
    RangeEnemy enemy;
    Animator animator;
	// Use this for initialization
	void Start () {
        enemy = this.GetComponent<RangeEnemy>();
        animator = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        //if (!enemy.timer)
        //{
        //    if (enemy.isMoving) animator.SetInteger("State", 1);
        //    else animator.SetInteger("State", 0);
        //}
        //else
        //{
        //    animator.SetInteger("State", 2);
        //}

        if (enemy.timer)
        {
            animator.SetInteger("State", 2);
        }
        else if (enemy.isMoving)
        {
            animator.SetInteger("State", 1);
        }
        else
        {
            animator.SetInteger("State", 0);
        }
    }
}
