using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public int points;

    public void AddScore()
    {
        GameManager.instance.score += points;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            AddScore();
            Destroy(gameObject);
            Invaders invaders = FindObjectOfType<Invaders>();
            if (invaders != null)
            {
                invaders.IncreaseSpeed();
            }
        }
    }
}
