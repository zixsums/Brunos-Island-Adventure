using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Core;

namespace RPG.Character
{
    public class NPCController : MonoBehaviour
    {
        public TextAsset inkJSON;
        private Canvas canvasCmp;
        private void Awake()
        {
            canvasCmp = GetComponentInChildren<Canvas>();
        }

        private void OnTriggerEnter()
        {
            canvasCmp.enabled = true;
        }
        private void OnTriggerExit()
        {
            canvasCmp.enabled = false;
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!context.performed || !canvasCmp.enabled) return;

            if (inkJSON == null)
            {
                Debug.LogWarning("Please add an ink file to the npc.");
                return;
            }
           
            EventManager.RaiseInitiateDialogue(inkJSON);
        }

    }
}