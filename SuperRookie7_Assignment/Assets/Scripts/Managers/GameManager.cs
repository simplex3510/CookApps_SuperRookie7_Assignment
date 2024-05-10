using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Singleton.Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField]
        private bool isDefeat = false;
        public bool IsDefeat { get { return isDefeat; } set { isDefeat = value; } }

        [SerializeField]
        private bool isVictory = false;
        public bool IsVictory { get { return isVictory; } set { isVictory = value; } }

        private bool isOnce = false;
        [SerializeField]
        private bool isBossPhase = false;
        public bool IsBossPhase { get => isBossPhase; }

        [SerializeField]
        private Text timerText;
        [SerializeField]
        private float timer = 180.0f;

        protected override void Awake()
        {
            base.Awake();
        }

        // Update is called once per frame
        void Update()
        {
            timer -= Time.deltaTime;
            
            if (0f < timer)
            {
                timerText.text = $"{timer : 0.00}";
            }
            else
            {
                if (!isOnce)
                {
                    isOnce = true;
                    isBossPhase = true;
                    timerText.text = $"Boss";
                    EntityManager.Instance.BossSpawn();
                }
            }

            if (IsVictory == true)
            {
                timerText.text = $"Victory";
            }

            if (IsDefeat == true)
            {
                timerText.text = $"Defeat";
                Invoke(nameof(ReGame), 5.0f);
            }
        }

        private void ReGame()
        {
            SceneManager.LoadScene(0/*"StageScene"*/);
        }
    }

}