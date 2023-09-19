using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utilities
{
    public class Singleton : MonoBehaviour
    {
        public static Singleton Instance;

        void Init()
        {
            if (Instance == null )
            {
                Instance = this;
            }
            else Destroy(this);
        }
        // Start is called before the first frame update
        void Start()
        {
            Init();
        }
    }

    public class PersistenceSingleton : MonoBehaviour
    {
        public static PersistenceSingleton Instance;
        void Init()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
            }    
            else Destroy(this);
        } 
        void Start()
        {
            Init();
        }
    }
}