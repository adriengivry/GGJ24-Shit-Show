using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AnimationCurve m_enemiesOverScore;
    [SerializeField] private float m_delayBetweenSpawningEnemies = 1.5f;

    [Header("References")]
    [SerializeField] private GameObject m_enemyPrefab;

    public SpriteAnimation[] m_spawnLocations;

    private int m_enemyCount = 0;

    private SpriteAnimation[] FindSpawnLocations()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("EnemySpawn");
        SpriteAnimation[] sprites = new SpriteAnimation[spawners.Length];
        
        for (int i = 0; i < sprites.Length; ++i)
        {
            sprites[i] = spawners[i].GetComponent<SpriteAnimation>();
        }
        return sprites;
    }

    private void Start()
    {
        m_spawnLocations = FindSpawnLocations();
        SpawnEnemies(GameManager.Instance.Score);
        GameManager.Instance.ScoreChangedEvent.AddListener(SpawnEnemies);
    }

    private void OnDestroy()
    {
        GameManager.Instance.ScoreChangedEvent.RemoveListener(SpawnEnemies);
    }

    private void SpawnEnemies(int score)
    {
        int totalEnemiesToSpawn = (int)m_enemiesOverScore.Evaluate(score);
        int enemiesToSpawn = totalEnemiesToSpawn - m_enemyCount;

        StartCoroutine(SpawnEnemiesEachXSeconds(enemiesToSpawn, m_delayBetweenSpawningEnemies));
    }

    private IEnumerator SpawnEnemiesEachXSeconds(int enemiesToSpawn, float delay)
    {
        for (int i = 0; i < enemiesToSpawn; ++i)
        {
            yield return new WaitForSeconds(delay);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        int spawnlocation = Random.Range(0, m_spawnLocations.Length);
        Debug.Assert(m_spawnLocations.Length > 0, "No spawn location found!");
        m_spawnLocations[spawnlocation].Animate();
        Transform targetSpawn = m_spawnLocations[spawnlocation].transform;
        Transform instance = Instantiate(m_enemyPrefab.transform, targetSpawn.position, Quaternion.identity);
        // TODO: set target direction based on a Spawn Location script instance.gameObject.GetComponent<Character>().SetTargetDirection(EMovementDirection)
        ++m_enemyCount;
    }
}
