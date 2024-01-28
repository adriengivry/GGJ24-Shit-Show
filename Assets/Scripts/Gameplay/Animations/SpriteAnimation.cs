using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer m_spriteRenderer;

    [Header("Settings")]
    [SerializeField] private Sprite m_alternativeSprite;
    [SerializeField] private float m_duration;

    private void Start()
    {
        //Animate();
    }

    public void Animate()
    {
        var previousSprite = m_spriteRenderer.sprite;
        m_spriteRenderer.sprite = m_alternativeSprite;
        StartCoroutine(RevertSpriteAfterDelay(previousSprite, m_duration));
    }

    private IEnumerator RevertSpriteAfterDelay(Sprite sprite, float delay)
    {
        yield return new WaitForSeconds(delay);
        m_spriteRenderer.sprite = sprite;
    }
}
