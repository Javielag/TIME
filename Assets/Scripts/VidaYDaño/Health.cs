using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    public int health;
    private bool invencible;
    // Use this for initialization
    void Start()
    {
        health = maxHealth;
        invencible = false;

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
                if (!this.GetComponent<KamikazeEnemy>().Kamikazing() && !invencible)
                {
                    //Si devuelve false (está en modo normal) ejecuta el método kamikaze, que induce al enemigo en su estado de kamikaze y le otorga una pequeña cantidad de vida
                    this.GetComponent<KamikazeEnemy>().Kamikaze();
                    invencible = true;                    
                    health = 50;
                    Invoke("Invencible", 2);
                }
                //Si devuelve true, ya está en estado kamikaze, y ñe hace explotar
                else if (this.GetComponent<KamikazeEnemy>().CheckDie())
                {
                    this.GetComponent<KamikazeEnemy>().Explode();
                }
            }
            else
            {
                PlayerDead die = GetComponent<PlayerDead>();
                if (die)
                    die.OnDead();
                Enemy en = GetComponent<Enemy>();
                if (en) en.OnDead();
            }
                    
        }
        else if (health > maxHealth) health = maxHealth;
    }

    public void ChangeMaxHealth(int newMax)
    {
        maxHealth = newMax;
    }
    public int GetMaxHealth() { return maxHealth; }

    public void Invencible()
    {
        invencible=false;
    }
}
