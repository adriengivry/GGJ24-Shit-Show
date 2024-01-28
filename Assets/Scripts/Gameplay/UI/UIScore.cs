using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_text;

    private string m_initialText;

    private void Awake()
    {
        m_initialText = m_text.text;
    }

    private void Start()
    {
        Refresh(GameManager.Instance.Score);
        GameManager.Instance.ScoreChangedEvent.AddListener(Refresh);
    }

    private void OnDestroy()
    {
        GameManager.Instance.ScoreChangedEvent.RemoveListener(Refresh);
    }

    public void Refresh(int newScore)
    {
        m_text.text = m_initialText.Replace("{value}", newScore.ToString());
    }
}
