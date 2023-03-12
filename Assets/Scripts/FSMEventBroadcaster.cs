using UnityEngine;

public class FSMEventBroadcaster : MonoBehaviour
{
    public void BroadcastNewState(string newState)
    {
        EventManager.TriggerEvent(newState);
    }
}

public struct GameStates
{
    public const string MAIN_MENU = "MainMenu";
    public const string INSTRUCTIONS = "Instructions";
    public const string COUNTDOWN = "Countdown";
    public const string GAME = "Game";
    public const string PAUSE = "Pause";
    public const string GAME_OVER = "GameOver";
}
