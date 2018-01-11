using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    public float speed;
    public bool isMoving = true;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.velocity = transform.right * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}