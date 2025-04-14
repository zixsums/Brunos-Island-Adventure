using System;
using UnityEngine;
using RPG.Utility;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG.Core;
using UnityEngine.InputSystem;


namespace RPG.Character
{
   public class Health : MonoBehaviour
   {
      public event UnityAction OnStartDefeat = () => { };
      [NonSerialized] public float healthPoints = 0f;
      [SerializeField] private int potionCount = 1;
      [SerializeField] private float healAmount = 15f;
      private Animator animatorCmp;
      private BubbleEvent bubbleEventCmp;
      [NonSerialized] public Slider sliderCmp;


      private bool isDefeated = false;
      private void Awake()
      {
         animatorCmp = GetComponentInChildren<Animator>();
         bubbleEventCmp = GetComponentInChildren<BubbleEvent>();
         sliderCmp = GetComponentInChildren<Slider>();
      }

      private void OnEnable()
      {
         bubbleEventCmp.OnBubbleCompleteDefeat += HandleBubbleCompletDefeat;
      }

      private void Start()
      {
         if (CompareTag(Constants.PLAYER_TAG))
         {
            EventManager.RaiseChangePlayerPotions(potionCount);
         }
      }

      private void OnDisable()
      {
         bubbleEventCmp.OnBubbleCompleteDefeat -= HandleBubbleCompletDefeat;
      }
      public void TakeDamage(float damageAmount)
      {
         healthPoints = MathF.Max(healthPoints - damageAmount, 0);
         if (CompareTag(Constants.PLAYER_TAG))
         {
            EventManager.RaiseChangePlayerHealth(healthPoints);
         }

         if (sliderCmp != null)
         {
            sliderCmp.value = healthPoints;
         }

         if (healthPoints == 0)
         {
            Defeated();
         }

      }

      private void Defeated()
      {
         if (isDefeated) return;
         if (CompareTag(Constants.ENEMY_TAG))
         {
            OnStartDefeat.Invoke();
         }
         isDefeated = true;
         animatorCmp.SetTrigger(Constants.DEFEATED_ANIMATOR_PARAM);
      }

      private void HandleBubbleCompletDefeat()
      {
         Destroy(gameObject);
      }

      public void HandleHeal(InputAction.CallbackContext context)
      {
         if (!context.performed || potionCount == 0) return;
         potionCount--;
         healthPoints += healAmount;
         EventManager.RaiseChangePlayerHealth(healthPoints);
         EventManager.RaiseChangePlayerPotions(potionCount);
      }
   }
}
