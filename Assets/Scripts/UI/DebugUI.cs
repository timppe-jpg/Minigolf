using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{

    private GolfBall ball;
    private InputManager inputManager;
    public Text BallVelocityText;
    public Text BallCanShootText;
    public Text BallAimModeText;

    void Awake()
    {
        ball = FindObjectOfType<GolfBall>();
        inputManager = FindObjectOfType<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ball == null)
        {
            return;
        }
        BallVelocityText.text = $"Ball velocity: {ball.Velocity}";
        BallCanShootText.text = $"Can shoot: {ball.IsMoving.ToString()}";
        if (inputManager == null)
        {
            return;
        }
        BallAimModeText.text = $"Aim mode: {Enum.GetName(typeof(AimMode), inputManager.CurrentAimSetting)}";
    }
}
