using System;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GolfBall ball;
    private AimAssistant aimAssistant;

    private bool isMouseDown;
    public float ShootVelocity = 1.0f;

    public AimMode CurrentAimSetting { get; private set; }

    /// <summary>
    /// Distance of how many world units the mouse has to be dragged before maximum shoot velocity is reached.
    /// </summary>
    public float MaxDragLengthWorldUnits = 10;

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<GolfBall>();
        aimAssistant = FindObjectOfType<AimAssistant>();
        aimAssistant.SetUp(ball);
        CurrentAimSetting = AimMode.Default;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            CurrentAimSetting = Enum.GetValues(typeof(AimMode)).Cast<AimMode>().SkipWhile(e => e != CurrentAimSetting).Skip(1).FirstOrDefault();
            Debug.Log($"Current aim: {CurrentAimSetting}");
        }

        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            aimAssistant.ShowLineRender(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            var shootVelocityPercentage =
                Math.Min(GetMouseDragLength(), MaxDragLengthWorldUnits) / MaxDragLengthWorldUnits;
            ball.Shoot(shootVelocityPercentage * ShootVelocity, GetDirection());
            aimAssistant.ShowLineRender(false);
        }

        if (isMouseDown)
        {
            aimAssistant.UpdateLineRenderer(Math.Min(MaxDragLengthWorldUnits, GetMouseDragLength()) , GetDirection());
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
        switch (CurrentAimSetting)
        {
            case AimMode.Default: return original * -1f;
            case AimMode.Reverse: return original;
            case AimMode.Right: return new Vector3(original.y, original.x * -1f, 1f);
            case AimMode.Left: return new Vector3(original.y * -1f, original.x, 1f);
        }
        return new Vector3();
    }
}
