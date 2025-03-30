using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bunker : MonoBehaviour
{
    public int bunkerHealth = 15;
    public float bunkerShrink = 0.05f;
    public GameObject player;
    private Vector3 originalSize;

    void Start()
    {
        originalSize = transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bunkerHealth--;
        ShrinkBunker();

        if (other.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            GameManager.instance.lives = 0;
            Destroy(gameObject);
            Destroy(player);
            SceneManager.LoadSceneAsync(0);
        }

        if (bunkerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void ShrinkBunker()
    {
        transform.localScale = new Vector3(transform.localScale.x - bunkerShrink, transform.localScale.y, transform.localScale.z);
    }
}
