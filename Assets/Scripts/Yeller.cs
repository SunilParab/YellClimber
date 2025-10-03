using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PhysicsObject))]
public class Yeller : MonoBehaviour
{

    PhysicsObject body;
    InputAction interactAction;

    [SerializeField] float pushForce = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<PhysicsObject>();
        interactAction = InputSystem.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Get mouse position
        Vector2 screenPosition = Mouse.current.position.ReadValue();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (interactAction.IsPressed())
        {
            Vector2 pushVector = ((Vector2)body.transform.position - worldPosition).normalized;
            pushVector *= pushForce;

            body.Push(pushVector); print("AHHHH");
        }
    }

}
