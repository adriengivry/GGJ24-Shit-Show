using UnityEngine;

public class FlagRegistry : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float m_positionDetectionThreshold = 0.2f;

    private Flag[] m_flags;

    private void Awake()
    {
        m_flags = FindObjectsOfType<Flag>();
    }

    public bool TryGetFlagAtPosition(Vector2 position, Vector2 direction, out Flag foundFlag)
    {
        foreach (Flag flag in m_flags)
        {
            Vector2 flagDirection = (new Vector2(flag.transform.position.x, flag.transform.position.y) - position).normalized;
            float dotProduct = Vector2.Dot(flagDirection, direction);

            if (dotProduct > 0 && Vector3.Distance(position, flag.transform.position) <= m_positionDetectionThreshold)
            {
                foundFlag = flag;
                return true;
            }
        }

        foundFlag = null;
        return false;
    }
}
