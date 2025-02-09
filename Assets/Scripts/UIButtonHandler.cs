using UnityEngine;
using UnityEngine.UIElements;

public class UIButtonHideUI : MonoBehaviour
{
    private VisualElement uiPanel;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Get the button and the UI panel
        Button myButton = root.Q<Button>("myButton");
        uiPanel = root.Q<VisualElement>("uiPanel"); // Ensure this matches your UI container's name

        // Hide the UI panel at start
        uiPanel.style.display = DisplayStyle.None;

        // Attach button click event to toggle the UI
        myButton.clicked += ToggleUI;
    }

    private void ToggleUI()
    {
        if (uiPanel.style.display == DisplayStyle.None)
        {
            uiPanel.style.display = DisplayStyle.Flex;
        }
        else
        {
            uiPanel.style.display = DisplayStyle.None;
        }
    }
}
