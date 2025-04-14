using UnityEngine.UIElements;
using UnityEngine;
using RPG.Core;

namespace RPG.UI

{
    public class UIMainMenuState : UIBaseState
    {
        public UIMainMenuState(UIController ui) : base(ui) { }

        public override void EnterState()
        {
            controller.mainMenuContainer.style.display = DisplayStyle.Flex;

            controller.buttons = controller.mainMenuContainer
            .Query<Button>(null, "menu-button")
            .ToList();

            controller.buttons[0].AddToClassList("active");
        }

        public override void SelectButton()
        {
            Button btn = controller.buttons[controller.currentSelection];
            if (btn.name == "start-button")
            {
                SceneTransition.Initiate(1);
            }
        

        }
    }
}
