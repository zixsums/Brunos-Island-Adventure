using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Ink.Runtime;
using RPG.Utility;

namespace RPG.UI
{
    public class UIDialogueState : UIBaseState
    {
        private VisualElement dialogueContainer;
        private Label dialogueText;
        private Story currentStory;
        private VisualElement nextButton;
        private VisualElement choicesGroup;
        private PlayerInput playerInputCmp;

        private bool hasChoices = false;


        public UIDialogueState(UIController ui) : base(ui) { }

        public override void EnterState()
        {
            dialogueContainer = controller.root.Q<VisualElement>("dialogue-container");
            dialogueText = controller.root.Q<Label>("dialogue-text");
            nextButton = dialogueContainer.Q<VisualElement>("next-button");
            choicesGroup = dialogueContainer.Q<VisualElement>("choices-group");

            dialogueContainer.style.display = DisplayStyle.Flex;
            playerInputCmp = GameObject.FindGameObjectWithTag(
                Constants.GAME_MANAGER_TAG
                ).GetComponent<PlayerInput>();
            playerInputCmp.SwitchCurrentActionMap(Constants.UI_ACTION_MAP);
        }

        public override void SelectButton()
        {
            UpdateDialogue();
        }

        public void SetStory(TextAsset inkJSON)
        {
            currentStory = new Story(inkJSON.text);
            UpdateDialogue();
        }

        public void UpdateDialogue()
        {
            dialogueText.text = currentStory.Continue();

            hasChoices = currentStory.currentChoices.Count > 0;

            if (hasChoices)
            {
                HandheldNewChoices(currentStory.currentChoices);
            }
            else
            {

                nextButton.style.display = DisplayStyle.Flex;
                choicesGroup.style.display = DisplayStyle.None;
            }
        }

        private void HandheldNewChoices(List<Choice> choices)
        {
            nextButton.style.display = DisplayStyle.None;
            choicesGroup.style.display = DisplayStyle.Flex;

            choicesGroup.Clear();
            controller.buttons.Clear();
        }
    }
}
