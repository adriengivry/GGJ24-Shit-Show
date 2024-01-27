using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Color m_unselectedColor;
    [SerializeField] private Color m_selectedColor;
    [SerializeField] private Color m_blockedColor;

    private EState m_state;

    public enum EState
    {
        Hidden,
        Unselected,
        Selected,
        Blocked
    }

    public void SetState(EState state)
    {
        m_state = state;
        switch (state)
        {
            case EState.Hidden:
                m_spriteRenderer.enabled = false;
                break;

            case EState.Selected:
                m_spriteRenderer.enabled = true;
                m_spriteRenderer.color = m_selectedColor;
                break;

            case EState.Unselected:
                m_spriteRenderer.enabled = true;
                m_spriteRenderer.color = m_unselectedColor;
                break;

            case EState.Blocked:
                m_spriteRenderer.enabled = true;
                m_spriteRenderer.color = m_blockedColor;
                break;
        }
    }
}
