using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hole : MonoBehaviour
{

    /// <summary>
    /// Gravity of which the hole will suck the ball in while the ball collider overlaps with the hole collider
    /// </summary>
    public float Gravity = 1.0f;

    /// <summary>
    /// Maximum velocity of the ball after which the ball is considered to be sucked in to the hole
    /// </summary>
    public float BallHoleVelocityThreshold = 0.5f;

    /// <summary>
    /// Maximum distance of the ball from the hole after which the ball is considered to be sucked in to the hole
    /// </summary>
    public float BallHoleDistanceThreshold = 0.1f;

    public Action<GolfBall> OnBallHoled;

    void OnTriggerEnter2D(Collider2D other)
    {
        var ball = other.GetComponent<GolfBall>();
        if (ball != null)
        {
            ball.SetLinearDrag(10f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var ball = other.GetComponent<GolfBall>();
        if (ball != null)
        {
            ball.SetLinearDrag(1f);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        var ball = other.GetComponent<GolfBall>();
        if (ball != null)
        {
            var distanceVector = transform.position - ball.Position;
            if (ball.Velocity < BallHoleVelocityThreshold && distanceVector.magnitude < BallHoleDistanceThreshold)
            {
                OnBallHoled?.Invoke(ball);
            }
            else
            {
                float pullForce;
                if (distanceVector.magnitude < BallHoleDistanceThreshold)
                {
                    ball.DrawTowardsPosition(transform.position, Gravity);
                }
                else
                {
                    pullForce = Math.Min(Gravity * 1f / distanceVector.magnitude, Gravity * (float)Math.PI);
                    ball.AddForce(pullForce, distanceVector.normalized);
                }
            }
        }
    }
}
