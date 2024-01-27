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

    public bool TryGetFlagAtPosition(Vector2 position, out Flag foundFlag)
    {
        foreach (Flag flag in m_flags)
        {
            if (Vector3.Distance(position, flag.transform.position) <= m_positionDetectionThreshold)
            {
                foundFlag = flag;
                return true;
            }
        }

        foundFlag = null;
        return false;
    }
}
