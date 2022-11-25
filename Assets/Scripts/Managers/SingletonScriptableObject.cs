using UnityEngine;

namespace Managers
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T: ScriptableObject
    {
        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                var results = Resources.FindObjectsOfTypeAll<T>();

                switch (results.Length)
                {
                    case 0:
                        Debug.LogError("SingletonScriptableObject -> Instance -> result length is 0 for type" + typeof(T).ToString() + ".");
                        return null;

                    case > 1:
                        Debug.LogError("SingletonScriptableObject -> Instance -> result length greater than 1 for type" + typeof(T).ToString() + ".");
                        return null;

                    default:
                        _instance = results[0];
                        _instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
                        break;
                }

                return _instance;
            }
        }


    }
}
