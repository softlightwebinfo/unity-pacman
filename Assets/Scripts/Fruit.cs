using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int score = 1000;
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            GameManager.sharedInstance.MakeInvincibleFor(15.0f);
            UiManager.sharedInstance.ScorePoints(this.score);
            Destroy(this.gameObject);
        }
    }
}
