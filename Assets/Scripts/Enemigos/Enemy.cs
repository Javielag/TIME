using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public bool barrera, invbarrera;
    public GameObject pompa;

    public void DestroyPompa()
    {
        Destroy(pompa);
    }

    public void OnDestroy()
    {
        GameManager.instance.EnemySlain();
    }
}
