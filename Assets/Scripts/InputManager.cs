using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GolfBall ball;

    private Vector3 mouseDownStartPosition;
    private bool isMouseDown;
    public float ShootVelocity = 1.0f;

    /// <summary>
    /// Distance of how many world units the mouse has to be dragged before maximum shoot velocity is reached.
    /// </summary>
    public float MaxDragLengthWorldUnits = 10;

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<GolfBall>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            mouseDownStartPosition = GetMousePosition();
            isMouseDown = true;
            ball.ShowLineRender(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            var shootVelocityPercentage =
                Math.Min(GetMouseDragLength(), MaxDragLengthWorldUnits) / MaxDragLengthWorldUnits;
            ball.Shoot(shootVelocityPercentage * ShootVelocity, GetDirection());
            ball.ShowLineRender(false);
        }

        if (isMouseDown)
        {
            ball.UpdateLineRenderer(Math.Min(MaxDragLengthWorldUnits, GetMouseDragLength()) , GetDirection());
        }
    }

    /// <summary>
    /// Get current mouse position in world space
    /// </summary>
    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
    /// <summary>
    /// Get mouse drag length in world units
    /// </summary>
    private float GetMouseDragLength()
    {
        return (mouseDownStartPosition - GetMousePosition()).magnitude;
    }

    /// <summary>
    /// Get direction of current mouse drag
    /// </summary>
    private Vector3 GetDirection()
    {
        return (mouseDownStartPosition - GetMousePosition()).normalized;
    }
}
