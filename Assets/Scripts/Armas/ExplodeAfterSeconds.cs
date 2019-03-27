using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeAfterSeconds : MonoBehaviour {

    public float time;
    AreaDamage areaDamage;
    private void Start()
    {
        areaDamage = GetComponent<AreaDamage>();
        Invoke("Explosion", time);
    }
    private void Explosion()
    {
        if (areaDamage)
        {
            areaDamage.PushArea();
            areaDamage.DealDamage();
            
        }
        Destroy(this.gameObject);
    }
}
