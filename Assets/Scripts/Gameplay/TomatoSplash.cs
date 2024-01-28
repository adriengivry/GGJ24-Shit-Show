using UnityEngine;

public class TomatoSplash : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float m_duration = 1.0f;
    [SerializeField] private float m_delayBeforeFade = 1.0f;

    [Header("Reference")]
    [SerializeField] private SpriteRenderer m_spriteRenderer;

    private float m_elapsed = 0.0f;

    private void OnEnable()
    {
        m_elapsed = 0.0f;
        m_spriteRenderer.color = Color.white;
    }

    private void Update()
    {
        m_elapsed += Time.deltaTime;

        if (m_elapsed >= m_delayBeforeFade)
        {
            float fadeCoeff = (m_elapsed - m_delayBeforeFade) / (m_duration - m_delayBeforeFade);
            m_spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - fadeCoeff);
        }

        if (m_elapsed >= m_duration)
        {
            gameObject.SetActive(false);
        }
    }
}
