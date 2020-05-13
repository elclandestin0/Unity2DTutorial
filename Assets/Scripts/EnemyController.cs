using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float speed;
    public bool vertical;
    float verticalValue = 1.0f;

    float switchMovementTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = rigidbody2d.position;
        if (vertical)
        {
            position.y = position.y + (speed * verticalValue * Time.deltaTime);
        }
        else
        {
            position.x = position.x + (speed * verticalValue * Time.deltaTime);
        }
        switchMovementTime -= Time.deltaTime;
        rigidbody2d.MovePosition(position);
        if (switchMovementTime <= 0.0f)
        {
            verticalValue = -verticalValue;
            switchMovementTime = 1.0f;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

}
