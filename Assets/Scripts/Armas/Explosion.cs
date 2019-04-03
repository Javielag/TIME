using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Explosion : MonoBehaviour
{
    AreaDamage areaDmg;

    // Use this for initialization
    void Start ()
    {
        //Utiliza el script de AreaDamage para hacer daño a todos los enemigos en el área de explosión y reproduce una animación
        areaDmg = GetComponent<AreaDamage>();      
        areaDmg.PushArea();
        areaDmg.DealDamage();
        //ReproduceAnimación
        Destroy(this.gameObject, 2);
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
