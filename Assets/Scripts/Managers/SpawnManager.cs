using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Spawner[] enemySpawners = new Spawner[7];

    public void Enemigos(int[] cants)
    {
        for (int i = 0; i < enemySpawners.Length; i++) 
        {
            enemySpawners[i].EmpiezaOleada(cants[i]);
        }
    }
    public void ShutDown()
    {
        foreach(Transform son in transform)
        {
            Spawner spawn = GetComponent<Spawner>();
            if (spawn)
            {
                spawn.ShutDown();
            }

        }
    }
}


    