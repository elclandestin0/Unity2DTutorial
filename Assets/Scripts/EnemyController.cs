using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2d;
    public float speed;
    public bool vertical;
    float direction = 1.0f;

    float switchMovementTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = rigidbody2d.position;
        if (vertical)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
            position.y = position.y + (speed * direction * Time.deltaTime);
        }
        else
        {
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
            position.x = position.x + (speed * direction * Time.deltaTime);
        }
        switchMovementTime -= Time.deltaTime;
        rigidbody2d.MovePosition(position);
        if (switchMovementTime <= 0.0f)
        {
            direction = -direction;
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
