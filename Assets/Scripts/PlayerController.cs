using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip collectible;
    public AudioClip hit;
    public AudioClip cog;
    AudioSource source;
    Rigidbody2D rigidbody2d;
    public int maxHealth;
    public GameObject projectilePrefab;
    // we set the changes of health here
    public int health { get { return currentHealth; } }
    int currentHealth;
    public float speed;
    public float projectileForce;
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        rigidbody2d = GetComponent<Rigidbody2D>();
        // QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontalValue, verticalValue);

        // MATHF Approximately approximates the amount of 0.0f. pretty darn helpful!
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = rigidbody2d.position;
        position = position + move * speed * Time.deltaTime;

        // stops the jittering
        rigidbody2d.MovePosition(position);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }


        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            NPC npc = hit.collider.GetComponent<NPC>() ? hit.collider.GetComponent<NPC>() : null; if (npc != null)
            {
                npc.DisplayDialog();
            }
            else
            {
                return;
            }
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible) return;
            isInvincible = true;
            invincibleTimer = timeInvincible;
            animator.SetTrigger("Hit");
            PlaySound(hit);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + " / " + maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, projectileForce);

        animator.SetTrigger("Launch");
        PlaySound(cog);
    }

    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
