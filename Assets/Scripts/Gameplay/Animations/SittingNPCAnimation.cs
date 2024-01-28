using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingNPCAnimation : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector2 m_speedRange = new Vector2(1.0f, 5.0f);
    [SerializeField] private Vector2 m_amplitudeRange = new Vector2(0.0f, 10.0f);

    private float m_speed = 0.0f;
    private float m_offset = 0.0f;
    private float m_amplitude = 0.0f;

    private void Start()
    {
        m_speed = Random.Range(m_speedRange.x, m_speedRange.y);
        m_offset = Random.Range(0.0f, 10.0f);
        m_amplitude = Random.Range(m_amplitudeRange.x, m_amplitudeRange.y);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Sin((Time.time + m_offset) * m_speed) * m_amplitude);
    }
}
