using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Values passed on by EnemySpawner
    [HideInInspector] public GameObject eldenTree;
    [HideInInspector] public AudioManager audioManager;
    [HideInInspector] public Player player;
    
    [SerializeField] GameObject healthBar;

    Rigidbody2D rb;

    float speed = 1f;

    float hpToRemove;
    int hp = 2;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        hpToRemove = healthBar.transform.localScale.x / hp;
    }

    private void Update()
    {
        CheckHP();
    }

    void CheckHP()
    {
        if (hp <= 0)
        {
            player.EnemyKilled();

            audioManager.PlaySFX("EnemyDeath");

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Acorn"))
        {
            Destroy(collision.gameObject);

            healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x - hpToRemove, healthBar.transform.localScale.y, 0);
            //Debug.Log("Healthbar length: " + healthBar.transform.localScale.x);

            hp--;
        }

        if (collision.transform.CompareTag("Guardian"))
        {
            Events.events.OnEnemyHitTree(gameObject);

            Destroy(gameObject);
        }

        if (collision.transform.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);

            healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x - hpToRemove, healthBar.transform.localScale.y, 0);
            //Debug.Log("Healthbar length: " + healthBar.transform.localScale.x);

            hp--;
        }
    }

    private void OnDestroy()
    {
        Events.events.OnEnemyDeath(gameObject);
    }

    //=================================================== physics ========================================================

    private void FixedUpdate()
    {
        rb.velocity = GetDirection() * speed;
    }

    private Vector2 GetDirection()
    {
        Vector2 diff = eldenTree.transform.localPosition - transform.localPosition;
        diff = diff.normalized;

        return diff;
    }
}
