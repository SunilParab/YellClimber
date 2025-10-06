using UnityEngine;

public class Trash : PhysicsObject
{
    Vector2 velocity;
    Rigidbody2D rb;

    [SerializeField] LayerMask floorLayers;

    [SerializeField] float gravityValue;

    [Header("Deceleration")]
    [SerializeField] float percentXDeceleration;
    [SerializeField] float flatXDeceleration;
    [SerializeField] float percentYDeceleration;
    [SerializeField] float flatYDeceleration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {

    }


    public override void Push(Vector2 force)
    {
        rb.AddForce(force,ForceMode2D.Impulse);
    }

}
