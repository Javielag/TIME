using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

    // Use this for initialization
    public float bulletsPerSecond = 2;
    public Bullet enemyBullet;
    Vector2 direction;
    Transform bulletPool;
    public Animator anim;

    void Start () {
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").transform;
        InvokeRepeating("Shoot", 0, 1 / bulletsPerSecond);
        LookAt aim = GetComponent<LookAt>();
        if (aim){
            aim.SetTarget(GameManager.instance.GetPlayer().transform);
        }
    }
	
    void Shoot ()
    {
        if (this.gameObject.GetComponentInParent<RangeEnemy>().timer)
        {
            Bullet newBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity, bulletPool);
            newBullet.PointAt(transform.right, 0);
        }
        
    }
}

