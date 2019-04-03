using UnityEngine;
using UnityEngine.UI;

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
                var images = trainingPanel.GetComponentsInChildren<Image>();
                foreach (var child in images)
                {
                    child.enabled = true;
                }

                var texts = trainingPanel.GetComponentsInChildren<Text>();
                foreach (var child in texts)
                {
                    child.enabled = true;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
