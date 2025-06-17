using Cinemachine;
using UnityEngine;

public class StageTranstition : MonoBehaviour
{
    public PolygonCollider2D mapBoundary;
    CinemachineConfiner confiner;
    public float push = 50f;
    public Direction direction;
    public enum Direction { Up, Down, Left, Right }
    void Awake()
    {
        confiner = FindFirstObjectByType<CinemachineConfiner>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.tag == "Tank_Ally")
        {
            confiner.m_BoundingShape2D = mapBoundary; 
            UpdatePlayerPosition(collision.gameObject);
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPos = player.transform.position;

        switch (direction)
        {
            case Direction.Up:
                newPos.y += push;
                break;
            case Direction.Down:
                newPos.y -= push;
                break;
            case Direction.Left:
                newPos.x -= push;
                break;
            case Direction.Right:
                newPos.x += push;
                break;
        }

        player.transform.position = newPos;
    }
}
