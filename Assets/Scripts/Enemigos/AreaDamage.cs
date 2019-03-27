using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour {

    public float rad,knock;
    public int dmg;
    public LayerMask HitLayer;

    public void PushArea()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, rad);
        foreach (Collider2D col in hit)
        {
            //Debug.Log(col.name + " hit");
           
            PlayKnockback knockback = col.GetComponent<PlayKnockback>();
            if (knockback)
            {
                Vector2 dir = (col.transform.position - transform.position).normalized;
                //Debug.Log(col.name + " pushed for " + dir*knock);
                knockback.KnockThis(dir,knock);
            }
        }
    }
    public void DealDamage()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, rad,HitLayer);
        foreach (Collider2D col in cols )
        {
            Debug.Log(col.name + "hit");
            GameManager.instance.ChangeHealth(-dmg, col.gameObject);
        }
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //dibuja el area de interaccion
        Gizmos.DrawWireSphere(transform.position, rad);
    }
}
