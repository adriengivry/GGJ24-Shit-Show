using UnityEngine;

public class IntroState : AGameState
{
    [SerializeField] private GameObject m_pauseUI;

    public override void OnEnterState(GameManager manager)
    {
        m_pauseUI.SetActive(true);
    }

    public override void OnExitState(GameManager manager)
    {
        m_pauseUI.SetActive(false);
    }
}