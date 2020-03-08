using UnityEngine;

public class AimAssistant : MonoBehaviour
{
    public Color ForceLineColor = Color.red;
    public float ForceLineWidth = 0.1f;
    private LineRenderer forceLine;

    public Color CrossHairColor = Color.black;
    public float CrossHairWidth = 0.1f;
    public float CrossHairLength = 1.0f;
    private LineRenderer crossHairXAxisLine;
    private LineRenderer crossHairYAxisLine;

    private LineRenderer mousePositionLine;
    public Color MousePositionLineColor = Color.black;
    public float MousePositionLineWidth = 0.05f;

    private GolfBall ball;

    // Use this for initialization
    void Awake()
    {
        InitLineRenderers();
    }

    private void InitLineRenderers()
    {
        forceLine = AddLineRenderer(ForceLineColor, ForceLineWidth, "ForceLine");
        mousePositionLine = AddLineRenderer(MousePositionLineColor, MousePositionLineWidth, "MousePositionLine");
        MakeDashed(mousePositionLine);
        crossHairXAxisLine = AddLineRenderer(CrossHairColor, CrossHairWidth, "CrossHairX");
        crossHairYAxisLine = AddLineRenderer(CrossHairColor, CrossHairWidth, "CrossHairY");
    }

    /// <summary>
    /// Adds a child object to this gameObject and attaches a line renderer to the child
    /// </summary>
    /// <param name="color">Color of the line renderer</param>
    /// <param name="width">Width of the line renderer</param>
    /// <param name="childName">Child gameObject name</param>
    /// <returns></returns>
    private LineRenderer AddLineRenderer(Color color, float width, string childName)
    {
        var child = new GameObject(childName);
        child.transform.SetParent(transform);
        var line = (LineRenderer) child.AddComponent(typeof(LineRenderer));
        line.material = new Material(Shader.Find("Unlit/Color")) {color = color};
        line.startColor = color;
        line.endColor = color;
        line.startWidth = width;
        line.endWidth = width;
        line.sortingOrder = 1;
        return line;
    }

    private static void MakeDashed(LineRenderer line)
    {
        line.material = new Material(Shader.Find("Unlit/Transparent"))
        {
            mainTexture = Resources.Load<Texture>(@"Sprites\DashedLine")
        };
        line.textureMode = LineTextureMode.Tile;
    }

    public void SetUp(GolfBall ball)
    {
        this.ball = ball;
    }
    
    public void ShowForceLineRender(bool show)
    {
        forceLine.enabled = show;
        mousePositionLine.enabled = show;
    }

    public void UpdateForceLineRenderer(float length, Vector3 direction)
    {
        var startPoint = ball.Position;
        startPoint.z = -1;
        var endPoint = startPoint + (direction * length);
        endPoint.z = -1;
        forceLine.SetPosition(0, startPoint);
        forceLine.SetPosition(1, endPoint);
    }

    public void UpdateCrossHairPosition(Vector3 worldPosition)
    {
        mousePositionLine.SetPosition(0, ball.Position);
        mousePositionLine.SetPosition(1, worldPosition);
        crossHairXAxisLine.SetPosition(0, worldPosition + new Vector3(-CrossHairLength/2, 0,0 ));
        crossHairXAxisLine.SetPosition(1, worldPosition + new Vector3(CrossHairLength/2, 0, 0));
        crossHairYAxisLine.SetPosition(0, worldPosition + new Vector3(0, CrossHairLength / 2, 0));
        crossHairYAxisLine.SetPosition(1, worldPosition + new Vector3(0, -CrossHairLength / 2, 0));
    }
}
