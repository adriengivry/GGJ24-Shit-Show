using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverState : AGameState
{
    [Header("Settings")]
    [SerializeField] private float m_duration = 5.0f;
    [SerializeField] private string m_nextSceneName = "Main Menu";
    [SerializeField] private TextMeshProUGUI m_text;

    [Header("References")]
    [SerializeField] private GameObject m_gameOverUI;

    public override void OnEnterState(GameManager manager)
    {
        m_gameOverUI.SetActive(true);
        m_text.text = m_text.text.Replace("{score}", manager.Score.ToString());
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