using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIScripts
{
    public class SceneLoader : MonoBehaviour
    {
        private static int _sceneToLoad;
        private static string _mode;

        private void Start()
        {
            _sceneToLoad = 0;
        }

        public void SceneSwitcher()
        {
            SceneManager.LoadScene(_sceneToLoad);
        }

        public void SelectScene(int nb)
        {
            _sceneToLoad = nb;
        }

        public void SelectMode(string mode)
        {
            _mode = mode;
        }
    
    }
}
