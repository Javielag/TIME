using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour {

    public float cd,knockBack;
    public int dmg;
    private float nextMel;
    IEnumerator weaponSwitch;
    //Pega en un rectángulo configurable desde el editor
    public Transform attackPoint,endPoint;
    // Use this for initialization
	void Start () {
        nextMel = Time.time;
    }
	void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(attackPoint.position, endPoint.position);   
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1) && Time.time > nextMel && !GameManager.instance.player.GetComponentInChildren<WeaponManager>().isSwitching)
        {
            Debug.Log("SAITAMA");
            GameManager.instance.player.GetComponentInChildren<WeaponManager>().CancelReload();
            MeleeAttack();
        }
	}
    void MeleeAttack()
    {
        Collider2D[] attack = Physics2D.OverlapAreaAll(attackPoint.position, endPoint.position);
        nextMel = Time.time + cd;

        foreach (Collider2D hit in attack)
        {
            Enemy en = hit.GetComponent<Enemy>();
            if (en)
            {
                if (en.barrera) //Al golpear con el melee se elimina la barrera -> barrera se vuelve false
                {
                    en.barrera = false;
                    en.DestroyPompa();
                }

                if (!en.invbarrera) //Hace daño solo si la barrera inversa es false
                {
                    Health hp = hit.GetComponent<Health>();
                    if (hp)
                    {
                        hp.ChangeHealth(-dmg);
                    }
                    PlayKnockback knb = hit.GetComponent<PlayKnockback>();
                    if (knb)
                    {
                        knb.KnockThis(transform.right, knockBack);
                    }
                }
            }


        }
    }
    

}
