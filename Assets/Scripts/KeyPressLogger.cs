using UnityEngine;

public class KeyPressLogger : MonoBehaviour, IInputObserver
{
    public InputManager bruh;

    void Start() {
        bruh.AddObserver(this);
    }

    public void OnKeyPressed(string key, int playerId)
    {
        Debug.Log($"Key pressed: {key} by player {playerId}");
    }

    public void OnMovePerformed(Vector2 movement, int playerId)
    {
        
    }

    public int GetPlayerId()
    {
        return -1;
    }
}