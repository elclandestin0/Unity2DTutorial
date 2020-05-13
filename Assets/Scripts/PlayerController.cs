using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public int maxHealth;
    // we set the changes of health here
    public int health { get { return currentHealth; }}
    int currentHealth;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 1;
        rigidbody2d = GetComponent<Rigidbody2D>();
        // QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");
        Vector2 position =  rigidbody2d.position;
        position.x = position.x + (speed * horizontalValue * Time.deltaTime);
        position.y = position.y + (speed * verticalValue * Time.deltaTime);

        // stops the jittering
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + " / " + maxHealth);
    }
}
