using Entity.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Singleton.Manager
{
    public class EntityManager : MonoSingleton<EntityManager>
    {
        [SerializeField]
        public Dictionary<int, BaseCharacter> spawnedCharactersDict = new Dictionary<int, BaseCharacter>();

        [SerializeField]
        public Dictionary<int, BaseMonster> spawnedMonstersDict = new Dictionary<int, BaseMonster>();
        [SerializeField]
        private int monstersCapacity;

        [SerializeField]
        private GameObject Prefab_Goblin;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            if (spawnedMonstersDict.Count < monstersCapacity)
            {
                Instantiate(Prefab_Goblin);
            }
        }
    }
}
