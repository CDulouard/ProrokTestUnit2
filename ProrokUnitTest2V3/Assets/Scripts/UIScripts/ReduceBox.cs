using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class ReduceBox : MonoBehaviour
    {
        // Start is called before the first frame update
        public void Reduce(GameObject box)
        {
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