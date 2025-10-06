using UnityEngine;

public class PlayerController : PhysicsObject
{

    Vector2 velocity;
    Rigidbody2D rb;
    public float minHeight = -8;

    [SerializeField] LayerMask floorLayers;

    [SerializeField] float gravityValue;

    [Header("Deceleration")]
    [SerializeField] float percentXDeceleration;
    [SerializeField] float flatXDeceleration;
    [SerializeField] float percentYDeceleration;
    [SerializeField] float flatYDeceleration;

    Vector2 spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minHeight)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        //transform.Translate(velocity);

        rb.linearVelocity = velocity;

        CheckGravity();
        Decelerate();
    }

    //Deceleration per physics step
    void Decelerate() //TODO do different for x and y
    {
        float velocityXMagnitude = Mathf.Abs(velocity.x);
        float xSign = Mathf.Sign(velocity.x);
        float velocityYMagnitude = Mathf.Abs(velocity.y);
        float ySign = Mathf.Sign(velocity.y);

        //Apply x deceleration
        velocityXMagnitude *= 1 - percentXDeceleration;
        velocityXMagnitude -= flatXDeceleration;

        //Don't go negative
        if (velocityXMagnitude <= 0)
        {
            velocityXMagnitude = 0;
        }

        if (velocity.y > 0)
        {
            //Apply y deceleration
            velocityYMagnitude *= 1 - percentYDeceleration;
            velocityYMagnitude -= flatYDeceleration;

            //Don't go negative
            if (velocityYMagnitude <= 0)
            {
                velocityYMagnitude = 0;
            }
        }

        velocity = new(velocityXMagnitude * xSign, velocityYMagnitude * ySign);
    }

    void CheckGravity()
    {
        if (!IsGrounded())
        {
            velocity -= new Vector2(0, gravityValue);
        }
        else
        {
            velocity = new(velocity.x, 0);
        }
    }

    bool IsGrounded()
    { //TODO improve this
        return Physics2D.OverlapBox(
                            new(transform.position.x, transform.position.y - 0.75f),
                            new(1.4f, 0.15f),
                            0,
                            floorLayers
                            );
    }

    public override void Push(Vector2 force)
    {
        velocity += force;
    }

    public void Die()
    {
        velocity = new();
        transform.position = spawnPoint;
        GetComponent<Yeller>().Reset();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint")) {
            spawnPoint = collision.transform.position;
        }
    }

}
