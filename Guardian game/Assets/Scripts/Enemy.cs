using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Values passed on by EnemySpawner
    public GameObject guardian;
    public EnemySpawner spawner;
    public AudioManager audioManager;
    public Player player;

    Rigidbody2D rb;
    Guardian guardianScript;
    
    [SerializeField] GameObject healthBar;
    [SerializeField] Turret turret;

    delegate void EnemyDelegate();

    float speed = 1f;
    public float distance;

    float hpToRemove;

    int hp = 2;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        guardian = GameObject.FindGameObjectWithTag("Guardian");
        guardianScript = guardian.GetComponent<Guardian>();
        spawner = GameObject.FindGameObjectWithTag("Game").GetComponent<EnemySpawner>();
        player = GameObject.FindGameObjectWithTag("Character").GetComponent<Player>();

        audioManager = GameObject.FindGameObjectWithTag("Game").GetComponent<AudioManager>();

        hpToRemove = healthBar.transform.localScale.x / hp;
    }

    private void Update()
    {
        GetDistanceToGuardian();

        if (hp <= 0)
        {
            if (spawner.enemies.Contains(gameObject))
                spawner.enemies.Remove(gameObject);

            if (turret.enemies.Contains(gameObject))
                turret.enemies.Remove(gameObject);

            player.EnemyKilled();

            audioManager.PlayEnemyDeathSound();

            Destroy(gameObject);
        }
    }

    private void GetDistanceToGuardian()
    {
        distance = (guardian.transform.localPosition - transform.localPosition).magnitude;
    }

    // phisycs jes

    private void FixedUpdate()
    {
        rb.velocity = GetDirection() * speed;
    }

    private Vector2 GetDirection()
    {
        Vector2 diff = guardian.transform.localPosition - transform.localPosition;
        diff = diff.normalized;

        return diff;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Acorn"))
        {
            Destroy(collision.gameObject);

            healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x - hpToRemove, healthBar.transform.localScale.y, 0);
            Debug.Log("Healthbar length: " + healthBar.transform.localScale.x);

            hp--;
        }

        if (collision.transform.CompareTag("Guardian"))
        {
            if (spawner.enemies.Contains(collision.gameObject))
                spawner.enemies.Remove(collision.gameObject);

            guardianScript.RemoveHealth();

            Destroy(gameObject);
        }

        if (collision.transform.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);

            healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x - hpToRemove, healthBar.transform.localScale.y, 0);
            Debug.Log("Healthbar length: " + healthBar.transform.localScale.x);

            hp--;
        }
    }
}
