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
        private GameObject prefab_Goblin;
        [SerializeField]
        private GameObject prefab_GoblinBoss;
        [SerializeField]
        private int monsterCapacity;
        private int monsterCount = 0;
        [SerializeField]
        private float monsterSpawnCycleTime;
        private float lastSpawnTime = 0;

        private Vector2 spawnPoint1 = new Vector2(-8, 2);
        private Vector2 spawnPonit2 = new Vector2(8.75f, -3.5f);

        #region Unity List-Cycle
        protected override void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            Time.timeScale = timeScale;

            if (!GameManager.Instance.IsBossPhase)
            {
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

            if (GameManager.Instance.IsBossPhase && spawnedMonstersDict.Count == 0)
            {
                GameManager.Instance.IsVictory = true;
            }

            if (spawnedCharactersDict.Count == 0)
            {
                GameManager.Instance.IsDefeat = true;
            }
        }

        private void OnDestroy()
        {
            // 씬이 변경될 때 생성된 게임 오브젝트 정리
            var entities = new List<BaseEntity>(spawnedMonstersDict.Values);
            entities = new List<BaseEntity>(spawnedCharactersDict.Values);
            foreach (var entity in entities)
            {
                Destroy(entity.gameObject);
            }
            spawnedCharactersDict.Clear();

            foreach (var entity in entities)
            {
                Destroy(entity.gameObject);
            }
            spawnedMonstersDict.Clear();

            entities = new List<BaseEntity>(readyMonsterQueue);
            foreach (var entity in entities)
            {
                Destroy(entity.gameObject);
            }
            readyMonsterQueue.Clear();
        }
        #endregion

        public void BossSpawn()
        {
            var monsters = new List<BaseMonster>(spawnedMonstersDict.Values);
            foreach (var monster in monsters)
            {
                monster.ChangeStateFSM(FSM.Base.State.EState.Die);
            }

            float xPos = Random.Range(spawnPoint1.x, spawnPonit2.x);
            float yPos = Random.Range(spawnPonit2.y, spawnPoint1.y);

            Vector2 point = new Vector2(xPos, yPos);
            Instantiate(prefab_GoblinBoss, point, Quaternion.identity);
        }
    
        public void Respawn<T>(T entity) where T : BaseEntity
        {
            if (GameManager.Instance.IsDefeat)
            {
                return;
            }

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

            Instantiate(prefab_Goblin, new Vector2(xPos, yPos), Quaternion.identity);
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
