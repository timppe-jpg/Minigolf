using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class GolfBall : MonoBehaviour {

    public Vector2 MinVelocity;
    public Vector2 MaxVelocity;
    private Rigidbody2D rb;
    private LineRenderer lineRenderer;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
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

    private void ClampSpeed()
    {
        rb.velocity = new Vector2(
             Math.Min(rb.velocity.x, MaxVelocity.x),
             Math.Min(rb.velocity.y, MaxVelocity.y)
        );
    }

    public void ShowLineRender(bool show)
    {
        lineRenderer.enabled = show;
    }

    public void UpdateLineRenderer(float velocity, Vector3 direction)
    {
        var startPoint = transform.position;
        startPoint.z = -1;
        var endPoint = startPoint + (direction * velocity);
        endPoint.z = -1;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }
}