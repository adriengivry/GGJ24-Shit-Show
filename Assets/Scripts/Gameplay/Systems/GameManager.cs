using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public enum EGameState
{
    Intro,
    Gameplay,
    Shooting,
    Pause,
    GameOver
}

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private EGameState m_initialGameState;

    [Header("References")]
    [SerializeField] private SerializedDictionary<EGameState, AGameState> m_gameStates;

    public EGameState CurrentState => m_gameStateLayers.Peek();

    private Stack<EGameState> m_gameStateLayers = new Stack<EGameState>();

    private void Awake()
    {
        PushState(m_initialGameState);
    }

    public void TogglePause()
    {
        if (m_gameStateLayers.TryPeek(out var layer) && layer == EGameState.Pause)
        {
            PopState();
        }
        else
        {
            PushState(EGameState.Pause);
        }
    }

    public void ToggleShooting()
    {
        if (m_gameStateLayers.TryPeek(out var layer) && layer == EGameState.Shooting)
        {
            PopState();
        }
        else
        {
            PushState(EGameState.Shooting);
        }
    }

    public void PushState(EGameState gameState)
    {
        Debug.Assert(!m_gameStateLayers.Contains(gameState), "Cannot push a state that is already registered");
        m_gameStateLayers.Push(gameState);
        m_gameStates[gameState].OnEnterState(this);
    }

    public void PopState()
    {
        Debug.Assert(m_gameStateLayers.Count > 0, "Cannot pop state if no state is currently registered");
        EGameState current = m_gameStateLayers.Peek();
        m_gameStates[current].OnExitState(this);
        m_gameStateLayers.Pop();
    }
}
