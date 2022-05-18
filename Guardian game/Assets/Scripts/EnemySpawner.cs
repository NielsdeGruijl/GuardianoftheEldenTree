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
    EnemySpawner enemySpawner;

    public List<GameObject> enemies = new List<GameObject>();

    int axis;
    float spawnDelay = 2;

    float spawnDelayDecrease = 0.05f;

    bool canIncreaseDifficulty = true;

    bool coroutineOver = true;

    private void Start()
    {
        audioManager = gameObject.GetComponent<AudioManager>();
        enemySpawner = gameObject.GetComponent<EnemySpawner>();
    }

    private void Update()
    {
        if(coroutineOver)
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
        coroutineOver = false;

        GameObject enemy = Instantiate(prefab, GetSpawnPoint(), Quaternion.identity);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        AssignEnemyValues(enemyScript);
        enemies.Add(enemy);
        yield return new WaitForSeconds(spawnDelay);

        coroutineOver = true;
    }

    IEnumerator IncreaseDifficulty()
    {
        canIncreaseDifficulty = false;
        yield return new WaitForSeconds(5);
        spawnDelay -= spawnDelay / 10;
        Debug.Log("spawn delay: " + (spawnDelay * 60));
        canIncreaseDifficulty = true;
    }

    void AssignEnemyValues(Enemy enemy)
    {
        enemy.guardian = guardian;
        enemy.player = player;
        enemy.audioManager = audioManager;
        enemy.spawner = enemySpawner;
    }
}
