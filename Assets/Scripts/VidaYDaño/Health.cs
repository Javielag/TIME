using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    public int health;
    [SerializeField]
    private bool invencible;
    public float fbTime, fbRate;
    public Color fbColor;
    // Use this for initialization
    void Start()
    {
        health = maxHealth;
        invencible = false;
    }

    public void ChangeHealth(int change)
    {
        if (!invencible)
        {
            if (health + change < health)
            {
                CancelInvoke("DamageFeedback");
                InvokeRepeating("DamageFeedback", 0, fbRate);
                Invoke("StopFeedback", fbTime);
            }
            health = health + change;
        }        
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
    void DamageFeedback()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr.color != fbColor)
        {
            sr.color = fbColor;
        }
        else sr.color = new Vector4(255, 255, 255, 255);
    }
    void StopFeedback()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        CancelInvoke("DamageFeedback");
        sr.color = new Vector4(255, 255, 255, 255);
    }
    public void CheatInvencible()
    {
        Debug.Log("yeeeeeeeeee");
        invencible = true;
    }
}
