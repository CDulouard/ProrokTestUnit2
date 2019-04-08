using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class BoxManager : MonoBehaviour
    {
        public void HideContent(GameObject box)
        {
            /*
            var images = box.GetComponentsInChildren<Image>();
            foreach (var child in images)
            {
                child.enabled = false;
            }

            var texts = box.GetComponentsInChildren<Text>();
            foreach (var child in texts)
            {
                child.enabled = false;
            }*/
            box.SetActive(false);

        }
        
        public void ShowContent(GameObject box)
        {
            /*
            var images = box.GetComponentsInChildren<Image>();
            foreach (var child in images)
            {
                child.enabled = true;
            }

            var texts = box.GetComponentsInChildren<Text>();
            foreach (var child in texts)
            {
                child.enabled = true;
            }*/
            /*==============================*/
            box.SetActive(true);
    
        }
        
        public static void HideContentGeneric(GameObject box)
        {
            /*
            var images = box.GetComponentsInChildren<Image>();
            foreach (var child in images)
            {
                child.enabled = false;
            }

            var texts = box.GetComponentsInChildren<Text>();
            foreach (var child in texts)
            {
                child.enabled = false;
            }*/
            box.SetActive(false);
            
        }
        
    }
}
