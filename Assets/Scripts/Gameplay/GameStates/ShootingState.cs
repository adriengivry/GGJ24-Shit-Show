using UnityEngine;

public class ShootingState : AGameState
{
    [Header("Settings")]
    [SerializeField] private float m_timeScale = 0.5f;
    [SerializeField] private float m_fillSpeed = 1.0f;
    [SerializeField] private float m_marginOfError = 0.1f;

    [Header("References")]
    [SerializeField] private Transform m_throwDestination;
    [SerializeField] private Throwable m_throwable;
    [SerializeField] private UIShooting m_shootingUI;

    private float m_currentValue;
    private float m_optimalValue;

    private float m_startTime = 0.0f;

    public override void OnEnterState(GameManager manager)
    {
        m_startTime = Time.time;
        m_optimalValue = Random.Range(0.0f, 1.0f);
        Time.timeScale = m_timeScale;
        m_shootingUI.gameObject.SetActive(true);
        m_shootingUI.SetOptimalValue(m_optimalValue, m_marginOfError);
    }

    public override void OnExitState(GameManager manager)
    {
        if (Mathf.Abs(m_currentValue - m_optimalValue) <= m_marginOfError)
        {
            Debug.Log("Successful throw!");
            m_throwable.Throw(manager.Player.transform.position, m_throwDestination.position);
            // Successfull throw
        }
        else
        {
            Debug.Log("Missed throw!");
            // Missed throw
        }

        Time.timeScale = 1.0f;
        m_shootingUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        m_currentValue = Mathf.PingPong((Time.time - m_startTime) * m_fillSpeed, 1.0f);
        m_shootingUI.SetValue(m_currentValue);
    }
}