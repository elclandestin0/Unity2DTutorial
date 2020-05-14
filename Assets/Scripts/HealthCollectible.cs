using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip clip;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Ruby")
        {
            PlayerController controller = collider.GetComponent<PlayerController>();
            if (controller != null)
            {
                if (controller.health < controller.maxHealth)
                {
                    controller.ChangeHealth(1);
                    controller.PlaySound(clip);
                    Destroy(gameObject);
                }
            }
        }
    }
}
