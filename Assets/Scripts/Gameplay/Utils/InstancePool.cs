using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstancePool : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject m_instancePrefab = null;

    [Header("Settings")]
    [SerializeField] private string m_instancePrefix = "Instance_";
    [SerializeField] protected int m_poolSize = 3;
    [SerializeField] private bool m_autoDisableInstances = true;

    protected GameObject[] m_instances = null;

    public GameObject[] instances => m_instances;

    private void Awake()
    {
        m_instances = new GameObject[m_poolSize];

        for (int i = 0; i < m_poolSize; ++i)
        {
            GameObject instance = Instantiate(m_instancePrefab, Vector3.zero, Quaternion.identity, transform);

            instance.name = m_instancePrefix + i;

            if (m_autoDisableInstances)
            {
                instance.SetActive(false);
            }

            m_instances[i] = instance;
        }
    }

    public GameObject GetAvailableInstance()
    {
        GameObject instance = Array.Find(m_instances, (element) => !element.activeInHierarchy);

        if (!instance)
        {
            Debug.LogWarning("Could not find available instance in pool, consider expanding the pool size");
        }

        return instance;
    }

    public void DisableAll()
    {
        for (int i = 0; i < m_poolSize; ++i)
        {
            m_instances[i].SetActive(false);
        }
    }
}
