using System.Collections.Generic;
using UnityEngine;

public class ScreenCollider : MonoBehaviour
{
    public GameManager gm;

    EdgeCollider2D ec;
    LineRenderer lr;

    public float colliderPadding;

    public Color edgeColor;
    public float edgeWidth;

    List<Vector2> boundPoints;
    float leftMost;
    float rightMost;

    void Awake()
    {
        ec = GetComponent<EdgeCollider2D>();
        lr = GetComponent<LineRenderer>();
        CreateEdgeCollider();
    }

    private void Start()
    {
        GetBoundsPoints();
        leftMost = boundPoints[0].x;
        rightMost = boundPoints[1].x;
    }

    public List<Vector2> GetBoundsPoints()
    {
        if (boundPoints == null)
        {
            float height = Screen.height;
            float width = Screen.width;
            float halfWidth = width / 2;
            boundPoints = new List<Vector2>
        {
            Camera.main.ScreenToWorldPoint(new Vector2(halfWidth - height, 0)) + (Vector3.up + Vector3.right) * colliderPadding,
            Camera.main.ScreenToWorldPoint(new Vector2(halfWidth + height, 0)) + (Vector3.up + Vector3.left) * colliderPadding,
            Camera.main.ScreenToWorldPoint(new Vector2(halfWidth + height, height)) + (Vector3.down + Vector3.left) * colliderPadding,
            Camera.main.ScreenToWorldPoint(new Vector2(halfWidth - height, height)) + (Vector3.down + Vector3.right) * colliderPadding,
            Camera.main.ScreenToWorldPoint(new Vector2(halfWidth - height, 0)) + (Vector3.up + Vector3.right) * colliderPadding
        };
        }
        return boundPoints;
    }

    private void CreateEdgeCollider()
    {
        List<Vector2> points = GetBoundsPoints();
        ec.SetPoints(points);

        lr.positionCount = points.Count;
        float halfEdgeWidth = edgeWidth / 2;
        for (int i = 0; i < points.Count; i++)
        {
            Vector3 point = transform.TransformPoint(points[i]);
            float newX = point.x < 0 ? point.x - halfEdgeWidth : point.x + halfEdgeWidth;
            float newY = point.y < 0 ? point.y - halfEdgeWidth : point.y + halfEdgeWidth;
            lr.SetPosition(i, new Vector3(newX, newY, 0));
        }
        lr.startColor = edgeColor;
        lr.endColor = edgeColor;
        lr.startWidth = edgeWidth;
        lr.endWidth = edgeWidth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gm.GetState() == GameState.InMatch && collision.gameObject.CompareTag("Ball")) {
            BallController ballController = collision.gameObject.GetComponent<BallController>();
            foreach (ContactPoint2D contactPoint in collision.contacts)
            {
                if (contactPoint.point.x <= leftMost)
                {
                    gm.IncPlayer2Score();
                    ballController.ResetBall();
                }
                if (contactPoint.point.x >= rightMost)
                {
                    gm.IncPlayer1Score();
                    ballController.ResetBall();
                }
            }
        }
    }
}
