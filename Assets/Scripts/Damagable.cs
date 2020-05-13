using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.name == "Ruby")
        {
            PlayerController controller = collider.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.ChangeHealth(-1);
            }
        }
    }
}
