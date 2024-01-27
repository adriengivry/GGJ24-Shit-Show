using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverState : AGameState
{
    [Header("Settings")]
    [SerializeField] private float m_duration = 5.0f;
    [SerializeField] private string m_nextSceneName = "Main Menu";

    [Header("References")]
    [SerializeField] private GameObject m_gameOverUI;

    public override void OnEnterState(GameManager manager)
    {
        m_gameOverUI.SetActive(true);

        StartCoroutine(GoToScene(m_nextSceneName, m_duration));
    }

    private IEnumerator GoToScene(string scene, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }

    public override void OnExitState(GameManager manager)
    {
        m_gameOverUI.SetActive(false);
    }
}