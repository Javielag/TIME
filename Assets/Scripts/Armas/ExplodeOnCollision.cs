using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnCollision : MonoBehaviour {

    AreaDamage areaDmg;
    private void Start()
    {
        areaDmg = GetComponent<AreaDamage>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (areaDmg)
        {
            areaDmg.PushArea();
            areaDmg.DealDamage();
        }
        Destroy(this.gameObject);
    }
}
