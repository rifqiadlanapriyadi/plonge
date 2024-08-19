using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameManager gm;

    public KeyCode upKey;
    public KeyCode downKey;

    public float moveSpeed;

    public ScreenCollider sc;

    float maxUp;
    float maxDown;
    float halfHeight;

    void Start()
    {
        List<Vector2> boundsPoints = sc.GetBoundsPoints();
        maxDown = boundsPoints[0].y;
        maxUp = boundsPoints[boundsPoints.Count - 2].y;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        halfHeight = sr.bounds.size.y / 2;

        GameManager.OnMainMenuStart += Reset;
    }

    void Update()
    {
        Vector2 pos = transform.position;
        if (Input.GetKey(upKey) && gm.GetState() == GameState.InMatch)
            pos += Vector2.up * moveSpeed * Time.deltaTime;
        if (Input.GetKey(downKey) && gm.GetState() == GameState.InMatch)
            pos += Vector2.down * moveSpeed * Time.deltaTime;
        if (pos.y + halfHeight > maxUp) pos.y = maxUp - halfHeight;
        if (pos.y - halfHeight < maxDown) pos.y = maxDown + halfHeight;
        transform.position = pos;
    }

    public void Reset()
    {
        Vector2 newPos = new Vector2(transform.position.x, 0f);
        transform.position = newPos;
    }
}
