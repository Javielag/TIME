using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnCollision : MonoBehaviour {

    AreaDamage areaDmg;
    public GameObject explosion;
    Transform bulletPool;
    private void Start()
    {
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").transform;
        areaDmg = GetComponent<AreaDamage>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (areaDmg)
        {
            areaDmg.PushArea();
            areaDmg.DealDamage();
        }
        GameObject newExplosion = Instantiate<GameObject>(explosion, transform.position, Quaternion.identity, bulletPool);
        Destroy(this.gameObject);
    }
}
