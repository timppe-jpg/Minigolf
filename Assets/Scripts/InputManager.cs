using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GolfBall ball;

    private Vector3 mouseDownStartPosition;
    private bool isMouseDown;

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
            var power = (mouseDownStartPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition)).magnitude;
            var direction = (mouseDownStartPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
            ball.Shoot(power, direction);
            ball.ShowLineRender(false);
        }

        if (isMouseDown)
        {
            ball.UpdateLineRenderer(mouseDownStartPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        
    }
}
