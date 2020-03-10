using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class GolfBall : MonoBehaviour {

    public float MinVelocity = 0.1f;
    public float MaxVelocity = 50f;
    private Rigidbody2D rb;

    public float Velocity => rb.velocity.magnitude;
    public Vector3 Position => transform.position;

    void Awake () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        ClampSpeed();
    }

    public bool IsMoving => rb.velocity.magnitude < MinVelocity;

    public void Shoot(float velocity, Vector2 direction)
    {
        if (!IsMoving)
        {
            Debug.Log("Cannot shoot, ball velocity above min speed");
            return;
        }
        rb.velocity = direction * velocity;
    }

    public void DrawTowardsPosition(Vector3 position, float force)
    {
        var distance = position - Position;
        rb.velocity = distance * force;
    }

    public void AddForce(float force, Vector2 direction)
    {
        rb.AddForce(force * direction , ForceMode2D.Force);
    }

    public void SetLinearDrag(float linearDrag)
    {
        rb.drag = linearDrag;
    }

    private void ClampSpeed()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxVelocity);
    }
}