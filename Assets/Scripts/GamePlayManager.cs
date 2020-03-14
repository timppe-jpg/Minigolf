using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    private InputManager inputManager;
    private Hole hole;
    private TilemapColliderHandler tileMapColliderHandler;
    private Dictionary<GolfBall, Vector3> ballShootPositions;

    // Start is called before the first frame update
    void Awake()
    {
        ballShootPositions = new Dictionary<GolfBall, Vector3>();
        hole = FindObjectOfType<Hole>();
        tileMapColliderHandler = FindObjectOfType<TilemapColliderHandler>();
        tileMapColliderHandler.OnTileEnter = OnBallEnterTile;
        inputManager = FindObjectOfType<InputManager>();
        inputManager.OnBallShoot = OnBallShoot;
        hole.OnBallHoled = OnBallHoled;
    }

    private static void OnBallHoled(GolfBall ball)
    {
        ball.gameObject.SetActive(false);
    }

    private void OnBallShoot(GolfBall ball)
    {
        ballShootPositions[ball] = ball.Position;
    }

    private void OnBallEnterTile(GolfBall ball, string tileName)
    {
        Debug.Log("Tile entered: " + tileName);
        switch (tileName)
        {
            case "Water":
                OnBallEnterWater(ball);
                break;
            default:
                // noop
                break;
                
        }
    }

    private void OnBallEnterWater(GolfBall ball)
    {
        Debug.Log("OnBallEnterWater" + ball);
        ball.Position = ballShootPositions[ball];
    }
}
