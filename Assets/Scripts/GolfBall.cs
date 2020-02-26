using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GolfBall : MonoBehaviour {

    public Vector2 MinSpeed;
    public Vector2 MaxSpeed;
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

    public bool IsMoving => rb.velocity.x < MinSpeed.x && rb.velocity.y < MinSpeed.y;

    public void Shoot(float power, Vector2 direction)
    {
        if (!IsMoving)
        {
            Debug.Log("Cannot shoot, ball velocity above min speed");
            return;
        }
        rb.velocity = direction * power;
    }

    private void ClampSpeed()
    {
        rb.velocity = new Vector2(
             Math.Min(rb.velocity.x, MaxSpeed.x),
             Math.Min(rb.velocity.y, MaxSpeed.y)
        );
    }

    public void ShowLineRender(bool show)
    {
        lineRenderer.enabled = show;
    }

    public void UpdateLineRenderer(float power, Vector3 direction)
    {
        var startPoint = transform.position;
        startPoint.z = -1;
        var endPoint = startPoint + (direction * power);
        endPoint.z = -1;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }
}
