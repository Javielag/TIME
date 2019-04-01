using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinder : MonoBehaviour
{
    GameObject WaypointManager;
    Waypoint[] waypoints;
    Transform player;   
    int sala = -1, wp = -1;
    bool isInRoute = false;
    int playerSala;
    Vector3 dir;
    private void Awake()
    {
        player = GameManager.instance.GetPlayer().transform;
        WaypointManager = GameObject.Find("WaypointManager");
        waypoints = new Waypoint[WaypointManager.transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = WaypointManager.transform.GetChild(i).GetComponent<Waypoint>();
        }
        wp = CloseWaypoint(this.transform);
        this.sala = waypoints[wp].sala;
    }
    // Use this for initialization
    void Start()
    {
        //WaypointManager = GameObject.Find("WaypointManager");
        //waypoints = new Waypoint[WaypointManager.transform.childCount];
        //for (int i = 0; i < waypoints.Length; i++)
        //{
        //    waypoints[i] = WaypointManager.transform.GetChild(i).GetComponent<Waypoint>();
        //}
        ////player = GameManager.instance.GetPlayer().transform;
        //wp = CloseWaypoint(this.transform);
        //this.sala = waypoints[wp].sala;
    }

    public Vector3 Direction()
    {
        RaycastHit2D hit;
        int casted = 0;
        do
        {
            casted++;
            if (player == null)
                Debug.Log("Noplayer");
            if (transform == null) Debug.Log("NoTransform");
            hit = Physics2D.Raycast(transform.position, player.position - transform.position, 1000, 1 << 12 | 1 << 13 | 1 << 16);
        } while (!hit && casted < 100);
        if(casted >= 100) { Debug.Log("ROTTTTO"); }
        if (hit.collider.gameObject.transform != player) //Si no ve al jugador
        {
            wp = CloseWaypoint(transform);
            if (Vector3.Distance(transform.position, waypoints[wp].transform.position) > 2 && !isInRoute)
            {
                return waypoints[wp].transform.position; //Va al waypoint más cercano
            }
            else //De ahí va a la sala en la que está el player pasando por los waypoints
            {
                isInRoute = true;
                if (Vector3.Distance(transform.position, waypoints[wp].transform.position) < 3)
                {
                    this.sala = waypoints[wp].sala; //Coge la sala del waypoint más cercano
                    playerSala = waypoints[CloseWaypoint(player)].sala; //Coge la sala del jugador

                }
                if (playerSala % 3 > sala % 3) //Si está a la derecha
                {
                    return waypoints[wp].closeWaypoints[1].position;
                }
                else if (playerSala % 3 < sala % 3) //Si está a la izquierda
                {
                    return waypoints[wp].closeWaypoints[3].position;
                }
                else if (playerSala / 3 > sala / 3) //Si está abajo
                {
                    return waypoints[wp].closeWaypoints[2].position;
                }
                else if (playerSala / 3 < sala / 3) //Si está arriba
                {
                    return waypoints[wp].closeWaypoints[0].position;
                }
                else return Vector2.zero;
            }
        }
        else
        {
            sala = waypoints[wp].sala; //Coge la sala del waypoint más cercano
            playerSala = waypoints[CloseWaypoint(player)].sala; //Coge la sala del jugador
            isInRoute = false;
            return player.position;
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
                distCercano = distActual;
                wpCercano = i;
            }
        }
        return wpCercano;
    }
    
}
