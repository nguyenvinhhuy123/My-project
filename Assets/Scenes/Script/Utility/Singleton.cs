using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utilities
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance;

        void Init()
        {
            if (Instance == null )
            {
                Instance = FindObjectOfType<T>();
                if (Instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    Instance = obj.AddComponent<T>();
                }
            }
            else Destroy(gameObject);
        }
        // Start is called before the first frame update
        protected virtual void Awake()
        {
            Init();
        }
    }

    public class PersistenceSingleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance;
        void Init()
        {
            if (Instance == null )
            {
                Instance = FindObjectOfType<T>();
                if (Instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    Instance = obj.AddComponent<T>();
                }
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        } 
        protected virtual void Awake()
        {
            Init();
        }
    }
}