using UnityEngine;

public class AimAssistant : MonoBehaviour
{
    public Color ForceLineColor = Color.red;
    public float ForceLineWidth = 0.1f;
    public int ForceLineZLayer = 2;
    private LineRenderer forceLine;

    public Color CrossHairColor = Color.black;
    public float CrossHairWidth = 0.1f;
    public float CrossHairLength = 1.0f;
    public int CrossHairZLayer = 2;
    private LineRenderer crossHairXAxisLine;
    private LineRenderer crossHairYAxisLine;

    private LineRenderer mousePositionLine;
    public Color MousePositionLineColor = Color.black;
    public float MousePositionLineWidth = 0.05f;
    public int MousePositionLineZLayer = 2;

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
        var endPoint = startPoint + (direction * length);
        forceLine.SetPosition(0, GetZCorrectedPosition(startPoint, ForceLineZLayer));
        forceLine.SetPosition(1, GetZCorrectedPosition(endPoint, ForceLineZLayer));
    }

    public void UpdateCrossHairPosition(Vector3 worldPosition)
    {
        mousePositionLine.SetPosition(0, GetZCorrectedPosition(ball.Position, MousePositionLineZLayer));
        mousePositionLine.SetPosition(1, GetZCorrectedPosition(worldPosition, MousePositionLineZLayer));
        crossHairXAxisLine.SetPosition(0, GetZCorrectedPosition(worldPosition + new Vector3(-CrossHairLength/2, 0, 0 ), CrossHairZLayer));
        crossHairXAxisLine.SetPosition(1, GetZCorrectedPosition(worldPosition + new Vector3(CrossHairLength/2, 0, 0), CrossHairZLayer));
        crossHairYAxisLine.SetPosition(0, GetZCorrectedPosition(worldPosition + new Vector3(0, CrossHairLength / 2, 0), CrossHairZLayer));
        crossHairYAxisLine.SetPosition(1, GetZCorrectedPosition(worldPosition + new Vector3(0, -CrossHairLength / 2, 0), CrossHairZLayer));
    }

    

    private Vector3 GetZCorrectedPosition(Vector3 vector, int zLayer)
    {
        return new Vector3(vector.x, vector.y, -zLayer);
    }
}
