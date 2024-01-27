using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager m_gameManager;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (m_gameManager.CurrentState == EGameState.Intro)
        {
            m_gameManager.SkipIntro();
        }
        else if (m_gameManager.CurrentState == EGameState.Gameplay)
        {
            var directionVector = context.ReadValue<Vector2>();
            EMovementDirection direction = MovementUtils.DirectionVectorToEnum(directionVector);

            // During gameplay, we can't go back to no direction, there is always a set direction
            if (direction != EMovementDirection.None)
            {
                m_gameManager.Player.SetTargetDirection(direction);
            }
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (m_gameManager.CurrentState == EGameState.Intro)
            {
                m_gameManager.SkipIntro();
            }
            else if (m_gameManager.CurrentState == EGameState.Gameplay || m_gameManager.CurrentState == EGameState.Shooting)
            {
                m_gameManager.ToggleShooting();
            }
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            m_gameManager.TogglePause();
        }
    }
}
