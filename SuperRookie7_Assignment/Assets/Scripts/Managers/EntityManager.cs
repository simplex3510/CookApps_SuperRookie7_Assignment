using Entity.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Singleton.Manager
{
    public class EntityManager : MonoSingleton<EntityManager>
    {
        public Dictionary<int, BaseCharacter> spawnedCharactersDict = new Dictionary<int, BaseCharacter>();
        public Dictionary<int, BaseMonster> spawnedMonstersDict = new Dictionary<int, BaseMonster>();

        [SerializeField]
        private GameObject Prefab_Goblin;
        [SerializeField]
        private int monsterCapacity;
        [SerializeField]
        private float monsterSpawnCycleTime;
        private float lastSpawnTime = 0;

        private Vector2 spawnPoint1 = new Vector2(-8, 2);
        private Vector2 spawnPonit2 = new Vector2(8.75f, -3.5f);

        private void Awake()
        {
            //Time.timeScale = 0.1f;
            //DonDestroySingleton();
        }

        private void Update()
        {
            if (monsterSpawnCycleTime < Time.time - lastSpawnTime)
            {
                if (spawnedMonstersDict.Count < monsterCapacity)
                {
                    float xPos = Random.Range(spawnPoint1.x, spawnPonit2.x);
                    float yPos = Random.Range(spawnPonit2.y, spawnPoint1.y);

                    Instantiate(Prefab_Goblin, new Vector2(xPos, yPos), Quaternion.identity);
                    lastSpawnTime = Time.time;
                }
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
