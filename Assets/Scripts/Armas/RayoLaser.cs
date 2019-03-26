using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayoLaser : MonoBehaviour {

    public int dmg;
    public float ancho,knockForce;
    Ray2D limit, laser;
    ContactFilter2D myFilter;
    //layer 17 paredes layer 18 enemigos
    public LayerMask enemyLayer,wallLayer;
    Vector2 ctr, size,end;
    public void Start()
    {
        end = FindEnd();
        ctr = new Vector2(transform.position.x + (end.x-transform.position.x) / 2,transform.position.y + (end.y - transform.position.y) / 2);
        size = new Vector2(Vector2.Distance(end, transform.position) / 2, ancho);
        Shoot(FindEnd());
    }
    private Vector2 FindEnd()
    {
        
        //float distance = 100f;
        Vector2 end = transform.right*100;
        int mask = 1 << wallLayer.value;
        RaycastHit2D leHit = Physics2D.Raycast(transform.position,transform.right,mask);       

        if (leHit)
        {  
           // distance = Vector2.Distance(leHit.point,transform.position);
            end = leHit.point;
        }
        Debug.DrawLine(transform.position,end,Color.cyan,2);
        return end;
        
    }
    private void Shoot(Vector2 end)
    {
              
        Collider2D[] lesHits = Physics2D.OverlapBoxAll(ctr, size,0);
        
        print("hit enemies: " + lesHits.Length);
        foreach (Collider2D hit in lesHits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Health hp = hit.GetComponent<Health>();
                if (hp)
                {
                    hp.ChangeHealth(-dmg);
                }
                PlayKnockback knock = hit.GetComponent<PlayKnockback>();
                if (knock)
                {
                    knock.KnockThis(transform.right, knockForce);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        //Gizmos.DrawWireSphere(ctr,5);
        Gizmos.DrawCube(ctr, size);
    }
    //https://answers.unity.com/questions/416919/making-raycast-ignore-multiple-layers.html

}
