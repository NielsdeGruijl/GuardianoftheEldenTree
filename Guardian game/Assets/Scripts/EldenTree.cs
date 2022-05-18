using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EldenTree : MonoBehaviour
{
    AudioSource audio;

    public float health;
    float healthRemove;

    [SerializeField] Image healthBar;
    
    private void Start()
    {
        healthRemove = healthBar.gameObject.transform.localScale.x / 10;
        audio = GetComponent<AudioSource>();

        Events.events.onEnemyHitTree += RemoveHealth;
    }

    private void Update()
    {
        if (healthBar.gameObject.transform.localScale.x <= 0)
            SceneManager.LoadScene(2);
    }

    public void RemoveHealth(GameObject enemy)
    {
        healthBar.gameObject.transform.localScale = new Vector3(healthBar.gameObject.transform.localScale.x - healthRemove, healthBar.transform.localScale.y);
        audio.PlayOneShot(audio.clip);
    }

    private void OnDestroy()
    {
        Events.events.onEnemyHitTree -= RemoveHealth;
    }
}
