using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 30f;
    public Vector3 direction;
    void Start()
    {
        Destroy(gameObject, 5f);
    }
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
