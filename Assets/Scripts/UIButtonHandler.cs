using UnityEngine;
using UnityEngine.UIElements;

public class UIButtonHideUI : MonoBehaviour
{
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Get the button and the UI panel (or root)
        Button myButton = root.Q<Button>("myButton");
        VisualElement uiPanel = root.Q<VisualElement>("uiPanel"); // Name of the UI container

        // Register the button click event
        myButton.clicked += () => uiPanel.style.display = uiPanel.style.display == DisplayStyle.None ? DisplayStyle.Flex : DisplayStyle.None;

    }
}
