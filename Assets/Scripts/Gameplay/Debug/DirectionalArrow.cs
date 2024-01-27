using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{
    [SerializeField] private GameObject m_arrowVisual;

    public void SetDirection(EMovementDirection direction)
    {
        m_arrowVisual.SetActive(direction != EMovementDirection.None);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, GetAngleFromDirection(direction));
    }

    private float GetAngleFromDirection(EMovementDirection direction)
    {
        switch (direction)
        {
            case EMovementDirection.Up: return 0.0f;
            case EMovementDirection.Right: return -90.0f;
            case EMovementDirection.Down: return -180.0f;
            case EMovementDirection.Left: return 90.0f;
        }

        return 0.0f;
    }
}
