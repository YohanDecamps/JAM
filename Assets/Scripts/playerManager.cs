using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log($"Player {playerInput.playerIndex} joined with device: {playerInput.devices[0].name}");
    }
}
