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
            Destroy(this.gameObject);
        }
        else if (health > maxHealth) health = maxHealth;
    }

    public void ChangeMaxHealth(int newMax)
    {
        maxHealth = newMax;
    }
    public int GetMaxHealth() { return maxHealth; }

}
