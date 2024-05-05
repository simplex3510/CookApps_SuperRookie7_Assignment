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

        private void Awake()
        {
            //Time.timeScale = 0.1f;
            //DonDestroySingleton();
        }

        private void Update()
        {
            if (spawnedMonstersDict.Count < monstersCapacity)
            {
                Instantiate(Prefab_Goblin);
            }
        }

        private void OnDestroy()
        {
            // 씬이 변경될 때 생성된 게임 오브젝트 정리
            var characters = new List<BaseCharacter>(spawnedCharactersDict.Values);
            foreach (var character in characters)
            {
                Destroy(character.gameObject);
            }
            spawnedCharactersDict.Clear();

            var monsters = new List<BaseMonster>(spawnedMonstersDict.Values);
            foreach (var monster in monsters)
            {
                Destroy(monster.gameObject);
            }
            spawnedMonstersDict.Clear();
        }
    }
}
