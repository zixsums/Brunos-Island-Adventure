using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using RPG.Core;

namespace RPG.UI
{
    public class UIController : MonoBehaviour
    {
        private UIDocument uiDocumentCmp;
        public VisualElement root;
        public List<Button> buttons;
        public VisualElement mainMenuContainer;
        public VisualElement playerInfoContainer;
        public Label healthLabel;
        public Label potionsLabel;

        public UIBaseState currentState;
        public UIMainMenuState mainMenuState;
        public UIDialogueState dialogueState;

        public int currentSelection = 0;

        private void Awake()
        {

            uiDocumentCmp = GetComponent<UIDocument>();
            root = uiDocumentCmp.rootVisualElement;


            mainMenuContainer = root.Q<VisualElement>("main-menu-container");
            playerInfoContainer = root.Q<VisualElement>("player-info-container");
            healthLabel = playerInfoContainer.Q<Label>("health-label");
            potionsLabel = playerInfoContainer.Q<Label>("potions-label");

            mainMenuState = new UIMainMenuState(this);
            dialogueState = new UIDialogueState(this);
        }

        private void OnEnable()
        {
            EventManager.OnChangePlayerHealth += HandleChangePlayerHealth;
            EventManager.OnChangePlayerPotions += HandleChangePlayerPotions;
            EventManager.OnInitiateDialogue += HandleInitiateDialogue;
        }


        // Start is called before the first frame update
        void Start()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (sceneIndex == 0)
            {
                currentState = mainMenuState;
                currentState.EnterState();
            }
            else
            {
                playerInfoContainer.style.display = DisplayStyle.Flex;
            }


        }

        private void OnDisable()
        {
            EventManager.OnChangePlayerHealth -= HandleChangePlayerHealth;
            EventManager.OnChangePlayerPotions -= HandleChangePlayerPotions;
            EventManager.OnInitiateDialogue -= HandleInitiateDialogue;
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            currentState.SelectButton();

        }

        public void HandleNavigate(InputAction.CallbackContext context)
        {
            if (!context.performed || buttons.Count == 0) return;

            buttons[currentSelection].RemoveFromClassList("active");

            Vector2 input = context.ReadValue<Vector2>();
            currentSelection += input.x > 0 ? 1 : -1;
            currentSelection = Mathf.Clamp(currentSelection, 0, buttons.Count - 1);
            buttons[currentSelection].AddToClassList("active");

        }

        private void HandleChangePlayerHealth(float newHealthPoints)
        {
            healthLabel.text = newHealthPoints.ToString();
        }

        private void HandleChangePlayerPotions(int newPotionCount)
        {
            potionsLabel.text = newPotionCount.ToString();
        }

        private void HandleInitiateDialogue(TextAsset inkJSON)
        {
            currentState = dialogueState;
            currentState.EnterState();
            (currentState as UIDialogueState).SetStory(inkJSON);
        }
    }
}