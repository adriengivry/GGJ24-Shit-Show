using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private EMovementDirection m_initialDirection;

    [Header("References")]
    [SerializeField] private GameManager m_gameManager;

    private void Start()
    {
        m_gameManager.Player.SetTargetDirection(m_initialDirection);
    }

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
            
            //if (m_animator != null) { 
            //    m_animator.SetFloat("X", directionVector.x);
            //    m_animator.SetFloat("Y", directionVector.y);
            //}

            // During gameplay, we can't go back to no direction, there is always a set direction
            if (direction != EMovementDirection.None)
            {
                m_gameManager.Player.SetTargetDirection(direction);
            }
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (m_gameManager.CurrentState == EGameState.Gameplay && context.started)
        {
            m_gameManager.StartShooting();
        }
        else if (m_gameManager.CurrentState == EGameState.Shooting && context.canceled)
        {
            m_gameManager.StopShooting();
        }
        else if (m_gameManager.CurrentState == EGameState.Intro && context.performed)
        { 
            m_gameManager.SkipIntro();
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
