using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GolfBall ball;

    private Vector3 mouseDownStartPosition;
    private bool isMouseDown;
    public float ShootPower = 1.0f;

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
            mouseDownStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMouseDown = true;
            ball.ShowLineRender(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            ball.Shoot(GetPower(), GetDirection());
            ball.ShowLineRender(false);
        }

        if (isMouseDown)
        {
            ball.UpdateLineRenderer(GetPower(), GetDirection());
        }
    }

    private float GetPower()
    {
        return (mouseDownStartPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition)).magnitude * ShootPower;
    }

    private Vector3 GetDirection()
    {
        return (mouseDownStartPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
    }
}
