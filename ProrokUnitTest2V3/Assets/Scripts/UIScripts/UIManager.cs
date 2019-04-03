using UnityEngine;

namespace UIScripts
{
    public class UIManager : MonoBehaviour
    {
        public GameObject mainCamera;
        public GameObject trainingPanel;

        private void Start()
        {
            if (SceneLoader.GetMode().ToLower() == "training")
            {
                var cam = mainCamera.GetComponent<Camera>();
                cam.farClipPlane = 1;
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
