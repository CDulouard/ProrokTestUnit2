using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIScripts
{
    public class SceneLoader : MonoBehaviour
    {
        private static int _sceneToLoad;
        private static string _mode = "Simulation";
        public static int activeScene;

        private void Start()
        {
            _sceneToLoad = 0;
        }

        public void SceneSwitcher()
        {
            if(Server.isActive) Server.StopServer();
            /*    Switch to the scene stores in _sceneToLoad    */
            SceneManager.LoadScene(_sceneToLoad);
            activeScene = _sceneToLoad;
        }

        public void SelectScene(int nb)
        {
            /*    Change _sceneToLoad    */
            _sceneToLoad = nb;
        }

        public void SelectMode(string mode)
        {
            /*    Change _mode    */
            _mode = mode;
        }

        public static string GetMode()
        {
            return _mode;
        }
    
    }
}
