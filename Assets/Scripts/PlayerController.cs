using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private enum State {idle, running, jumping, falling, hurt}
    private State state = State.idle;
    private Collider2D coll;
    public AudioSource enemies;
    public AudioClip enemydeath;
    public AudioClip collectable;
    public AudioClip hurted;
    public GameObject Panel;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 9f;
    [SerializeField] private int peanuts = 0;
    [SerializeField] private Text peanutsText;
    [SerializeField] private float hurtForce = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        Panel.SetActive(false);
        enemies = GetComponent<AudioSource>();
        if (enemies == null)
        {
            enemies = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if(state != State.hurt)
        {
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int)state);
    } 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
            enemies.clip = collectable;
            enemies.Play();
            peanuts += 1;
            peanutsText.text = peanuts.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(state == State.falling)
            {
                Destroy(other.gameObject);
                if (enemies != null)
                {
                    enemies.clip = enemydeath;
                    enemies.Play();
                }
                Jump();
            }
            else
            {
                state = State.hurt;
                if(other.gameObject.transform.position.x > transform.position.x)
                {
                    gameObject.SetActive(false);
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                    if (enemies != null)
                    {
                        enemies.clip = hurted;
                        enemies.Play();
                    }
                    Panel.SetActive(true);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                    if (enemies != null)
                    {
                        enemies.clip = hurted;
                        enemies.Play();
                    }
                    Panel.SetActive(true);
                }
            }
        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }

        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        else
        {

        }

        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers())
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }

    private void AnimationState()
    {
        if(state == State.jumping)
        {
            if(rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }

        else if(state == State.falling)
        {
            if(coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }

        else if(state == State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }

        else if(Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }

        else
        {
            state = State.idle;
        }
    }
}
