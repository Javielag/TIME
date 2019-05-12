using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cientifico : MonoBehaviour {

    public int curacion;
    private void OnDestroy()
    {
        GameManager.instance.GeneraOleada();
        GameManager.instance.ChangeHealth(curacion, GameManager.instance.GetPlayer());
    }
    public void Ofrece(GameObject esto)
    {
        Instantiate(esto, transform.GetChild(0).position, Quaternion.identity, transform);
    }
    public void Ofrece2(WeaponPickup esto)
    {
        Instantiate(esto, transform.GetChild(1).position, Quaternion.identity, transform);
    }
}
