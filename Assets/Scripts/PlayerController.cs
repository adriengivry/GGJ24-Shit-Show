using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Character m_character;

    public void OnMove(InputAction.CallbackContext context)
    {
        var directionVector = context.ReadValue<Vector2>();
        EMovementDirection direction = MovementUtils.DirectionVectorToEnum(directionVector);

        // During gameplay, we can't go back to no direction, there is always a set direction
        if (direction != EMovementDirection.None)
        {
            m_character.SetTargetDirection(direction);
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
    }
}
