using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeAfterSeconds : MonoBehaviour {

    public float time;
    AreaDamage areaDamage;
    public GameObject explosion;
    Transform bulletPool;
    private void Start()
    {
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").transform;
        areaDamage = GetComponent<AreaDamage>();
        Invoke("Explosion", time);
    }
    private void Explosion()
    {
        if (areaDamage)
        {
            areaDamage.PushArea();
            areaDamage.DealDamage();
            
        }
        GameObject newExplosion = Instantiate<GameObject>(explosion, transform.position, Quaternion.identity, bulletPool);
        Destroy(this.gameObject);
    }
}
