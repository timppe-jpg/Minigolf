using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AimAssistant : MonoBehaviour
{

    private LineRenderer lineRenderer;
    private GolfBall ball;

    // Use this for initialization
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetUp(GolfBall ball)
    {
        this.ball = ball;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLineRender(bool show)
    {
        lineRenderer.enabled = show;
    }

    public void UpdateLineRenderer(float length, Vector3 direction)
    {
        var startPoint = ball.Position;
        startPoint.z = -1;
        var endPoint = startPoint + (direction * length);
        endPoint.z = -1;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }
}
