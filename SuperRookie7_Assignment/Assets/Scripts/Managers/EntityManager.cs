using Entity.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Singleton.Manager
{
    public class EntityManager : MonoSingleton<EntityManager>
    {
        [SerializeField]
        [Range(0.01f, 5f)]
        private float timeScale;

        public Dictionary<int, BaseCharacter> spawnedCharactersDict = new Dictionary<int, BaseCharacter>();
        public Dictionary<int, BaseMonster> spawnedMonstersDict = new Dictionary<int, BaseMonster>();

        public Queue<BaseMonster> readyMonsterQueue = new Queue<BaseMonster>();

        [SerializeField]
        private GameObject Prefab_Goblin;
        [SerializeField]
        private int monsterCapacity;
        private int monsterCount = 0;
        [SerializeField]
        private float monsterSpawnCycleTime;
        private float lastSpawnTime = 0;

        private Vector2 spawnPoint1 = new Vector2(-8, 2);
        private Vector2 spawnPonit2 = new Vector2(8.75f, -3.5f);

        #region Unity List-Cycle
        private void Awake()
        {
            //DonDestroySingleton();
        }

        private void Update()
        {
            Time.timeScale = timeScale;

            // When the time is monster spawn time
            if (monsterSpawnCycleTime < Time.time - lastSpawnTime)
            {
                // if monster count is bigger than capacity - respawn
                if (monsterCapacity <= monsterCount)
                {
                    if (0 < readyMonsterQueue.Count)
                    {
                        Respawn<BaseMonster>(readyMonsterQueue.Dequeue());
                        lastSpawnTime = Time.time;
                    }
                    else
                    {
                        lastSpawnTime = Time.time;
                    }
                }
                else // if monster count is smaller than capacity - sapwn
                {
                    Spawn();
                    lastSpawnTime = Time.time;
                }
            }
        }

        private void OnDestroy()
        {
            //// 씬이 변경될 때 생성된 게임 오브젝트 정리
            //var characters = new List<BaseCharacter>(spawnedCharactersDict.Values);
            //foreach (var character in characters)
            //{
            //    Destroy(character.gameObject);
            //}
            //spawnedCharactersDict.Clear();

            //var monsters = new List<BaseMonster>(spawnedMonstersDict.Values);
            //foreach (var monster in monsters)
            //{
            //    Destroy(monster.gameObject);
            //}
            //spawnedMonstersDict.Clear();
        }
        #endregion
    
        public void Respawn<T>(T entity) where T : BaseEntity
        {
            if (entity is BaseMonster)
            {
                Spawn(entity as BaseMonster);
            }
            else
            {
                Spawn(entity as BaseCharacter);
            }
        }

        private void Spawn()
        {
            float xPos = Random.Range(spawnPoint1.x, spawnPonit2.x);
            float yPos = Random.Range(spawnPonit2.y, spawnPoint1.y);

            Instantiate(Prefab_Goblin, new Vector2(xPos, yPos), Quaternion.identity);
            ++monsterCount;
        }

        private void Spawn(BaseEntity entity)
        {
            if (entity is BaseMonster)
            {
                float xPos = Random.Range(spawnPoint1.x, spawnPonit2.x);
                float yPos = Random.Range(spawnPonit2.y, spawnPoint1.y);

                (entity as BaseMonster).Root.transform.position = new Vector2(xPos, yPos);
                entity.Start();
            }
            else
            {
                entity.transform.position = Vector2.zero;
                entity.Start();
            }
        }
    }
}
