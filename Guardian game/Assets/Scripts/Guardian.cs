using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Guardian : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private GameObject acorn;
    private GameObject closestEnemy;
    AudioSource audio;

    public float health;
    float healthRemove;

    [SerializeField] Image healthBar;

    private void Start()
    {
        healthRemove = healthBar.gameObject.transform.localScale.x / 10;
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (healthBar.gameObject.transform.localScale.x <= 0)
            SceneManager.LoadScene(2);
    }

/*    public GameObject FindClosestEnemy()
    {
        if (spawner.enemies.Count > 0)
        {

            float closestDistance = float.MaxValue;
            GameObject currentEnemy = null;

            foreach (GameObject enemy in spawner.enemies)
            {
                if(enemy != null)
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

    }*/

    public void RemoveHealth()
    {
        healthBar.gameObject.transform.localScale = new Vector3(healthBar.gameObject.transform.localScale.x - healthRemove, healthBar.transform.localScale.y);
        audio.PlayOneShot(audio.clip);
    }

    /*    IEnumerator shootClosestEnemy()
        {
            canSpawnEnemy = false;
            Instantiate(acorn, transform.localPosition, Quaternion.identity);

            yield return new WaitForSeconds(fireRate);
            canSpawnEnemy = true;
        }*/
}
