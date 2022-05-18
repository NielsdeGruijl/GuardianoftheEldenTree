using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Main attack")]
    [SerializeField] GameObject shootPoint;
    [SerializeField] GameObject bullet;

    [Header("Abilities")]
    [SerializeField] GameObject turret;
    [SerializeField] AudioSource turretAudio;

    [Header("UI")]
    [SerializeField] GameObject XPbar;
    [SerializeField] Text PressEText;
    [SerializeField] Text scoreText;

    Rigidbody2D rb;
    SpriteRenderer sprite;
    AudioSource shootSFX;

    //player variables
    Vector2 movementDir = new Vector2(0, 0);
    Vector2 playerDir;
    Vector2 velocity;
    float speed = 2f;
    float maxXP = 200;
    float XP;
    public float score;

    //shooting variables
    Vector2 bulletOrigin;
    Vector2 dir;
    float bulletSpeed = 10f;
    float clickFireRate = 0.1f;
    //float holdFireRate = 0.15f;
    bool canShoot = true;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        shootSFX = GetComponent<AudioSource>();
    }


    private void Update()
    {
        MovementInput();
        SpriteFlipping();
        shoot();
        Ability();

        bulletOrigin = shootPoint.transform.position;
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity * speed;
    }

    void MovementInput()
    {
        float xVelocity = Input.GetAxis("Horizontal");
        float yVelocity = Input.GetAxis("Vertical");

        velocity = new Vector2(xVelocity, yVelocity);
    }

    void shoot()
    {
        if (canShoot)
        { 
            if (Input.GetMouseButtonDown(0))
            {
                GameObject projectile = Instantiate(bullet, bulletOrigin, Quaternion.identity);
                shootSFX.PlayOneShot(shootSFX.clip);

                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

                dir = dir.normalized;
                rb.velocity = dir * bulletSpeed;

                StartCoroutine(shootCD(clickFireRate));
            }
        }
    }

    IEnumerator shootCD(float fireRate)
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    public void EnemyKilled()
    {
        score += 10;
        scoreText.text = "Score: " + score;
        
        if(XP < maxXP)
        {
            XP += 10;
            XPbar.transform.localScale = new Vector3(XP / maxXP, XPbar.transform.localScale.y);
        }
    }

    void Ability()
    {
        if (XP >= maxXP)
            PressEText.gameObject.SetActive(true);
        else
            PressEText.gameObject.SetActive(false);

        if (XP >= maxXP && Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(turret, transform.position, Quaternion.identity);
            turretAudio.Play();
            XP = 0;
            XPbar.transform.localScale = new Vector3(XP / maxXP, XPbar.transform.localScale.y);
        }
    }

    void SpriteFlipping()
    {
        dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(bulletOrigin.x, bulletOrigin.y));
        playerDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(transform.position.x, transform.position.y));

        if (playerDir.x < 0 && !sprite.flipX)
        {
            sprite.flipX = true;
        }
        else if (playerDir.x > 0 && sprite.flipX)
        {
            sprite.flipX = false;
        }

        float originalX = dir.x;
        if (sprite.flipX)
        {
            bulletOrigin.x -= shootPoint.transform.localPosition.x * 2;
            dir.x += shootPoint.transform.localPosition.x * 2;
        }
        else
        {
            dir.x = originalX;
        }
    }
}
