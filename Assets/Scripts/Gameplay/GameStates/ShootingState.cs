using UnityEngine;

public class ShootingState : AGameState
{
    [Header("Settings")]
    [SerializeField] private float m_timeScale = 0.5f;
    [SerializeField] private Vector2 m_fillSpeedRange = new Vector2(3.0f, 8.0f);
    [SerializeField] private float m_throwingStrength = 20.0f;
    [SerializeField] private Vector2 m_marginOfErrorRange = new Vector2(0.05f, 0.1f);

    [Header("References")]
    [SerializeField] private Transform m_throwDestination;
    [SerializeField] private Transform[] m_missedThrowLocations;
    [SerializeField] private Throwable m_throwable;
    [SerializeField] private UIShooting m_shootingUI;

    private float m_currentValue;
    private float m_optimalValue;

    private float m_startTime = 0.0f;
    private float m_currentMarginOfError = 0.0f;
    private float m_currentFillSpeed = 0.0f;

    public override void OnEnterState(GameManager manager)
    {
        m_startTime = Time.time;
        m_optimalValue = Random.Range(0.3f, 1.0f);
        Time.timeScale = m_timeScale;
        m_shootingUI.gameObject.SetActive(true);
        m_currentMarginOfError = Random.Range(m_marginOfErrorRange.x, m_marginOfErrorRange.y);
        m_currentFillSpeed = Random.Range(m_fillSpeedRange.x, m_fillSpeedRange.y);
        m_shootingUI.SetOptimalValue(m_optimalValue, m_currentMarginOfError);
    }

    public override void OnExitState(GameManager manager)
    {
        manager.RemoveTomato();

        if (Mathf.Abs(m_currentValue - m_optimalValue) <= m_currentMarginOfError)
        {
            Debug.Log("Successful throw!");
            m_throwable.Throw(manager.Player.transform.position, m_throwDestination.position, m_throwingStrength);
            manager.IncrementScore(1);
        }
        else
        {
            Debug.Log("Missed throw!");
            Debug.Assert(m_missedThrowLocations.Length > 0, "No missed throw locations set!");
            int locationIndex = Random.Range(0, m_missedThrowLocations.Length);
            m_throwable.Throw(manager.Player.transform.position, m_missedThrowLocations[locationIndex].position, m_throwingStrength);
        }

        Time.timeScale = 1.0f;
        m_shootingUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        m_currentValue = Mathf.PingPong((Time.time - m_startTime) * m_currentFillSpeed, 1.0f);
        m_shootingUI.SetValue(m_currentValue);
    }
}