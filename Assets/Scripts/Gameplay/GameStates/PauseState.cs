using UnityEngine;

public class PauseState : AGameState
{
    [SerializeField] private GameObject m_pauseUI;

    public override void OnEnterState(GameManager manager)
    {
        Time.timeScale = 0.0f;
        m_pauseUI.SetActive(true);
    }

    public override void OnExitState(GameManager manager)
    {
        Time.timeScale = 1.0f;
        m_pauseUI.SetActive(false);
    }
}