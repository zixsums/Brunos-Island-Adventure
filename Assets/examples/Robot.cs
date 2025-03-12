using System;
using UnityEngine;

// RPG is the main namespace for our game.
namespace RPG.Example
{
    public class Robot : MonoBehaviour
    {
        private battleryRegulations includedbattlery;

        public Robot()
        {
            includedbattlery = new Battlery(80f);
            includedbattlery.checkHealth();
            Charger.chargeBattlery(includedbattlery);
            includedbattlery.checkHealth();
            print(Charger.chargeInUse);
        }
    }

    public class Battlery : battleryRegulations
    {
        public Battlery(float newHealth)
        {
            health = newHealth;
            Debug.Log("New Battlery created");
        }

        public override void checkHealth()
        {
            Debug.Log("health");
        }
    }

    static class Charger
    {
        public static bool chargeInUse;

        public static void chargeBattlery(battleryRegulations battleryToCharge)
        {
            battleryToCharge.health = 100f;
            chargeInUse = true;
        }
    }

    public abstract class battleryRegulations
    {
        public float health;
        public abstract void checkHealth();
    }
}
