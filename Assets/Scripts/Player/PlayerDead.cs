using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour {

	public void OnDead()
    {
        GameManager.instance.SaveScore();
        GameManager.instance.SaveVolume();
        GameManager.instance.ChangeScene("Menu");
    }
}
