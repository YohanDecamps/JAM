using UnityEngine;

public class KeyPressLogger : MonoBehaviour, IInputObserver
{
    public InputManager bruh;

    void Start() {
        bruh.AddObserver(this);
    }

    public void OnKeyPressed(string key, int playerId)
    {
        Debug.Log($"Action {key} was triggered by player {playerId}");
    }

    public void OnMovePerformed(Vector2 movement, int playerId)
    {
        Debug.Log($"player {playerId} moved: {movement}");
    }
}