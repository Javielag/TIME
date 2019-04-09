using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public int maxEnemies, minEnemies;
    public float delay,distance;
    private int remaining = 0;
    public bool portal = false;
    Transform enemyPool;
    private void Start()
    {
        enemyPool = GameObject.FindGameObjectWithTag("EnemyPool").transform;
    }
    //asigna una cantidad de enemigos que debe generar y empeiza
    public void EmpiezaOleada(int cant)
    {
        
        remaining = cant;
        //GeneraEnemigos();
        Invoke("GeneraEnemigos",0);
    }
    //si quedan enemigos por generar, los spawnea
    public void GeneraEnemigos()
    {
        
       if(remaining > 0)
       {
            Invoke("Spawn",0);
       }
       else if (portal)
       {
            Portal portal = GetComponent<Portal>();
            if (portal)
            {
                portal.PortalSpawn();
            }
       }
    }
   private void Spawn()
   {
        int enemies = Random.Range(minEnemies,maxEnemies);
        bool[] available = new bool[transform.childCount];
        Vector2 playerPos = GameManager.instance.GetPlayer().transform.position;
        int maxSpawns=0;
        for(int j = 0; j < available.Length; j++)
        {
            Vector2 pos = transform.GetChild(j).transform.position;
            if (Vector2.Distance(playerPos, pos) > distance)
            {
                available[j] = true;
                maxSpawns++;
            }
                
            else
                available[j] = false;
        }
        int i = 0;
        while (i < enemies && i < maxSpawns && remaining > 0) 
        {
            int index;
            
            do
            {
                index = Random.Range(0, transform.childCount);  
                
            } while (!available[index]);

            Vector2 pos = transform.GetChild(index).transform.position;



           Instantiate<GameObject>(enemyPrefab,pos,Quaternion.identity,enemyPool);
           remaining--;
           if (portal)
           {
                GameManager.instance.XtraEnemies(1);
                //Debug.Log("Generado " + enemyPrefab.name + " a " + Vector2.Distance(pos, GameManager.instance.GetPlayer().transform.position));
           }
           i++;

           
        }
        //vuelve a comprobar si ha acabado
        Invoke("GeneraEnemigos", delay);
    }
    public void SetSpawner(GameObject newSpawn)
    {
        enemyPrefab = newSpawn;
    }
    public void ShutDown()
    {
        StopAllCoroutines();
        remaining = 0;
    }
    
	
}
