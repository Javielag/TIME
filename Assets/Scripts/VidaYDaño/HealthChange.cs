using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChange : MonoBehaviour
{
    public int damage = 10;
    bool makeDamage = true;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si la variable de Enemy barrera es true, makedamage es false -> no deja hacer daño con los proyectiles y viceversa
        if (collision.GetComponent<Barreras>().barrera)
        {
            makeDamage = false;
        }
        else
        {
            makeDamage = true;
        }


        if (makeDamage == true)
        {
            GameManager.instance.ChangeHealth(-damage, collision.gameObject); //Aquí van cosas de enemigos, de momento hacen daño por contacto
            if (gameObject.tag == "Projectile")
            {
                collision.GetComponent<Barreras>().invbarrera = false;  //Cuando hace daño la var de Enemy barrera inversa que convierte en false
                collision.GetComponent<Barreras>().DestroyPompa();
                Destroy(this.gameObject);
            }
        }
    }
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}
