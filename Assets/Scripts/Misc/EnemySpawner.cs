using Assets.Scripts.Observer;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, ISubject
{
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int spawnEnemy = 100;
    [SerializeField] public List<GameObject> enemySpawns;
    private int spawnedEnemies = 0;
    private bool isSpawn = true;
    private int time = 10;
    private List<IObserver> observers = new List<IObserver>();
    private void Start()
    {
        StartCoroutine(SpawnEmenies());

        Ally[] allies = FindObjectsOfType<Ally>();
        foreach (var ally in allies)
        {
            RegisterObserver(ally);
        }
    }
    public void RemoveEnemy(GameObject enemy)
    {
        enemySpawns.Remove(enemy);

        if (enemySpawns.Count == 0)
        {
            StartCoroutine(StartCountdown());
        }
    }
    private IEnumerator SpawnEmenies()
    {
        while (isSpawn)
        {
            int enemiesToSpawn = Mathf.Min(1, spawnEnemy - spawnedEnemies);
            spawnedEnemies += enemiesToSpawn;

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform;

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                enemySpawns.Add(newEnemy);
                newEnemy.GetComponent<EnemyHealth>()?.SetEnemySpawner(this);

            }

            yield return new WaitForSeconds(Random.Range(0.1f, 0.25f));

            if (spawnedEnemies >= 100)
                isSpawn = false;
        }
    }

    private IEnumerator StartCountdown()
    {
        int currentTime = time;
        NotifyObserver("Новая волна врагов приближается");

        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            if (currentTime == 0)
            {
                spawnEnemy = +10;
                isSpawn = true;
                StartCoroutine(SpawnEmenies());
            }
        }
    }

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObject(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObserver(string message)
    {
        foreach (IObserver observer in observers)
        {
            observer.OnNofity(message);
        }
    }

    public void NotifyHealthObserver(float health)
    {
        foreach (IObserver observer in observers)
        {
            observer.OnHealthUpdate(health);
        }
    }
}
