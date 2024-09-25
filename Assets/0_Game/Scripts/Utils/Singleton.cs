using UnityEngine;

namespace Utils
{

    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static readonly object _lock = new object();
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance = FindObjectOfType<T>();
                        if (_instance == null)
                        {
                            Debug.LogError($"No instance of {typeof(T)} found in the scene. Please make sure there is one in the scene or create one.");
                        }
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else if (_instance == this)
            {
                Destroy(gameObject);
            }
        }
    }
}