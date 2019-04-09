using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    public int health;
    // Use this for initialization
    void Start()
    {
        health = maxHealth;
    }

    public void ChangeHealth(int change)
    {
        health = health + change;
        if (health <= 0)
        {
            health = 0;

            //ENEMIGO KAMIKAZE
            //Si Health registra el compoonente kamikaze se ejecuta el siguiente código
            if (this.GetComponent<KamikazeEnemy>())
            {
                //Kamikazing() devuelve un booleano que indica su estado (enemigo normal/ kamikaze)
                if (!this.GetComponent<KamikazeEnemy>().Kamikazing())
                {
                    //Si devuelve false (está en modo normal) ejecuta el método kamikaze, que induce al enemigo en su estado de kamikaze y le otorga una pequeña cantidad de vida
                    this.GetComponent<KamikazeEnemy>().Kamikaze();
                    health = 50;
                }
                //Si devuelve true, ya está en estado kamikaze, y ñe hace explotar
                else
                {
                    this.GetComponent<KamikazeEnemy>().Explode();
                }
            }
            else
            {
                PlayerDead die = GetComponent<PlayerDead>();
                if (die)
                    die.OnDead();
                Destroy(this.gameObject);
            }
                    
        }
        else if (health > maxHealth) health = maxHealth;
    }

    public void ChangeMaxHealth(int newMax)
    {
        maxHealth = newMax;
    }
    public int GetMaxHealth() { return maxHealth; }

}
