using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using UnityEngine.Events;

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
    [SerializeField] private FlagRegistry m_flagRegistry;
    [SerializeField] private SerializedDictionary<EGameState, AGameState> m_gameStates;

    public UnityEvent<int> ScoreChangedEvent = new UnityEvent<int>();
    public UnityEvent<int> TomatoCountChangedEvent = new UnityEvent<int>();

    public static GameManager Instance => m_instance;

    public Character Player => m_player;
    public FlagRegistry FlagRegistry => m_flagRegistry;
    public int Score => m_score;
    public int Tomatoes => m_tomatoes;

    public EGameState CurrentState => m_gameStateLayers.Peek();

    private Stack<EGameState> m_gameStateLayers = new Stack<EGameState>();

    private GameObject FindPlayerGameObject() => GameObject.FindGameObjectWithTag("Player");
    private Character m_player;
    private int m_score;
    private int m_tomatoes;

    private static GameManager m_instance = null;

    private void Awake()
    {
        m_instance = this;
        GameObject playerGameObject = FindPlayerGameObject();
        m_player = playerGameObject?.GetComponent<Character>();
        m_score = 0;
        m_tomatoes = 0;
        Debug.Assert(m_player != null, "No player has been found! Make sure to add the Player prefab in your scene.");
        PushState(m_initialGameState);
    }

    public void AddTomato()
    {
        ++m_tomatoes;
        TomatoCountChangedEvent.Invoke(m_tomatoes);
    }

    public void RemoveTomato()
    {
        --m_tomatoes;
        TomatoCountChangedEvent.Invoke(m_tomatoes);
    }

    public void IncrementScore(int value)
    {
        m_score += value;
        ScoreChangedEvent.Invoke(m_score);
    }

    public void SkipIntro()
    {
        Debug.Assert(m_gameStateLayers.TryPeek(out var layer) && layer == EGameState.Intro, "Cannot invoke SkipIntro if the intro is not the current game state");
        PopState();
        PushState(EGameState.Gameplay);
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
        else if (m_tomatoes > 0)
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
