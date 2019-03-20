using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    public int sizeMin,sizeMax;
    public int healthRecover = 15;
    public GameObject[] possibleEnemies = new GameObject[3];
    Spawner portalSpawner;

    private void Start()
    {
        portalSpawner = GetComponent<Spawner>();
        PortalSpawn();
    }
    public void PortalSpawn()
    {
        Spawner portalSpawner = GetComponent<Spawner>();
        if (portalSpawner)
        {
            portalSpawner.SetSpawner(possibleEnemies[Random.Range(0, possibleEnemies.Length)]);
            portalSpawner.EmpiezaOleada(Random.Range(sizeMin,sizeMax));
        }
    }
    private void OnDestroy()
    {
        GameManager.instance.ChangeHealth(healthRecover,GameManager.instance.GetPlayer());
    }
}
