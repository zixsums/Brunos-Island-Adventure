using System;
using UnityEngine;
using RPG.Utility;
using UnityEngine.Events;


namespace RPG.Character
{
   public class Health : MonoBehaviour
   {
      public event UnityAction OnStartDefeat = () => { };
      [NonSerialized] public float healthPoints = 0f;
      private Animator animatorCmp;
      private BubbleEvent bubbleEventCmp;
      private bool isDefeated = false;
      private void Awake()
      {
         animatorCmp = GetComponentInChildren<Animator>();
         bubbleEventCmp = GetComponentInChildren<BubbleEvent>();
      }

      private void OnEnable()
      {
         bubbleEventCmp.OnBubbleCompleteDefeat += HandleBubbleCompletDefeat;
      }

      private void OnDisable()
      {
         bubbleEventCmp.OnBubbleCompleteDefeat -= HandleBubbleCompletDefeat;
      }
      public void TakeDamage(float damageAmount)
      {
         healthPoints = MathF.Max(healthPoints - damageAmount, 0);
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
   }
}
