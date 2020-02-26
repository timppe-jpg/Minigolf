using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private ball ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<ball>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ball.isAiming = true;
            ball.aimStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            ball.isAiming = false;
        }
    }
}
