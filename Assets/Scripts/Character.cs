using UnityEngine;
using UnityEngine.Events;
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

    public EMovementDirection CurrentDirection => m_currentDirection;

    public UnityEvent<Flag> FlagReachedEvent = new UnityEvent<Flag>();

    private EMovementDirection m_targetDirection = EMovementDirection.None;
    // private EMovementDirection m_lastValidDirection = EMovementDirection.None;
    private EMovementDirection m_currentDirection = EMovementDirection.None;

    private Flag m_currentFlag = null;

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

    private void FixedUpdate()
    {
        if (m_targetDirection != EMovementDirection.None &&
            (m_currentDirection == EMovementDirection.None || MovementUtils.AreOppositeDirections(m_targetDirection, m_currentDirection)))
        {
            m_currentDirection = m_targetDirection;
        }

        // Check if the character is close to a flag
        if (!m_currentFlag && m_flagRegistry.TryGetFlagAtPosition(transform.position, m_rigidbody.velocity, out Flag flag))
        {
            if (flag != m_currentFlag)
            {
                m_currentFlag = flag;

                // Snap the character position to the flag position (Make sure the player is always properly aligned with flags)
                m_rigidbody.MovePosition(flag.transform.position);

                FlagReachedEvent.Invoke(flag);
            }
        }
        else if (m_currentFlag)
        {
            if (m_currentFlag.CanMoveInDirection(m_targetDirection))
            {
                // m_lastValidDirection = m_targetDirection;
                m_currentDirection = m_targetDirection;
                m_currentFlag = null;
            }
            else
            {
                m_currentDirection = EMovementDirection.None;
            }
        }

        // Update current direction when approaching an intersection

        Vector2 directionVector = MovementUtils.DirectionEnumToVector(m_currentDirection);

        m_rigidbody.velocity = directionVector * m_moveSpeed;
    }
}
