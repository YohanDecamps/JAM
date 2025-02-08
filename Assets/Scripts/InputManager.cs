using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private readonly List<IInputObserver> observers = new();

    // Reference to the Input Action Asset
    public InputActionAsset inputActions;

    // Input Actions
    private InputAction shootAction;
    private InputAction dashAction;
    private InputAction menuAction;
    private InputAction pauseAction;
    private InputAction deplacementJoueurAction; // Movement action
    private InputAction emoteAction;

    private void Awake()
    {
        // Enable the Input Action Asset
        inputActions.Enable();

        // Find the actions
        shootAction = inputActions.FindAction("Fire");
        dashAction = inputActions.FindAction("Dash");
        menuAction = inputActions.FindAction("Menu");
        pauseAction = inputActions.FindAction("Pause");
        deplacementJoueurAction = inputActions.FindAction("Movement");
        emoteAction = inputActions.FindAction("Emote");

        // Subscribe to the "performed" event
        shootAction.performed += OnShootPerformed;
        dashAction.performed += OnDashPerformed;
        menuAction.performed += OnMenuPerformed;
        pauseAction.performed += OnPausePerformed;
        deplacementJoueurAction.performed += OnMovePerformed;
        emoteAction.performed += OnEmotePerformed;

        // Subscribe to the "canceled" event for movement to handle stopping
        deplacementJoueurAction.canceled += OnMoveCanceled;
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        int playerId = GetPlayerId(context);
        NotifyObservers("Fire", playerId);
    }

    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        int playerId = GetPlayerId(context);
        NotifyObservers("Dash", playerId);
    }

    private void OnMenuPerformed(InputAction.CallbackContext context)
    {
        int playerId = GetPlayerId(context);
        NotifyObservers("Menu", playerId);
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        int playerId = GetPlayerId(context);
        NotifyObservers("Pause", playerId);
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        int playerId = GetPlayerId(context);
        Vector2 movement = context.ReadValue<Vector2>(); // Read the movement input
        NotifyMovementObservers(movement, playerId); // Notify observers with movement direction
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        int playerId = GetPlayerId(context);
        NotifyMovementObservers(Vector2.zero, playerId); // Notify observers that movement has stopped
    }

    private void OnEmotePerformed(InputAction.CallbackContext context)
    {
        int playerId = GetPlayerId(context);
        NotifyObservers("Emote", playerId);
    }

    private int GetPlayerId(InputAction.CallbackContext context)
    {
        // Assuming that the player ID is stored in the control's device name or some other property
        // You may need to adjust this logic based on how your input devices are set up
        return context.control.device.deviceId;
    }

    public void AddObserver(IInputObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IInputObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyObservers(string key, int playerId)
    {
        foreach (var observer in observers)
        {
            observer.OnKeyPressed(key, playerId);
        }
    }

    private void NotifyMovementObservers(Vector2 movement, int playerId)
    {
        foreach (var observer in observers)
        {
            observer.OnMovePerformed(movement, playerId); // Notify observers with movement direction
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the events to avoid memory leaks
        shootAction.performed -= OnShootPerformed;
        dashAction.performed -= OnDashPerformed;
        menuAction.performed -= OnMenuPerformed;
        pauseAction.performed -= OnPausePerformed;
        deplacementJoueurAction.performed -= OnMovePerformed;
        deplacementJoueurAction.canceled -= OnMoveCanceled;
        emoteAction.performed -= OnEmotePerformed;
    }
}