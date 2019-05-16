using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    public int sizeMin,sizeMax;
    public int healthRecover = 15;
    public GameObject[] possibleEnemies = new GameObject[3];
    Spawner portalSpawner;     
    string pos;

    private void Start()
    {
        portalSpawner = GetComponent<Spawner>();
        PortalSpawn();
        Posicion();
        Debug.Log(pos);
        GameManager.instance.AvisoPortal(pos);
    }
    public void PortalSpawn()
    {
        Spawner portalSpawner = GetComponent<Spawner>();
        int cant;
        if (portalSpawner)
        {
            portalSpawner.SetSpawner(possibleEnemies[Random.Range(0, possibleEnemies.Length)]);
            cant = Random.Range(sizeMin, sizeMax);
            portalSpawner.EmpiezaOleada(cant);
        }
    }
    private void OnDestroy()
    {
        PointAtPortal[] arrows = new PointAtPortal[GameManager.instance.GetPlayer().GetComponentsInChildren<PointAtPortal>().Length];
        int i = 0;
        while (arrows[i] != null && i < arrows.Length && arrows[i].Target != this) i++;
        Destroy(arrows[i]);
        GameManager.instance.enemyCount--;
        GameManager.instance.ChangeHealth(healthRecover,GameManager.instance.GetPlayer());
    }

    public string Posicion()
    {
        Vector2 portalpos = gameObject.transform.position;
        Vector2 apos = GameManager.instance.GetA().transform.position;
        Vector2 bpos = GameManager.instance.GetB().transform.position;
        Vector2 cpos = GameManager.instance.GetC().transform.position;
        Vector2 dpos = GameManager.instance.GetD().transform.position;

        if (portalpos.y > bpos.y)
        {
            if (portalpos.y > apos.y) pos = " arriba ";
            else pos = "l centro ";            
        }
        else pos = " abajo ";
        
        if (portalpos.x < dpos.x)
        {
            if (portalpos.x < cpos.x) pos = pos + "a la izquierda";
            else if (pos != "l centro ")
                pos = pos + "en el medio";
            
        }
        else pos = pos + "a la derecha";
        
        return pos;
    }


}
