using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Speed : MonoBehaviour
{
    public string description;
    public float speedBonus;
    public bool isPorcentual;
    public void Interacted()
    {
        PlayerController mov = GameManager.instance.GetPlayer().GetComponent<PlayerController>();
        if (mov)
        {
            if (!isPorcentual)
            {
                mov.setXtraSpeed(mov.speedMax + speedBonus);
            }
            else
                mov.setXtraSpeed(mov.speedMax + mov.speedMax * (speedBonus / 100f));
            GameManager.instance.UpdatePerk("Velocidad");
            GameManager.instance.Description(description);
        }
    }
}
