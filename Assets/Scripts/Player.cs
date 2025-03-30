using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 15f;
    public Laser laserPrefab;
    private float horizontal;
    private Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Projectile();
        }
        CheckScreenBounds();
    }

    void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontal, 0);
        transform.position += direction * speed * Time.deltaTime;
    }

    private void CheckScreenBounds()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        if (transform.position.x > rightEdge.x)
        {
            transform.position = new Vector3(leftEdge.x, transform.position.y, transform.position.z);
        }

        else if (transform.position.x < leftEdge.x)
        {
            transform.position = new Vector3(rightEdge.x, transform.position.y, transform.position.z);
        }
    }

    void Projectile()
    {
        Instantiate(laserPrefab, transform.position, transform.rotation);
    }

    public void LoseLife()
    {
        GameManager.instance.lives--;

        if (GameManager.instance.lives > 0)
        {
            ResetPlayerPosition();
        }
        else
        {
            Destroy(gameObject);
            SceneManager.LoadSceneAsync(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyLaser"))
        {
            LoseLife();
        }
    }

    public void ResetPlayerPosition()
    {
        transform.position = spawnPosition;
    }
}
