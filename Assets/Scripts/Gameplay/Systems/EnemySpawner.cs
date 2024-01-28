using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AnimationCurve m_enemiesOverScore;
    [SerializeField] private float m_delayBetweenSpawningEnemies = 1.5f;

    [Header("References")]
    [SerializeField] private GameObject m_enemyPrefab;

    private GameObject[] m_spawnLocations;

    private int m_enemyCount = 0;

    private GameObject[] FindSpawnLocations()
    {
        return GameObject.FindGameObjectsWithTag("EnemySpawn");
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
            Debug.Assert(m_spawnLocations.Length > 0, "No spawn location found!");
            Transform targetSpawn = m_spawnLocations[Random.Range(0, m_spawnLocations.Length)].transform;
            yield return new WaitForSeconds(delay / 2.0f);
            targetSpawn.GetComponent<SpriteAnimation>()?.Animate(); // Try to animate the spawn if there is a SpriteAnimation component
            yield return new WaitForSeconds(delay / 2.0f);
            SpawnEnemy(targetSpawn.position);
        }
    }

    private void SpawnEnemy(Vector3 position)
    {
        Instantiate(m_enemyPrefab.transform, position, Quaternion.identity);
        ++m_enemyCount;
    }
}
