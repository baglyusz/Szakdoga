using UnityEngine;

namespace Managers
{
    [CreateAssetMenu(menuName = "Singletons/MasterManager")]
    public class MasterManager : SingletonScriptableObject<MasterManager>
    {
        [SerializeField] 
        private GameSettings gameSettings;

        public static GameSettings GameSettings
        {
            get { return Instance.gameSettings;}
        }
    }
}
