using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(PhysicsObject))]
public class Yeller : MonoBehaviour
{

    PhysicsObject body;
    InputAction interactAction;

    [SerializeField] float selfPushForce = 2;
    [SerializeField] float otherPushForce = 2;
    [SerializeField] Collider2D pushCone;

    [SerializeField] float maxCharge = 100;
    float curCharge;
    [SerializeField] Image fillBar;

    int canCharge = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<PhysicsObject>();
        interactAction = InputSystem.actions.FindAction("Attack");

        curCharge = maxCharge;
    }

    // Update is called once per frame
    void Update()
    {
        fillBar.fillAmount = curCharge / maxCharge;

        Vector2 screenPosition = Mouse.current.position.ReadValue();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        pushCone.transform.eulerAngles = new(0,0, Vector2.SignedAngle(new Vector2(0,-1), worldPosition - (Vector2)transform.position));
    }

    void FixedUpdate()
    {

        if (canCharge <= 0) {
            GainCharge(1);
        }

        //Get mouse position
        Vector2 screenPosition = Mouse.current.position.ReadValue();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (interactAction.IsPressed())
        {
            if (curCharge > 1)
            {
                Vector2 pushVector = ((Vector2)body.transform.position - worldPosition).normalized;
                pushVector *= selfPushForce;

                //Push player
                body.Push(pushVector);

                //Push others
                List<Collider2D> hits = new();
                pushCone.Overlap(hits);

                foreach (Collider2D col in hits)
                {
                    PhysicsObject cur = col.GetComponent<PhysicsObject>();
                    if (cur == null)
                    {
                        continue;
                    }
                    if (cur == body)
                    {
                        continue;
                    }

                    Vector2 otherPushVector = (worldPosition - (Vector2)cur.transform.position).normalized;
                    otherPushVector *= otherPushForce;

                    cur.Push(otherPushVector);

                }

                //Use Charge
                curCharge -= 1;
                canCharge++;
                Invoke(nameof(AllowCharge), 1.5f);
            }
        }
    }

    public void GainCharge(float amount)
    {
        curCharge += amount;
        if (curCharge > maxCharge)
        {
            curCharge = maxCharge;
        }
    }

    void AllowCharge()
    {
        canCharge--;
    }

}
