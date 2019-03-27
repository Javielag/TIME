using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinder : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject WaypointManager;
    [SerializeField] Waypoint[] waypoints;
    Transform player;   
    public float speed = 0.5f;
    int sala = -1, wp = -1;

    // Use this for initialization
    void Start()
    {
        waypoints = new Waypoint[WaypointManager.transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = WaypointManager.transform.GetChild(i).GetComponent<Waypoint>();
        }
        player = GameManager.instance.GetPlayer().transform;
        wp = CloseWaypoint(this.transform);
        this.sala = waypoints[wp].sala;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, player.position - this.transform.position);
        if (hit.collider.gameObject.transform != player) //Si no ve al jugador
        {
            wp = CloseWaypoint(this.transform);
            if (Vector3.Distance(this.transform.position, waypoints[wp].transform.position) > 10)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, waypoints[wp].transform.position, speed); //Va al waypoint más cercano
            }
            else //De ahí va a la sala en la que está el player pasando por los waypoints
            {
                this.sala = waypoints[wp].sala; //Coge la sala del waypoint más cercano
                int playerSala = waypoints[CloseWaypoint(player)].sala; //Coge la sala del jugador
                if (playerSala % 3 > this.sala % 3) //Si está a la derecha
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, waypoints[wp].closeWaypoints[1].position, speed);
                }
                else if (playerSala % 3 < this.sala % 3) //Si está a la izquierda
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, waypoints[wp].closeWaypoints[3].position, speed);
                }
                else if (playerSala / 3 > this.sala / 3) //Si está abajo
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, waypoints[wp].closeWaypoints[2].position, speed);
                }
                else if (playerSala / 3 < this.sala / 3) //Si está arriba
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, waypoints[wp].closeWaypoints[0].position, speed);
                }
            }
        }
        else 
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed);
        }
    }
        
    int CloseWaypoint(Transform transform)
    {
        float distCercano = Vector2.Distance(transform.position, waypoints[0].transform.position);
        int wpCercano = 0;
        for (int i = 1; i < waypoints.Length; i++)
        {
            float distActual = Vector2.Distance(transform.position, waypoints[i].transform.position);
            if (distCercano > distActual)
            {
                wpCercano = i;
            }
        }
        return wpCercano;
    }
    
}
