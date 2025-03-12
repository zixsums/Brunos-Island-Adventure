using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Utility;

namespace RPG.Quests
{
    public class TreasureChest : MonoBehaviour
    {
        public Animator animatorCmp;
        private bool isInteractable = false;
        private bool hasBeenOpened = false;
        private void OnTriggerEnter()
        {
            isInteractable = true;
        }
        private void OnTriggerExit()
        {
            isInteractable = false;
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!isInteractable || hasBeenOpened) return;

            animatorCmp.SetBool(Constants.IS_SHAKING_ANIMATOR_PARAM, false);
            hasBeenOpened = true;
        }


    }
}