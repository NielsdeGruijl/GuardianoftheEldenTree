using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public static Events events;

    private void Awake()
    {
        events = this;
    }

    public delegate void EnemyDelegate(GameObject enemy);

    public event EnemyDelegate onEnemyHitTree;
    public void OnEnemyHitTree(GameObject enemy)
    {
        if(onEnemyHitTree != null)
            onEnemyHitTree(enemy);
    }
    
    public event EnemyDelegate onEnemyDeath;
    public void OnEnemyDeath(GameObject enemy)
    {
        if (onEnemyDeath != null)
            onEnemyDeath(enemy);
    }

}
