using System;

public static class EventBroadcaster
{
    public static void GameStarted()
    {
        OnGameStarted?.Invoke();
    }
    public static event Action OnGameStarted;

    public static void GamePaused()
    {
        OnGamePaused?.Invoke();
    }
    public static event Action OnGamePaused;

    public static void GameResumed()
    {
        OnGameResumed?.Invoke();
    }
    public static event Action OnGameResumed;

    public static void GameFinished()
    {
        OnGameFinished?.Invoke();
    }
    public static event Action OnGameFinished;

    public static void RiddleFinished(IRiddle riddle)
    {
        OnRiddleFinished?.Invoke(riddle);
    }
    public static event Action<IRiddle> OnRiddleFinished;

    public static void ConnectionMade(Plug input, Plug output)
    {
        OnConnectionMade?.Invoke(input, output);
    }
    public static event Action<Plug, Plug> OnConnectionMade;

    public static void PlugDisconnected(Plug p)
    {
        OnPlugDisconnected?.Invoke(p);
    }
    public static event Action<Plug> OnPlugDisconnected;

    public static void KnobValueChanged(Knob k, float value)
    {
        OnKnobValueChanged?.Invoke(k, value);
    }
    public static event Action<Knob, float> OnKnobValueChanged;

}
