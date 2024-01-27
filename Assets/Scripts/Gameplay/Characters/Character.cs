using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float m_moveSpeed = 1.0f;

    [Header("References")]
    [SerializeField] private Rigidbody2D m_rigidbody;

    [Header("Debug")]
    [SerializeField] private DirectionalArrow m_debugDirectionalArrow;

    public EMovementDirection CurrentDirection => m_currentDirection;

    public UnityEvent<Flag> FlagReachedEvent = new UnityEvent<Flag>();

    private EMovementDirection m_targetDirection = EMovementDirection.None;
    private EMovementDirection m_currentDirection = EMovementDirection.None;

    private Flag m_currentFlag = null;

    public void SetTargetDirection(EMovementDirection direction)
    {
        m_debugDirectionalArrow.SetDirection(direction);
        m_targetDirection = direction;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentState == EGameState.Gameplay || GameManager.Instance.CurrentState == EGameState.Shooting)
        {
            if (m_targetDirection != EMovementDirection.None &&
                (m_currentDirection == EMovementDirection.None || MovementUtils.AreOppositeDirections(m_targetDirection, m_currentDirection)))
            {
                m_currentDirection = m_targetDirection;
            }

            // Check if the character is close to a flag
            if (!m_currentFlag && GameManager.Instance.FlagRegistry.TryGetFlagAtPosition(transform.position, m_rigidbody.velocity, out Flag flag))
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
                    m_currentDirection = m_targetDirection;
                    m_currentFlag = null;
                }
                else
                {
                    m_currentDirection = EMovementDirection.None;
                }
            }

            Vector2 directionVector = MovementUtils.DirectionEnumToVector(m_currentDirection);

            m_rigidbody.velocity = directionVector * m_moveSpeed;
        }
        else
        {
            m_rigidbody.velocity = Vector2.zero;
        }
    }
}
