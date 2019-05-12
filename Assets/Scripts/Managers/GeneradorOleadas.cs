using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorOleadas : MonoBehaviour { 

    public SpawnManager spawnManager;
    public bool TESTING;
    private void Start()
    {
        GameManager.instance.SetGeneraOleadas(this);
        GameManager.instance.SetOleada(0);
        GameManager.instance.GeneraOleada();
    }
    public void GeneraOleada(int ol, out int enemyCount)
    {
       
        int[] cants = new int[9]; 
        enemyCount = 0;
        if (!TESTING)
        {
            int a, b; //Variables auxiliares para números aleatorios
            cants = new int[9]; //mel,ranged,caballo,portal,barrera,invbarrera,mago,kamikaze,worm
            enemyCount = 0;
            //ol = 1;
            cants[0] = (int)Mathf.Pow(1.2f, ol) + 4;
            if (ol >= 5)
            {
                cants[1] = (int)(Mathf.Pow(1.2f, ol) + 4) / 2;  //A partir de la oleada 5 genera enemigos a rango
                if (ol >= 10)   //A partir de la oleada 10 genera enemigos élite
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

            if (ol % 3 == 2) //Genera portales cada 3 oleadas
                cants[3] = (ol + 1) / 6;
            else cants[3] = 0;

            for (int i = 0; i < cants.Length; i++)
            {
                enemyCount += cants[i];
            }

            int x = 0;
            Debug.Log("OLEADA " + ol);
            for (int i = 0; i < cants.Length; i++)
            {
                Debug.Log(i + " " + cants[i]);
            }
        }
        else
        {
            //cada oleada tendrá
            //5 melées 5 enemigos a rango
            //y 5 elites
            cants[0] = 5;
            cants[1] = 5;
            cants[ol+1] = 5;
            for(int i = 0; i < cants.Length; i++) { enemyCount += cants[i]; }
        }
        spawnManager.Enemigos(cants);
    }
    public void AcabaOleada()
    {
        spawnManager.ShutDown();
    }

}
