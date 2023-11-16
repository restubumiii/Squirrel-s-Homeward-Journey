using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float walkLength = 4f;
    [SerializeField] private LayerMask ground;
    private Collider2D coll;
    private Rigidbody2D rb;

    private bool facingLeft = true;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }

                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-walkLength, rb.velocity.y);
                }
            }

            else
            {
                facingLeft = false;
            }
        }

        else
        {
            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(walkLength, rb.velocity.y);
                }

            }
            else
            {
                facingLeft = true;
            }
        }
    }


}
