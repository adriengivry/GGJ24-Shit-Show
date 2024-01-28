using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Character character;
    [SerializeField] private Animator m_animator;

    private EMovementDirection CharacterDirection = EMovementDirection.None;
    private Vector2 m_MovementDirection = new Vector2(0f,0f);

    public void Start()
    {
        m_animator = GetComponent<Animator>();
        character = GetComponentInParent<Character>();
    }

    public void Update()
    {
        if (CharacterDirection != character.CurrentDirection)
        {
            CharacterDirection = character.CurrentDirection;
            m_MovementDirection = GetAngleFromDirection(character.CurrentDirection);

            if (m_animator != null)
            {
                m_animator.SetFloat("X", m_MovementDirection.x);
                m_animator.SetFloat("Y", m_MovementDirection.y);
            }
        }
    }

    private Vector2 GetAngleFromDirection(EMovementDirection direction)
    {
        switch (direction)
        {
            case EMovementDirection.Up: return new Vector2(0f,1f);
            case EMovementDirection.Right: return new Vector2(1f, 0f);
            case EMovementDirection.Down: return new Vector2(0f, -1f);
            case EMovementDirection.Left: return new Vector2(-1f, 0f);
        }

        return new Vector2(0f, 0f);
    }
}
