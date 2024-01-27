using UnityEngine;

public class PauseState : AGameState
{
    public override void OnEnterState(GameManager manager)
    {
        Time.timeScale = 0.0f;
    }

    public override void OnExitState(GameManager manager)
    {
        Time.timeScale = 1.0f;
    }
}