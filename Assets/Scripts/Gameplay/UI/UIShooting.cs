using UnityEngine;
using UnityEngine.UI;

public class UIShooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider m_slider;
    [SerializeField] private RectTransform m_sweetSpotZone;

    public static float Remap(float value, float low1, float high1, float low2, float high2) =>
        low2 + (value - low1) * (high2 - low2) / (high1 - low1);

    public void SetOptimalValue(float optimalValue)
    {
        float containerH = ((RectTransform)m_sweetSpotZone.parent.transform).rect.height;
        float sweetSpotZoneH = m_sweetSpotZone.rect.height;
        float h = (containerH / 2.0f) - (sweetSpotZoneH / 2.0f);
        float posY = Remap(optimalValue, 0.0f, 1.0f, -h, h);

        m_sweetSpotZone.localPosition = new Vector3(
            m_sweetSpotZone.localPosition.x,
            posY,
            m_sweetSpotZone.localPosition.z
        );
    }

    public void SetValue(float currentValue)
    {
        m_slider.value = currentValue;
    }
}
