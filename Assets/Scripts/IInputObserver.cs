using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInputObserver
{
    void OnKeyPressed(string key, int playerId);
    void OnMovePerformed(Vector2 movement, int playerId);
    int GetPlayerId();
}
