using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour
{
    public float speed = 0.4f;
    Vector2 destination = Vector2.zero;

    void Start()
    {
        this.destination = this.transform.position;
    }

    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.gamePaused || !GameManager.sharedInstance.gameStarted)
        {
            GetComponent<AudioSource>().volume = 0.0f;
        }
        else
        {
            GetComponent<AudioSource>().volume = 0.205f;
            // Calculamos el nuevo punto donde hay que ir en base a la variable de destino
            Vector2 newPos = Vector2.MoveTowards(this.transform.position, this.destination, this.speed * Time.deltaTime);
            // Usamos el riigidbody para transportar a Pacman hasta dicha posición
            GetComponent<Rigidbody2D>().MovePosition(newPos);
            float distanceToDestination = Vector2.Distance((Vector2)this.transform.position, this.destination);

            if (distanceToDestination < 2.0f)
            {
                if (Input.GetKey(KeyCode.W) && this.CanMoveTo(Vector2.up))
                {
                    this.destination = (Vector2)this.transform.position + Vector2.up;
                }

                if (Input.GetKey(KeyCode.D) && this.CanMoveTo(Vector2.right))
                {
                    this.destination = (Vector2)this.transform.position + Vector2.right;
                }

                if (Input.GetKey(KeyCode.A) && this.CanMoveTo(Vector2.left))
                {
                    this.destination = (Vector2)this.transform.position + Vector2.left;
                }


                if (Input.GetKey(KeyCode.S) && this.CanMoveTo(Vector2.down))
                {
                    this.destination = (Vector2)this.transform.position + Vector2.down;
                }
            }

            Vector2 dir = this.destination - (Vector2)this.transform.position;
            GetComponent<Animator>().SetFloat("DirX", dir.x);
            GetComponent<Animator>().SetFloat("DirY", dir.y);
        }
    }
    bool CanMoveTo(Vector2 direction)
    {
        Vector2 pacmanPosition = this.transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pacmanPosition + direction, pacmanPosition);
        return hit.collider == GetComponent<Collider2D>();
    }
}
