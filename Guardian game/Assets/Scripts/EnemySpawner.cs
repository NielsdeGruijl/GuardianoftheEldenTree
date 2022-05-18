using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject map;
    [SerializeField] GameObject enemyPrefab;

    [Header("Enemy info")]
    [SerializeField] GameObject guardian;
    [SerializeField] Player player;
    AudioManager audioManager;

    public List<GameObject> enemies = new List<GameObject>();

    float spawnDelay = 2f;
    float increaseDifficultyDelay = 5f;

    bool canIncreaseDifficulty = true;
    bool canSpawnEnemy = true;

    private void Start()
    {
        audioManager = gameObject.GetComponent<AudioManager>();

        Events.events.onEnemyDeath += EnemyKilled;
    }

    private void Update()
    {
        if(canSpawnEnemy)
            StartCoroutine(spawnEnemies(enemyPrefab));

        if (canIncreaseDifficulty && spawnDelay >= 0.5f)
            StartCoroutine(IncreaseDifficulty());
    }

    private Vector3 GetSpawnPoint()
    {
        float spawnValue = Random.Range(0, (map.transform.localScale.x)) - map.transform.localScale.x / 2;
        bool spawnUp = Random.Range(0f, 1f) > 0.5f;

        float yValue;

        if(spawnUp) { yValue = map.transform.localScale.y / 2; }
        else { yValue = -map.transform.localScale.y / 2; }

        return new Vector3(spawnValue, yValue, 0);
    }

    IEnumerator spawnEnemies(GameObject prefab)
    {
        canSpawnEnemy = false;

        GameObject enemy = Instantiate(prefab, GetSpawnPoint(), Quaternion.identity);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        AssignEnemyValues(enemyScript);
        enemies.Add(enemy);
        yield return new WaitForSeconds(spawnDelay);

        canSpawnEnemy = true;
    }

    void AssignEnemyValues(Enemy enemy)
    {
        enemy.eldenTree = guardian;
        enemy.player = player;
        enemy.audioManager = audioManager;
    }

    IEnumerator IncreaseDifficulty()
    {
        canIncreaseDifficulty = false;
        yield return new WaitForSeconds(increaseDifficultyDelay);
        spawnDelay -= spawnDelay / 10;
        //Debug.Log("spawn delay: " + (spawnDelay + " seconds"));
        canIncreaseDifficulty = true;
    }

    private void EnemyKilled(GameObject enemy)
    {
        if (enemies.Contains(enemy))
            enemies.Remove(enemy);

        Debug.Log("Enemy removed from spawner list!");
    }

    private void OnDestroy()
    {
        Events.events.onEnemyDeath -= EnemyKilled;
    }
}
