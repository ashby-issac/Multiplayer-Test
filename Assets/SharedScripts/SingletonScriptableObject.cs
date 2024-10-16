using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    public static T instance = null;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                T[] instances = Resources.FindObjectsOfTypeAll<T>();
                if (instances.Length < 1)
                {
                    Debug.Log($":: No instances of singleton scriptables");
                    return null;
                }
                else if (instances.Length > 1)
                {
                    Debug.Log($":: Multiple instances of singleton scriptables");
                    return null;
                }
                return instances[0];
            }

            return instance;
        }
    }
}
