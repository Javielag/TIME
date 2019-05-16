using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheater : MonoBehaviour {

    public KeyCode skipKey,totKey, dorifKey, inmKey;
    public bool yeetThroughOleadas, Totalitarismo, Dorifto, inmatable;
    public float newSpeedMax, newAccelMax;
    private GameObject totalidad;
    public GameObject totalidadPrefab;
    // Update is called once per frame
    void Update () {
        if (yeetThroughOleadas && Input.GetKeyDown(skipKey))
        {
            GameManager.instance.SaltaOleada();

        }
        if(Totalitarismo && Input.GetKeyDown(totKey))
        {
            //se asegura de que sea el unico conjunto de armas
            if (totalidad != null)
                Destroy(totalidad);
            totalidad = Instantiate<GameObject>(totalidadPrefab,GameManager.instance.GetPlayer().transform.position,Quaternion.identity);

        }
        if(Dorifto && Input.GetKeyDown(dorifKey))
        {
            PlayerController mov = GameManager.instance.GetPlayer().GetComponent<PlayerController>();
            mov.CheatSpeed(newSpeedMax, newAccelMax);
            Dorifto = false;
        }
        if(inmatable && Input.GetKeyDown(inmKey))
        {
            Health h = GetComponent<Health>();
            h.CheatInvencible();
            inmatable = false;
        }
    }
}
