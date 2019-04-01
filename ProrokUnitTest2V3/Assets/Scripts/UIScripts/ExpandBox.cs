using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class ExpandBox : MonoBehaviour
    {
        public void Expand(GameObject box)
        {
            /*    Enable all the components of the expanded box    */
            box.GetComponent<Image>().enabled = true;
            var images = box.GetComponentsInChildren<Image>();
            foreach (var child in images)
            {
                child.enabled = true;
            }

            var texts = box.GetComponentsInChildren<Text>();
            foreach (var child in texts)
            {
                child.enabled = true;
            }

            ServerDisplayManager.isReduced = false;
        }

        public void HideReduce(GameObject boxReduced)
        {
            /*    Disable all the components of the reduced box    */
            var images = boxReduced.GetComponentsInChildren<Image>();
            foreach (var child in images)
            {
                child.enabled = false;
            }

            var texts = boxReduced.GetComponentsInChildren<Text>();
            foreach (var child in texts)
            {
                child.enabled = false;
            }
        }
    }
}