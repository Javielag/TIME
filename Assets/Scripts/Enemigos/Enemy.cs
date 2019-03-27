using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public bool barrera, invbarrera;

    public void OnDestroy()
    {
        GameManager.instance.EnemySlain();
    }
}
