using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static int _sceneToLoad;

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
    
}
