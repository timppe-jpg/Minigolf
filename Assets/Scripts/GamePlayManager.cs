using UnityEngine;

public class GamePlayManager : MonoBehaviour
{

    private Hole hole;

    // Start is called before the first frame update
    void Awake()
    {
        hole = FindObjectOfType<Hole>();
        hole.OnBallHoled = OnBallHoled;
    }

    private void OnBallHoled(GolfBall ball)
    {
        ball.gameObject.SetActive(false);
    }
}
