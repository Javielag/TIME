using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorOleadas : MonoBehaviour { 

    public SpawnManager spawnManager;
    private void Start()
    {
        GameManager.instance.SetGeneraOleadas(this);
        GameManager.instance.SetOleada(0);
        GameManager.instance.GeneraOleada();
    }
    public void GeneraOleada(int ol, out int enemyCount)
    {
        int a, b; //Variables auxiliares para números aleatorios
        int[] cants = new int[9];
        enemyCount = 0;
        //ol = 1;
        cants[0] = (int)Mathf.Pow(1.2f, ol) + 4;
        if (ol >= 5)
        {
            cants[1] = (int)(Mathf.Pow(1.2f, ol) + 4) / 2;
            if (ol >= 10)
            {
                a = Random.Range(2, cants.Length);
                cants[a] = (int)(Mathf.Pow(1.2f, ol) + 4) / 8;
                do
                    b = Random.Range(2, cants.Length);
                while (b == a);
                cants[b] = (int)(Mathf.Pow(1.2f, ol) + 4) / 8;
            }
                
        }
        else
        {
            cants[1] = 0;
            cants[2] = 0;
        }

        if (ol % 3 == 2)
            cants[3] = (ol + 1) / 6;
        else cants[3] = 0;

        for (int i = 0; i<cants.Length;i++)
        {
            enemyCount += cants[i];
        }
        spawnManager.Enemigos(cants);
    }
    public void StopOleadas()
    {
        spawnManager.ShutDown();
    }

}
