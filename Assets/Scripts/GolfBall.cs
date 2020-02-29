using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class GolfBall : MonoBehaviour {

    public Vector2 MinVelocity;
    public Vector2 MaxVelocity;
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

    public bool IsMoving => rb.velocity.x < MinVelocity.x && rb.velocity.y < MinVelocity.y;

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

    private void ClampSpeed()
    {
        rb.velocity = new Vector2(
             Math.Min(rb.velocity.x, MaxVelocity.x),
             Math.Min(rb.velocity.y, MaxVelocity.y)
        );
    }
}