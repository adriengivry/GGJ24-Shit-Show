using UnityEngine;

public class ShootingState : AGameState
{
    [Header("Settings")]
    [SerializeField] private float m_timeScale = 0.5f;

    [Header("References")]
    [SerializeField] private GameObject m_shootingUI;

    public override void OnEnterState(GameManager manager)
    {
        Time.timeScale = m_timeScale;
        m_shootingUI.SetActive(true);
    }

    public override void OnExitState(GameManager manager)
    {
        Time.timeScale = 1.0f;
        m_shootingUI.SetActive(false);
    }
}