using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class MovimentEnemic : MonoBehaviour
{
   private Transform target;
   private int wavepointIndex = 0;
   private Enemy enemy;
    
    
    void Start() {
        //Crida la classe Waypoints i n'agafa la el primer waypoint
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];
    }

    void Update()
    {
        //Per buscar una direccio restem la posicio de lloc on hem de mirar - la posico del Object
        Vector3 dir =    target.position - transform.position;
        //movem aquest objecte en direccio dir, (el .normalized s'ha de posar) * velocitat * framerate, en el space world
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);
        //si estem a prop, nem a apuntar al seguent waypont
        if(Vector3.Distance(transform.position, target.position) <= 0.4f) {
            GetNextWaypoint();
        }
        enemy.speed = enemy.startSpeed;

    }
    void GetNextWaypoint() {
        //si el wavepoint actual es mes gran que el total de waypoints es que ja estem alla y destruim aquest game object
        if(wavepointIndex >= Waypoints.points.Length - 1) {
            EndPath();
            return;
        }
        // si no sumem 1 al waypoint actual y el marquem com a target per que al update el mogui a la direccio que toca
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
    void EndPath() {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }
}
