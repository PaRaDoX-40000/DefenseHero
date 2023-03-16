using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolOfEnemies : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject _bossEnemyPrefab;
    private List<GameObject> _enemies = new List<GameObject>();
    private List<GameObject> _bossEnemies= new List<GameObject>();



    private void Start()
    {
        for(int i =0; i <= 10; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab, new Vector3(10, 10, 0),Quaternion.identity);
            enemy.transform.parent = transform;
            enemy.SetActive(false);
            _enemies.Add(enemy);
        }
        for (int i = 0; i <= 2; i++)
        {
            GameObject enemy = Instantiate(_bossEnemyPrefab, new Vector3(10, 10, 0), Quaternion.identity);
            enemy.transform.parent = transform;
            enemy.SetActive(false);
            _bossEnemies.Add(enemy);
        }
    }

    public GameObject TryGetEnemies()
    {
        GameObject enemy = _enemies.FirstOrDefault(q => q.activeSelf == false);
        if (enemy == null)
        {
            Debug.Log("Не хватает врагов для спавна");
            return null;
        }
        return enemy;
    }

    public GameObject TryGetBossEnemies()
    {
        GameObject enemy = _bossEnemies.FirstOrDefault(q => q.activeSelf == false);
        if (enemy == null)
        {
            Debug.Log("Не хватает боссов для спавна");
            return null;
        }
        return enemy;
    }

}
