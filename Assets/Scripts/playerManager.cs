using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    private readonly Dictionary<int, GameObject> playerToNPCMap = new();
    private UIDocument uiDocument;
    private VisualElement uiPanel;
    private Button messageLabel;

    private void Start()
    {
        // Find the UIDocument in the scene
        uiDocument = FindObjectOfType<UIDocument>();
        
        if (uiDocument != null)
        {
            var root = uiDocument.rootVisualElement;
            uiPanel = root.Q<VisualElement>("uiPanel");  // UI container
            messageLabel = root.Q<Button>("myButton"); // Text element

            // Ensure UI is hidden at the start
            uiPanel.style.display = DisplayStyle.None;
        }
        else
        {
            Debug.LogWarning("UIDocument not found in the scene.");
        }
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log($"Player {playerInput.playerIndex} joined");
        
        GameObject playerController = playerInput.gameObject;
        
        if (playerController.transform.parent != null)
        {
            Debug.LogWarning("PlayerController already parented. Skipping setup.");
            return;
        }

        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
        if (npcs.Length == 0)
        {
            Debug.LogError("No available NPCs!");
            return;
        }

        // Select and setup NPC
        GameObject npc = npcs[Random.Range(0, npcs.Length)];
        SetupPlayerNPC(playerInput, playerController, npc);
    }

     private void SetupPlayerNPC(PlayerInput input, GameObject controller, GameObject npc)
    {
        // Parent controller to NPC
        controller.transform.SetParent(npc.transform);
        controller.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        // Update NPC metadata
        npc.tag = "Player";
        npc.name = $"Player{input.playerIndex}";

        // Track relationship
        playerToNPCMap[input.playerIndex] = npc;

        // Remove NPC-specific components
        Destroy(npc.GetComponent<NpcBehaviourScript>());
        Destroy(npc.GetComponent<UnityEngine.AI.NavMeshAgent>());
    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {
        Debug.Log($"Player {playerInput.playerIndex} left");

        if (playerToNPCMap.TryGetValue(playerInput.playerIndex, out GameObject npc))
        {
            ResetNPC(npc);
            playerToNPCMap.Remove(playerInput.playerIndex);
            npc.AddComponent<NpcBehaviourScript>();
            npc.AddComponent<UnityEngine.AI.NavMeshAgent>();
        }
    }

    private void ResetNPC(GameObject npc)
    {
        // Reset NPC properties
        npc.tag = "NPC";
        npc.name = "NPC";

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
        if (players.Length <= 1)
        {
            string message;

            if (players.Length == 0)
            {
                message = "Draw! ...somehow...";
            }
            else
            {
                message = $"{players[0].name} wins!";
            }

            // Update UI text and show panel
            ShowUI(message);
        }
    }

    private void ShowUI(string message)
    {
        if (uiPanel != null && messageLabel != null)
        {
            messageLabel.text = message;
            uiPanel.style.display = DisplayStyle.Flex;
        }
        else
        {
            Debug.LogWarning("UI Panel or Message Label is not assigned.");
        }
    }
}
