using UnityEngine;

public abstract class AGameState : MonoBehaviour
{
    public abstract void OnEnterState(GameManager manager);
    public abstract void OnExitState(GameManager manager);
}