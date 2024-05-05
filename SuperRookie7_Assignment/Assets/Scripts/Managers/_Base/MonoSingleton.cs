using UnityEngine;

namespace Singleton
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject singleton = GameObject.Find(typeof(T).Name);
                    if (singleton == null)
                    {
                        singleton = new GameObject(typeof(T).Name);
                        instance = singleton.AddComponent<T>();
                    }
                    else
                    {
                        instance = singleton.GetComponent<T>();
                    }
                }

                return instance;
            }
        }

        protected void DonDestroySingleton()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}