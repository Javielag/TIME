using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Spawner[] enemySpawners = new Spawner[3];

    public void Enemigos(int[] cants)
    {
        for (int i = 0; i < enemySpawners.Length; i++) 
        {
            enemySpawners[i].EmpiezaOleada(cants[i]);
        }
    }
}


    