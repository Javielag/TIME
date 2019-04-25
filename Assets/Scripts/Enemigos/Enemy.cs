using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public bool barrera, invbarrera;
    public GameObject pompa;

    public void DestroyPompa()  // Este método se llama desde PlayerMelee y HealtChange
    {
        Destroy(pompa);
    }

    public void OnDead()
    {
        GameManager.instance.EnemySlain();
        Destroy(this.gameObject);
    }
}
