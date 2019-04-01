using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class ReduceBox : MonoBehaviour
    {
        public void Reduce(GameObject box)
        {
            /*    Disable all the components of the expanded box    */
            box.GetComponent<Image>().enabled = false;
            var images = box.GetComponentsInChildren<Image>();
            foreach (var child in images)
            {
                child.enabled = false;
            }

            var texts = box.GetComponentsInChildren<Text>();
            foreach (var child in texts)
            {
                child.enabled = false;
            }

            ServerDisplayManager.isReduced = true;
        }

        public void ShowReduce(GameObject boxReduced)
        {
            /*    Enable all the components of the reduced box    */
            var images = boxReduced.GetComponentsInChildren<Image>();
            foreach (var child in images)
            {
                child.enabled = true;
            }

            var texts = boxReduced.GetComponentsInChildren<Text>();
            foreach (var child in texts)
            {
                child.enabled = true;
            }
        }
    }
}