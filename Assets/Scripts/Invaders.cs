using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;
    public GameObject laserPrefab;
    public int rows = 5;
    public int columns = 10;
    public float speed = 1.0f;
    public float speedIncrease = 0.05f;
    public float attackRate = 1.0f;
    float width;
    float height;
    Vector3 direction = Vector2.right;

    private void Awake()
    {
        for (int i = 0; i < rows; i++) 
        {
            width = 2.0f * (columns - 1);
            height = 2.0f * (rows - 1);
            Vector2 center = new Vector2(-width/2, -height/2);

            for (int j = 0; j < columns; j++)
            {
                Invader invader = Instantiate(prefabs[i % prefabs.Length], transform);
                Vector3 position = new Vector3(center.x + (j * 2.0f), center.y + (i * 2.0f), 0.0f);
                invader.transform.localPosition = position;

                if (i < 2) invader.points = 10;
                else if (i < 4) invader.points = 20;
                else invader.points = 30;
            }
        }
    }

    private void Start()
    {
        InvokeRepeating("EnemyAttack", attackRate, attackRate);
    }
    private void Update()
    {
        InvadersMovement();
    }

    private void InvadersMovement()
    {
        transform.position += direction * speed * Time.deltaTime;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy) 
            {
                continue;
            }

            if (direction == Vector3.right && invader.position.x > (rightEdge.x - 1.0f))
            {
                AdvanceRow();
            }

            else if (direction == Vector3.left && invader.position.x < (leftEdge.x + 1.0f))
            {
                AdvanceRow();
            }
        }
    }

    private void AdvanceRow()
    {
        direction.x *= -1.0f;
        Vector3 position = transform.position;
        position.y -= 1.0f;
        transform.position = position;
    }

    private void EnemyAttack()
    {
        List<Transform> activeInvaders = new List<Transform>();

        foreach (Transform invader in transform)
        {
            if (invader.gameObject.activeInHierarchy)
            {
                activeInvaders.Add(invader);
            }
        }

        if (activeInvaders.Count > 0)
        {
            Transform shootingShip = activeInvaders[Random.Range(0, activeInvaders.Count)];
            Instantiate(laserPrefab, shootingShip.position, shootingShip.rotation);
        }
    }

    public void IncreaseSpeed()
    {
        speed += speedIncrease;
    }
}
