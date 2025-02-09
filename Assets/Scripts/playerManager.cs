using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    // Maps player indices to their controlled NPCs
    private readonly Dictionary<int, GameObject> playerToNPCMap = new();

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
        controller.transform.localPosition = Vector3.zero;
        controller.transform.localRotation = Quaternion.identity;

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
            // Re-enable NPC-specific components
            npc.AddComponent<NpcBehaviourScript>();
            npc.AddComponent<UnityEngine.AI.NavMeshAgent>();
        }
    }

    private void ResetNPC(GameObject npc)
    {
        // Reset NPC properties
        npc.tag = "NPC";
        npc.name = "NPC";
        
        // Optional: Add visual/behavioral reset logic here
    }
}
