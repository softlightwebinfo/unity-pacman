using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public Transform[] waypoints;
    int currentWaypoint = 0;
    public float speed = 0.1f;
    public int scoreInvincible = 5000;
    public bool shouldWaitHome = false;

    private void Update()
    {
        if (GameManager.sharedInstance.invincibleTime > 0)
        {
            //GetComponent<SpriteRenderer>().color = Color.blue;
            GetComponent<Animator>().SetBool("PacmanInvincible", true);
        }
        else
        {
            //GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<Animator>().SetBool("PacmanInvincible", false);
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.gamePaused || !GameManager.sharedInstance.gameStarted)
        {
            GetComponent<AudioSource>().volume = 0.0f;
        }
        else
        {
            if (!this.shouldWaitHome)
            {
                GetComponent<AudioSource>().volume = 0.100f;
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
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            if (GameManager.sharedInstance.invincibleTime <= 0)
            {
                GameManager.sharedInstance.RestartGame();
                Destroy(otherCollider.gameObject);
            }
            else
            {
                GameObject home = GameObject.Find("GhostHome");
                this.transform.position = home.transform.position;
                this.currentWaypoint = 0;
                this.shouldWaitHome = true;
                UiManager.sharedInstance.ScorePoints(this.scoreInvincible);
                StartCoroutine("AwakeFromHome");
            }
        }
    }

    IEnumerator AwakeFromHome()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        this.shouldWaitHome = false;
        this.speed *= 1.2f;
        this.scoreInvincible = (int)(this.scoreInvincible * 1.2f);
    }
}
