using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public HashSet<EMovementDirection> AllowedDirections => m_allowedDirections;

    private HashSet<EMovementDirection> m_allowedDirections;

    public bool CanMoveInDirection(EMovementDirection direction)
    {
        return m_allowedDirections.Contains(direction);
    }

    private void CheckDirection(Vector2 direction)
    {
        // Cast a ray and get all hits
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, 1.0f);

        // Check if any of the hits are obstacles
        foreach (RaycastHit2D hit in hits)
        {
            // You can customize this condition based on your game logic and obstacle properties
            if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
            {
                return;
            }
        }

        m_allowedDirections.Add(MovementUtils.DirectionVectorToEnum(direction));
    }

    private void Awake()
    {
        m_allowedDirections = new HashSet<EMovementDirection>();

        CheckDirection(Vector2.up);
        CheckDirection(Vector2.left);
        CheckDirection(Vector2.down);
        CheckDirection(Vector2.right);
    }

    private void Update()
    {
        foreach (var direction in m_allowedDirections)
        {
            Vector2 dirVector = MovementUtils.DirectionEnumToVector(direction);
            Debug.DrawRay(transform.position, dirVector, Color.green);
        }
    }
}
