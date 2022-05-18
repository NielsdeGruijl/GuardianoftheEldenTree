using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();

    EnemySpawner spawner;
    GameObject closestEnemy;
    [SerializeField] GameObject acorn;

    float DestructionTimer = 10f;
    float range = 3f;

    float fireRate = 1f;

    bool canShoot = true;

    GameObject target;

    float bulletSpeed = 10f;

    private void Awake()
    {
        spawner = GameObject.FindGameObjectWithTag("Game").GetComponent<EnemySpawner>();
        StartCoroutine(DestroySelf());
    }

    private void Update()
    {
        foreach (GameObject enemy in spawner.enemies)
        {
            if(enemy != null) 
            {
                float distance = (enemy.transform.localPosition - transform.localPosition).magnitude;

                if (distance <= range)
                    enemies.Add(enemy);
            }
        }

        target = FindClosestEnemy();

        if(target != null && canShoot)
        {
            StartCoroutine(ShootClosestEnemy(target));
        }
    }

    GameObject FindClosestEnemy()
    {
        if (enemies.Count > 0)
        {
            float closestDistance = float.MaxValue;
            GameObject currentEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    float distance = (enemy.transform.localPosition - transform.localPosition).magnitude;

                    if (distance < closestDistance)
                    {
                        currentEnemy = enemy;
                        closestDistance = distance;
                    }

                    closestEnemy = currentEnemy;
                }
            }

            return closestEnemy;
        }

        return null;

    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(DestructionTimer);
        Destroy(gameObject);
    }

    IEnumerator ShootClosestEnemy(GameObject enemy)
    {
        if(target!= null)
        {
            canShoot = false;
            Vector3 dir = (target.transform.localPosition - transform.localPosition).normalized;
            GameObject projectile = Instantiate(acorn, transform.localPosition, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = dir * bulletSpeed;
        }


        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
