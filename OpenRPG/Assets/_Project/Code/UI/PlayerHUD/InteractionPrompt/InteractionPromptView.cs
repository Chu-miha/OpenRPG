using UnityEngine;
using UnityEngine.UIElements;

public class InteractionPromptView
{
    private readonly VisualElement _root;
    private readonly Label _text;

    public InteractionPromptView(VisualElement root)
    {
        _root = root;

        _text = root.Q<Label>("InteractionPromptText");

        Hide();
    }

    public void Show(string message)
    {
        _text.text = message;

        _root.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        _root.style.display = DisplayStyle.None;
    }
    
    public void Update(IInteractable interactable)
    {
        if(interactable == null)
        {
            Hide();
            return;
        }


        if(interactable is IInteractionInfo info)
        {
            Show(info.InteractionText);
            return;
        }

        Hide();
    }

}
