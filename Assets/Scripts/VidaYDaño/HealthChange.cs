using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChange : MonoBehaviour
{
    public int damage = 10;
    bool makeDamage = true;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (makeDamage == true)
        {
            GameManager.instance.ChangeHealth(-damage, collision.gameObject); //Aquí van cosas de enemigos, de momento hacen daño por contacto
            if(gameObject.tag == "Projectile")
            {
                Destroy(this.gameObject);
            }
        }         
    }
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}
