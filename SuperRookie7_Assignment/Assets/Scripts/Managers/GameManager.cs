using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;

namespace Singleton.Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (EntityManager.Instance.spawnedCharactersDict.Count == 0)
            {
                Destroy(EntityManager.Instance.gameObject);
            }
        }
    }

}