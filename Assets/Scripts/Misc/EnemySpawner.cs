using Assets.Scripts.Observer;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, ISubject
{
    public static EnemySpawner Instance { get; private set; }

    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnEnemy;
    [SerializeField] public List<GameObject> enemySpawns = new List<GameObject>();
    private float spawnedEnemies = 0;
    private bool isSpawn = true;
    private int time = 10;
    [SerializeField] private int currentWave = 1;
    private List<IObserver> observers = new List<IObserver>();
    private void Start()
    {
        Instance = this;
        StartCoroutine(SpawnEmenies());

        Ally[] allies = FindObjectsOfType<Ally>();
        foreach (var ally in allies)
        {
            RegisterObserver(ally);
        }
    }
    public int GetCurrentWave() => currentWave;
    public void RemoveEnemy(GameObject enemy)
    {
        enemySpawns.Remove(enemy);

        if (enemySpawns.Count == 0)
        {
            StartCoroutine(StartCountdown());
            currentWave++;
        }
    }
    private IEnumerator SpawnEmenies()
    {
        while (isSpawn)
        {
            float enemiesToSpawn = Mathf.Min(1, spawnEnemy - spawnedEnemies);
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
    public List<Vector3> GetEnemyPositions()
    {
        return enemySpawns.Select(enemy => enemy.transform.position).ToList();
    }
    private IEnumerator StartCountdown()
    {
        int currentTime = time;
        NotifyObserver("Новая волна врагов приближается");

        while (currentTime > 0)
        {
            currentTime--;

            yield return new WaitForSeconds(1f);
            if (currentTime == 0)
            {
                spawnEnemy += Mathf.RoundToInt(spawnEnemy * 1.5f);
                isSpawn = true;
                StartCoroutine(SpawnEmenies());
            }
        }
    }

    public void RegisterObserver(IObserver observer) => observers.Add(observer);

    public void RemoveObject(IObserver observer) => observers.Remove(observer);

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
