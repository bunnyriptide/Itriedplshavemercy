using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
public class HunterScript : MonoBehaviour
{
    public GameObject player;

    public Vector3 lastPlayerPos;

    public float speed = 1f;

    public Vector3 Display;

    public bool visao;

    public GameObject waypoint1;
    public GameObject waypoint2;
    public GameObject waypoint3;
    public GameObject waypoint4;

    public GameObject previousPosition;

    [Task]
    bool CanSeePlayer()
    {
        if (visao == true)
        {
            lastPlayerPos = player.transform.position;
            return true;
        }
        else
        {
            return false;
        }
    }

    [Task]
    void Patrol()
    {
        switch (previousPosition.name)
        {
            case "Waypoint 1":
                transform.position = Vector3.MoveTowards(transform.position, waypoint2.transform.position, speed/2);
                if(transform.position.x == waypoint2.transform.position.x && transform.position.z == waypoint2.transform.position.z)
                {
                    previousPosition = waypoint2;
                    Task.current.Succeed();
                }
                break;
            case "Waypoint 2":
                transform.position = Vector3.MoveTowards(transform.position, waypoint3.transform.position, speed/2);
                if (transform.position.x == waypoint3.transform.position.x && transform.position.z == waypoint3.transform.position.z)
                {
                    previousPosition = waypoint3;
                    Task.current.Succeed();
                }
                break;
            case "Waypoint 3":
                transform.position = Vector3.MoveTowards(transform.position, waypoint4.transform.position, speed/2);
                if (transform.position.x == waypoint4.transform.position.x && transform.position.z == waypoint4.transform.position.z)
                {
                    previousPosition = waypoint4;
                    Task.current.Succeed();
                }
                break;
            case "Waypoint 4":
                transform.position = Vector3.MoveTowards(transform.position, waypoint1.transform.position, speed/2);
                if (transform.position.x == waypoint1.transform.position.x && transform.position.z == waypoint1.transform.position.z)
                {
                    previousPosition = waypoint1;
                    Task.current.Succeed();
                }
                break;
        }
        
    }


    [Task]
    void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed/4);
        Task.current.Succeed();
    }


    [Task]
    void ReportLastSeen()
    {
        lastPlayerPos = player.transform.position;
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
        
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out RaycastHit hit, 30) && hit.collider.CompareTag("Player"))
        {
            visao = true;
        }
        else
        {
            visao = false;
        }
    }



}