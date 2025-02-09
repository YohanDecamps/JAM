using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log($"Player {playerInput.playerIndex} joined with device: {playerInput.devices[0].name}");
        // Get all the game objects with the tag "PlayerController" and with no parents
        GameObject[] playerControllers = GameObject.FindGameObjectsWithTag("PlayerController");
        // Filter out the player controllers that have a parent
        playerControllers = System.Array.FindAll(playerControllers, playerController => playerController.transform.parent == null);
        

        // Loop through all the player controllers
        foreach (GameObject playerController in playerControllers)
        {
            // Get all the game objects with the tag "NPC"
            GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
            // Get a random NPC and child the player controller to it
            GameObject npc = npcs[Random.Range(0, npcs.Length)];
            playerController.transform.parent = npc.transform;
            // Reset the position of the player controller
            playerController.transform.localPosition = Vector3.zero;
            playerController.transform.localRotation = Quaternion.identity;
            // Change the tag of the NPC to "Player"
            npc.tag = "Player";
            // Change the name of the NPC to the name of the player controller
            npc.name = "Player" + playerInput.playerIndex;
            // Remove the nav mesh agent from the NPC
            Destroy(npc.GetComponent<UnityEngine.AI.NavMeshAgent>());
            // Remove the NPC script from the NPC
            Destroy(npc.GetComponent<NpcBehaviourScript>());
        }
    }
}
