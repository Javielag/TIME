using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour {
    GameObject player;
    public string GameScene = "CheatsGuerrilla";
    private void Start()
    {
        player = GameManager.instance.GetPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            SceneManager.LoadScene(GameScene);
        }
    }
}
