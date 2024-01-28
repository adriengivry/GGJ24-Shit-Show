using AYellowpaper.SerializedCollections;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameOverState : AGameState
{
    [Header("Settings")]
    [SerializeField] private float m_duration = 5.0f;
    [SerializeField] private string m_nextSceneName = "Main Menu";
    [SerializeField] private TextMeshProUGUI m_scoreText;
    [SerializeField] private TextMeshProUGUI m_quoteText;

    [Header("References")]
    [SerializeField] private GameObject m_gameOverUI;
    [SerializeField] private SerializedDictionary<int, string> m_quotePerScoreMilestone;

    public override void OnEnterState(GameManager manager)
    {
        m_gameOverUI.SetActive(true);

        foreach (var milestone in m_quotePerScoreMilestone)
        {
            if (manager.Score >= milestone.Key)
            {
                // If the score meets or exceeds the milestone, display the corresponding quote
                m_quoteText.text = milestone.Value;
                break; // Exit the loop after finding the first milestone that has been reached
            }
        }

        m_scoreText.text = m_scoreText.text.Replace("{score}", manager.Score.ToString());
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