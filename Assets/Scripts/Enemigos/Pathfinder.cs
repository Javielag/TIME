using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinder : MonoBehaviour
{
    GameObject WaypointManager;
    Waypoint[] waypoints;
    Transform player;   
    public int sala = -1, wp = -1;
    bool isInRoute = false;
    int playerSala;
    Vector3 dir;
    Vector2 extents;
    public Vector3[] corners;
    [SerializeField] Transform[] hit = new Transform[4];
    private void Awake()
    {
        //player = GameManager.instance.GetPlayer().transform;
        //WaypointManager = GameObject.Find("WaypointManager");
        //waypoints = new Waypoint[WaypointManager.transform.childCount];
        //for (int i = 0; i < waypoints.Length; i++)
        //{
        //    waypoints[i] = WaypointManager.transform.GetChild(i).GetComponent<Waypoint>();
        //}
        //wp = CloseWaypoint(this.transform);
        //this.sala = waypoints[wp].sala;
    }
    // Use this for initialization
    void Start()
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
        extents = GetComponent<BoxCollider2D>().size / 2 * transform.lossyScale;
        corners = new Vector3[4];
    }

    public Vector3 Direction()
    {        
        int casted = 0;
        do
        {
            casted++;
            //if (player == null)
                //Debug.Log("Noplayer");
            corners[0] = new Vector2(transform.position.x + extents.x, transform.position.y + extents.y);
            corners[1] = new Vector2(transform.position.x + extents.x, transform.position.y - extents.y);
            corners[2] = new Vector2(transform.position.x - extents.x, transform.position.y + extents.y);
            corners[3] = new Vector2(transform.position.x - extents.x, transform.position.y - extents.y);

            if (transform == null) Debug.Log("NoTransform");

            hit[0] = Physics2D.Raycast(corners[0], player.position - corners[0], 1000, 1 << 12 | 1 << 13 | 1 << 16).collider.transform;
            hit[1] = Physics2D.Raycast(corners[1], player.position - corners[1], 1000, 1 << 12 | 1 << 13 | 1 << 16).collider.transform;
            hit[2] = Physics2D.Raycast(corners[2], player.position - corners[2], 1000, 1 << 12 | 1 << 13 | 1 << 16).collider.transform;
            hit[3] = Physics2D.Raycast(corners[3], player.position - corners[3], 1000, 1 << 12 | 1 << 13 | 1 << 16).collider.transform;

        } while (!hit[0] && !hit[1] && !hit[2] && !hit[3] && casted < 100);
        if(casted >= 100) { Debug.Log("ROTTTTO"); }
        if (hit[0] != player || hit[1] != player || hit[2] != player || hit[3] != player) //Si no ve al jugador
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
                    return waypoints[wp].closeWaypoints[1].position; //Devuelve la dirección del waypoint en la dirección hacia la que tiene que ir
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
