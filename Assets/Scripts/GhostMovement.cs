using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public Transform[] waypoints;
    int currentWaypoint = 0;
    public float speed = 0.1f;

    private void FixedUpdate()
    {
        // Distancia entre el fantasma y el punto de destino
        float distanceToWaypoint = Vector2.Distance((Vector2)this.transform.position, (Vector2)this.waypoints[this.currentWaypoint].position);


        if (distanceToWaypoint < 0.1f)
        {
            this.currentWaypoint = (this.currentWaypoint + 1) % this.waypoints.Length;
            Vector2 newDirection = this.waypoints[this.currentWaypoint].position - this.transform.position;
            GetComponent<Animator>().SetFloat("DirX", newDirection.x);
            GetComponent<Animator>().SetFloat("DirY", newDirection.y);
        }
        else
        {
            Vector2 newPos = Vector2.MoveTowards(this.transform.position, this.waypoints[this.currentWaypoint].position, this.speed * Time.deltaTime);
            GetComponent<Rigidbody2D>().MovePosition(newPos);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            Destroy(otherCollider.gameObject);
        }
    }
}
