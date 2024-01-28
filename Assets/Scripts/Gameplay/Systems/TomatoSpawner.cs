using System.Collections;
using UnityEngine;

public class TomatoSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector2 m_spawnCooldownRange = new Vector2(5.0f, 30.0f);

    [Header("References")]
    [SerializeField] private GameObject m_tomatoPrefab;

    private GameObject[] m_spawnLocations;

    private float m_elapsed = 0.0f;
    private float m_nextSpawnTimer = 0.0f;

    private GameObject[] FindSpawnLocations()
    {
        return GameObject.FindGameObjectsWithTag("TomatoSpawn");
    }

    private void Start()
    {
        m_spawnLocations = FindSpawnLocations();
        SpawnTomato();
        FindNextSpawnTimer();
    }

    private void FindNextSpawnTimer()
    {
        m_elapsed = 0.0f;
        m_nextSpawnTimer = Random.Range(m_spawnCooldownRange.x, m_spawnCooldownRange.y);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState == EGameState.Intro) return;

        m_elapsed += Time.deltaTime;

        if (m_elapsed >= m_nextSpawnTimer)
        {
            FindNextSpawnTimer();
            SpawnTomato();
        }
    }


    private void SpawnTomato()
    {
        Debug.Assert(m_spawnLocations.Length > 0, "No spawn location found!");
        Transform targetSpawn = m_spawnLocations[Random.Range(0, m_spawnLocations.Length)].transform;
        Instantiate(m_tomatoPrefab, targetSpawn.position, Quaternion.identity);
    }
}
