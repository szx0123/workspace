using System.Collections.Generic;
using UnityEngine;

public class SingleInstance<T> where T : class, new ()
{
    static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }        
    }

    public void Create()
    {

    }
};

public class SingleMonoBehaviour<T> : MonoBehaviour  where T : MonoBehaviour
{
    static T _instance;
    private static object _lock = new object();
    public static bool applicationIsQuitting = false;

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogError("[Singleton] Instance '" + typeof(T) + "' already destroyed on application quit." + " Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " + " - there should never be more than 1 singleton!" + " Reopening the scene might fix it.");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(SingleMonoBehaviour) " + typeof(T).ToString();
                        DontDestroyOnLoad(singleton);
                        Debug.Log("[Singleton] An instance of " + typeof(T) + " is needed in the scene, so '" + singleton + "' was created with DontDestroyOnLoad.");
                    }
                    else
                    {
                        Debug.Log("[Singleton] Using instance already created: " + _instance.gameObject.name);
                    }
                }
                return _instance;
            }
        }
        set
        {
            if (_instance != null)
            {
                Destroy(value);
                return;
            }
            _instance = value;
            DontDestroyOnLoad(_instance);
        }
    }

    public virtual void OnDestroy()
    {
        applicationIsQuitting = true;
    }
    public virtual void Create()
    {

    }
};

public class GlobalDefine : SingleInstance<GlobalDefine>
{
    public readonly string configDataUrl = "Assets/Scripts/ConfigData/";
    public readonly string configUrl = "Assets/Configs/";
    public readonly uint bigNumberIntervel = 1000;
    public readonly uint bigNumberIntervelIgnoreBit = 6;
    public readonly Dictionary<uint, BigNumberType> bigNumberDic = new Dictionary<uint, BigNumberType>
    {
        {0, new BigNumberType(0, 3, "")}, {1, new BigNumberType(3, 6, "K")}, {2, new BigNumberType(6, 9, "M")},
        {3, new BigNumberType(9, 12, "B")}, {4, new BigNumberType(12, 15, "A")}, {5, new BigNumberType(15, 18, "C")},
        {6, new BigNumberType(18, 99999, "D")}
    };
    public readonly float autoAttackInterval = 0.1f;
}

