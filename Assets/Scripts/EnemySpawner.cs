
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private PoolOfEnemies _poolOfEnemies;
    [SerializeField] private int _spawnFrequencyOfEnemies=3;
    [SerializeField] private int _spawnFrequencyOfBossEnemies = 15;
    [SerializeField] private float _spawnDistance=10;

    [SerializeField] private GameObject target;
    private List<Health> _activeEnemies = new List<Health>();

    public List<Health> ActiveEnemies => _activeEnemies;

    void Start()
    {
        StartCoroutine(EnemySpawnCycle());
        StartCoroutine(BossEnemySpawnCycle());
    }

    private IEnumerator EnemySpawnCycle()
    {
        while(true)
        {
            yield return new WaitForSeconds(_spawnFrequencyOfEnemies);
            GameObject enemy = _poolOfEnemies.TryGetEnemies();
            if (enemy != null)
            {
                InitializingEnemy(enemy);
            }
        
        }
    }

    private IEnumerator BossEnemySpawnCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnFrequencyOfBossEnemies);
            GameObject enemy = _poolOfEnemies.TryGetBossEnemies();
            if (enemy != null)
            {
                InitializingEnemy(enemy);
            }
        }
    }

    private void InitializingEnemy(GameObject enemy)
    {
        enemy.GetComponent<EnemyStateMachine>().SetTarget(target);
        Health enemyhealth = enemy.GetComponent<Health>();
        enemyhealth.CharacterDeath.AddListener(RemoveNotActiveEnemies);
        _activeEnemies.Add(enemyhealth);
        enemy.transform.position = RandomVectorAtDistance(_spawnDistance);
        enemy.SetActive(true);
    }

    private Vector2 RandomVectorAtDistance(float distance)
    {
        float x = Random.Range(-distance, distance);
        float y = (float)System.Math.Sqrt(distance* distance - x * x);
        int random = Random.Range(1, 3);
        if (random == 2)
            y *= -1;      
        return new Vector2(x, y);
    }  

    private void RemoveNotActiveEnemies()
    {
        List<Health> notActiveEnemies = new List<Health>();
        foreach (Health healthEnemy in _activeEnemies)
        {
            if (healthEnemy.gameObject.activeSelf == false)
            {
                notActiveEnemies.Add(healthEnemy);
            }
        }
        foreach (Health healthEnemy in notActiveEnemies)
        {
            _activeEnemies.Remove(healthEnemy);
        }
    }
}
