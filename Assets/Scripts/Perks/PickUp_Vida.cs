using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Vida : MonoBehaviour {
    public int healthGain;//cantidad de vida que se gana
    public bool isPercentage;//marcar si es porcentual, en caso contrario es absoluto
    // Use this for initialization
    public void Interacted()
    {
        
            Health playerHealth = GameManager.instance.GetPlayer().GetComponent<Health>();
            if (playerHealth)
            {
                int max = playerHealth.GetMaxHealth();
                if (!isPercentage)
                {
                    playerHealth.ChangeMaxHealth(max + healthGain);
                GameManager.instance.UpdateMaxHealth(max + healthGain);
                }
                else
                {
                    playerHealth.ChangeMaxHealth(max + Mathf.RoundToInt((max * (healthGain / 100f))));
                    GameManager.instance.UpdateMaxHealth(max + Mathf.RoundToInt((max * (healthGain / 100f))));
                }
                GameManager.instance.ChangeHealth(playerHealth.GetMaxHealth(), GameManager.instance.GetPlayer());
                GameManager.instance.UpdatePerk("Vida");
            }
        
    }
}
