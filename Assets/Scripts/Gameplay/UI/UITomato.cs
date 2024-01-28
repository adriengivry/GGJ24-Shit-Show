using TMPro;
using UnityEngine;

public class UITomato : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_text;

    private string m_initialText;

    private void Awake()
    {
        m_initialText = m_text.text;
    }

    private void Start()
    {
        Refresh(GameManager.Instance.Tomatoes);
        GameManager.Instance.TomatoCountChangedEvent.AddListener(Refresh);
    }

    private void OnDestroy()
    {
        GameManager.Instance.TomatoCountChangedEvent.RemoveListener(Refresh);
    }

    public void Refresh(int tomatoes)
    {
        m_text.text = m_initialText.Replace("{value}", tomatoes.ToString());
    }
}
