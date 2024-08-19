using UnityEngine;

public class BallController : MonoBehaviour
{
    public bool randomDirection;
    public float velocity;
    public Vector2 manualVelocity;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetBall();

        GameManager.OnMainMenuStart += ResetBall;
        GameManager.OnMatchStart += ResetBall;
        GameManager.OnGameOver += ResetBall;
    }

    void LaunchBall()
    {
        if (randomDirection)
        {
            float randomAngle = Random.Range(0f, Mathf.PI * 2f);
            Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
            rb.velocity = randomDirection * velocity;
        }
        else rb.velocity = manualVelocity;
    }

    public void ResetBall()
    {
        transform.position = Vector2.zero;
        LaunchBall();
    }
}
