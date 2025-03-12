using UnityEngine;

namespace RPG.Character
{
    [CreateAssetMenu(fileName = "Character Stats",
     menuName = "RPG/Character Stats SO",
     order = 0
    )]
    public class CharaterStatsSO : ScriptableObject
    {
        public float health = 100;
        public float damage = 10;
        public float walkSpeed = 1f;
        public float runSpeed = 1.5f;
    }

}


