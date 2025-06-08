using Cinemachine;
using UnityEngine;

public class StageTranstition : MonoBehaviour
{
    public PolygonCollider2D mapBoundary;
    CinemachineConfiner confiner;
    public Direction direction;
    public enum Direction { Up, Down }
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
                newPos.y += 20;
                break;
            case Direction.Down:
                newPos.y -= 20;
                break;
        }

        player.transform.position = newPos;
    }
}
