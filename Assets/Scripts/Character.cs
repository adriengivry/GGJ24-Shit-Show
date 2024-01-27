using UnityEngine;
using UnityEngine.InputSystem;
public class Character : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float m_moveSpeed = 1.0f;

    [Header("References")]
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private FlagRegistry m_flagRegistry;

    [Header("Debug")]
    [SerializeField] private DirectionalArrow m_leftArrow;
    [SerializeField] private DirectionalArrow m_rightArrow;
    [SerializeField] private DirectionalArrow m_upArrow;
    [SerializeField] private DirectionalArrow m_downArrow;

    private EMovementDirection m_targetDirection = EMovementDirection.None;
    private EMovementDirection m_lastValidDirection = EMovementDirection.None;
    private EMovementDirection m_currentDirection = EMovementDirection.None;
    private Flag m_lastFlag = null;

    public void SetTargetDirection(EMovementDirection direction)
    {
        m_leftArrow.SetState(
            direction == EMovementDirection.Left ? DirectionalArrow.EState.Selected :
            DirectionalArrow.EState.Unselected);

        m_rightArrow.SetState(
            direction == EMovementDirection.Right ? DirectionalArrow.EState.Selected :
            DirectionalArrow.EState.Unselected);

        m_downArrow.SetState(
            direction == EMovementDirection.Down ? DirectionalArrow.EState.Selected :
            DirectionalArrow.EState.Unselected);

        m_upArrow.SetState(
            direction == EMovementDirection.Up ? DirectionalArrow.EState.Selected :
            DirectionalArrow.EState.Unselected);

        m_targetDirection = direction;
    }

    private void Update()
    {
        if (m_targetDirection != EMovementDirection.None && m_currentDirection == EMovementDirection.None)
        {
            m_currentDirection = m_targetDirection;
        }

        // Check if the character is close to a flag
        if (m_flagRegistry.TryGetFlagAtPosition(transform.position, out Flag flag))
        {
            if (flag != m_lastFlag)
            {
                // Snap the character position to the flag position (Make sure the player is always properly aligned with flags)
                m_rigidbody.MovePosition(flag.transform.position);

                if (flag.CanMoveInDirection(m_targetDirection) && !MovementUtils.AreOppositeDirections(m_targetDirection, m_lastValidDirection))
                {
                    m_lastValidDirection = m_targetDirection;
                    m_currentDirection = m_targetDirection;
                    m_lastFlag = flag;
                }
                else
                {
                    m_currentDirection = EMovementDirection.None;
                }
            }
        }

        // Update current direction when approaching an intersection

        Vector2 directionVector = DirectionEnumToVector(m_currentDirection);

        m_rigidbody.velocity = directionVector * m_moveSpeed;
    }

    private EMovementDirection DirectionVectorToEnum(Vector2 direction)
    {
        if (direction.x < 0) return EMovementDirection.Left;
        if (direction.x > 0) return EMovementDirection.Right;
        if (direction.y < 0) return EMovementDirection.Down;
        if (direction.y > 0) return EMovementDirection.Up;

        return EMovementDirection.None;
    }

    private Vector2 DirectionEnumToVector(EMovementDirection direction)
    {
        switch (m_currentDirection)
        {
            case EMovementDirection.Left: return Vector2.left;
            case EMovementDirection.Right: return Vector2.right;
            case EMovementDirection.Down: return Vector2.down;
            case EMovementDirection.Up: return Vector2.up;
        }

        return Vector2.zero;
    }
}
