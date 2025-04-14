using UnityEngine;
using RPG.Core;

namespace RPG.Character
{
    public class PlayerContoller : MonoBehaviour
    {
        public CharaterStatsSO stats;
        private Health healthCmp;
        private Combat combatCmp;

        private void Awake()
        {
            if (stats == null)
            {
                Debug.LogWarning($"{name} does not have stats ");
            }

            healthCmp = GetComponent<Health>();
            combatCmp = GetComponent<Combat>();
        }

        private void Start()
        {
            healthCmp.healthPoints = stats.health;
            combatCmp.damage = stats.damage;

            EventManager.RaiseChangePlayerHealth(healthCmp.healthPoints);
        }
    }
}

