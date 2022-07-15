using UnityEngine;

namespace ETGgames.Utils
{
    public abstract class SingletonMonoBehaviour<T> : Singleton where T : MonoBehaviour
    {


        public virtual bool IsScenePersistent => false;

        public static bool Exists => _instance != null;
        public static T Instance
        {
            [RuntimeInitializeOnLoadMethod] //without this, it fux up if fast scene reload is on. with it, it fux up in builds.
            get
            {
                if (IsQuitting)
                {
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)

                    Debug.LogWarning($"[{nameof(Singleton)}<{typeof(T)}>] Application is in quitting state in singleton getter");
#endif
                    return _instance; //why tf does having this here not cause the problem "Some objects were not cleaned up when closing the scene. (Did you spawn new GameObjects from OnDestroy?)" // it's because its still in C# memory and hasn't been garbage collected so we can still access it. however, when we do the null check, it uses unity's overload null comparison operator and retruns if it is destroyed on the unity side of things (cpp), which it has been on app quit (and the order is random) //so the verdict is it's better to do a null check but we've been keeping local references to singletons in MBs so much at this point it's not worth going back and changing it.
                }
                if (_instance != null)
                    return _instance;

                lock (_lock)
                {
                    var instances = FindObjectsOfType<T>();
                    var count = instances.Length;
                    if (count > 0)
                    {
                        if (count == 1)
                        {
                            _instance = instances[0]; //must be set before onawake
                            (instances[0] as SingletonMonoBehaviour<T>).PreOnAwake();
                            return _instance;
                        }
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
                        Debug.LogWarning($"[{nameof(Singleton)}<{typeof(T)}>] There should never be more than one {nameof(Singleton)} of type {typeof(T)} in the scene, but {count} were found. The first instance found will be used, and all others will be destroyed.");
#endif
                        for (var i = 1; i < instances.Length; i++)
                            Destroy(instances[i].gameObject);
                        (instances[0] as SingletonMonoBehaviour<T>).PreOnAwake();
                        return _instance = instances[0];
                    }

                    Debug.Log($"[{nameof(Singleton)}<{typeof(T)}>] An instance is needed in the scene and no existing instances were found, so a new instance will be created.");
                    return _instance = new GameObject($"({nameof(Singleton)}){typeof(T)}").AddComponent<T>();
                }
            }
        }

        private static T _instance;
        private static readonly object _lock = new object();

        private void PreOnAwake()
        {
            if (IsScenePersistent)
            {
                gameObject.transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
            }
            OnAwake();

        }
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                PreOnAwake();
            }
            else if (_instance != this)
            {
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
                Debug.LogWarning($"Singleton of Type {GetType()} already exists, so destroying one that is currently trying to be made.", this);
#endif
                Destroy(gameObject);

            }
        }
        protected virtual void OnAwake() { }

    }
}

public abstract class Singleton : MonoBehaviour
{

    public static bool IsQuitting { get; private set; }

    protected virtual void OnApplicationQuit()
    {
        IsQuitting = true;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    private static void ResetState()
    {
        IsQuitting = false;
    }
}