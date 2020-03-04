using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GolfBall ball;
    private AimigAssistant aimigAssistant;

    private Vector3 mouseDownStartPosition;
    private bool isMouseDown;
    public float ShootVelocity = 1.0f;

    private enum aimModes
    {
        Default,
        Reverse,
        Rigth,
        Left
    }
    private aimModes currentAimSetting;

    /// <summary>
    /// Distance of how many world units the mouse has to be dragged before maximum shoot velocity is reached.
    /// </summary>
    public float MaxDragLengthWorldUnits = 10;

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<GolfBall>();
        aimigAssistant = FindObjectOfType<AimigAssistant>();
        currentAimSetting = aimModes.Default;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            currentAimSetting = Enum.GetValues(typeof(aimModes)).Cast<aimModes>().SkipWhile(e => e != currentAimSetting).Skip(1).FirstOrDefault();
            Debug.Log($"Current aim: {currentAimSetting}");
        }

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
            if(currentAimSetting != aimModes.Default)aimigAssistant.UpdateLineRenderer(ball.Position, GetMousePosition());
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
        return (ball.Position - GetMousePosition()).magnitude;
    }

    /// <summary>
    /// Get direction of current mouse drag
    /// </summary>
    private Vector3 GetDirection()
    {
        return AddAimModeModifier((ball.Position - GetMousePosition()).normalized);
    }

    private Vector3 AddAimModeModifier(Vector3 original)
    {
        switch (currentAimSetting)
        {
            case aimModes.Default: return original * -1f;
            case aimModes.Reverse: return original;
            case aimModes.Rigth: return new Vector3(original.y, original.x * -1f, 1f);
            case aimModes.Left: return new Vector3(original.y * -1f, original.x, 1f);
        }
        return new Vector3();
    }
}
